using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public class ExamObject
    {
        private bool Border;
        private ButtonBorderStyle BorderStyle;
        private Color BorderColor = Colors.TL_EntityBorder;
        public Panel Panel { get; private set; }
        public int Id { get; private set; }
        public string Date { get; private set; } // DateTime?
        public string Time { get; private set; }
        public string Examroom { get; private set; }
        public string Preparationroom { get; private set; }
        public int StudentId { get; private set; }
        public int Student2Id { get; private set; }
        public int Student3Id { get; private set; }
        public StudentObject Student { get; private set; }
        public StudentObject Student2 { get; private set; }
        public StudentObject Student3 { get; private set; }
        public string Teacher1Id { get; private set; }
        public string Teacher2Id { get; private set; }
        public string Teacher3Id { get; private set; }
        public TeacherObject Teacher1 { get; private set; }
        public TeacherObject Teacher2 { get; private set; }
        public TeacherObject Teacher3 { get; private set; }
        public string Subject { get; private set; }
        public int Duration { get; private set; }
        [JsonConstructor]
        public ExamObject(int id, string date, string time, string examroom, string preparationroom, int studentid, int student2id, int student3id, StudentObject student, StudentObject student2, StudentObject student3, string teacher1id, string teacher2id, string teacher3id, TeacherObject t1, TeacherObject t2, TeacherObject t3, string subject, int duration)
        {
            this.Id = id;
            this.Date = date;
            this.Time = time;
            this.Examroom = examroom;
            this.Preparationroom = preparationroom;
            this.StudentId = studentid;
            this.Student2Id = student2id;
            this.Student3Id = student3id;
            this.Student = student;
            this.Student2 = student2;
            this.Student3 = student3;
            this.Teacher1Id = teacher1id;
            this.Teacher2Id = teacher2id;
            this.Teacher3Id = teacher3id;
            this.Teacher1 = t1;
            this.Teacher2 = t2;
            this.Teacher3 = t3;
            this.Subject = subject;
            this.Duration = duration;
        }
        public ExamObject(int id, string date, string time, string examroom, string preparationroom, int student, int student2, int student3, string teacher1, string teacher2, string teacher3, string subject, int duration)
        {
            this.Id = id;
            this.Date = date;
            DateTime dt;
            if (!date.Contains('-'))
            {
                dt = DateTime.ParseExact(date, "dd.MM.yyyy", null);
                this.Date = dt.ToString("yyyy-MM-dd");
            }
            this.Time = time;
            if (DateTime.ParseExact(time, "HH:mm", null).Hour > 18) Edit(time: "18:00");
            if (DateTime.ParseExact(time, "HH:mm", null).Hour < 7) Edit(time: "07:00");
            this.Examroom = examroom;
            this.Preparationroom = preparationroom;
            this.StudentId = student;
            this.Student2Id = student2;
            this.Student3Id = student3;
            if (student != 0) { this.StudentId = student; this.Student = Program.database.GetStudentByID(student); }
            if (student2 != 0) { this.Student2Id = student2; this.Student2 = Program.database.GetStudentByID(student2); }
            if (student3 != 0) { this.Student3Id = student3; this.Student3 = Program.database.GetStudentByID(student3); }
            this.Teacher1Id = teacher1;
            this.Teacher2Id = teacher2;
            this.Teacher3Id = teacher3;
            if (teacher1 != null) { this.Teacher1Id = teacher1; this.Teacher1 = Program.database.GetTeacherByID(teacher1); }
            if (teacher2 != null) { this.Teacher2Id = teacher2; this.Teacher2 = Program.database.GetTeacherByID(teacher2); }
            if (teacher3 != null) { this.Teacher3Id = teacher3; this.Teacher3 = Program.database.GetTeacherByID(teacher3); }
            this.Subject = subject;
            this.Duration = duration;
        }
        //TODO: return error string, if null -> added
        public bool Edit(string date = null, string time = null, string examroom = null, string preparationroom = null, int student = 0, int student2 = 0, int student3 = 0, string teacher1 = null, string teacher2 = null, string teacher3 = null, string subject = null, int duration = 0, LinkedList<ExamObject> excludeList = null)
        {
            if (date != null) this.Date = date;
            if (time != null) this.Time = time;
            if (examroom != null) this.Examroom = examroom;
            if (preparationroom != null) this.Preparationroom = preparationroom;
            if (student != 0) { this.StudentId = student; this.Student = Program.database.GetStudentByID(student); }
            if (student2 != 0) { this.Student2Id = student2; this.Student2 = Program.database.GetStudentByID(student2); }
            if (student3 != 0) { this.Student3Id = student3; this.Student3 = Program.database.GetStudentByID(student3); }
            if (teacher1 == "-") Teacher1Id = null;
            else if (teacher1 != null) { this.Teacher1Id = teacher1; this.Teacher1 = Program.database.GetTeacherByID(teacher1); }
            if (teacher2 == "-") Teacher2Id = null;
            else if (teacher2 != null) { this.Teacher2Id = teacher2; this.Teacher2 = Program.database.GetTeacherByID(teacher2); }
            if (teacher3 == "-") Teacher3Id = null;
            else if (teacher3 != null) { this.Teacher3Id = teacher3; this.Teacher3 = Program.database.GetTeacherByID(teacher3); }
            if (subject != null) this.Subject = subject;
            if (duration != 0) this.Duration = duration;

            string checkRoom = CheckRoom(excludeList);
            if (checkRoom != null) MessageBox.Show(checkRoom, "Fehler");
            string checkTeacher = CheckTeacher();
            if (checkTeacher != null) MessageBox.Show(checkTeacher, "Fehler");
            string checkStudent = CheckStudent();
            if (checkStudent != null) MessageBox.Show(checkStudent, "Fehler");
            if (checkRoom == null && checkTeacher == null && checkStudent == null)
                Program.database.EditExam(this.Id, this.Date, this.Time, this.Examroom, this.Preparationroom, this.StudentId, this.Student2Id, this.Student3Id, this.Teacher1Id, this.Teacher2Id, this.Teacher3Id, this.Subject, this.Duration);
            else { UpdateDBData(); return false; }
            return true;
        }
        public bool EditDatabase()
        {
            string checkRoom = CheckRoom();
            if (checkRoom != null) MessageBox.Show(checkRoom, "Fehler");
            string checkTeacher = CheckTeacher();
            if (checkTeacher != null) MessageBox.Show(checkTeacher, "Fehler");
            string checkStudent = CheckStudent();
            if (checkStudent != null) MessageBox.Show(checkStudent, "Fehler");
            if (checkRoom == null && checkTeacher == null && checkStudent == null)
            {
                Program.database.EditExam(this.Id, this.Date, this.Time, this.Examroom, this.Preparationroom, this.StudentId, this.Student2Id, this.Student3Id, this.Teacher1Id, this.Teacher2Id, this.Teacher3Id, this.Subject, this.Duration);
            }
            else { UpdateDBData(); return false; }
            return true;
        }
        public bool AddToDatabase(bool noError = false, bool checkTeacherDB = true)
        {
            string checkRoom = CheckRoom();
            if (checkRoom != null && !noError) MessageBox.Show(checkRoom, "Fehler");
            string checkTeacher = null; if (checkTeacherDB) checkTeacher = CheckTeacher();
            if (checkTeacher != null && !noError) MessageBox.Show(checkTeacher, "Fehler");
            string checkStudent = CheckStudent();
            if (checkStudent != null && !noError) MessageBox.Show(checkStudent, "Fehler");
            if (checkRoom == null && checkTeacher == null && checkStudent == null)
            {
                Program.database.AddExam(this.Date, this.Time, this.Examroom, this.Preparationroom, this.StudentId, this.Student2Id, this.Student3Id, this.Teacher1Id, this.Teacher2Id, this.Teacher3Id, this.Subject, this.Duration);
                return true;
            }
            else UpdateDBData();
            return false;
        }
        public void UpdateDBData(ExamObject eo = null)
        {
            if (Id == 0 && eo == null)
            {
                LinkedList<ExamObject> list = Program.database.GetAllExamsAtDateTimeAndRoom(Date, Time, Examroom);
                if (list.Count > 0) eo = list.First();
            }
            if (eo == null) eo = Program.database.GetExamById(Id);
            if (eo == null) return;
            this.Date = eo.Date;
            this.Time = eo.Time;
            this.Examroom = eo.Examroom;
            this.Preparationroom = eo.Preparationroom;
            this.StudentId = eo.StudentId;
            this.Student2Id = eo.Student2Id;
            this.Student3Id = eo.Student3Id;
            this.Student = eo.Student;
            this.Student2 = eo.Student2;
            this.Student3 = eo.Student3;
            this.Teacher1Id = eo.Teacher1Id;
            this.Teacher2Id = eo.Teacher2Id;
            this.Teacher3Id = eo.Teacher3Id;
            this.Subject = eo.Subject;
            this.Duration = eo.Duration;
        }
        public void Delete()
        {
            Program.database.DeleteExam(this.Id);
        }
        private void CreatePanel()
        {
            this.Panel = new Panel();
            DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime examTime = DateTime.ParseExact(Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
            //float unit_per_minute = 200F / 60F;
            float unit_per_minute = Properties.Settings.Default.PixelPerHour / 60F;
            float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;
            this.Panel.Location = new Point(Convert.ToInt32(startpoint), 10);
            this.Panel.Size = new Size(Convert.ToInt32(unit_per_minute * Duration), 60);
            this.Panel.Name = Id.ToString();
            this.Panel.BackColor = Colors.TL_Entity;
            this.Panel.Paint += panel_time_line_entity_Paint;
        }

        public void UpdatePanel(bool updateDB = false)
        {
            if (updateDB)
            {
                ExamObject eo = Program.database.GetExamById(Id);
                this.Date = eo.Date;
                this.Time = eo.Time;
                this.Examroom = eo.Examroom;
                this.Preparationroom = eo.Preparationroom;
                this.StudentId = eo.StudentId;
                this.Student2Id = eo.Student2Id;
                this.Student3Id = eo.Student3Id;
                this.Student = eo.Student;
                this.Student2 = eo.Student2;
                this.Student3 = eo.Student3;
                this.Teacher1Id = eo.Teacher1Id;
                this.Teacher2Id = eo.Teacher2Id;
                this.Teacher3Id = eo.Teacher3Id;
                this.Teacher1 = eo.Teacher1;
                this.Teacher2 = eo.Teacher2;
                this.Teacher3 = eo.Teacher3;
                this.Subject = eo.Subject;
                this.Duration = eo.Duration;
            }
            DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime examTime = DateTime.ParseExact(Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
            float unit_per_minute = Properties.Settings.Default.PixelPerHour / 60F;
            float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;
            this.Panel.Location = new Point(Convert.ToInt32(startpoint), 10);
            this.Panel.Size = new Size(Convert.ToInt32(unit_per_minute * Duration), 60);
            this.Panel.Refresh();
        }
        private string CheckRoom(LinkedList<ExamObject> excludeList = null)
        {
            foreach (ExamObject s in Program.database.GetAllExamsAtDateAndRoom(Date, Examroom)) // TODO: return if examroom or preproom
            {
                if (excludeList != null)
                {
                    if (excludeList.Count(x => x.Id == s.Id) == 0)
                        if (Id != s.Id && CheckTime(s.Time, s.Duration)) return s.Examroom + " Raum besetzt ab " + s.Time;
                }
                else if (Id != s.Id && CheckTime(s.Time, s.Duration)) return s.Examroom + " Raum besetzt ab " + s.Time;
            }
            if (Preparationroom != null && Preparationroom.Length > 0)
                foreach (ExamObject s in Program.database.GetAllExamsAtDateAndRoom(Date, Preparationroom))
                    if (Id != s.Id && CheckTime(s.Time, s.Duration)) return s.Examroom + " Raum besetzt ab " + s.Time;
            return null;
        }
        private string CheckTeacher()
        {
            if (Teacher1Id != null && Teacher1Id.Length > 0 && Program.database.GetTeacherByID(Teacher1Id) == null) return "Lehrer1 " + Teacher1Id + " nicht gefunden";
            if (Teacher2Id != null && Teacher2Id.Length > 0 && Program.database.GetTeacherByID(Teacher2Id) == null) return "Lehrer2 " + Teacher2Id + " nicht gefunden";
            if (Teacher3Id != null && Teacher3Id.Length > 0 && Program.database.GetTeacherByID(Teacher3Id) == null) return "Lehrer3 " + Teacher3Id + " nicht gefunden";
            string ts;
            TeacherObject to = null;
            string tName = null;
            if (Teacher1Id != null && Teacher1Id.Length > 0 && (ts = TeacherInOtherRooms(Teacher1Id)) != null)
            {
                to = Program.database.GetTeacherByID(Teacher1Id);
                if (to != null) tName = to.Fullname();
                return tName + " (" + Teacher1Id + ") befindet sich in einem anderem Raum: " + ts;
            }
            if (Teacher2Id != null && Teacher2Id.Length > 0 && (ts = TeacherInOtherRooms(Teacher2Id)) != null)
            {
                to = Program.database.GetTeacherByID(Teacher2Id);
                if (to != null) tName = to.Fullname();
                return tName + " (" + Teacher2Id + ") befindet sich in einem anderem Raum: " + ts;
            }
            if (Teacher3Id != null && Teacher3Id.Length > 0 && (ts = TeacherInOtherRooms(Teacher3Id)) != null)
            {
                to = Program.database.GetTeacherByID(Teacher3Id);
                if (to != null) tName = to.Fullname();
                return tName + " (" + Teacher3Id + ") befindet sich in einem anderem Raum: " + ts;
            }
            if (Teacher1Id == null && Teacher2Id == null && Teacher3Id == null) return "Kein Lehrer!";
            string TeacherInOtherRooms(string teacher)
            {
                foreach (ExamObject s in Program.database.GetAllExamsFromTeacherAtDate(Date, teacher))
                    if (Id != s.Id && Examroom != s.Examroom)
                        if (CheckTime(s.Time, s.Duration)) return s.Examroom;
                return null;
            }
            return null;
        }
        private string CheckStudent()
        {
            if (Student == null && (Student = Program.database.GetStudentByID(StudentId)) == null) return "Schüler fehlt";
            if (Student != null && StudentInOtherRooms(Student)) return Student.Fullname() + " befindet sich in einem anderem Raum";
            if (Student2 != null && StudentInOtherRooms(Student2)) return Student2.Fullname() + " befindet sich in einem anderem Raum";
            if (Student3 != null && StudentInOtherRooms(Student3)) return Student3.Fullname() + " befindet sich in einem anderem Raum";

            bool StudentInOtherRooms(StudentObject so)
            {
                foreach (ExamObject s in Program.database.GetAllExamsFromStudentAtDate(Date, so.Id))
                    if (Id != s.Id && Examroom != s.Examroom)
                        if (CheckTime(s.Time, s.Duration)) return true;
                return false;
            }
            return null;
        }
        /// <summary>returns true if timespan is in the Examtime</summary>
        private bool CheckTime(string tm, int d)
        {
            DateTime start = DateTime.ParseExact(tm, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime end = DateTime.ParseExact(tm, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(d);
            DateTime timestart = DateTime.ParseExact(Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime timeend = DateTime.ParseExact(Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Duration);
            if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))
                return true;
            return false;
        }

        public Panel GetTimelineEntity(bool preview = false)
        {
            if (this.Panel == null) CreatePanel();
            if (preview) Panel.BackColor = Color.FromArgb(80, Colors.TL_Entity);
            return this.Panel;
        }
        private void panel_time_line_entity_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl_entity = sender as Panel;
            Font drawFont = new Font("Microsoft Sans Serif", 8);  // Arial
            ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid);
            StudentObject student = Student;
            if (student == null)
                student = new StudentObject(0, "Schüler nicht gefunden!", " ", "-");
            if (Border)
            {
                ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                BorderColor, 2, BorderStyle,
                BorderColor, 2, BorderStyle,
                BorderColor, 2, BorderStyle,
                BorderColor, 2, BorderStyle);
            }
            // ---- Text ----
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rectL1 = new Rectangle(1, 2 + (panel_tl_entity.Height - 4) / 4 * 0, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL2 = new Rectangle(1, 2 + (panel_tl_entity.Height - 4) / 4 * 1, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL3 = new Rectangle(1, 2 + (panel_tl_entity.Height - 4) / 4 * 2, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL4 = new Rectangle(1, 2 + (panel_tl_entity.Height - 4) / 4 * 3, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            e.Graphics.DrawString(student.Fullname() + " [" + student.Grade + "]", drawFont, Brushes.Black, rectL1, stringFormat);
            string sCount = null;
            if (Student2 != null) sCount = "[2xS]";
            if (Student3 != null) sCount = "[3xS]";

            string t1 = Teacher1Id, t2 = Teacher2Id, t3 = Teacher3Id;
            if (Teacher1Id == null || Teacher1Id.Length < 1) t1 = " - ";
            if (Teacher2Id == null || Teacher2Id.Length < 1) t2 = " - ";
            if (Teacher3Id == null || Teacher3Id.Length < 1) t3 = " - ";
            if (Duration < 40)
            {
                e.Graphics.DrawString(Time + "; " + Duration + "min  " + sCount, drawFont, Brushes.Black, rectL2, stringFormat);
                e.Graphics.DrawString(t1 + "," + t2 + "," + t3, drawFont, Brushes.Black, rectL3, stringFormat);
            }
            else
            {
                e.Graphics.DrawString(Time + "   " + Duration + "min  " + sCount, drawFont, Brushes.Black, rectL2, stringFormat);
                e.Graphics.DrawString(t1 + "  " + t2 + "  " + t3, drawFont, Brushes.Black, rectL3, stringFormat);
            }
            e.Graphics.DrawString(Subject + " " + Examroom + " [" + Preparationroom + "]", drawFont, Brushes.Black, rectL4, stringFormat);
            // ---- ToolTip ----
            string line1 = student.Fullname() + "  [" + student.Grade + "]\n";
            string line11 = null; if (Student2 != null) line11 = Student2.Fullname() + "  [" + Student2.Grade + "]\n";
            string line12 = null; if (Student3 != null) line12 = Student3.Fullname() + "  [" + Student3.Grade + "]\n";
            string line2 = Time + "     " + Duration + "min\n";
            string line3 = t1 + "  " + t2 + "  " + t3 + "\n";
            string line4 = Subject + "  " + Examroom + "  [" + Preparationroom + "]";
            ToolTip sfToolTip1 = new ToolTip();
            sfToolTip1.SetToolTip(panel_tl_entity, line1 + line11 + line12 + line2 + line3 + line4);
        }
        public void SetBorder(Color borderColor, bool solidBorder)
        {
            this.Border = true;
            if (solidBorder) BorderStyle = ButtonBorderStyle.Solid;
            else BorderStyle = ButtonBorderStyle.Dashed;
            this.BorderColor = borderColor;
        }
        public void RemoveBorder()
        {
            Border = false;
            if (Panel != null)
                Panel.Refresh();
        }
    }
}
