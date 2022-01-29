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

        SQLiteConnection connection;
        public Database()
        {
            string path = Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager\\");
            if (Properties.Settings.Default.databasePath == "default")
                Console.WriteLine(Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager"));
            else
            { Console.WriteLine(path); path = Properties.Settings.Default.databasePath; }

            connection = CreateConnection(path);
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


        private SQLiteConnection CreateConnection(string path)
        {
            SQLiteConnection sqlite_conn;
            if (!File.Exists(path + "database.db")) { Directory.CreateDirectory(path); SQLiteConnection.CreateFile(path + "database.db"); }
            sqlite_conn = new SQLiteConnection("Data Source=" + path + "database.db; Version = 3; New = False; Compress = True; ");
            try { sqlite_conn.Open(); } catch (Exception e) { Console.WriteLine(e.Message); }
            return sqlite_conn;
        }

        // ---- STUDENT ---- ########################################################################################################################## 
        // ID firstname lastname grade Email TelNummer
        public void AddStudent(string firstname, string lastname, string grade, string email = null, string phone_number = "0")
        {
            if (email == null) email = firstname.Split(' ')[0] + "." + lastname.Replace(" ", ".") + "@gymrahden.de";
            if (GetStudent(firstname, lastname, grade) != null) return;                     // TODO: ERROR Message
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO student (firstname, lastname, grade, email, phone_number) VALUES(@firstname,@lastname,@grade,@email,@phone_number); ";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            sqlite_cmd.Parameters.AddWithValue("@email", email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.ExecuteNonQuery();
        }
        public void InsertStudentFileIntoDB(string file, string grade)
        {
            bool editDoppelnamen = false;

            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (!line[0].Equals('#'))
                        if (line.Split(' ').Length > 2)
                        {
                            if (!editDoppelnamen)   // TODO: Abfrage am Anfang!
                            {
                                string tempfirstname = null;
                                for (int i = 0; i < line.Split(' ').Length - 1; i++)
                                    tempfirstname += line.Split(' ')[i] += " ";
                                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                                string templastname = null;
                                templastname += line.Split(' ')[line.Split(' ').Length - 1];
                                AddStudent(tempfirstname, templastname, grade);
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
                                    if (result == DialogResult.Yes) { AddStudent(tempfirstname, templastname, grade); break; }
                                }
                            }
                        }
                        else AddStudent(line.Split(' ')[0], line.Split(' ')[1], grade);
                }
            }
        }

        public string[] GetStudent(string firstname, string lastname, string grade)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE LOWER(firstname) = LOWER(@firstname) AND LOWER(lastname) = LOWER(@lastname) AND grade = @grade";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[6];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < 6; i++)
                        data[i] = reader.GetValue(i).ToString();
            else return null;
            return data;
        }
        public string[] GetStudentByID(int id)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[6];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < 6; i++)
                        data[i] = reader.GetValue(i).ToString();
            else return null;
            return data;
        }

        public LinkedList<string[]> GetAllStudents()
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[6];
                    for (int i = 0; i < 6; i++)
                        rowData[i] = reader.GetValue(i).ToString();
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }

        public LinkedList<string[]> GetAllStudentsFromGrade(string grade)
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE grade=@grade";
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[6];
                    for (int i = 0; i < 6; i++)
                        rowData[i] = reader.GetValue(i).ToString();
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }

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
        public void DeleteStudent(int id)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM student WHERE id = @id ";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }
        public void DeleteGrade(string grade)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM student WHERE grade = @grade ";
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            sqlite_cmd.ExecuteNonQuery();
        }
        // ---- TEACHER ---- ##########################################################################################################################
        // Kürzel firstname lastname TelNummer Faecher
        public void AddTeacher(string short_name, string firstname, string lastname, string phone_number, string subject1, string subject2 = null, string subject3 = null)
        {
            if (GetTeacherByID(short_name) != null) { return; }
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO teacher (short_name, firstname, lastname, phone_number, subject1, subject2, subject3) VALUES(@short_name,@firstname,@lastname,@phone_number,@subject1,@subject2,@subject3); ";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.Parameters.AddWithValue("@subject1", subject1);
            sqlite_cmd.Parameters.AddWithValue("@subject2", subject2);
            sqlite_cmd.Parameters.AddWithValue("@subject3", subject3);
            sqlite_cmd.ExecuteNonQuery();
        }

        public LinkedList<string[]> GetAllTeachers()
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[7];
                    for (int i = 0; i < 7; i++)
                        rowData[i] = reader.GetValue(i).ToString();
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }

        public string[] GetTeacherByID(string short_name)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher WHERE short_name = @short_name";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[7];
            if (reader.HasRows)
                while (reader.Read())
                    for (int i = 0; i < 7; i++)
                        data[i] = reader.GetValue(i).ToString();
            else return null;
            return data;
        }

        public string[] GetTeacherByName(string firstname, string lastname)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher WHERE firstname = @firstname AND lastname = @lastname";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[7];
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < 7; i++)
                        data[i] = reader.GetValue(i).ToString();
                }
            }
            else return null;
            return data;
        }
        public void EditTeacher(string short_name, string firstname, string lastname, string phone_number, string subject1, string subject2 = null, string subject3 = null)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE teacher SET firstname=@firstname, lastname=@lastname, phone_number=@phone_number, subject1=@subject1, subject2=@subject2, subject3=@subject3 WHERE short_name = @short_name";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.Parameters.AddWithValue("@subject1", subject1);
            sqlite_cmd.Parameters.AddWithValue("@subject2", subject2);
            sqlite_cmd.Parameters.AddWithValue("@subject3", subject3);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void DeleteTeacher(string short_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM teacher WHERE short_name = @short_name ";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            sqlite_cmd.ExecuteNonQuery();
        }

        // ---- EXAM ---- ##################################################################################################################################
        // ID student_ID VorsitzKuerzel PrueferKuerzel ProtokollKuerzel Fach Raum_Pruefung Raum_Vorbereitung Raum_Abholen Datum Uhrzeit Schulstunden
        public void AddExam(string date, string time, string exam_room, string preparation_room, string student, string t1, string t2, string t3, string subject, int duartion = 45)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO exam (date, time, exam_room, preparation_room, student, teacher_vorsitz, teacher_pruefer, teacher_protokoll, subject, duration) VALUES(@date,@time,@exam_room,@preparation_room,@student,@teacher_vorsitz,@teacher_pruefer,@teacher_protokoll,@subject,@duration)";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            sqlite_cmd.Parameters.AddWithValue("@preparation_room", preparation_room);
            sqlite_cmd.Parameters.AddWithValue("@student", student);
            sqlite_cmd.Parameters.AddWithValue("@teacher_vorsitz", t1);
            sqlite_cmd.Parameters.AddWithValue("@teacher_pruefer", t2);
            sqlite_cmd.Parameters.AddWithValue("@teacher_protokoll", t3);
            sqlite_cmd.Parameters.AddWithValue("@subject", subject);
            sqlite_cmd.Parameters.AddWithValue("@duration", duartion);
            sqlite_cmd.ExecuteNonQuery();
        }

        public LinkedList<string[]> GetAllExams()
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam ";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rowData[i] = rowData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rowData[i] = rowData[i].Split(' ')[1];
                            rowData[i] = rowData[i].Remove(rowData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }

        public string[] GetExamById(int id)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        data[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            data[i] = data[i].Split(' ')[0];
                        if (i == 2)
                        {
                            data[i] = data[i].Split(' ')[1];
                            data[i] = data[i].Remove(data[i].Length - 3, 3);
                        }
                    }
                }
                reader.NextResult();
            }
            else { return null; }
            return data;
        }

        public LinkedList<string[]> GetAllExamsAtDate(string date)
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE date = @date";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rowData[i] = rowData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rowData[i] = rowData[i].Split(' ')[1];
                            rowData[i] = rowData[i].Remove(rowData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }
        public LinkedList<string[]> GetAllExamsAtDateAndRoom(string date, string exam_room)
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
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
                    string[] rowData = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                        if (i == 1)
                            rowData[i] = rowData[i].Split(' ')[0];
                        if (i == 2)
                        {
                            rowData[i] = rowData[i].Split(' ')[1];
                            rowData[i] = rowData[i].Remove(rowData[i].Length - 3, 3);
                        }
                    }
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }

        public void EditExam(int id, string date, string time, string exam_room, string preparation_room, string student, string t1, string t2, string t3, string subject, int duartion)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE exam SET date=@date, time=@time, exam_room=@exam_room, preparation_room=@preparation_room, student=@student, teacher_vorsitz=@teacher_vorsitz, teacher_pruefer=@teacher_pruefer, teacher_protokoll=@teacher_protokoll, subject=@subject, duration=@duration WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            sqlite_cmd.Parameters.AddWithValue("@preparation_room", preparation_room);
            sqlite_cmd.Parameters.AddWithValue("@student", student);
            sqlite_cmd.Parameters.AddWithValue("@teacher_vorsitz", t1);
            sqlite_cmd.Parameters.AddWithValue("@teacher_pruefer", t2);
            sqlite_cmd.Parameters.AddWithValue("@teacher_protokoll", t3);
            sqlite_cmd.Parameters.AddWithValue("@subject", subject);
            sqlite_cmd.Parameters.AddWithValue("@duration", duartion);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void EditExamRoom(string date, string old_exam_room, string exam_room)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE exam SET exam_room=@exam_room WHERE date=@date AND exam_room=@old_exam_room";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@old_exam_room", old_exam_room);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void DeleteExam(int id)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM exam WHERE id = @id ";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
        }

        public bool CheckTimeAndRoom(string date, string time, string exam_room)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE  date = @date AND time = @time AND exam_room = @exam_room";
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            if (reader.HasRows) return true;
            else return false;
        }

        // ---- room ---- #############################################################################################################################

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

        public void DeleteRoom(string room_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM room WHERE room_name = @room_name ";
            sqlite_cmd.Parameters.AddWithValue("@room_name", room_name);
            sqlite_cmd.ExecuteNonQuery();
        }

        // ---- subject ---- ######################################################################################################################

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

        public void DeleteSubject(string subject_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM subject WHERE subject_name = @subject_name ";
            sqlite_cmd.Parameters.AddWithValue("@subject_name", subject_name);
            sqlite_cmd.ExecuteNonQuery();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ---- CREATE-DATABASE ---- //
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void CreateStudentDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS student (id INTEGER PRIMARY KEY AUTOINCREMENT, firstname TEXT NOT NULL, lastname TEXT NOT NULL, grade TEXT, email TEXT, phone_number TEXT)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateTeacherDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS teacher (short_name TEXT PRIMARY KEY NOT NULL, firstname TEXT NOT NULL, lastname TEXT NOT NULL, phone_number TEXT, subject1 TEXT NOT NULL, subject2 TEXT, subject3 TEXT)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateExamDB()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS exam (id INTEGER PRIMARY KEY AUTOINCREMENT, date DATE, time TIME NOT NULL, exam_room TEXT NOT NULL, preparation_room TEXT, student TEXT, teacher_vorsitz TEXT, teacher_pruefer TEXT, teacher_protokoll TEXT, subject TEXT, duration INTEGER DEFAULT 45)";
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
    }
}
