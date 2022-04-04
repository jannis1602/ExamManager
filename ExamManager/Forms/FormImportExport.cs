using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormImportExport : Form
    {
        private readonly Database database;
        private LinkedList<StudentObject> StudentList;
        private LinkedList<TeacherObject> TeacherList;
        private LinkedList<ExamObject> ExamList;
        private LinkedList<string> RoomList;
        private string SelectedFile = null;
        LinkedList<string[]> DataArrayList;

        enum DataType { Exam, Student, Teacher, Room };
        enum DataFormat { json, csv, txt };

        DataType dataType;
        DataFormat dataFormat;
        public FormImportExport(int tab = 0)
        {
            database = Program.database;
            InitializeComponent();
            tabControl.SelectedIndex = tab;
            dtp_table_date.Value = DateTime.Now;
            cb_export_days.Visible = false;
            cb_export_grade.Visible = false;
            cb_import_nameorder.SelectedIndex = 0;
            StudentList = new LinkedList<StudentObject>();
            TeacherList = new LinkedList<TeacherObject>();
            ExamList = new LinkedList<ExamObject>();
            // autocomplete
            LinkedList<StudentObject> allStudents = Program.database.GetAllStudents();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (StudentObject s in allStudents)
                if (!gradeList.Contains(s.Grade))
                    gradeList.AddLast(s.Grade);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList();
            gradeList = new LinkedList<string>(templist);
            string[] list = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                list[i] = gradeList.ElementAt(i);
            cb_import_grade.Items.AddRange(list);
            cb_export_grade.Items.Add("");
            cb_export_grade.Items.AddRange(list);
            // examdays
            LinkedList<Item> dayList = new LinkedList<Item>();
            foreach (ExamObject s in Program.database.GetAllExams(true))
            {
                if (!dayList.Any(n => n.date == s.Date))
                {
                    dayList.AddLast(new Item(s.Date, s.Date + "  ->  " + Program.database.GetAllExamsAtDate(s.Date).Count().ToString() + " Prüfungen"));
                }
            }
            Item[] dates = new Item[dayList.Count];
            for (int i = 0; i < dayList.Count; i++)
                dates[i] = dayList.ElementAt(i);
            cb_export_days.DisplayMember = nameof(Item.title);
            cb_export_days.Items.Clear();
            cb_export_days.Items.AddRange(dates);
            List<Item> item = new List<Item>();
            // table page
            cb_table_grade.Items.AddRange(list);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Import

        private void btn_import_add_Click(object sender, EventArgs e)
        {
            if (dataFormat == DataFormat.txt)
            {
                if (dataType == DataType.Exam && ExamList.Count > 0)
                { }
                else if (dataType == DataType.Student && StudentList.Count > 0)
                {
                    if (cb_import_grade.Text.Length < 1) { MessageBox.Show("Stufe auswählen", "Achtung"); return; }
                    foreach (StudentObject so in StudentList)
                    {
                        so.SetGrade(cb_import_grade.Text);
                        if (cb_import_generateemail.Checked) so.GenerateEmail();
                        so.AddToDatabase();
                    }
                    Console.WriteLine(StudentList.Count());
                    foreach (StudentObject so in StudentList)
                        Console.WriteLine(so.Fullname());
                }
                else if (dataType == DataType.Teacher && TeacherList.Count > 0)
                {
                    foreach (TeacherObject to in TeacherList)
                    {
                        if (cb_import_generateemail.Checked) to.GenerateEmail();
                        if (to.Subject1 != null && to.Subject1.Length > 0) database.AddSubject(to.Subject1);
                        if (to.Subject2 != null && to.Subject2.Length > 0) database.AddSubject(to.Subject2);
                        if (to.Subject3 != null && to.Subject3.Length > 0) database.AddSubject(to.Subject3);
                        to.AddToDatabase();
                    }
                    Console.WriteLine(TeacherList.Count());
                    foreach (TeacherObject to in TeacherList)
                        Console.WriteLine(to.Fullname());
                }
                else if (dataType == DataType.Room && RoomList.Count > 0)
                {
                    foreach (string s in RoomList)
                    {
                        Program.database.AddRoom(s);
                    }
                }
            }
            else if (dataFormat == DataFormat.json)
            {
                if (dataType == DataType.Exam && ExamList.Count > 0)
                {
                    foreach (ExamObject eo in ExamList)
                    {
                        eo.Student.AddToDatabase();
                        eo.Teacher1.AddToDatabase();
                        // TODO: check t2+t3 & s2+s3
                        // check student & teacher -> add
                        eo.AddToDatabase();
                    }
                    Console.WriteLine(ExamList.Count());
                    foreach (ExamObject eo in ExamList)
                        Console.WriteLine(eo.Time);
                }
                else if (dataType == DataType.Student && StudentList.Count > 0)
                {
                    if (cb_import_grade.Text.Length < 1) { MessageBox.Show("Stufe auswählen", "Achtung"); return; }
                    foreach (StudentObject so in StudentList)
                    {
                        if (cb_import_generateemail.Checked) so.GenerateEmail();
                        so.AddToDatabase();
                    }
                    Console.WriteLine(StudentList.Count());
                    foreach (StudentObject so in StudentList)
                        Console.WriteLine(so.Fullname());
                }
                else if (dataType == DataType.Teacher && TeacherList.Count > 0)
                {
                    foreach (TeacherObject to in TeacherList)
                    {
                        if (cb_import_generateemail.Checked) to.GenerateEmail();
                        if (to.Subject1 != null && to.Subject1.Length > 0) database.AddSubject(to.Subject1);
                        if (to.Subject2 != null && to.Subject2.Length > 0) database.AddSubject(to.Subject2);
                        if (to.Subject3 != null && to.Subject3.Length > 0) database.AddSubject(to.Subject3);
                        to.AddToDatabase();
                    }
                    Console.WriteLine(TeacherList.Count());
                    foreach (TeacherObject to in TeacherList)
                        Console.WriteLine(to.Fullname());
                }

            }
            else if (dataFormat == DataFormat.csv)
            {
                if (dataType == DataType.Exam && ExamList.Count > 0)
                {
                    foreach (ExamObject eo in ExamList)
                    {
                        // check student & teacher -> add
                        eo.AddToDatabase();
                    }
                    Console.WriteLine(ExamList.Count());
                    foreach (ExamObject eo in ExamList)
                        Console.WriteLine(eo.Time);
                }
                else if (dataType == DataType.Student && StudentList.Count > 0)
                {
                    if (cb_import_grade.Text.Length < 1) { MessageBox.Show("Stufe auswählen", "Achtung"); return; }
                    foreach (StudentObject so in StudentList)
                    {
                        if (cb_import_generateemail.Checked) so.GenerateEmail();
                        so.AddToDatabase();
                    }
                    Console.WriteLine(StudentList.Count());
                    foreach (StudentObject so in StudentList)
                        Console.WriteLine(so.Fullname());
                }
                else if (dataType == DataType.Teacher && TeacherList.Count > 0)
                {
                    foreach (TeacherObject to in TeacherList)
                    {
                        if (cb_import_generateemail.Checked) to.GenerateEmail();
                        if (to.Subject1 != null && to.Subject1.Length > 0) database.AddSubject(to.Subject1);
                        if (to.Subject2 != null && to.Subject2.Length > 0) database.AddSubject(to.Subject2);
                        if (to.Subject3 != null && to.Subject3.Length > 0) database.AddSubject(to.Subject3);
                        to.AddToDatabase();
                    }
                    Console.WriteLine(TeacherList.Count());
                    foreach (TeacherObject to in TeacherList)
                        Console.WriteLine(to.Fullname());
                }
            }
            MessageBox.Show("Daten hinzugefügt", "Info");

        }
        // txt
        private void btn_import_exam_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = false;
            flp_import_nameorder.Visible = false;
            MessageBox.Show("keine Funktion", "Achtung");
        }
        private void btn_import_student_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = true;
            flp_import_email.Visible = true;
            flp_import_nameorder.Visible = true;
            dataFormat = DataFormat.txt;
            dataType = DataType.Student;
            StudentList = new LinkedList<StudentObject>();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Schülerliste auswählen",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                lbl_import_file.Text = file;
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        if (!line[0].Equals('#'))
                        {
                            string tempfirstname = null;
                            for (int i = 0; i < line.Split(' ').Length - 1; i++)
                                tempfirstname += line.Split(' ')[i] += " ";
                            tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                            string templastname = null;
                            templastname += line.Split(' ')[line.Split(' ').Length - 1];
                            tempfirstname = tempfirstname.Replace(' ', '_');
                            templastname = templastname.Replace(' ', '_');
                            if (cb_import_nameorder.SelectedIndex == 0)
                                StudentList.AddLast(new StudentObject(0, tempfirstname, templastname, "0"));
                            else if (cb_import_nameorder.SelectedIndex == 1)
                                StudentList.AddLast(new StudentObject(0, templastname, tempfirstname, "0"));
                        }
                    }
                }
            }
        }
        private void btn_import_teacher_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = true;
            flp_import_nameorder.Visible = true;
            dataFormat = DataFormat.txt;
            dataType = DataType.Teacher;
            TeacherList = new LinkedList<TeacherObject>();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Lehrerliste auswählen",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                lbl_import_file.Text = file;
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        if (!line[0].Equals('#'))
                        {
                            string[] t = line.Replace("Dr. ", "").Replace(",", "").Split(' ');
                            if (cb_import_nameorder.SelectedIndex == 1)
                            {
                                string ln = t[2];
                                t[2] = t[1];
                                t[1] = ln;
                            }
                            if (t.Length == 5) TeacherList.AddLast(new TeacherObject(t[3], t[1], t[2], null, null, t[4], null, null));
                            else if (t.Length == 6) TeacherList.AddLast(new TeacherObject(t[3], t[1], t[2], null, null, t[4], t[5], null));
                            else if (t.Length == 7) TeacherList.AddLast(new TeacherObject(t[3], t[1], t[2], null, null, t[4], t[5], t[6]));
                        }
                    }
                }
            }


        }
        private void btn_import_room_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = false;
            flp_import_nameorder.Visible = false;
            dataFormat = DataFormat.txt;
            dataType = DataType.Room;
            RoomList = new LinkedList<string>();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Raumliste auswählen",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                lbl_import_file.Text = file;
                if (File.Exists(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        if (!line[0].Equals('#'))
                        {
                            if (!RoomList.Contains(line) && line.Length > 0) RoomList.AddLast(line);
                        }
                    }
                }
            }
        }
        // json
        private void btn_import_exam_json_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = false;
            flp_import_nameorder.Visible = false;
            dataFormat = DataFormat.json;
            dataType = DataType.Exam;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Prüfungsdatei auswählen",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                lbl_import_file.Text = file;
                using (StreamReader r = new StreamReader(file))
                {
                    string jsonString = r.ReadToEnd();
                    List<ExamObject> eoList = JsonConvert.DeserializeObject<List<ExamObject>>(jsonString);
                    ExamList = new LinkedList<ExamObject>(eoList);
                }
            }
        }
        private void btn_import_student_json_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = false;
            flp_import_nameorder.Visible = false;
            dataFormat = DataFormat.json;
            dataType = DataType.Student;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Schülerdatei auswählen",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                lbl_import_file.Text = file;
                using (StreamReader r = new StreamReader(file))
                {
                    string jsonString = r.ReadToEnd();
                    List<StudentObject> soList = JsonConvert.DeserializeObject<List<StudentObject>>(jsonString);
                    StudentList = new LinkedList<StudentObject>(soList);
                }
            }
        }
        private void btn_import_teacher_json_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = false;
            flp_import_nameorder.Visible = false;
            dataFormat = DataFormat.json;
            dataType = DataType.Teacher;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Lehrerdatei auswählen",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                lbl_import_file.Text = file;
                using (StreamReader r = new StreamReader(file))
                {
                    string jsonString = r.ReadToEnd();
                    List<TeacherObject> toList = JsonConvert.DeserializeObject<List<TeacherObject>>(jsonString);
                    TeacherList = new LinkedList<TeacherObject>(toList);
                }
            }
        }
        // csv
        private void btn_import_exam_csv_Click(object sender, EventArgs e)
        {
            // nameorder
            MessageBox.Show("keine Funktion", "Achtung");
        }
        private void btn_import_student_csv_Click(object sender, EventArgs e)
        {
            MessageBox.Show("keine Funktion", "Achtung");
        }
        private void btn_import_teacher_csv_Click(object sender, EventArgs e)
        {
            MessageBox.Show("keine Funktion", "Achtung");
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////


        #region export
        // json        
        private void btn_export_exam_json_Click(object sender, EventArgs e)
        {
            // TODO: select -> all exams, one day etc.
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Daten speichern",
                FileName = "ExamData.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var json = JsonConvert.SerializeObject(Program.database.GetAllExams(), Formatting.Indented);
            if (cb_export_singleexamday.Checked && cb_export_days.SelectedItem != null)
            {
                Item itm = cb_export_days.SelectedItem as Item;
                DateTime date = DateTime.ParseExact(itm.date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                json = JsonConvert.SerializeObject(Program.database.GetAllExamsAtDate(date.ToString("yyyy-MM-dd")), Formatting.Indented);
            }
            File.WriteAllText(sfd.FileName, json);
        }
        private void btn_export_student_json_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Schülerdaten speichern",
                FileName = "StudentData.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                CheckFileExists = true,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            LinkedList<StudentObject> sList = null;
            if (cb_export_singlegrade.Checked && cb_export_grade.Text.Length > 0) sList = database.GetAllStudentsFromGrade(cb_export_grade.Text);
            else sList = database.GetAllStudents();
            var json = JsonConvert.SerializeObject(sList, Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }
        private void btn_export_teacher_json_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Lehrerdaten speichern",
                FileName = "TeachertData.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                CheckFileExists = true,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var json = JsonConvert.SerializeObject(Program.database.GetAllTeachers(), Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }
        // csv
        private void btn_export_exam_csv_Click(object sender, EventArgs e)
        {
            if (cb_export_singleexamday.Checked == false)
            {
                cb_export_singleexamday.Checked = true;
                MessageBox.Show("Datum auswählen", "Achtung");
                return;
            }
            Item itm = cb_export_days.SelectedItem as Item;
            string date = itm.date;
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Prüfungstag speichern",
                FileName = "Prüfungstag-" + date + ".csv",
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            if (!File.Exists(sfd.FileName))
            {
                var csv = new StringBuilder();
                var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach", "Dauer");
                csv.AppendLine(firstLine);
                foreach (TeacherObject teacher in database.GetAllTeachers())
                    foreach (ExamObject exam in database.GetAllExamsFromTeacherAtDate(date, teacher.Shortname))
                    {
                        StudentObject student = exam.Student;
                        DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                        DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                        string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", teacher.Fullname(), time, exam.Examroom, exam.Preparationroom, student.Fullname(), exam.Teacher1Id, exam.Teacher2Id, exam.Teacher3Id, exam.Subject, exam.Duration);
                        csv.AppendLine(newLine);
                    }
                File.WriteAllText(sfd.FileName, csv.ToString());
            }
        }
        private void btn_export_student_csv_Click(object sender, EventArgs e)
        {
            // TODO: all or single grade

            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Schülerdaten speichern",
                FileName = "StudentData.csv",
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            if (!File.Exists(sfd.FileName))
            {
                var csv = new StringBuilder();
                var firstLine = string.Format("{0},{1},{2},{3},{4}", "Vorname", "Nachname", "Stufe", "Email", "Telefonnummer");
                csv.AppendLine(firstLine);
                LinkedList<StudentObject> sList = null;
                if (cb_export_singlegrade.Checked && cb_export_grade.Text.Length > 0)
                    sList = database.GetAllStudentsFromGrade(cb_export_grade.Text);
                else sList = database.GetAllStudents();
                foreach (StudentObject so in sList)
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4}", so.Firstname, so.Lastname, so.Grade, so.Email, so.Phonenumber);
                    csv.AppendLine(newLine);
                }
                File.WriteAllText(sfd.FileName, csv.ToString());
            }
        }
        private void btn_export_teacher_csv_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Lehrerdaten speichern",
                FileName = "TeacherData.csv",
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            if (!File.Exists(sfd.FileName))
            {
                var csv = new StringBuilder();
                var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "Kürzel", "Vorname", "Nachname", "Email", "Telefonnummer", "Fach1", "Fach2", "Fach3");
                csv.AppendLine(firstLine);
                foreach (TeacherObject to in database.GetAllTeachers())
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", to.Shortname, to.Firstname, to.Lastname, to.Email, to.Phonenumber, to.Subject1, to.Subject2, to.Subject3);
                    csv.AppendLine(newLine);
                }
                File.WriteAllText(sfd.FileName, csv.ToString());
            }
        }
        ////
        private void cb_export_singleexamday_CheckedChanged(object sender, EventArgs e)
        {
            cb_export_days.Visible = cb_export_singleexamday.Checked;
        }
        private void cb_export_singlegrade_CheckedChanged(object sender, EventArgs e)
        {
            cb_export_grade.Visible = cb_export_singlegrade.Checked;
        }
        class Item
        {
            public string date { get; }
            public string title { get; }

            public Item(string date, string title)
            {
                this.date = date;
                this.title = title;
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region table
        private LinkedList<string[]> ReadFile()
        {
            DataArrayList = new LinkedList<string[]>();

            FileStream fStream = File.Open(SelectedFile, FileMode.Open, FileAccess.Read);
            IExcelDataReader edr = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            string grade = cb_table_grade.Text;
            if (grade == null || grade.Length == 0) { MessageBox.Show("keine Stufe auswählt", "Fehler"); return null; }
            string subject = null;
            string teacher = null;
            string student = null;
            string course = null;
            while (edr.Read())
            {
                if (edr.GetValue(2) != null && edr.GetValue(2).ToString().Length > 3)
                {
                    subject = edr.GetValue(2).ToString().Split(' ')[0];
                    if (cb_table_remove_numbers.Checked) subject = string.Concat(subject.Where(char.IsLetter));
                    course = edr.GetValue(2).ToString().Split(' ')[1];
                }
                if (edr.GetValue(4) != null && edr.GetValue(4).ToString().Length > 1)
                    teacher = edr.GetValue(4).ToString();
                if (edr.GetValue(1) != null && edr.GetValue(1).ToString().Length > 3 && edr.GetValue(1).ToString().Contains(','))
                {
                    student = edr.GetValue(1).ToString();
                    Console.WriteLine("Readed data: " + subject + "  " + teacher + "  " + student);
                    string[] data = { subject, teacher, student };
                    DataArrayList.AddLast(data);
                }
            }
            fStream.Close();
            AddData(DataArrayList, grade);

            return DataArrayList;
        }

        private void AddData(LinkedList<string[]> list, string grade)
        {
            // ---- check teacher and student names ----
            string date = dtp_table_date.Value.ToString("yyyy-MM-dd");
            int duration = int.Parse(tb_table_duration.Text);
            int examCount = 0;
            int room = 0;
            LinkedList<StudentObject> missingStudentList = new LinkedList<StudentObject>();
            LinkedList<string> missingTeacherList = new LinkedList<string>();
            LinkedList<string> missingSubjectList = new LinkedList<string>();
            int element = 0;
            int t = 1;
            while (list.Count > examCount)
            {
                examCount++;
                // ---- room names ----
                if (list.Count == examCount) break;
                DateTime time = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration * t);
                if (time.AddMinutes(duration).Hour > DateTime.ParseExact("17:30", "HH:mm", null, System.Globalization.DateTimeStyles.None).Hour)
                { t = 1; room++; time = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration * t); }
                string[] d = list.ElementAt(element);
                string s = d[2].Replace(", ", ",").Replace(" ", "_");
                Program.database.AddRoom("R" + room);
                if (Program.database.GetSubject(d[0]) == null && !missingSubjectList.Contains(d[0])) missingSubjectList.AddLast(d[0]);

                // ---- check names in db ----
                if (Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]) == null && Program.database.GetTeacherByID(d[1]) == null)
                {
                    missingTeacherList.AddLast(d[1]);
                    missingStudentList.AddLast(new StudentObject(0, s.Split(',')[1], s.Split(',')[0], grade));
                }
                else if (Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]) == null)
                { missingStudentList.AddLast(new StudentObject(0, s.Split(',')[1], s.Split(',')[0], grade)); }
                else if (Program.database.GetTeacherByID(d[1]) == null)
                { missingTeacherList.AddLast(d[1]); }
                //Console.WriteLine(examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + 30);
                t++;
                element++;
            }

            string students = "";
            Console.WriteLine("Missing Students: ");
            foreach (StudentObject so in missingStudentList)
            { Console.WriteLine(so.Fullname()); students += so.Fullname() + " "; }

            if (missingStudentList.Count > 0)
            {
                DialogResult resultAddStudents = MessageBox.Show(missingStudentList.Count + " Schüler hinzufügen?\n" + students, "Achtung", MessageBoxButtons.YesNoCancel);
                if (resultAddStudents == DialogResult.Yes) foreach (StudentObject so in missingStudentList)
                        if (Program.database.GetStudentByName(so.Firstname, so.Lastname) == null) so.AddToDatabase();
                if (resultAddStudents == DialogResult.Cancel) return;
            }

            if (missingSubjectList.Count > 0)
            {
                string subjects = "";
                foreach (string su in missingSubjectList) subjects += su + " ";
                DialogResult resultSubjects = MessageBox.Show(missingSubjectList.Count + " Fächer hinzufügen?\n" + subjects, "Achtung", MessageBoxButtons.YesNo);
                if (resultSubjects == DialogResult.Yes) foreach (string su in missingSubjectList)
                        if (Program.database.GetSubject(su) == null) Program.database.AddSubject(su);
            }

            DialogResult result = MessageBox.Show(list.Count() + " Prüfungen hinzufügen?", "Warnung", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;
            AddMissingDataToDB(list);
        }

        private void AddMissingDataToDB(LinkedList<string[]> list)
        {
            string date = dtp_table_date.Value.ToString("yyyy-MM-dd");
            int duration = int.Parse(tb_table_duration.Text);
            int examCount = 0;
            int room = 0;
            LinkedList<ExamObject> examList = new LinkedList<ExamObject>();
            int element = 0;
            int t = 1;
            while (list.Count > examCount)
            {
                examCount++;
                if (list.Count == examCount) break;
                DateTime time = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration * t);
                if (time.AddMinutes(duration).Hour > DateTime.ParseExact("17:30", "HH:mm", null, System.Globalization.DateTimeStyles.None).Hour)
                { t = 1; room++; time = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration * t); }

                string[] d = list.ElementAt(element);
                string s = d[2].Replace(", ", ",").Replace(" ", "_"); // TODO: teacher -> t1/t2/t3?
                Program.database.AddRoom("R" + room);
                //if (Program.database.GetSubject(d[0]) == null && !missingSubjectList.Contains(d[0])) missingSubjectList.AddLast(d[0]);
                //if (Program.database.GetAllExamsAtDateTimeAndRoom(date, time.ToString("HH:mm"), "R" + room).Count == 0)
                //{
                if (Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]) == null && Program.database.GetTeacherByID(d[1]) == null)
                {
                    examList.AddLast(new ExamObject(0, date, time.ToString("HH:mm"), "R" + room, null, 0, 0, 0, d[1] + "*", null, null, d[0], duration));
                    Console.WriteLine("NOTFOUND-TS: " + examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " NODB:" + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + duration); ;
                }
                else if (Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]) == null)
                {
                    examList.AddLast(new ExamObject(0, date, time.ToString("HH:mm"), "R" + room, "", 0, 0, 0, Program.database.GetTeacherByID(d[1]).Shortname, null, null, d[0], duration));
                    Console.WriteLine("NOTFOUND-S: " + examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + duration);
                }
                else if (Program.database.GetTeacherByID(d[1]) == null)
                {
                    examList.AddLast(new ExamObject(0, date, time.ToString("HH:mm"), "R" + room, null, Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]).Id, 0, 0, d[1] + "*", null, null, d[0], duration));
                    Console.WriteLine("NOTFOUND-T: " + examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + duration);
                }
                else
                {
                    Console.WriteLine(examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]).Id + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + Program.database.GetTeacherByID(d[1]).Shortname + " -  - " + d[0] + " " + duration);
                    examList.AddLast(new ExamObject(0, date, time.ToString("HH:mm"), "R" + room, null, Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]).Id, 0, 0, Program.database.GetTeacherByID(d[1]).Shortname, null, null, d[0], duration));
                }
                element++;
                t++;
                //}
            }
            foreach (ExamObject eo in examList) eo.AddToDatabase(checkTeacherDB: false, noError: true);
        }

        private void btn_table_select_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "xlsx Datei auswählen";
            ofd.Multiselect = false;
            ofd.Filter = "Text files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (ofd.ShowDialog() != DialogResult.OK) return;
            lbl_table_filename.Text = ofd.FileName;
            SelectedFile = ofd.FileName;
        }
        private void btn_table_add_Click(object sender, EventArgs e)
        {
            ReadFile();
        }
        private void tb_table_duration_TextChanged(object sender, EventArgs e)
        {
            if (tb_table_duration.Text.Length == 0) return;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb_table_duration.Text, "[^0-9]"))
                tb_table_duration.Text = tb_table_duration.Text.Remove(tb_table_duration.Text.Length - 1);
        }
        #endregion

        private void cb_import_generateemail_CheckedChanged(object sender, EventArgs e)
        {
            string domain = Properties.Settings.Default.EmailDomain;
            if (cb_import_generateemail.Checked && domain.Length < 2) { cb_import_generateemail.Checked = false; MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung"); }
        }

    }
}
