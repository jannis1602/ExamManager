using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Pruefungen
{
    class Database // C:\Users\mattl\source\repos\Pruefungen\Pruefungen\bin\Debug\database.db
    {

        // #### TODO: conn.close ####

        SQLiteConnection connection;
        public Database()
        {
            connection = CreateConnection();
            // #####################################################################################
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            /*sqlite_cmd.CommandText = "DROP TABLE IF EXISTS exam";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS teacher";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "DROP TABLE IF EXISTS students";
            sqlite_cmd.ExecuteNonQuery();
            CreateStudentDB();
            CreateTeacherDB();
            CreateExamDB();*/
            // #####################################################################################
            //AddTeacher("user", "test", "user", "01234", "ma", "ph");
            //AddExam("2022-01-28", "09:00:00", "O-201", "O-202", "student1", "abc", "def", "ghi", "ma", 45);
            // #####################################################################################

            //Console.WriteLine("alle student: " + getAllstudent().Count);

            if (File.Exists("student.txt")) Console.WriteLine(" --- student.txt exists --- ");
            //InsertStudentFileIntoDB();

        }


        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            if (!File.Exists("database.db")) SQLiteConnection.CreateFile("database.db");
            //Console.WriteLine("db-File Exists: " + File.Exists(".\\database.db"));
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = False; Compress = True; ");
            try { sqlite_conn.Open(); } catch (Exception e) { Console.WriteLine(e.Message); }
            return sqlite_conn;
        }

        // [[TODO addPause(int time)]]

        // student
        // ID firstname lastname grade Email TelNummer
        private void AddStudent(string firstname, string lastname, string grade, string email = null, string phone_number = "0")
        {
            if (email == null) email = firstname.Split(' ')[0] + "." + lastname.Replace(" ", ".") + "@gymrahden.de";
            //if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            if (GetStudent(firstname, lastname, grade) != null) return;                     // TODO: ERROR Message
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO students (firstname, lastname, grade, email, phone_number) VALUES(@firstname,@lastname,@grade,@email,@phone_number); ";
            sqlite_cmd.Parameters.AddWithValue("@firstname", firstname);
            sqlite_cmd.Parameters.AddWithValue("@lastname", lastname);
            sqlite_cmd.Parameters.AddWithValue("@grade", grade);
            sqlite_cmd.Parameters.AddWithValue("@email", email);
            sqlite_cmd.Parameters.AddWithValue("@phone_number", phone_number);
            sqlite_cmd.ExecuteNonQuery();
            //conn.Close();  
        }
        private void InsertStudentFileIntoDB()
        {
            bool editDoppelnamen = false;

            string[] lines = File.ReadAllLines("schueler.txt"); // Filechooser if default null
            foreach (string line in lines)
            {
                //Console.WriteLine(line);
                if (line.Split(' ').Length > 2)
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
                else AddStudent(line.Split(' ')[0], line.Split(' ')[1], "Q2");
            }
        }

        public string[] GetStudent(string firstname, string lastname, string grade) // nur string[] keine List!!!
        {
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM students WHERE LOWER(firstname) = LOWER(@firstname) AND LOWER(lastname) = LOWER(@lastname) AND grade = @grade"; // nur wenn kein student-object
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
                    sqlite_cmd.CommandText = "SELECT * FROM students WHERE firstname = @firstname AND lastname = @lastname AND grade = @grade";
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
            sqlite_cmd.CommandText = "SELECT * FROM students WHERE id = @id";
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
            sqlite_cmd.CommandText = "SELECT * FROM students WHERE id = @id";
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
        public LinkedList<string[]> GetAllStudents()      // TODO: List with List
        {
            LinkedList<string[]> data = new LinkedList<string[]>();
            SQLiteDataReader reader;
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM students";
            reader = sqlite_cmd.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[3];
                    for (int i = 0; i < 3; i++)
                    {
                        rowData[i] = reader.GetValue(i).ToString();
                    }
                    data.AddLast(rowData);
                }
                reader.NextResult();
            }
            return data;
        }

        // Lehrer
        // Kürzel firstname lastname TelNummer Faecher
        private void AddTeacher(string short_name, string firstname, string lastname, string phone_number, string subject1, string subject2 = null, string subject3 = null)
        {
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

        private bool CheckTimeAndRoom(string time, string exam_room)
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM exam WHERE time  = @time AND exam_room  = @exam_room";
            sqlite_cmd.Parameters.AddWithValue("@time", time);
            sqlite_cmd.Parameters.AddWithValue("@exam_room", exam_room);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            if (reader.HasRows) return true;
            else return false;
        }
        // student
        // ID firstname lastname grade Email TelNummer
        private void CreateStudentDB()
        {
            connection = CreateConnection();
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS students (id INTEGER PRIMARY KEY AUTOINCREMENT, firstname TEXT NOT NULL, lastname TEXT NOT NULL, grade TEXT, email TEXT, phone_number TEXT)";
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
    }
}
