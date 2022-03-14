using ExcelDataReader;
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
    public partial class FormLoadTable : Form
    {
        private string SelectedFile = null;
        private LinkedList<ExamObject> ExamList;
        public FormLoadTable()
        {
            ExamList = new LinkedList<ExamObject>();
            InitializeComponent();
            dtp_date.Value = DateTime.Now;
            cb_grade.Items.Clear();
            LinkedList<StudentObject> allStudents = Program.database.GetAllStudents();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (StudentObject s in allStudents)
                if (!gradeList.Contains(s.Grade))
                    gradeList.AddLast(s.Grade);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList();
            gradeList = new LinkedList<string>(templist);
            string[] listGrade = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                listGrade[i] = gradeList.ElementAt(i);
            cb_grade.Items.AddRange(listGrade);
        }

        private LinkedList<string[]> ReadFile()
        {
            LinkedList<string[]> DataArrayList = new LinkedList<string[]>();

            FileStream fStream = File.Open(SelectedFile, FileMode.Open, FileAccess.Read);
            IExcelDataReader edr = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            string grade = cb_grade.Text;
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
            string date = dtp_date.Value.ToString("yyyy-MM-dd");
            int duration = int.Parse(tb_duration.Text);
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
                if (time.AddMinutes(duration).Hour > DateTime.ParseExact("17:00", "HH:mm", null, System.Globalization.DateTimeStyles.None).Hour)
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
                //Console.WriteLine(examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + 30); ;
                element++;
            }

            string students = "";
            Console.WriteLine("Missing Students: ");
            foreach (StudentObject so in missingStudentList)
            { Console.WriteLine(so.Fullname()); students += so.Fullname() + " "; }

            DialogResult resultAddStudents = MessageBox.Show(missingStudentList.Count + " Schüler hinzufügen?\n" + students, "Achtung", MessageBoxButtons.YesNo);
            if (resultAddStudents == DialogResult.Yes) foreach (StudentObject so in missingStudentList)
                    if (Program.database.GetStudentByName(so.Firstname, so.Lastname) == null) so.AddToDatabase();

            string subjects = "";
            Console.WriteLine("Missing Subjects: ");
            foreach (string su in missingSubjectList) subjects += su + " ";
            DialogResult resultSubjects = MessageBox.Show(missingSubjectList.Count + " Fächer hinzufügen?\n" + subjects, "Achtung", MessageBoxButtons.YesNo);
            if (resultSubjects == DialogResult.Yes) foreach (string su in missingSubjectList)
                    if (Program.database.GetSubject(su) == null) Program.database.AddSubject(su);

            DialogResult result = MessageBox.Show(list.Count() + " Prüfungen hinzufügen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;
            Console.WriteLine("Add missing data to db");
            AddMissingDataToDB(list);
        }

        private void AddMissingDataToDB(LinkedList<string[]> list)
        {
            string date = dtp_date.Value.ToString("yyyy-MM-dd");
            int duration = int.Parse(tb_duration.Text);
            int examCount = 0;
            int room = 0;
            LinkedList<ExamObject> examList = new LinkedList<ExamObject>();
            while (list.Count > examCount)
            {
                for (int t = 1; t < 20 + 1; t++)
                {
                    examCount++;
                    if (list.Count == examCount) break;
                    string[] d = list.ElementAt(room * 20 + t);
                    string s = d[2].Replace(", ", ",").Replace(" ", "_"); // TODO: teacher -> t1/t2/t3?
                    Program.database.AddRoom("R" + room);
                    //if (Program.database.GetSubject(d[0]) == null && !missingSubjectList.Contains(d[0])) missingSubjectList.AddLast(d[0]);
                    DateTime time = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration * t);
                    if (Program.database.GetAllExamsAtDateTimeAndRoom(date, time.ToString("HH:mm"), "R" + room).Count == 0)
                    {
                        if (Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]) == null || Program.database.GetTeacherByID(d[1]) == null)
                        {
                            if (Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]) == null && Program.database.GetTeacherByID(d[1]) == null)
                            {
                                examList.AddLast(new ExamObject(0, date, time.ToString("HH:mm"), "R" + room, "", 0, 0, 0, d[1] + "*", null, null, d[0], duration));
                                Console.WriteLine("NOTFOUND-TS: " + examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + duration); ;
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
                            //Console.WriteLine("NOTFOUND: " + examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + d[1] + " -  - " + d[0] + " " + 30); ;
                        }
                        else
                        {
                            Console.WriteLine(examCount + " " + date + " " + time.ToString("HH:mm") + " " + "R" + room + " " + "" + " " + Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]).Id + " " + s.Split(',')[1] + s.Split(',')[0] + " 0  0 " + Program.database.GetTeacherByID(d[1]).Shortname + " -  - " + d[0] + " " + duration);
                            examList.AddLast(new ExamObject(0, date, time.ToString("HH:mm"), "R" + room, null, Program.database.GetStudentByName(s.Split(',')[1], s.Split(',')[0]).Id, 0, 0, Program.database.GetTeacherByID(d[1]).Shortname, null, null, d[0], duration));
                        }
                    }
                }
                room++;
            }
            foreach (ExamObject eo in examList) eo.AddToDatabase(checkTeacherDB: false);
        }

        private void btn_select_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "xlsx Datei auswählen";
            ofd.Multiselect = false;
            ofd.Filter = "Text files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (ofd.ShowDialog() != DialogResult.OK) return;
            btn_select_file.Text = ofd.FileName;
            SelectedFile = ofd.FileName;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void tb_duration_TextChanged(object sender, EventArgs e)
        {

        }

        private void t(object sender, EventArgs e)
        {

        }
    }
}
