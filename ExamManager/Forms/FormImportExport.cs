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

        enum DataType { Exam, Student, Teacher };
        enum DataFormat { json, csv, txt };

        DataType dataType;
        DataFormat dataFormat;
        public FormImportExport()
        {
            database = Program.database;
            InitializeComponent();
            StudentList = new LinkedList<StudentObject>();
            TeacherList = new LinkedList<TeacherObject>();
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
        }

        private void btn_exam_export_Click(object sender, EventArgs e)
        {
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
            File.WriteAllText(sfd.FileName, json);
        }

        private void btn_student_export_Click(object sender, EventArgs e)
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
            var json = JsonConvert.SerializeObject(Program.database.GetAllStudents(), Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }

        private void btn_teacher_export_Click(object sender, EventArgs e)
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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                        // ---->>>> addToDB
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
                        // check subjects else add to DB
                        // ---->>>> addToDB
                    }
                    Console.WriteLine(TeacherList.Count());
                    foreach (TeacherObject to in TeacherList)
                        Console.WriteLine(to.Fullname());
                }
            }
            else if (dataFormat == DataFormat.json)
            {
                if (dataType == DataType.Exam && ExamList.Count > 0)
                {
                    foreach (ExamObject eo in ExamList)
                    {
                        // check student & teacher -> add
                        // ---->>>> addToDB
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
                        // ---->>>> addToDB
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
                        // check subjects else add to DB
                        // ---->>>> addToDB
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
                        // ---->>>> addToDB
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
                        // ---->>>> addToDB
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
                        // TODO: check subjects else add to DB
                        // ---->>>> addToDB
                    }
                    Console.WriteLine(TeacherList.Count());
                    foreach (TeacherObject to in TeacherList)
                        Console.WriteLine(to.Fullname());
                }
            }

        }
        #region Import
        #region TextFile
        private void btn_import_exam_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = false;
            MessageBox.Show("keine Funktion", "Achtung");
        }
        private void btn_import_student_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = true;
            flp_import_email.Visible = true;
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
                            StudentList.AddLast(new StudentObject(0, tempfirstname, templastname, "0"));
                        }
                    }
                }
            }
        }
        private void btn_import_teacher_txt_Click(object sender, EventArgs e)
        {
            flp_import_grade.Visible = false;
            flp_import_email.Visible = true;
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
                            string[] t = line.Replace("Dr.", "").Replace(",", "").Split(' ');

                            if (t.Length == 5) TeacherList.AddLast(new TeacherObject(t[3], t[1], t[2], null, null, t[4], null, null));
                            else if (t.Length == 6) TeacherList.AddLast(new TeacherObject(t[3], t[1], t[2], null, null, t[4], t[5], null));
                            else if (t.Length == 7) TeacherList.AddLast(new TeacherObject(t[3], t[1], t[2], null, null, t[4], t[5], t[6]));
                        }
                    }
                }
            }


        }
        #endregion

        #region JsonFile
        private void btn_import_exam_json_Click(object sender, EventArgs e)
        {
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
        #endregion

        #region CsvFile
        private void btn_import_exam_csv_Click(object sender, EventArgs e)
        {
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

        #endregion

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
