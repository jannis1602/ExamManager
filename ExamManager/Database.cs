using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace ExamManager
{
    class Database // C:\Users\mattl\source\repos\Pruefungen\Pruefungen\bin\Debug\database.db
                   //  C:\Users\mattl\AppData\Roaming
                   // Environment.ExpandEnvironmentVariables("%AppData%\\DateLinks.xml");
    {

        // #### TODO: conn.close ####

        SQLiteConnection connection;
        public Database()
        {
            Console.WriteLine(Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager"));
            connection = CreateConnection();
            CreateStudentDB();
            CreateTeacherDB();
            CreateExamDB();
            CreateRoomDB();
            CreateSubjectDB();
            //InsertStudentFileIntoDB("schueler.txt");
            // #####################################################################################
            /*SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS exam";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS teacher";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS student";
            sqlite_cmd.ExecuteNonQuery();*/
            // #####################################################################################
            //AddTeacher("user", "test", "user", "01234", "ma", "ph");
            //AddExam("2022-01-28", "09:00:00", "O-201", "O-202", "student1", "abc", "def", "ghi", "ma", 45);
            // #####################################################################################

            //Console.WriteLine("alle student: " + getAllstudent().Count);

            if (File.Exists("schueler.txt")) Console.WriteLine(" --- schueler.txt exists --- ");
            //InsertStudentFileIntoDB();
            AddTeacher("DÖ", "Anette", "Döding", "0", "Mathe", "Informatik", "Physik");
            AddTeacher("DRN", "Gesine", "Dronsz", "0", "Englisch", "Geschichte", "ev.Religion");
            AddTeacher("BRER", "Silke", "Breier", "0", "Deutsch", "Englisch");
            AddTeacher("RE", "Nils", "Rehm", "0", "Deutsch", "Sowi");
            AddTeacher("BS", "Kai", "Bechstein", "0", "Chemie", "Physik");


        }


        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            string path = Environment.ExpandEnvironmentVariables("%AppData%\\ExamManager\\");
            if (!File.Exists(path + "database.db")) { Directory.CreateDirectory(path); SQLiteConnection.CreateFile(path + "database.db"); }
            //Console.WriteLine("db-File Exists: " + File.Exists(".\\database.db"));
            sqlite_conn = new SQLiteConnection("Data Source=" + path + "database.db; Version = 3; New = False; Compress = True; ");
            try { sqlite_conn.Open(); } catch (Exception e) { Console.WriteLine(e.Message); }
            return sqlite_conn;
        }

        // [[TODO addPause(int time)]]

        // student
        // ID firstname lastname grade Email TelNummer
        public void AddStudent(string firstname, string lastname, string grade, string email = null, string phone_number = "0")
        {
            if (email == null) email = firstname.Split(' ')[0] + "." + lastname.Replace(" ", ".") + "@gymrahden.de";
            //if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            if (GetStudent(firstname, lastname, grade) != null) return;                     // TODO: ERROR Message
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO student (firstname, lastname, grade, email, phone_number) VALUES(@firstname,@lastname,@grade,@email,@phone_number); ";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            sqlite_cmd.Parameters.AddWithValue("@email", email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.ExecuteNonQuery();
            //conn.Close();  
        }
        public void InsertStudentFileIntoDB(string file)
        {
            bool editDoppelnamen = false;

            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file); // Filechooser if default null
                foreach (string line in lines)
                {
                    if (!line[0].Equals('#'))

                        //Console.WriteLine(line);
                        if (line.Split(' ').Length > 2)
                        {
                            if (!editDoppelnamen)
                            {
                                string tempfirstname = null;
                                for (int i = 0; i < line.Split(' ').Length - 1; i++)
                                    tempfirstname += line.Split(' ')[i] += " ";
                                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                                string templastname = null;
                                templastname += line.Split(' ')[line.Split(' ').Length - 1];
                                AddStudent(tempfirstname, templastname, "Q2");
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
                                    if (result == DialogResult.Yes) { AddStudent(tempfirstname, templastname, "Q2"); break; }
                                    // MessageBox.Show(line + " nicht hinzugefügt!", "Info!", MessageBoxButtons.OK); // cancle -> retry
                                }
                            }
                        }
                        else AddStudent(line.Split(' ')[0], line.Split(' ')[1], "Q2");
                }
            }
        }

        public string[] GetStudent(string firstname, string lastname, string grade) // nur string[] keine List!!!
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE LOWER(firstname) = LOWER(@firstname) AND LOWER(lastname) = LOWER(@lastname) AND grade = @grade"; // nur wenn kein student-object
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[4];
            if (reader.HasRows)                          // TODO if rows > 1 => ERROR
            {
                while (reader.Read())
                {
                    for (int i = 0; i < 4; i++)               // ID ??? ------------ // oder student object erstellen?
                        data[i] = reader.GetValue(i).ToString();
                }
            }
            else return null;
            return data;
        }

        /*        public List<string[]> GetStudent(string firstname, string lastname, string grade) // nur string[] keine List!!!
                {
                    List<string[]> data = null;
                    SQLiteDataReader reader;
                    SQLiteCommand sqlite_cmd = connection.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT * FROM student WHERE firstname = @firstname AND lastname = @lastname AND grade = @grade";
                    sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
                    sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
                    sqlite_cmd.Parameters.AddWithValue("@grade", grade);
                    reader = sqlite_cmd.ExecuteReader();
                    while (reader.HasRows)                          // TODO if rows > 1 => ERROR
                    {
                        data = new List<string[]>();
                        while (reader.Read())
                        {
                            string[] rowData = new string[3];
                            for (int i = 1; i < 3; i++)               // ID ??? ------------ // oder student object erstellen?
                                rowData[i] = reader.GetString(i);
                            data.Add(rowData);
                        }
                        reader.NextResult();
                    }
                    return data;
                }*/
        public string[] GetStudentByID(int id)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            reader = sqlite_cmd.ExecuteReader();
            string[] data = new string[4];
            if (reader.HasRows)                          // TODO if rows > 1 => ERROR
            {
                while (reader.Read())
                {
                    for (int i = 0; i < 4; i++)               // ID ??? ------------ // oder student object erstellen?
                        data[i] = reader.GetValue(i).ToString();
                }
            }
            else return null;
            return data;
        }

        /*public List<string[]> GetStudentByID(int id)
        {
            List<string[]> data = new List<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM student WHERE id = @id";
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)                          // TODO if rows > 1 => ERROR
            {
                while (reader.Read())
                {
                    string[] rowData = new string[3];
                    for (int i = 1; i < 3; i++)               // ID ??? ------------ // oder student object erstellen?
                        rowData[i] = reader.GetString(i);
                    data.Add(rowData);
                }
                reader.NextResult();
            }
            return data;
        }*/


        // ## DEV ##
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
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                    }
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


        // Lehrer
        // Kürzel firstname lastname TelNummer Faecher
        public void AddTeacher(string short_name, string firstname, string lastname, string phone_number, string subject1, string subject2 = null, string subject3 = null)
        {
            // TODO: check if teacher exists
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
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                    }
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
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


        public string[] GetTeacherByID(string short_name)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM teacher WHERE short_name = @short_name";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
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
        public void DeleteTeacher(string short_name)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM teacher WHERE short_name = @short_name ";
            sqlite_cmd.Parameters.AddWithValue("@short_name", short_name);
            sqlite_cmd.ExecuteNonQuery();
        }

        // Pruefung
        // ID student_ID VorsitzKuerzel PrueferKuerzel ProtokollKuerzel Fach Raum_Pruefung Raum_Vorbereitung Raum_Abholen Datum Uhrzeit Schulstunden

        // ## DEV ##
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

        // room database /////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            {
                while (reader.Read())
                {
                    for (int i = 0; i < 1; i++)
                    {
                        data[i] = reader.GetValue(i).ToString();
                    }
                }
            }
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
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                    }
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

        // subject database //////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            {
                while (reader.Read())
                {
                    for (int i = 0; i < 1; i++)
                    {
                        data[i] = reader.GetValue(i).ToString();
                    }
                }
            }
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
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                    }
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


        // student
        // ID firstname lastname grade Email TelNummer
        private void CreateStudentDB()
        {
            connection = CreateConnection();
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS student (id INTEGER PRIMARY KEY AUTOINCREMENT, firstname TEXT NOT NULL, lastname TEXT NOT NULL, grade TEXT, email TEXT, phone_number TEXT)";
            sqlite_cmd.ExecuteNonQuery();
        }
        // Lehrer
        // Kürzel firstname lastname TelNummer Faecher
        private void CreateTeacherDB()
        {
            connection = CreateConnection();
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS teacher (short_name TEXT PRIMARY KEY NOT NULL, firstname TEXT NOT NULL, lastname TEXT NOT NULL, phone_number TEXT, subject1 TEXT NOT NULL, subject2 TEXT, subject3 TEXT)";
            sqlite_cmd.ExecuteNonQuery();
        }
        // Pruefung (Kuerzel=id)
        // ID student_ID VorsitzKuerzel PrueferKuerzel ProtokollKuerzel Fach Raum_Pruefung Raum_Vorbereitung Raum_Abholen DatumUhrzeit (Dauer+default) Schulstunden(get with time)
        private void CreateExamDB()
        {
            connection = CreateConnection();
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS exam (id INTEGER PRIMARY KEY AUTOINCREMENT, date DATE, time TIME NOT NULL, exam_room TEXT NOT NULL, preparation_room TEXT, student TEXT, teacher_vorsitz TEXT, teacher_pruefer TEXT, teacher_protokoll TEXT, subject TEXT, duration INTEGER DEFAULT 45)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateRoomDB()
        {
            connection = CreateConnection();
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS room (room_name TEXT NOT NULL)";
            sqlite_cmd.ExecuteNonQuery();
        }
        private void CreateSubjectDB()
        {
            connection = CreateConnection();
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS subject (subject_name TEXT NOT NULL)";
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
