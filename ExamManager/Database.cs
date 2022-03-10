using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace ExamManager
{
    class Database
    {
        // #### TODO: conn.close ####

        readonly SQLiteConnection connection;
        public Database()
        {
            string path = Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager\\") + "database.db";
            if (Properties.Settings.Default.DatabasePath != "default")
            { path = Properties.Settings.Default.DatabasePath; }

            Console.WriteLine(path);

            connection = CreateConnection(path);
            //Console.WriteLine("connection.State: "+connection.State);
            CreateStudentDB();
            CreateTeacherDB();
            CreateExamDB();
            CreateRoomDB();
            CreateSubjectDB();

            // #####################################################################################
            /*SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS exam";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS teacher";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS student";
            sqlite_cmd.ExecuteNonQuery();*/
            // #####################################################################################
            // Console.WriteLine("alle student: " + getAllstudent().Count);

        }
        /// <summary>Creates a connection to the sqlite database</summary>
        private SQLiteConnection CreateConnection(string path)
        {
            if (!File.Exists(path))
            {
                DialogResult result = MessageBox.Show("Existierende Datenbank wählen?", "Achtung", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "database files (*.db)|*.db|All files (*.*)|*.*";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.Title = "Lokale Datenbank auswählen";
                        openFileDialog.RestoreDirectory = true;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog.FileName;
                            Properties.Settings.Default.DatabasePath = filePath;
                            Properties.Settings.Default.Save();
                            Program.database = new Database();
                        }
                    }
                }
                else
                {
                    path = Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager\\") + "database.db";
                    Properties.Settings.Default.DatabasePath = path;
                    Properties.Settings.Default.Save();
                }
            }
            SQLiteConnection sqlite_conn;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                SQLiteConnection.CreateFile(path);
            }//+ "database.db");
            sqlite_conn = new SQLiteConnection("Data Source=" + path + "; Version = 3; New = False; Compress = True; ");
            try { sqlite_conn.Open(); } catch (Exception e) { Console.WriteLine("ERROR: " + e.Message); }
            return sqlite_conn;
        }

        #region -------- STUDENT --------
        // ID firstname lastname grade Email TelNummer
        /// <summary>Adds a new student to the database.</summary>
        /// <returns>returns <see langword="true"/> if the student was added successfully</returns>
        public bool AddStudent(StudentObject s)
        {
            StudentObject ts = GetStudentByName(s.Firstname, s.Lastname);
            if (s.Firstname.Contains(" ") || s.Lastname.Contains(" "))
            {
                // Console.WriteLine("Space!>>> " + firstname + " - " + lastname);
                // TODO: replace " " with "_"
            }
            if (ts != null)
            {
                DialogResult result = MessageBox.Show("Ein Schüler mit dem Namen " + s.Firstname + " " + s.Lastname + " exestiert bereits in der Stufe " + ts.Grade +
                    "!\nEinen weiteren in der Stufe " + s.Grade + " erstellen?", "Warnung!", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                { return false; }
                if (result != DialogResult.Yes)
                { return true; }
            }
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO student (firstname, lastname, grade, email, phone_number) VALUES(@firstname,@lastname,@grade,@email,@phone_number) ";
            sqlite_cmd.Parameters.AddWithValue("@firstname", s.Firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", s.Lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", s.Grade);
            sqlite_cmd.Parameters.AddWithValue("@email", s.Email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", s.Phonenumber);
            sqlite_cmd.ExecuteNonQuery();
            return true;
        }
        /// <summary>Adds all students from a file into the database.</summary>
        public void InsertStudentFileIntoDB(string file, string grade, bool mailgenerator) // TODO: in FileReaderClass
        {
            bool editDoppelnamen = false;
            LinkedList<int> studentIdList = new LinkedList<int>();
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (!line[0].Equals('#'))
                        if (line.Split(' ').Length > 2) // Doppelnamen
                        {
                            if (!editDoppelnamen)   // Abfrage am Anfang?
                            {
                                string tempfirstname = null;
                                for (int i = 0; i < line.Split(' ').Length - 1; i++)
                                    tempfirstname += line.Split(' ')[i] += " ";
                                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                                string templastname = null;
                                templastname += line.Split(' ')[line.Split(' ').Length - 1];
                                tempfirstname = tempfirstname.Replace(' ', '_');
                                templastname = templastname.Replace(' ', '_');
                                if (mailgenerator)
                                {
                                    string domain = Properties.Settings.Default.EmailDomain;
                                    string mail = tempfirstname.ToLower().Replace(' ', '.').Replace('_', '.') + "." + templastname.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
                                    if (AddStudent(new StudentObject(0, tempfirstname, templastname, grade, mail)))
                                        studentIdList.AddLast(GetStudentByName(tempfirstname, templastname, grade).Id);
                                    else break;
                                }
                                else
                                {
                                    if (AddStudent(new StudentObject(0, tempfirstname, templastname, grade)))
                                        studentIdList.AddLast(GetStudentByName(tempfirstname, templastname, grade).Id);
                                    else break;
                                }

                            }
                            else
                            {
                                for (int i = 1; i < line.Split(' ').Length; i++)
                                {
                                    string tempfirstname = null;
                                    for (int fn = 0; fn < i; fn++)
                                        tempfirstname += line.Split(' ')[fn] += " ";
                                    tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                                    string templastname = null;
                                    for (int ln = i; ln < line.Split(' ').Length; ln++)
                                        templastname += line.Split(' ')[ln];
                                    DialogResult result = MessageBox.Show("Auswahl: " + tempfirstname + " - " + templastname, "Info!", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        if (mailgenerator)
                                        {
                                            string domain = Properties.Settings.Default.EmailDomain;
                                            string mail = tempfirstname.ToLower().Replace(' ', '.').Replace('_', '.') + "." + templastname.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
                                            AddStudent(new StudentObject(0, tempfirstname, templastname, grade, mail));
                                        }
                                        else AddStudent(new StudentObject(0, tempfirstname, templastname, grade));
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {       // kein doppelnamen
                            if (mailgenerator)
                            {
                                string domain = Properties.Settings.Default.EmailDomain;
                                string mail = line.Split(' ')[0].ToLower().Replace(' ', '.').Replace('_', '.') + "." + line.Split(' ')[1].ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
                                if (AddStudent(new StudentObject(0, line.Split(' ')[0], line.Split(' ')[1], grade, mail)))
                                    studentIdList.AddLast(GetStudentByName(line.Split(' ')[0], line.Split(' ')[1], grade).Id);
                                else break;
                            }
                            else
                            {
                                if (AddStudent(new StudentObject(0, line.Split(' ')[0], line.Split(' ')[1], grade)))
                                    studentIdList.AddLast(GetStudentByName(line.Split(' ')[0], line.Split(' ')[1], grade).Id);
                                else break;
                            }
                        }
                }
                if (studentIdList.Count > 0)
                {
                    FormStudentData form = new FormStudentData(studentIdList);
                    form.ShowDialog();
                }
            }
        }
        /// <summary>Searches a student by firstname and lastname (and grade) in the database.</summary>
        /// <returns>Returns the student as <see cref="StudentObject"/>. If it doesn't exist, null is returned</returns>
        public StudentObject GetStudentByName(string firstname, string lastname, string grade = null)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            if (grade == null) sqlite_cmd.CommandText = "SELECT * FROM student WHERE LOWER(firstname) = LOWER(@firstname) AND LOWER(lastname) = LOWER(@lastname)";
            else sqlite_cmd.CommandText = "SELECT * FROM student WHERE LOWER(firstname) = LOWER(@firstname) AND LOWER(lastname) = LOWER(@lastname) AND grade = @grade";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            reader = sqlite_cmd.ExecuteReader();
            string[] rData = new string[6];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
            else return null;
            return new StudentObject(Int32.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], rData[5]);
        }
        /// <summary>Searches a student by <paramref name="id"/> in the database.</summary>
        /// <returns>Returns the student as <see cref="StudentObject"/>. If it doesn't exist, null is returned</returns>
        public StudentObject GetStudentByID(int id)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            reader = sqlite_cmd.ExecuteReader();
            string[] rData = new string[6];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
            else return null;
            return new StudentObject(Int32.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], rData[5]);
        }
        /// <summary>Searches all students in the database.</summary>
        /// <returns>Returns all students as a <see cref="LinkedList{T}"/> with <see cref="StudentObject"/></returns>
        public LinkedList<StudentObject> GetAllStudents(bool orderFirstname = false)
        {
            LinkedList<StudentObject> data = new LinkedList<StudentObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            if (orderFirstname) sqlite_cmd.CommandText = "SELECT * FROM student ORDER BY grade ASC, firstname ASC";
            else sqlite_cmd.CommandText = "SELECT * FROM student ORDER BY grade ASC, lastname ASC";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[6];
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
                    data.AddLast(new StudentObject(Int32.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], rData[5]));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches all students in the database.</summary>
        /// <returns>Returns all students as a <see cref="LinkedList{t}"/> with <see cref="StudentObject"/></returns>
        public LinkedList<StudentObject> GetAllStudentsFromGrade(string grade, bool orderFirstname = false)
        {
            LinkedList<StudentObject> data = new LinkedList<StudentObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            if (orderFirstname) sqlite_cmd.CommandText = "SELECT * FROM student WHERE grade=@grade ORDER BY grade ASC, firstname ASC";
            else sqlite_cmd.CommandText = "SELECT * FROM student WHERE grade=@grade ORDER BY grade ASC, lastname ASC";
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[6];
                    for (int i = 0; i < 6; i++)
                        rData[i] = reader.GetValue(i).ToString();
                    data.AddLast(new StudentObject(Int32.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], rData[5]));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Edits a student in the database.</summary>
        public void EditStudent(int id, string firstname, string lastname, string grade, string email, string phone_number)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE student SET firstname=@firstname, lastname=@lastname, grade=@grade, email=@email, phone_number=@phone_number WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            sqlite_cmd.Parameters.AddWithValue("@email", email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Changes the grade of all students in a grade from the database.</summary>
        public void ChangeGrade(string old_grade, string new_grade)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE student SET grade=@grade WHERE grade = @old_grade";
            sqlite_cmd.Parameters.AddWithValue("@grade", new_grade);
            sqlite_cmd.Parameters.AddWithValue("@old_grade", old_grade);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Removes a student from the database.</summary>
        public void DeleteStudent(int id)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM student WHERE id = @id ";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Removes all students in a grade from the database.</summary>
        public void DeleteGrade(string grade)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM student WHERE grade = @grade ";
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            sqlite_cmd.ExecuteNonQuery();
        }
        #endregion
        #region -------- TEACHER --------
        /// <summary>Adds s teacher to the database.</summary>
        public void AddTeacher(TeacherObject t)
        {
            if (GetTeacherByID(t.Shortname) != null) { return; }
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO teacher (short_name, firstname, lastname,email, phone_number, subject1, subject2, subject3) VALUES(@short_name,@firstname,@lastname,@email,@phone_number,@subject1,@subject2,@subject3); ";
            sqlite_cmd.Parameters.AddWithValue("@short_name", t.Shortname);
            sqlite_cmd.Parameters.AddWithValue("@firstname", t.Firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", t.Lastname);
            sqlite_cmd.Parameters.AddWithValue("@email", t.Email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", t.Phonenumber);
            sqlite_cmd.Parameters.AddWithValue("@subject1", t.Subject1);
            sqlite_cmd.Parameters.AddWithValue("@subject2", t.Subject2);
            sqlite_cmd.Parameters.AddWithValue("@subject3", t.Subject3);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Adds all teachers from a file into the database.</summary>
        public void InsertTeacherFileIntoDB(string file, bool mailgenerator) // TODO: in FileReaderClass // TODO: Doppelnamen in file with _ info?
        {
            //bool editDoppelnamen = false;
            LinkedList<string> teacherIdList = new LinkedList<string>();    // TODO: Teacher mail generator
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (!line[0].Equals('#'))
                        if (line.Split(' ').Length > 2)
                        {       // kein doppelnamen
                            string[] t = line.Replace("Dr.", "").Replace(",", "").Split(' ');
                            if (mailgenerator)
                            {
                                string domain = Properties.Settings.Default.EmailDomain;
                                string mail = line.Split(' ')[0].ToLower().Replace(' ', '.').Replace('_', '.') + "." + line.Split(' ')[1].ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
                                if (t.Length == 5)
                                    AddTeacher(new TeacherObject(t[3], t[1], t[2], mail, null, t[4], null, null));
                                else if (t.Length == 6)
                                    AddTeacher(new TeacherObject(t[3], t[1], t[2], mail, null, t[4], t[5], null));
                                else if (t.Length == 7)
                                    AddTeacher(new TeacherObject(t[3], t[1], t[2], mail, null, t[4], t[5], t[6]));
                                teacherIdList.AddLast(t[3]);
                            }
                            else
                            {
                                if (t.Length == 5)
                                    AddTeacher(new TeacherObject(t[3], t[1], t[2], null, null, t[4], null, null));
                                else if (t.Length == 6)
                                    AddTeacher(new TeacherObject(t[3], t[1], t[2], null, null, t[4], t[5], null));
                                else if (t.Length == 7)
                                    AddTeacher(new TeacherObject(t[3], t[1], t[2], null, null, t[4], t[5], t[6]));
                                teacherIdList.AddLast(t[3]);
                            }
                            if (t.Length >= 5)
                            {
                                if (GetSubject(t[4]) == null)
                                    AddSubject(t[4]);
                            }
                            if (t.Length >= 6)
                            {
                                if (GetSubject(t[5]) == null)
                                    AddSubject(t[5]);
                            }
                            if (t.Length == 7)
                            {
                                if (GetSubject(t[6]) == null)
                                    AddSubject(t[6]);
                            }
                        }
                }
            }
            FormTeacherData form = new FormTeacherData(teacherIdList);
            form.ShowDialog();
        }
        /// <summary>Searches all teachers in the database.</summary>
        /// <returns>Returns all teachers as a <see cref="LinkedList{T}"/> with <see cref="TeacherObject"/></returns>
        public LinkedList<TeacherObject> GetAllTeachers(bool orderFirstname = false)
        {
            LinkedList<TeacherObject> data = new LinkedList<TeacherObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            if (orderFirstname) sqlite_cmd.CommandText = "SELECT * FROM teacher ORDER BY firstname ASC";
            else sqlite_cmd.CommandText = "SELECT * FROM teacher ORDER BY lastname ASC";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[8];
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
                    data.AddLast(new TeacherObject(rData[0], rData[1], rData[2], rData[3], rData[4], rData[5], rData[6], rData[7]));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches a teacher by shortname in the database.</summary>
        /// <returns>Returns the teacher as <see cref="TeacherObject"/></returns>
        public TeacherObject GetTeacherByID(string short_name)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher WHERE short_name = @short_name";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            reader = sqlite_cmd.ExecuteReader();
            string[] rData = new string[8];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
            else return null;
            return new TeacherObject(rData[0], rData[1], rData[2], rData[3], rData[4], rData[5], rData[6], rData[7]);
        }
        /// <summary>Searches a teacher by subject in the database.</summary>
        /// <returns>Returns the teacher as <see cref="TeacherObject"/></returns>
        public LinkedList<TeacherObject> GetTeacherBySubject(string subject)
        {
            LinkedList<TeacherObject> data = new LinkedList<TeacherObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher WHERE subject1 = @subject OR subject2 = @subject OR subject3 = @subject ORDER BY lastname";
            sqlite_cmd.Parameters.AddWithValue("@subject", subject);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[8];
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
                    data.AddLast(new TeacherObject(rData[0], rData[1], rData[2], rData[3], rData[4], rData[5], rData[6], rData[7]));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches a teacher by firstname and lastname in the database.</summary>
        /// <returns>Returns the teacher as <see cref="TeacherObject"/></returns>
        public TeacherObject GetTeacherByName(string firstname, string lastname)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher WHERE LOWER(firstname) = LOWER(@firstname) AND LOWER(lastname) = LOWER(@lastname)";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            reader = sqlite_cmd.ExecuteReader();
            string[] rData = new string[8];
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < rData.Length; i++)
                        rData[i] = reader.GetValue(i).ToString();
                }
            }
            else return null;
            return new TeacherObject(rData[0], rData[1], rData[2], rData[3], rData[4], rData[5], rData[6], rData[7]);
        }
        /// <summary>Edits a teacher in the database.</summary>
        public void EditTeacher(string short_name, string firstname, string lastname, string email, string phone_number, string subject1, string subject2 = null, string subject3 = null)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE teacher SET firstname=@firstname, lastname=@lastname,email=@email, phone_number=@phone_number, subject1=@subject1, subject2=@subject2, subject3=@subject3 WHERE short_name = @short_name";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@email", email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.Parameters.AddWithValue("@subject1", subject1);
            sqlite_cmd.Parameters.AddWithValue("@subject2", subject2);
            sqlite_cmd.Parameters.AddWithValue("@subject3", subject3);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Removes a teacher from the database.</summary>
        public void DeleteTeacher(string short_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM teacher WHERE short_name = @short_name ";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            sqlite_cmd.ExecuteNonQuery();
        }
        #endregion
        #region -------- EXAM --------
        /// <summary>Adds an exam to the database.</summary>
        public void AddExam(string date, string time, string exam_room, string preparation_room, int student, int student2, int student3, string t1, string t2, string t3, string subject, int duartion = 45)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO exam (date, time, exam_room, preparation_room, student, student2, student3, teacher_vorsitz, teacher_pruefer, teacher_protokoll, subject, duration) VALUES(@date,@time,@exam_room,@preparation_room,@student,@student2,@student3,@teacher_vorsitz,@teacher_pruefer,@teacher_protokoll,@subject,@duration)";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            sqlite_cmd.Parameters.AddWithValue("@preparation_room", preparation_room);
            sqlite_cmd.Parameters.AddWithValue("@student", student);
            sqlite_cmd.Parameters.AddWithValue("@student2", student2);
            sqlite_cmd.Parameters.AddWithValue("@student3", student3);
            sqlite_cmd.Parameters.AddWithValue("@teacher_vorsitz", t1);
            sqlite_cmd.Parameters.AddWithValue("@teacher_pruefer", t2);
            sqlite_cmd.Parameters.AddWithValue("@teacher_protokoll", t3);
            sqlite_cmd.Parameters.AddWithValue("@subject", subject);
            sqlite_cmd.Parameters.AddWithValue("@duration", duartion);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Searches all exams in the database.</summary>
        /// <returns>Returns all exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExams(bool orderByDate = false)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            if (orderByDate) sqlite_cmd.CommandText = "SELECT * FROM exam ORDER BY date";
            else sqlite_cmd.CommandText = "SELECT * FROM exam";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    if (rData[6].Length == 0) rData[6] = 0.ToString();
                    if (rData[7].Length == 0) rData[7] = 0.ToString();
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches a exam by id in the database.</summary>
        /// <returns>Returns the exam as <see cref="ExamObject"/></returns>
        public ExamObject GetExamById(int id)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            reader = sqlite_cmd.ExecuteReader();
            string[] rData = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                }
                reader.NextResult();
            }
            else { return null; }
            return new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12]));

        }
        /// <summary>Searches all exams at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsAtDate(string date)
        {
            //DateTime dt = DateTime.ParseExact(date, "dd.MM.yyyy", null);
            //date = dt.ToString("yyyy-MM-dd");
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    if (rData[6].Length == 0) rData[6] = 0.ToString();
                    if (rData[7].Length == 0) rData[7] = 0.ToString();
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches all exams of a teacher at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsFromTeacherAtDate(string date, string teacher)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date AND (teacher_vorsitz=@teacher OR teacher_pruefer=@teacher OR teacher_protokoll=@teacher)";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@teacher", teacher);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches all exams of a student at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsFromStudentAtDate(string date, int student_id)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date AND student = @student";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@student", student_id.ToString());
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches all exams of all students of a grade at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsFromGradeAtDate(string date, string grade)
        {
            LinkedList<StudentObject> tempStudentList = GetAllStudentsFromGrade(grade);
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            foreach (StudentObject s in tempStudentList)
                foreach (ExamObject studentExam in GetAllExamsFromStudentAtDate(date, s.Id))
                    data.AddLast(studentExam);
            return data;
        }
        /// <summary>Searches all exams of all students of a subject at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsFromSubjectAtDate(string date, string subject)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date AND subject = @subject";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@subject", subject);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches all exams in a room at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsAtDateAndRoom(string date, string exam_room)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date AND exam_room = @exam_room";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Searches all exams in a room at a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsAtDateTimeAndRoom(string date, string time, string exam_room)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date AND time = @time AND exam_room = @exam_room";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Edits an exam in the database.</summary>
        public void EditExam(int id, string date = null, string time = null, string exam_room = null, string preparation_room = null, int student = 0, int student2 = 0, int student3 = 0, string t1 = null, string t2 = null, string t3 = null, string subject = null, int duration = 0)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            if (date != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET date=@date WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@date", date);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (time != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET time=@time WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@time", time);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (exam_room != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET exam_room=@exam_room WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (preparation_room != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET preparation_room=@preparation_room WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@preparation_room", preparation_room);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (student != 0)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET student=@student WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@student", student);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (student2 != 0)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET student2=@student2 WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@student2", student2);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (student3 != 0)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET student3=@student3 WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@student3", student3);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (t1 != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET teacher_vorsitz=@teacher_vorsitz WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@teacher_vorsitz", t1);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (t2 != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET teacher_pruefer=@teacher_pruefer WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@teacher_pruefer", t2);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (t3 != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET teacher_protokoll=@teacher_protokoll WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@teacher_protokoll", t3);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (subject != null)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET subject=@subject WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@subject", subject);
                sqlite_cmd.ExecuteNonQuery();
            }
            if (duration != 0)
            {
                sqlite_cmd.CommandText = "UPDATE exam SET duration=@duration WHERE id = @id";
                sqlite_cmd.Parameters.AddWithValue("@id", id);
                sqlite_cmd.Parameters.AddWithValue("@duration", duration);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        /// <summary>Searches all exams before a date in the database.</summary>
        /// <returns>Returns the exams as a <see cref="LinkedList{T}"/> with <see cref="ExamObject"/></returns>
        public LinkedList<ExamObject> GetAllExamsBeforeDate(string date)
        {
            LinkedList<ExamObject> data = new LinkedList<ExamObject>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date < @date ";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rData = new string[13];
                    for (int i = 0; i < rData.Length; i++)
                    {
                        rData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rData[i] = rData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rData[i] = rData[i].Split(' ')[1];
                            rData[i] = rData[i].Remove(rData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(new ExamObject(int.Parse(rData[0]), rData[1], rData[2], rData[3], rData[4], int.Parse(rData[5]), int.Parse(rData[6]), int.Parse(rData[7]), rData[8], rData[9], rData[10], rData[11], int.Parse(rData[12])));
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Changes the room of all exams at one date in the database.</summary>
        public void EditExamRoom(string date, string old_exam_room, string exam_room)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE exam SET exam_room=@exam_room WHERE date=@date AND exam_room=@old_exam_room";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@old_exam_room", old_exam_room);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Removes an Exam from the database.</summary>
        public void DeleteExam(int id)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM exam WHERE id = @id ";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Removes all Exams before this date from the database.</summary>
        public void DeleteOldExams(string date)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM exam WHERE date < @date ";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.ExecuteNonQuery();
        }
        /// <summary>Checks if a room is empty in the database.</summary>
        /// <returns>Returns the room state as a <see cref="bool"/></returns>
        public bool CheckTimeAndRoom(string date, string time, string exam_room)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE  date = @date AND time = @time AND (exam_room = @exam_room OR preparation_room = @exam_room)";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            if (reader.HasRows) return true;
            else return false;
        }
        #endregion
        #region -------- ROOM --------
        /// <summary>Adds an room to the database.</summary>
        public void AddRoom(string room_name)
        {
            if (GetRoom(room_name) == null)
            {
                SQLiteCommand sqlite_cmd = connection.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO room (room_name) VALUES(@room_name)";
                sqlite_cmd.Parameters.AddWithValue("@room_name", room_name);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        /// <summary>Searches a room by roomname in the database.</summary>
        /// <returns>Returns the rooms as <see cref="string"/> <see cref="Array"/></returns>
        public string[] GetRoom(string room_name)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM room WHERE room_name = @room_name";
            sqlite_cmd.Parameters.AddWithValue("@room_name", room_name);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[1];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < 1; i++)
                        data[i] = reader.GetValue(i).ToString();
            else return null;
            return data;
        }
        /// <summary>Searches all rooms in the database.</summary>
        /// <returns>Returns the rooms as <see cref="LinkedList{T}"/> with <see cref="string"/> <see cref="Array"/></returns>
        public LinkedList<string[]> GetAllRooms()
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM room ORDER BY room_name";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[1];
                    for (int i = 0; i < 1; i++)
                        rowData[i] = reader.GetValue(i).ToString();
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Removes a room from the database.</summary>
        public void DeleteRoom(string room_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM room WHERE room_name = @room_name ";
            sqlite_cmd.Parameters.AddWithValue("@room_name", room_name);
            sqlite_cmd.ExecuteNonQuery();
        }
        #endregion
        #region -------- SUBJECT --------
        /// <summary>Adds an subject to the database.</summary>
        public void AddSubject(string subject_name)
        {
            if (GetSubject(subject_name) == null)
            {
                SQLiteCommand sqlite_cmd = connection.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO subject (subject_name) VALUES(@subject_name)";
                sqlite_cmd.Parameters.AddWithValue("@subject_name", subject_name);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        /// <summary>Searches a subject by subjectname in the database.</summary>
        /// <returns>Returns the subject as <see cref="string"/> <see cref="Array"/></returns>
        public string[] GetSubject(string subject_name)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM subject WHERE subject_name = @subject_name";
            sqlite_cmd.Parameters.AddWithValue("@subject_name", subject_name);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[1];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < 1; i++)
                        data[i] = reader.GetValue(i).ToString();
            else return null;
            return data;
        }
        /// <summary>Searches all subjects in the database.</summary>
        /// <returns>Returns the subjects as <see cref="LinkedList{T}"/> with <see cref="string"/> <see cref="Array"/></returns>
        public LinkedList<string[]> GetAllSubjects()
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM subject ORDER BY subject_name";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[1];
                    for (int i = 0; i < 1; i++)
                        rowData[i] = reader.GetValue(i).ToString();
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }
        /// <summary>Removes a subject from the database</summary>
        public void DeleteSubject(string subject_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM subject WHERE subject_name = @subject_name ";
            sqlite_cmd.Parameters.AddWithValue("@subject_name", subject_name);
            sqlite_cmd.ExecuteNonQuery();
        }
        #endregion
        #region -------- DBCreation --------
        private void CreateStudentDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS student (id INTEGER PRIMARY KEY AUTOINCREMENT, firstname TEXT NOT NULL, lastname TEXT NOT NULL, grade TEXT, email TEXT, phone_number TEXT)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateTeacherDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS teacher (short_name TEXT PRIMARY KEY NOT NULL, firstname TEXT NOT NULL, lastname TEXT NOT NULL,email TEXT, phone_number TEXT, subject1 TEXT NOT NULL, subject2 TEXT, subject3 TEXT)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateExamDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS exam (id INTEGER PRIMARY KEY AUTOINCREMENT, date DATE, time TIME NOT NULL, exam_room TEXT NOT NULL, preparation_room TEXT, student INTEGER, student2 INTEGER, student3 INTEGER, teacher_vorsitz TEXT, teacher_pruefer TEXT, teacher_protokoll TEXT, subject TEXT, duration INTEGER DEFAULT 45)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateRoomDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS room (room_name TEXT NOT NULL)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateSubjectDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS subject (subject_name TEXT NOT NULL)";
            sqlite_cmd.ExecuteNonQuery();
        }
        #endregion
    }
}
