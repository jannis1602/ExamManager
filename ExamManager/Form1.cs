using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class Form1 : Form
    {
        public string search = null;
        public enum Search { all, student, teacher, subject, room, grade }
        public Search searchMode = Search.all;
        public string filter = null;
        public enum Filter { all, grade, teacher, student }
        public Filter filterMode = Filter.all;
        private ExamObject SwapExam = null;

        private readonly bool editExamPreview = true;
        private Panel editPanel = null; // TODO: EditPanel in ExamObject
        private Point oldPoint;
        private Panel oldTimeLine;

        private readonly Database database;
        private ExamObject EditExam = null;
        private readonly LinkedList<Panel> time_line_list;
        private LinkedList<ExamObject> tl_exam_entity_list;

        private readonly LinkedList<Panel> time_line_room_list;
        private readonly string[] edit_mode = { "neue Prüfung erstellen", "Prüfung bearbeiten" };
        private readonly string[] add_mode = { "Prüfung hinzufügen", "Prüfung übernehmen" };
        private Point panelScrollPos1 = new Point();
        private Point panelScrollPos2 = new Point();
        private Panel panel_empty;
        public Form1()
        {
            database = Program.database;
            time_line_list = new LinkedList<Panel>();
            tl_exam_entity_list = new LinkedList<ExamObject>();
            time_line_room_list = new LinkedList<Panel>();
            InitializeComponent();

            if (Properties.Settings.Default.timeline_date.Length > 2)
                dtp_timeline_date.Value = DateTime.ParseExact(Properties.Settings.Default.timeline_date, "dd.MM.yyyy", null);
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            if (database.GetAllExamsAtDate(date).Count < 1) dtp_timeline_date.Value = DateTime.Now;
            if (Properties.Settings.Default.timeline_date.Length > 2)
                dtp_date.Value = DateTime.ParseExact(Properties.Settings.Default.timeline_date, "dd.MM.yyyy", null);
            date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            if (database.GetAllExamsAtDate(date).Count < 1) dtp_date.Value = DateTime.Now;
            UpdateTimeline();
            UpdateAutocomplete();
        }
        /// <summary>reloads the autocomplete and dropdownlist</summary>
        private void UpdateAutocomplete()
        {
            // subjects
            cb_subject.Items.Clear();
            LinkedList<string[]> subjectList = Program.database.GetAllSubjects();
            string[] subjects = new string[subjectList.Count];
            for (int i = 0; i < subjectList.Count; i++)
                subjects[i] = subjectList.ElementAt(i)[0];
            cb_subject.Items.AddRange(subjects);
            // students
            var autocomplete_student = new AutoCompleteStringCollection();
            LinkedList<StudentObject> allStudentsList = database.GetAllStudents();
            if (cb_student_onetime.Checked)
            {
                LinkedList<StudentObject> tempAllStudentsList = new LinkedList<StudentObject>();
                string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
                foreach (StudentObject s in database.GetAllStudents())
                    if (database.GetAllExamsFromStudentAtDate(date, s.Id).Count == 0)
                        tempAllStudentsList.AddLast(s);
                allStudentsList = tempAllStudentsList;
            }
            string[] students = new string[allStudentsList.Count];
            for (int i = 0; i < allStudentsList.Count; i++)
                students[i] = (allStudentsList.ElementAt(i).Firstname + " " + allStudentsList.ElementAt(i).Lastname);
            autocomplete_student.AddRange(students);
            this.cb_student.AutoCompleteCustomSource = autocomplete_student;
            this.cb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb_student2.AutoCompleteCustomSource = autocomplete_student;
            this.cb_student2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_student2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb_student3.AutoCompleteCustomSource = autocomplete_student;
            this.cb_student3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_student3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //
            cb_student.Items.Clear();
            cb_student2.Items.Clear();
            cb_student3.Items.Clear();
            LinkedList<StudentObject> studentList = new LinkedList<StudentObject>();
            List<StudentObject> tempStudentList = new List<StudentObject>(allStudentsList);
            tempStudentList = tempStudentList.OrderBy(x => x.Lastname).ToList();
            studentList = new LinkedList<StudentObject>(tempStudentList);
            string[] listStudent = new string[studentList.Count];
            for (int i = 0; i < studentList.Count; i++)
                listStudent[i] = studentList.ElementAt(i).Firstname + " " + studentList.ElementAt(i).Lastname;
            cb_student.Items.AddRange(listStudent);
            cb_student2.Items.AddRange(listStudent);
            cb_student3.Items.AddRange(listStudent);
            cb_student2.Items.Add("");
            cb_student3.Items.Add("");

            // grade
            cb_grade.Items.Clear();
            LinkedList<StudentObject> allStudents = database.GetAllStudents();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (StudentObject s in allStudents)
                if (!gradeList.Contains(s.Grade))
                    gradeList.AddLast(s.Grade);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList(); // .ThenBy( x => x.Bar)
            gradeList = new LinkedList<string>(templist);
            string[] listGrade = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                listGrade[i] = gradeList.ElementAt(i);
            cb_grade.Items.AddRange(listGrade);
            // teacher
            var autocomplete_teacher = new AutoCompleteStringCollection();
            LinkedList<TeacherObject> teacherList1 = database.GetAllTeachers();
            string[] teacher = new string[teacherList1.Count];
            for (int i = 0; i < teacherList1.Count; i++)
                teacher[i] = teacherList1.ElementAt(i).Firstname + " " + teacherList1.ElementAt(i).Lastname;
            autocomplete_teacher.AddRange(teacher);
            this.cb_teacher1.AutoCompleteCustomSource = autocomplete_teacher;
            this.cb_teacher1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_teacher1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb_teacher2.AutoCompleteCustomSource = autocomplete_teacher;
            this.cb_teacher2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_teacher2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb_teacher3.AutoCompleteCustomSource = autocomplete_teacher;
            this.cb_teacher3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_teacher3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // 
            cb_teacher1.Items.Clear();
            cb_teacher2.Items.Clear();
            cb_teacher3.Items.Clear();
            LinkedList<TeacherObject> allTeachers = database.GetAllTeachers();
            LinkedList<TeacherObject> teacherList = new LinkedList<TeacherObject>();
            List<TeacherObject> tempTeacherList = new List<TeacherObject>(allTeachers);
            tempTeacherList = tempTeacherList.OrderBy(x => x.Lastname).ToList();
            teacherList = new LinkedList<TeacherObject>(tempTeacherList);
            string[] listTeacher = new string[teacherList.Count];
            for (int i = 0; i < teacherList.Count; i++)
                listTeacher[i] = teacherList.ElementAt(i).Firstname + " " + teacherList.ElementAt(i).Lastname;
            cb_teacher1.Items.AddRange(listTeacher);
            cb_teacher2.Items.AddRange(listTeacher);
            cb_teacher3.Items.AddRange(listTeacher);
            cb_teacher2.Items.Add("");
            cb_teacher3.Items.Add("");
            // exam_room & prep_room
            cb_exam_room.Items.Clear();
            cb_preparation_room.Items.Clear();
            LinkedList<string> roomList = new LinkedList<string>();
            LinkedList<string[]> rooms = database.GetAllRooms();
            foreach (string[] s in rooms)
                if (!roomList.Contains(s[0])) roomList.AddLast(s[0]);
            List<string> temproomlist = new List<string>(roomList);
            temproomlist = temproomlist.OrderBy(x => x).ToList();
            roomList = new LinkedList<string>(temproomlist);
            string[] item_list = new string[roomList.Count];
            for (int i = 0; i < roomList.Count; i++)
                item_list[i] = roomList.ElementAt(i);
            cb_exam_room.Items.AddRange(item_list);
            cb_preparation_room.Items.AddRange(item_list);
        }
        /// <summary>Checks the entered values ​​and adds an exam to the database</summary>
        private void AddExam()
        {
            if (cb_exam_room.Text.Length < 1 || cb_preparation_room.Text.Length < 1)
            { MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return; }
            string date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            string time = this.dtp_time.Value.ToString("HH:mm");
            string exam_room = cb_exam_room.Text;
            string preparation_room = cb_preparation_room.Text;
            string studentName = cb_student.Text;
            string student2Name = cb_student2.Text;
            string student3Name = cb_student3.Text;
            string grade = null;
            if (cb_grade.SelectedItem != null) grade = cb_grade.SelectedItem.ToString();
            if (cb_teacher1.Text.Length < 1) // || cb_teacher2.Text.Length < 1 || cb_teacher3.Text.Length < 1)
            { MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return; }

            string teacher1 = null;
            string teacher2 = null;
            string teacher3 = null;
            try
            {
                if (cb_teacher1.Text.Length > 1) teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]).Shortname;
                if (cb_teacher2.Text.Length > 1) teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]).Shortname;
                if (cb_teacher3.Text.Length > 1) teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]).Shortname;
            }
            catch (Exception) { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }
            if (teacher1 == null && teacher2 == null && teacher3 == null) { MessageBox.Show("Kein Lehrer!", "Warnung"); return; }
            string subject = cb_subject.Text;
            int duration = Int32.Parse(tb_duration.Text);
            // check if not empty
            if (exam_room.Length == 0 || studentName.Length == 0 || teacher1.Length == 0 || subject.Length == 0 || duration == 0) // || teacher1.Length == 0 || teacher2.Length == 0 
            { MessageBox.Show("Felder fehlen!", "Warnung"); return; }
            // check room
            if (EditExam != null)
            {
                if (time != EditExam.Time || date != EditExam.Date)
                    foreach (ExamObject s in database.GetAllExamsAtDateAndRoom(date, exam_room))
                        if (EditExam.Id != s.Id)
                        {
                            DateTime start = DateTime.ParseExact(s.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                            DateTime end = DateTime.ParseExact(s.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(s.Duration);
                            DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                            DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                            if ((start < timestart && timestart < end) || (timestart < start && start < timeend))
                            { MessageBox.Show("Raum besetzt!", "Warnung"); return; }
                        }
            }
            else
                foreach (ExamObject s in database.GetAllExamsAtDateAndRoom(date, exam_room))
                {
                    DateTime start = DateTime.ParseExact(s.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(s.Duration);
                    DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                    if ((start < timestart && timestart < end) || (timestart < start && start < timeend))
                    { MessageBox.Show("Raum besetzt!", "Warnung"); return; }
                }
            // check names in database
            string tempfirstname = null;
            string templastname = null;
            try
            {
                for (int i = 0; i < studentName.Split(' ').Length - 1; i++)
                    tempfirstname += studentName.Split(' ')[i] += " ";
                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                templastname += studentName.Split(' ')[studentName.Split(' ').Length - 1];
            }
            catch (NullReferenceException)
            { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }
            StudentObject student = null;
            StudentObject student2 = null;
            StudentObject student3 = null;
            try
            {
                student = database.GetStudentByName(studentName.Split(' ')[0], studentName.Split(' ')[1], grade);
                if (student2Name.Length > 1) student2 = database.GetStudentByName(student2Name.Split(' ')[0], student2Name.Split(' ')[1], grade);
                if (student3Name.Length > 1) student3 = database.GetStudentByName(student3Name.Split(' ')[0], student3Name.Split(' ')[1], grade);
            }
            catch (Exception) { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }

            if (student == null)
            { MessageBox.Show("Schüler nicht gefunden!", "Warnung"); return; }
            if (student2Name.Length > 1 && student2 == null)
            { MessageBox.Show("Schüler 2 nicht gefunden!", "Warnung"); return; }
            if (student3Name.Length > 1 && student3 == null)
            { MessageBox.Show("Schüler 3 nicht gefunden!", "Warnung"); return; }
            if (database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]) == null)
            { MessageBox.Show("Lehrer 1 nicht gefunden!", "Warnung"); return; }
            if (database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]) == null)
            { MessageBox.Show("Lehrer 2 nicht gefunden!", "Warnung"); return; }
            if (database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]) == null)
            { MessageBox.Show("Lehrer 3 nicht gefunden!", "Warnung"); return; }

            if (student2 == null && student3 != null) { MessageBox.Show("erst Schüler 2 vor Schüler 3 belegen!", "Warnung"); return; }
            if (preparation_room.Length > 1)
                foreach (ExamObject s in database.GetAllExamsAtDateAndRoom(date, preparation_room))
                {
                    if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                    {
                        if (!checkTimeIsFree(s.Time, s.Duration))
                        { MessageBox.Show("Vorbereitungsraum belegt: " + s.Examroom, "Warnung"); return; }
                    }
                }

            bool checkTimeIsFree(string t, int d)
            {
                DateTime start = DateTime.ParseExact(t, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime end = DateTime.ParseExact(t, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(d);
                DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))
                    return false;
                return true;
            }
            // check teacher in other rooms
            foreach (ExamObject s in database.GetAllExamsFromTeacherAtDate(date, teacher1))
            {
                if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                {
                    if (!checkTimeIsFree(s.Time, s.Duration))
                    { MessageBox.Show(database.GetTeacherByID(teacher1).Firstname + " " + database.GetTeacherByID(teacher1).Lastname + " befindet sich in einem anderem Raum: " + s.Examroom, "Warnung"); return; }
                }
            }
            if (teacher2.Length > 1)
                foreach (ExamObject s in database.GetAllExamsFromTeacherAtDate(date, teacher2))
                {
                    if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                    {
                        if (!checkTimeIsFree(s.Time, s.Duration))
                        { MessageBox.Show(database.GetTeacherByID(teacher2).Firstname + " " + database.GetTeacherByID(teacher2).Lastname + " befindet sich in einem anderem Raum: " + s.Examroom, "Warnung"); return; }
                    }
                }
            if (teacher3.Length > 1)
                foreach (ExamObject s in database.GetAllExamsFromTeacherAtDate(date, teacher3))
                {
                    if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                    {
                        if (!checkTimeIsFree(s.Time, s.Duration))
                        { MessageBox.Show(database.GetTeacherByID(teacher3).Firstname + " " + database.GetTeacherByID(teacher3).Lastname + " befindet sich in einem anderem Raum: " + s.Examroom, "Warnung"); return; }
                    }
                }
            // check student in other rooms
            foreach (ExamObject s in database.GetAllExamsFromStudentAtDate(date, database.GetStudentByName(tempfirstname, templastname, grade).Id))
            {
                if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                {
                    if (!checkTimeIsFree(s.Time, s.Duration))
                    { MessageBox.Show(studentName + " befindet sich in einem anderem Raum: " + s.Examroom, "Warnung"); return; }
                }
            }
            if (student2Name.Length > 1)
                foreach (ExamObject s in database.GetAllExamsFromStudentAtDate(date, database.GetStudentByName(student2Name.Split(' ')[0], student2Name.Split(' ')[1], grade).Id))
                {
                    if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                    {
                        if (!checkTimeIsFree(s.Time, s.Duration))
                        { MessageBox.Show(student2Name + " befindet sich in einem anderem Raum: " + s.Examroom, "Warnung"); return; }
                    }
                }
            if (student3Name.Length > 1) foreach (ExamObject s in database.GetAllExamsFromStudentAtDate(date, database.GetStudentByName(student3Name.Split(' ')[0], student3Name.Split(' ')[1], grade).Id))
                {
                    if (EditExam == null || (exam_room != s.Examroom && s.Examroom != EditExam.Examroom))
                    {
                        if (!checkTimeIsFree(s.Time, s.Duration))
                        { MessageBox.Show(student3Name + " befindet sich in einem anderem Raum: " + s.Examroom, "Warnung"); return; }
                    }
                }
            int[] sIDs = new int[3];
            sIDs[0] = student.Id;
            sIDs[1] = 0;
            sIDs[2] = 0;
            if (student2 != null) sIDs[1] = student2.Id;
            if (student3 != null) sIDs[3] = student3.Id;

            // Add / Edit / Clear
            if (EditExam != null)
                database.EditExam(EditExam.Id, date, time, exam_room, preparation_room, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, subject, duration);
            if (EditExam == null)
                database.AddExam(date, time, exam_room, preparation_room, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, subject, duration);
            EditExam = null;
            this.lbl_mode.Text = edit_mode[0];
            this.btn_add_exam.Text = add_mode[0];
            this.dtp_timeline_date.Value = this.dtp_date.Value;
            Properties.Settings.Default.timeline_date = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
            UpdateTimeline();
            if (this.cb_add_next_time.Checked) { this.dtp_time.Value = this.dtp_time.Value.AddMinutes(Int32.Parse(tb_duration.Text)); }
            if (this.cb_keep_data.Checked)
            {
                if (!Properties.Settings.Default.keep_subject) cb_subject.Text = null;
                if (!Properties.Settings.Default.keep_examroom) cb_exam_room.Text = null;
                if (!Properties.Settings.Default.keep_preparationroom) cb_preparation_room.Text = null;
                if (!Properties.Settings.Default.keep_teacher) { cb_teacher1.Text = null; cb_teacher2.Text = null; cb_teacher3.Text = null; }
                if (!Properties.Settings.Default.keep_grade) cb_grade.Text = null;
                if (!Properties.Settings.Default.keep_student) { cb_student.Text = null; cb_student2.Text = null; cb_student3.Text = null; }
            }
            else
            {
                this.cb_exam_room.Text = null;
                this.cb_preparation_room.Text = null;
                this.cb_student.Text = null;
                this.cb_student2.Text = null;
                this.cb_student3.Text = null;
                this.cb_grade.Text = null;
                this.cb_subject.Text = null;
                this.cb_teacher1.Text = null;
                this.cb_teacher2.Text = null;
                this.cb_teacher3.Text = null;
            }
        }
        /// <summary>Adds a timeline for a <paramref name="room"/></summary>
        public void AddTimeline(string room)
        {
            // -- roomlist --
            Panel panel_room = new Panel
            {
                Location = new Point(2, 6 + panel_top_time.Height + 5 + 85 * time_line_list.Count),
                Size = new Size(panel_side_room.Width - 17 - 4, 80 - 12),
                Padding = new Padding(3),
                //BackColor = Color.LightSlateGray,
                BackColor = Colors.TL_RoomBorder,
                Name = room
            };
            Label lbl_room = new Label
            {
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Dock = DockStyle.Fill,
                Name = "lbl_room",
                BackColor = Colors.TL_RoomEntityBg,
                Text = room,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel_room.Controls.Add(lbl_room);
            this.panel_side_room.HorizontalScroll.Value = 0;
            panel_side_room.Controls.Add(panel_room);
            time_line_room_list.AddLast(panel_room);
            panel_side_room.Refresh();
            // -- timeline --
            this.panel_time_line.HorizontalScroll.Value = 0;
            Panel panel_tl = new Panel
            {
                Location = new Point(0, panel_top_time.Height + 5 + 85 * time_line_list.Count),
                Name = room,
                Size = new Size(2400, 80),
                BackColor = Colors.TL_TimeLineBg,
            };
            panel_tl.Paint += panel_time_line_Paint;
            panel_tl.MouseDown += panel_time_line_MouseDown;
            void panel_time_line_MouseDown(object sender, MouseEventArgs e)
            {
                if (editPanel != null)
                {
                    Panel p = sender as Panel;
                    if (oldTimeLine == p) return;
                    oldTimeLine = p;
                    cb_exam_room.SelectedItem = p.Name;
                    UpdateEditPanel();
                }
            }
            this.panel_time_line.Controls.Add(panel_tl);
            time_line_list.AddLast(panel_tl);
        }
        /// <summary>Updates the timeline for all rooms of the day</summary>
        public void UpdateTimeline()
        {
            if (filter == null || filter.Length == 0) filterMode = Filter.all;
            if (panel_empty != null) panel_side_room.Controls.Remove(panel_empty);
            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_room_list) p.Dispose();
            time_line_list.Clear();
            tl_exam_entity_list.Clear();
            time_line_room_list.Clear();

            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            LinkedList<ExamObject> examList = null;
            if (filterMode == Filter.teacher) examList = database.GetAllExamsFromTeacherAtDate(date, filter);
            else if (filterMode == Filter.grade) examList = database.GetAllExamsFromGradeAtDate(date, filter);
            else examList = database.GetAllExamsAtDate(date);
            tl_exam_entity_list = examList;
            LinkedList<string> room_list = new LinkedList<string>();
            // ---------- Rooms ----------
            foreach (ExamObject s in examList)
            { if (!room_list.Contains(s.Examroom)) room_list.AddLast(s.Examroom); }
            List<string> temp_room_list = new List<string>(room_list);
            temp_room_list.Sort();
            room_list = new LinkedList<string>(temp_room_list);
            foreach (string s in room_list) AddTimeline(s);
            // SideBottomPanel 
            if (panel_empty == null) panel_empty = new Panel();
            panel_empty.Location = new Point(0, panel_top_time.Height + 5 + 85 * time_line_list.Count);
            panel_empty.Size = new Size(panel_side_room.Width - 17, 12);
            panel_empty.Name = "empty";
            panel_side_room.Controls.Add(panel_empty);
            LinkedList<string> tempRoomFilterList = new LinkedList<string>();
            // ---------- TimeLineEntities ----------
            //UpdateTLEntityBorder(examList);

            foreach (ExamObject exam in examList)
            {
                if (search != null)
                {
                    bool border = false;
                    if (searchMode == Search.teacher)
                    {// teacher
                        if (search.Equals(exam.Teacher1)) border = true;
                        if (exam.Teacher2.Length > 1 && search.Equals(exam.Teacher2)) border = true;
                        if (exam.Teacher3.Length > 1 && search.Equals(exam.Teacher3)) border = true;

                    }
                    if (searchMode == Search.student)
                    { // student
                        if (search.Equals(exam.Student.Fullname())) border = true;
                        if (exam.Student2 != null && search.Equals(exam.Student2.Fullname())) border = true;
                        if (exam.Student3 != null && search.Equals(exam.Student3.Fullname())) border = true;
                    }
                    if (searchMode == Search.subject) // subject
                        if (search.Equals(exam.Subject)) border = true;
                    if (searchMode == Search.room) // room
                        if (search.Equals(exam.Examroom) || search.Equals(exam.Preparationroom)) border = true;
                    if (searchMode == Search.grade) // grade
                        if (search.Equals(exam.Student.Grade)) border = true;
                    if (border) exam.SetBorder(Color.Red, true);
                }
                if (SwapExam != null && SwapExam.Id == exam.Id) exam.SetBorder(Color.Orange, false);
                if (EditExam != null && EditExam.Id == exam.Id) exam.SetBorder(Color.DarkRed, false);
            }

            foreach (ExamObject s in examList)
            {
                Panel panel_tl_entity = s.GetTimelineEntity();
                /*Panel panel_tl_entity = new Panel();
                DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime examTime = DateTime.ParseExact(s.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
                float unit_per_minute = 200F / 60F;
                float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;
                panel_tl_entity.Location = new Point(Convert.ToInt32(startpoint), 10);
                panel_tl_entity.Size = new Size(Convert.ToInt32(unit_per_minute * s.Duration), 60);
                panel_tl_entity.Name = s.Id.ToString();
                panel_tl_entity.BackColor = Colors.TL_Entity;
                panel_tl_entity.Paint += panel_time_line_entity_Paint;*/

                panel_tl_entity.MouseDoubleClick += panel_tl_entity_double_click;
                // ---------- context menu ----------
                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuEdit = new ToolStripMenuItem("Bearbeiten");
                ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Kopieren");
                ToolStripMenuItem mnuSwap = new ToolStripMenuItem("Tauschen");
                ToolStripMenuItem mnuDelete = new ToolStripMenuItem("Löschen");
                mnuEdit.Click += new EventHandler(panel_menu_click_edit);
                mnuCopy.Click += new EventHandler(panel_menu_click_copy);
                mnuSwap.Click += new EventHandler(panel_menu_click_swap);
                mnuDelete.Click += new EventHandler(panel_menu_click_delete);
                mnuEdit.Name = s.Id.ToString();
                mnuCopy.Name = s.Id.ToString();
                mnuSwap.Name = s.Id.ToString();
                mnuDelete.Name = s.Id.ToString();
                mnu.Items.AddRange(new ToolStripItem[] { mnuEdit, mnuCopy, mnuSwap, mnuDelete });
                panel_tl_entity.ContextMenuStrip = mnu;
                foreach (Panel p in time_line_list)
                {
                    if (p.Name.Equals(s.Examroom))
                    {
                        p.Controls.Add(s.GetTimelineEntity());
                        //time_line_entity_list.AddLast(panel_tl_entity);
                        //tl_exam_entity_list.AddLast(s);
                        break;
                    }
                }
            }

            if (search != null && search.Length >= 1) { lbl_search.Text = "Suche:\n" + search; panel_sidetop_empty.BackColor = Color.Yellow; }
            else if (filter != null && filter.Length >= 1) { lbl_search.Text = "Filter:\n" + filter; panel_sidetop_empty.BackColor = Color.Yellow; }
            else { lbl_search.Text = null; panel_sidetop_empty.BackColor = panel_side_room.BackColor; }
            if ((search != null && search.Length >= 1) && (filter != null && filter.Length >= 1)) tooltip_search_filter.SetToolTip(lbl_search, "Suche: " + search + "\nFilter: " + filter);
            else if (search != null && search.Length >= 1) tooltip_search_filter.SetToolTip(lbl_search, "Suche: " + search);
            else if (filter != null && filter.Length >= 1) tooltip_search_filter.SetToolTip(lbl_search, "Filter: " + filter);
        }
        // ----------------- panel menu click -----------------
        private void panel_menu_click_copy(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            ExamObject exam = database.GetExamById(Int32.Parse(itm.Name));
            lbl_mode.Text = edit_mode[1];
            btn_add_exam.Text = add_mode[1];
            this.dtp_date.Value = DateTime.ParseExact(exam.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
            this.dtp_time.Value = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            this.cb_exam_room.SelectedItem = exam.Examroom;
            this.cb_preparation_room.SelectedItem = exam.Preparationroom;
            StudentObject st = exam.Student;
            if (st == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
            else { this.cb_student.Text = st.Firstname + " " + st.Lastname; this.cb_grade.SelectedItem = st.Grade; }
            if (database.GetTeacherByID(exam.Teacher1) == null) this.cb_teacher1.Text = "";
            else this.cb_teacher1.Text = database.GetTeacherByID(exam.Teacher1).Firstname + " " + database.GetTeacherByID(exam.Teacher1).Lastname;
            if (database.GetTeacherByID(exam.Teacher2) == null) this.cb_teacher2.Text = "";
            else this.cb_teacher2.Text = database.GetTeacherByID(exam.Teacher2).Firstname + " " + database.GetTeacherByID(exam.Teacher2).Lastname;
            if (database.GetTeacherByID(exam.Teacher3) == null) this.cb_teacher3.Text = "";
            else this.cb_teacher3.Text = database.GetTeacherByID(exam.Teacher3).Firstname + " " + database.GetTeacherByID(exam.Teacher3).Lastname;
            this.cb_subject.Text = exam.Subject;
            this.tb_duration.Text = exam.Duration.ToString();
            EditExam.RemoveBorder();
            EditExam = null;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
        }
        private void panel_menu_click_edit(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            ExamObject exam = null;
            foreach (ExamObject te in tl_exam_entity_list)
                if (te.Id == Int32.Parse(itm.Name)) { exam = te; break; }
            if (EditExam != null)
            {
                EditExam.RemoveBorder();
                if (EditExam.Id == exam.Id) { EditExam = null; editPanel.Dispose(); return; }
            }
            EditExam = exam;
            lbl_mode.Text = edit_mode[1];
            btn_add_exam.Text = add_mode[1];
            this.dtp_date.Value = DateTime.ParseExact(exam.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
            this.dtp_time.Value = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            this.cb_exam_room.SelectedItem = exam.Examroom;
            this.cb_preparation_room.SelectedItem = exam.Preparationroom;
            if (exam.Student == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
            else { this.cb_student.Text = exam.Student.Firstname + " " + exam.Student.Lastname; this.cb_grade.SelectedItem = exam.Student.Grade; }
            if (exam.Student2 == null) { this.cb_student2.Text = null; }
            else { this.cb_student2.Text = exam.Student2.Firstname + " " + exam.Student2.Lastname; }
            if (exam.Student3 == null) { this.cb_student3.Text = null; }
            else { this.cb_student3.Text = exam.Student3.Firstname + " " + exam.Student3.Lastname; }

            if (database.GetTeacherByID(exam.Teacher1) == null) this.cb_teacher1.Text = "";
            else this.cb_teacher1.Text = database.GetTeacherByID(exam.Teacher1).Firstname + " " + database.GetTeacherByID(exam.Teacher1).Lastname;
            if (database.GetTeacherByID(exam.Teacher2) == null) this.cb_teacher2.Text = "";
            else this.cb_teacher2.Text = database.GetTeacherByID(exam.Teacher2).Firstname + " " + database.GetTeacherByID(exam.Teacher2).Lastname;
            if (database.GetTeacherByID(exam.Teacher3) == null) this.cb_teacher3.Text = "";
            else this.cb_teacher3.Text = database.GetTeacherByID(exam.Teacher3).Firstname + " " + database.GetTeacherByID(exam.Teacher3).Lastname;
            this.cb_subject.Text = exam.Subject;
            this.tb_duration.Text = exam.Duration.ToString();

            //foreach (Panel p in time_line_entity_list) { p.Refresh(); }
            EditExam.SetBorder(Color.DarkRed, false);
            UpdateEditPanel();
            if (cb_show_subjectteacher.Checked)
            { UpdateAutocompleteTeacher(database.GetTeacherBySubject(exam.Subject)); }
            else { UpdateAutocompleteTeacher(database.GetAllTeachers()); }
        }
        private void panel_menu_click_swap(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            if (SwapExam == null)
            {
                foreach (ExamObject exam in tl_exam_entity_list)
                    if (exam.Id == database.GetExamById(Int32.Parse(itm.Name)).Id)
                    { SwapExam = exam; break; }

                foreach (ExamObject exam in tl_exam_entity_list)
                    if (exam.Id == SwapExam.Id)
                    {
                        exam.SetBorder(Color.Orange, false);
                        exam.Panel.Refresh();
                        /*ExamObject exam = database.GetExamById(swapExamId);
                        time_line_entity_list.Remove(p);
                        UpdateTLEntityBorder();
                        time_line_entity_list.AddLast(exam.GetTimelineEntity*/
                        break;
                    }
            }
            else if (SwapExam != null)
            {
                ExamObject e1 = SwapExam;
                ExamObject e2 = database.GetExamById(Int32.Parse(itm.Name));
                if (e1.Id == e2.Id) { SwapExam.RemoveBorder(); SwapExam = null; return; }
                foreach (ExamObject exam in tl_exam_entity_list)
                {
                    if (exam.Id == e1.Id) exam.UpdatePanel();
                }
                DialogResult result = MessageBox.Show("Prüfung Tauschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DateTime dt1 = DateTime.ParseExact(e2.Date, "dd.MM.yyyy", null);
                    DateTime dt2 = DateTime.ParseExact(e1.Date, "dd.MM.yyyy", null);
                    database.EditExam(e1.Id, date: dt1.ToString("yyyy-MM-dd"), time: e2.Time, exam_room: e2.Examroom, preparation_room: e2.Preparationroom);
                    database.EditExam(e2.Id, date: dt2.ToString("yyyy-MM-dd"), time: e1.Time, exam_room: e1.Examroom, preparation_room: e1.Preparationroom);
                    SwapExam = null;
                    UpdateTimeline();
                }
                else
                {
                    SwapExam.RemoveBorder();
                    SwapExam = null;
                }
            }
        }
        private void panel_menu_click_delete(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            ExamObject exam = database.GetExamById(Int32.Parse(itm.Name));
            DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                exam.Delete();
                UpdateTimeline();
            }
        }
        // ----------------- panel click -----------------
        private void panel_tl_entity_double_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                ExamObject exam = database.GetExamById(Int32.Parse(p.Name));
                StudentObject student = exam.Student;

                DialogResult result = MessageBox.Show("Prüfung von " + student.Firstname + " " + student.Lastname + " Bearbeiten?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    EditExam = exam;
                    lbl_mode.Text = edit_mode[1];
                    btn_add_exam.Text = add_mode[1];
                    this.dtp_date.Value = DateTime.ParseExact(exam.Date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None);
                    this.dtp_time.Value = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    this.cb_exam_room.SelectedItem = exam.Examroom;
                    this.cb_preparation_room.SelectedItem = exam.Preparationroom;
                    StudentObject st = exam.Student;
                    this.cb_student.Text = st.Firstname + " " + st.Lastname;
                    this.cb_grade.SelectedItem = st.Grade;
                    this.cb_teacher1.Text = database.GetTeacherByID(exam.Teacher1).Firstname + " " + database.GetTeacherByID(exam.Teacher1).Lastname;
                    this.cb_teacher2.Text = database.GetTeacherByID(exam.Teacher2).Firstname + " " + database.GetTeacherByID(exam.Teacher2).Lastname;
                    this.cb_teacher3.Text = database.GetTeacherByID(exam.Teacher3).Firstname + " " + database.GetTeacherByID(exam.Teacher3).Lastname;
                    this.cb_subject.Text = exam.Subject;
                    this.tb_duration.Text = exam.Duration.ToString();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {

                /*Panel p = sender as Panel;
                    string[] exam = database.GetExamById(Int32.Parse(p.Name));
                    DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        database.DeleteExam(Int32.Parse(exam[0]));
                        update_timeline();
                    }*/
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// ---- PAINT ---- ////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void panel_side_room_Paint(object sender, PaintEventArgs e)
        {
            if (panel_time_line.AutoScrollPosition != panelScrollPos1)
            {
                panel_side_room.AutoScrollPosition = new Point(-panel_time_line.AutoScrollPosition.X, -panel_time_line.AutoScrollPosition.Y);
                panelScrollPos1 = panel_side_room.AutoScrollPosition;
            }
            else if (panel_side_room.AutoScrollPosition != panelScrollPos2)
            {
                panel_time_line.AutoScrollPosition = new Point(-panel_side_room.AutoScrollPosition.X, -panel_side_room.AutoScrollPosition.Y);
                panelScrollPos2 = panel_side_room.AutoScrollPosition;
            }
        }
        private void panel_time_line_master_Paint(object sender, PaintEventArgs e)
        {
            if (panel_time_line.AutoScrollPosition != panelScrollPos1)
            {
                panel_side_room.AutoScrollPosition = new Point(-panel_time_line.AutoScrollPosition.X, -panel_time_line.AutoScrollPosition.Y);
                panelScrollPos1 = panel_side_room.AutoScrollPosition;
            }
            else if (panel_side_room.AutoScrollPosition != panelScrollPos2)
            {
                panel_time_line.AutoScrollPosition = new Point(-panel_side_room.AutoScrollPosition.X, -panel_side_room.AutoScrollPosition.Y);
                panelScrollPos2 = panel_side_room.AutoScrollPosition;
            }
        }
        private void panel_top_time_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            Color c = Colors.TL_TimeBorder;
            byte b = 3; // border
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid);
            Font drawFont = new Font("Microsoft Sans Serif", 10);
            StringFormat drawFormat = new StringFormat();
            for (int i = 0; i < 12; i++)
            {
                float[] dashValues = { 1, 1 };
                Pen pen = new Pen(Colors.TL_MinLine, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Colors.TL_MinLine, 2);
                pen2.DashPattern = dashValues;
                e.Graphics.DrawLine(new Pen(Colors.TL_MinLine, 2), b + panel_tl.Width / 12 * i, b, b + panel_tl.Width / 12 * i, panel_tl.Height - b);
                e.Graphics.DrawLine(pen2, b + panel_tl.Width / 12 * i + panel_tl.Width / 24, b, b + panel_tl.Width / 12 * i + panel_tl.Width / 24, panel_tl.Height - b);
                e.Graphics.DrawLine(pen, b + panel_tl.Width / 12 * i + panel_tl.Width / 48, b, b + panel_tl.Width / 12 * i + panel_tl.Width / 48, panel_tl.Height - b);
                e.Graphics.DrawLine(pen, b + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, b, b + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, panel_tl.Height - b);
                e.Graphics.DrawString(7 + i + " Uhr", drawFont, new SolidBrush(Colors.TL_MinLine), 5 + panel_tl.Width / 12 * i, panel_tl.Height - 20, drawFormat);
            }
        }
        private void panel_time_line_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            for (int i = 0; i < 12; i++)
            {
                float[] dashValues = { 2, 2 };
                float[] dashValues2 = { 1, 1 };
                Pen pen = new Pen(Colors.TL_MinLine, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Colors.TL_MinLine, 2);
                pen2.DashPattern = dashValues2;
                e.Graphics.DrawLine(new Pen(Colors.TL_MinLine, 2), 4 + panel_tl.Width / 12 * i, 4, 4 + panel_tl.Width / 12 * i, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen2, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 24, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 24, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, panel_tl.Height - 4);
            }
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid);
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
            ExamObject exam = database.GetExamById(Int32.Parse(panel_tl_entity.Name));
            StudentObject student = exam.Student;
            if (student == null)
                student = new StudentObject(0, "Schüler nicht gefunden!", " ", "-");
            if (search != null)
            {
                bool border = false;
                if (searchMode == Search.teacher) // teacher
                    if (search.Equals(exam.Teacher1) || search.Equals(exam.Teacher2) || search.Equals(exam.Teacher3)) border = true;
                if (searchMode == Search.student) // student
                    if (search.Equals(student.Firstname + " " + student.Lastname)) border = true;
                if (searchMode == Search.subject) // subject
                    if (search.Equals(exam.Subject)) border = true;
                if (searchMode == Search.room) // room
                    if (search.Equals(exam.Examroom) || search.Equals(exam.Preparationroom)) border = true;
                if (searchMode == Search.grade) // grade
                    if (search.Equals(student.Grade)) border = true;
                if (border)
                {
                    ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid);
                }
            }
            if (SwapExam.Id == Int32.Parse(panel_tl_entity.Name))
            {
                ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                Color.Orange, 3, ButtonBorderStyle.Dashed,
                Color.Orange, 3, ButtonBorderStyle.Dashed,
                Color.Orange, 3, ButtonBorderStyle.Dashed,
                Color.Orange, 3, ButtonBorderStyle.Dashed);
            }
            if (EditExam.Id == Int32.Parse(panel_tl_entity.Name))
            {
                ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                Color.DarkRed, 3, ButtonBorderStyle.Dashed,
                Color.DarkRed, 3, ButtonBorderStyle.Dashed,
                Color.DarkRed, 3, ButtonBorderStyle.Dashed,
                Color.DarkRed, 3, ButtonBorderStyle.Dashed);
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rectL1 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 0, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL2 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 1, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL3 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 2, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL4 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 3, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            e.Graphics.DrawString(student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]", drawFont, Brushes.Black, rectL1, stringFormat);
            e.Graphics.DrawString(exam.Time + "     " + exam.Duration + "min", drawFont, Brushes.Black, rectL2, stringFormat);
            e.Graphics.DrawString(exam.Teacher1 + "  " + exam.Teacher2 + "  " + exam.Teacher3, drawFont, Brushes.Black, rectL3, stringFormat);
            e.Graphics.DrawString(exam.Subject + "  " + exam.Examroom + "  [" + exam.Preparationroom + "]", drawFont, Brushes.Black, rectL4, stringFormat);
            string line1 = student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]\n";
            string line11 = student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]\n";
            string line12 = student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]\n";
            //string line11 = exam.Student2.Firstname + " " + exam.Student2.Lastname + "  [" + exam.Student2.Grade + "]\n";
            //string line12 = exam.Student3.Firstname + " " + exam.Student3.Lastname + "  [" + exam.Student3.Grade + "]\n";
            string line2 = exam.Time + "     " + exam.Duration + "min\n";
            string line3 = exam.Teacher1 + "  " + exam.Teacher2 + "  " + exam.Teacher3 + "\n";
            string line4 = exam.Subject + "  " + exam.Examroom + "  [" + exam.Preparationroom + "]";

            ToolTip sfToolTip1 = new ToolTip();
            sfToolTip1.SetToolTip(panel_tl_entity, line1 + line11 + line12 + line2 + line3 + line4);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// ---- BTN ---- ////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////    
        private void btn_add_exam_Click(object sender, EventArgs e)
        {
            AddExam();
        }
        private void btn_delete_exam_Click(object sender, EventArgs e)
        {
            if (EditExam.Id != 0)
            {
                DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //database.DeleteExam(id);
                    EditExam.Delete();
                    EditExam = null;
                    lbl_mode.Text = edit_mode[0];
                    btn_add_exam.Text = add_mode[0];
                    UpdateTimeline();
                    if (this.cb_keep_data.Checked)
                    {
                        if (!Properties.Settings.Default.keep_subject) cb_subject.Text = null;
                        if (!Properties.Settings.Default.keep_examroom) cb_exam_room.Text = null;
                        if (!Properties.Settings.Default.keep_preparationroom) cb_preparation_room.Text = null;
                        if (!Properties.Settings.Default.keep_teacher) { cb_teacher1.Text = null; cb_teacher2.Text = null; cb_teacher3.Text = null; }
                        if (!Properties.Settings.Default.keep_grade) cb_grade.Text = null;
                        if (!Properties.Settings.Default.keep_student) { cb_student.Text = null; cb_student2.Text = null; cb_student3.Text = null; }
                        //this.cb_student.Text = null; // TODO: KEEP DATA ----------------------------------
                    }
                    else
                    {
                        this.cb_exam_room.Text = null;
                        this.cb_preparation_room.Text = null;
                        this.cb_student.Text = null;
                        this.cb_student2.Text = null;
                        this.cb_student3.Text = null;
                        this.cb_grade.Text = null;
                        this.cb_subject.Text = null;
                        this.cb_teacher1.Text = null;
                        this.cb_teacher2.Text = null;
                        this.cb_teacher3.Text = null;
                    }
                }
            }
        }
        private void btn_reuse_exam_Click(object sender, EventArgs e)
        {
            if (EditExam != null)
            {
                EditExam = null;
                lbl_mode.Text = edit_mode[0];
                btn_add_exam.Text = add_mode[0];
            }
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (EditExam != null)
                EditExam.RemoveBorder();
            EditExam = null;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[1];
            //foreach (Panel p in time_line_entity_list) { p.Refresh(); }
            foreach (ExamObject exam in tl_exam_entity_list) exam.UpdatePanel();
            this.cb_exam_room.Text = null;
            this.cb_preparation_room.Text = null;
            this.cb_student.Text = null;
            this.cb_grade.Text = null;
            this.cb_subject.Text = null;
            this.cb_teacher1.Text = null;
            this.cb_teacher2.Text = null;
            this.cb_teacher3.Text = null;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// ---- TSMI ---- ////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // ----------------- tsmi table-----------------
        private void tsmi_table_exams_Click(object sender, EventArgs e)
        {
            new Form_grid(0).ShowDialog();
        }
        private void tsmi_table_students_Click(object sender, EventArgs e)
        {
            new Form_grid(2).ShowDialog();
        }
        private void tsmi_table_teacher_Click(object sender, EventArgs e)
        {
            new Form_grid(1).ShowDialog();
        }
        // ----------------- tsmi search -----------------
        private void update_search_Event(object sender, EventArgs e)
        {
            string s = sender as string;
            search = s;
            UpdateTimeline();
        }
        private void tsmi_search_teacher_Click(object sender, EventArgs e)
        {
            searchMode = Search.teacher;
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_student_Click(object sender, EventArgs e)
        {
            searchMode = Search.student;
            FormSearch form = new FormSearch(1);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_subject_Click(object sender, EventArgs e)
        {
            searchMode = Search.subject;
            FormSearch form = new FormSearch(2);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();

        }
        private void tsmi_search_room_Click(object sender, EventArgs e)
        {
            searchMode = Search.room;
            FormSearch form = new FormSearch(3);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_grade_Click(object sender, EventArgs e)
        {
            searchMode = Search.grade;
            FormSearch form = new FormSearch(4);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_delete_Click(object sender, EventArgs e)
        {
            search = null;
            searchMode = Search.all;
            UpdateTimeline();
        }
        // ----------------- tsmi data -----------------
        private void tsmi_data_students_Click(object sender, EventArgs e)
        {
            FormStudentData form = new FormStudentData();
            form.FormClosed += update_autocomplete_Event;
            form.ShowDialog();
        }
        private void tsmi_data_rooms_Click(object sender, EventArgs e)
        {
            FormRoomData form = new FormRoomData();
            form.FormClosed += update_autocomplete_Event;
            form.ShowDialog();
        }
        private void tsmi_data_subjects_Click(object sender, EventArgs e)
        {
            FormSubjectData form = new FormSubjectData();
            form.FormClosed += update_autocomplete_Event;
            form.ShowDialog();
        }
        private void tsmi_data_teachers_Click(object sender, EventArgs e)
        {
            FormTeacherData formTeacherData = new FormTeacherData();
            formTeacherData.FormClosed += update_autocomplete_Event;
            formTeacherData.ShowDialog();
        }
        private void tsmi_data_editgrade_move_Click(object sender, EventArgs e)
        {
            FormRenameGrade form = new FormRenameGrade();
            void update_Event(object se, EventArgs ea)
            {
                UpdateTimeline();
                UpdateAutocomplete();
            }
            form.FormClosed += update_Event;
            form.ShowDialog();
        }
        private void tsmi_data_editgrade_delete_Click(object sender, EventArgs e)
        {
            FormDeleteGrade form = new FormDeleteGrade();
            void update_Event(object se, EventArgs ea)
            {
                UpdateTimeline();
                UpdateAutocomplete();
            }
            form.FormClosed += update_Event;
            form.ShowDialog();
            // remove exams in grade?
        }
        private void tsmi_data_loadteacher_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    Program.database.InsertTeacherFileIntoDB(filePath, false);
                }
            }
        }
        private void tsmi_data_loadstudents_Click(object sender, EventArgs e)
        {
            FormLoadStudents form = new FormLoadStudents();
            form.FormClosed += update_autocomplete_Event;
            form.ShowDialog();
        }
        // ----------------- tsmi exam -----------------
        private void tsmi_exam_changeroom_Click(object sender, EventArgs e)
        {
            FormChangeRoom form = new FormChangeRoom(this.dtp_date.Value.ToString("yyyy-MM-dd"));
            form.FormClosed += update_timeline_Event;
            form.ShowDialog();
        }
        private void tsmi_exam_examdates_Click(object sender, EventArgs e)
        {
            FormExamDateListView form = new FormExamDateListView(this);
            form.ShowDialog();
        }
        // ----------------- tsmi settings -----------------
        private void tsmi_settings_keepdata_Click(object sender, EventArgs e)
        {
            new KeepDataForm().ShowDialog();
        }
        // ----------------- tsmi filter -----------------
        private void tsmi_filter_grade_Click(object sender, EventArgs e)
        {
            filterMode = Filter.grade;
            FormSearch form = new FormSearch(4);
            form.UpdateSearch += update_filter_Event;
            form.ShowDialog();
        }
        private void tsmi_filter_all_Click(object sender, EventArgs e)
        {
            filterMode = Filter.all;
            filter = null;
            UpdateTimeline();
        }
        private void tsmi_filter_teacher_Click(object sender, EventArgs e)
        {
            filterMode = Filter.teacher;
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_filter_Event;
            form.ShowDialog();
        }
        private void update_filter_Event(object sender, EventArgs e)
        {
            string s = sender as string;
            filter = s;
            UpdateTimeline();
        }
        // ----------------- tsmi tools -----------------
        private void tsmi_tools_export_Click(object sender, EventArgs e)
        {
            Colors.Theme tempTheme = Colors.theme;
            DialogResult result = MessageBox.Show("Zeitstrahl in schwarz-weiß exportieren?", "Achtung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Colors.ColorTheme(Colors.Theme.bw);
                UpdateTimeline();
                panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
                panel_time_line.BackColor = Colors.TL_Bg;
                panel_top_time.BackColor = Colors.TL_TimeBg;
                panel_side_room.BackColor = Colors.TL_RoomBg;
            }

            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            lbl_search.Text = date;
            Panel tempPanel = new Panel();
            tempPanel.Width = panel_side_room.Width + 2400; // 200 per houer
            tempPanel.Height = panel_top_time.Height + 5 + 85 * time_line_list.Count + 15;
            tempPanel.Controls.Add(tlp_timeline_view);
            Bitmap bmp = new Bitmap(tempPanel.Width, tempPanel.Height - 20);
            tempPanel.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            lbl_search.Text = null;
            this.tlp_main.Controls.Add(this.tlp_timeline_view, 0, 1);
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Save Timeline",
                FileName = "Prüfungen-" + date + ".png",
                DefaultExt = "png",
                Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            Console.WriteLine(sfd.FileName);
            bmp.Save(sfd.FileName, ImageFormat.Png);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Colors.ColorTheme(tempTheme);
            UpdateTimeline();
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
        }
        private void tsmi_tools_deleteOldExams_Click(object sender, EventArgs e)
        {
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            DialogResult result = MessageBox.Show("Alle " + database.GetAllExamsBeforeDate(date).Count() + " Prüfungen vor dem " + date + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) database.DeleteOldExams(date);
            else return;
        }
        private void tsmi_tools_exportexamday_Click(object sender, EventArgs e)
        {
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
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
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", teacher.Firstname + " " + teacher.Lastname, time, exam.Examroom, exam.Preparationroom, student.Firstname + " " + student.Lastname, exam.Teacher1, exam.Teacher2, exam.Teacher3, exam.Subject, exam.Duration);
                        csv.AppendLine(newLine);
                    }
                File.WriteAllText(sfd.FileName, csv.ToString());
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// ---- METHODS ---- ////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdateEditPanel()
        {
            if (!editExamPreview) return;
            if (editPanel != null) editPanel.Dispose();
            string room = cb_exam_room.Text;
            editPanel = new Panel();
            DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime examTime = DateTime.ParseExact(dtp_time.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
            float unit_per_minute = 200F / 60F;
            float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;
            editPanel.Location = new Point(Convert.ToInt32(startpoint), 10);
            editPanel.Size = new Size(Convert.ToInt32(unit_per_minute * Int32.Parse(tb_duration.Text)), 60);
            editPanel.Name = "0";
            editPanel.BackColor = Color.FromArgb(80, Colors.TL_Entity);
            editPanel.Paint += panel_tl_edit_entity_Paint;
            foreach (Panel p in time_line_list)
            {
                if (p.Name.Equals(room))
                {
                    p.Controls.Add(editPanel);
                    p.Controls.SetChildIndex(editPanel, 0);
                    p.Update();
                    break;
                }
            }

            editPanel.MouseMove += editPanel_MouseMove;
            editPanel.MouseDown += editPanel_MouseEnter;
            void editPanel_MouseEnter(object sender, MouseEventArgs e)
            {
                oldPoint = new Point(e.X, e.Y);
            }
            void editPanel_MouseMove(object sender, MouseEventArgs e)
            {
                DateTime oldTime = dtp_time.Value;
                if (oldPoint == null)
                    oldPoint = new Point(e.X, e.Y);
                Panel p = sender as Panel;
                if (e.Button == MouseButtons.Left)  // panel position relative to mouse position (mouse enter -> set start)
                {
                    if (e.X - oldPoint.X > 10)
                    {
                        this.dtp_time.Value = this.dtp_time.Value.AddMinutes((e.X - oldPoint.X) / 4);
                        string time = dtp_time.Value.Hour + ":" + dtp_time.Value.Minute / 10 * 10;
                        this.dtp_time.Value = RoundUp(dtp_time.Value, TimeSpan.FromMinutes(15));
                    }
                    else if (oldPoint.X - e.X > 10)
                    {       // float unit_per_minute = 200F / 60F;
                        this.dtp_time.Value = this.dtp_time.Value.AddMinutes(-(oldPoint.X - e.X) / 4);
                        string time = dtp_time.Value.Hour + ":" + dtp_time.Value.Minute / 10 * 10;
                        this.dtp_time.Value = RoundUp(dtp_time.Value, TimeSpan.FromMinutes(15));
                    }
                    else return;
                    // 2400 / panel_time_line.Width;
                    if (dtp_time.Value != oldTime)
                        UpdateEditPanel();
                }
            }

            DateTime RoundUp(DateTime dt, TimeSpan d)
            {
                return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
            }

            void panel_tl_edit_entity_Paint(object sender, PaintEventArgs e)
            {
                Panel panel_tl_entity = sender as Panel;
                Font drawFont = new Font("Microsoft Sans Serif", 8);  // Arial
                ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                Color.FromArgb(75, Colors.TL_EntityBorder), 2, ButtonBorderStyle.Solid,
                Color.FromArgb(75, Colors.TL_EntityBorder), 2, ButtonBorderStyle.Solid,
                Color.FromArgb(75, Colors.TL_EntityBorder), 2, ButtonBorderStyle.Solid,
                Color.FromArgb(75, Colors.TL_EntityBorder), 2, ButtonBorderStyle.Solid);
                string teacher1 = null;
                string teacher2 = null;
                string teacher3 = null;
                if (cb_teacher1.Text.Length < 1) teacher1 = " - "; else teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]).Shortname;
                if (cb_teacher2.Text.Length < 1) teacher2 = " - "; else teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]).Shortname;
                if (cb_teacher3.Text.Length < 1) teacher3 = " - "; else teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]).Shortname;
                string student_name = cb_student.Text; //= new string[] { "0", dtp_date.Text, dtp_time.Text, cb_grade.Text, " - ", " - " };
                string tempfirstname = null;
                string templastname = null;
                string subject = null;
                if (cb_subject.Text.Length > 1) subject = cb_subject.Text;
                string duration = "0";
                if (tb_duration.Text.Length > 1) duration = tb_duration.Text;
                if (student_name.Length > 1 && student_name.Contains(' '))
                {
                    for (int i = 0; i < student_name.Split(' ').Length - 1; i++)
                        tempfirstname += student_name.Split(' ')[i] += " ";
                    tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                    templastname += student_name.Split(' ')[student_name.Split(' ').Length - 1];
                }
                StudentObject student = database.GetStudentByName(tempfirstname, templastname);

                if (student == null)
                    student = new StudentObject(0, "Schüler nicht gefunden!", " ", " ");
                string[] exam = new string[] { "0", tempfirstname, templastname, cb_exam_room.Text, cb_preparation_room.Text, " - ", teacher1, teacher2, teacher3, subject, duration };
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;
                Rectangle rectL1 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 0, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
                Rectangle rectL2 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 1, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
                Rectangle rectL3 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 2, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
                Rectangle rectL4 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 3, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
                e.Graphics.DrawString(student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]", drawFont, Brushes.Black, rectL1, stringFormat);
                e.Graphics.DrawString(exam[2] + "     " + exam[10] + "min", drawFont, Brushes.Black, rectL2, stringFormat);
                e.Graphics.DrawString(exam[6] + "  " + exam[7] + "  " + exam[8], drawFont, Brushes.Black, rectL3, stringFormat);
                e.Graphics.DrawString(exam[9] + "  " + exam[3] + "  [" + exam[4] + "]", drawFont, Brushes.Black, rectL4, stringFormat);
            }
        }
        public void SetDate(DateTime date)
        {
            dtp_timeline_date.Value = date;
            Properties.Settings.Default.timeline_date = date.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }
        private void UpdateAutocompleteTeacher(LinkedList<TeacherObject> list)
        {
            cb_teacher1.Items.Clear();
            cb_teacher2.Items.Clear();
            cb_teacher3.Items.Clear();
            var autocomplete_teacher = new AutoCompleteStringCollection();
            string[] teacher = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                teacher[i] = list.ElementAt(i).Firstname + " " + list.ElementAt(i).Lastname;
            autocomplete_teacher.AddRange(teacher);
            this.cb_teacher1.AutoCompleteCustomSource = autocomplete_teacher;
            this.cb_teacher1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_teacher1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb_teacher2.AutoCompleteCustomSource = autocomplete_teacher;
            this.cb_teacher2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_teacher2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb_teacher3.AutoCompleteCustomSource = autocomplete_teacher;
            this.cb_teacher3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_teacher3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //
            LinkedList<TeacherObject> teacherList = new LinkedList<TeacherObject>(list);
            List<TeacherObject> tempTeacherList = new List<TeacherObject>(list);
            tempTeacherList = tempTeacherList.OrderBy(x => x.Lastname).ToList();
            teacherList = new LinkedList<TeacherObject>(tempTeacherList);
            string[] listTeacher = new string[teacherList.Count];
            for (int i = 0; i < teacherList.Count; i++)
                listTeacher[i] = teacherList.ElementAt(i).Firstname + " " + teacherList.ElementAt(i).Lastname;
            cb_teacher1.Items.AddRange(listTeacher);
            cb_teacher2.Items.AddRange(listTeacher);
            cb_teacher3.Items.AddRange(listTeacher);
            cb_teacher2.Items.Add("");
            cb_teacher3.Items.Add("");

            // TODO: ComboBox List link element to id
            /*Item[] dates = new Item[list.Count];
            for (int i = 0; i < list.Count; i++)
                dates[i] = list.ElementAt(i);
            lb_exam_date.DisplayMember = nameof(Item.title);
            lb_exam_date.Items.Clear();
            lb_exam_date.Items.AddRange(dates);
            List<Item> item = new List<Item>();
            class Item
            {
                public string date { get; }
                public string title { get; }
                public Item(string date, string title)
                {
                    this.date = date;
                    this.title = title;
                } 
            }*/
        }
        private void UpdateAutocompleteStudent(LinkedList<TeacherObject> list)  //TODO: update autocomplete student
        {

        }

        // ----------------- events -----------------
        private void Form1_Load(object sender, EventArgs e)
        {
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
            //flp_menu.BackColor = Colors.Menu_Bg;
        }
        private void update_autocomplete_Event(object sender, EventArgs a)
        {
            UpdateAutocomplete();
        }
        private void update_timeline_Event(object sender, EventArgs a)
        {
            UpdateTimeline();
        }
        private void update_color_Event(object sender, EventArgs a)
        {
            UpdateTimeline();
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
        }
        private void tb_duration_TextChanged(object sender, EventArgs e)
        {
            if (tb_duration.Text.Length == 0) return;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb_duration.Text, "[^0-9]"))
                tb_duration.Text = tb_duration.Text.Remove(tb_duration.Text.Length - 1);
            cb_add_next_time.Text = "Nächste + " + this.tb_duration.Text + "min";
        }
        private void cb_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_grade.SelectedItem != null && cb_grade.SelectedItem.ToString() != null)
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<StudentObject> allStudents = database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString());
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i).Firstname + " " + allStudents.ElementAt(i).Lastname);
                autocomplete_student.AddRange(students);
                this.cb_student.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.cb_student2.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.cb_student3.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //
                cb_student.Items.Clear();
                cb_student2.Items.Clear();
                cb_student3.Items.Clear();
                LinkedList<StudentObject> studentList = new LinkedList<StudentObject>();
                LinkedList<StudentObject> allStudentsList = database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString());
                List<StudentObject> tempStudentList = new List<StudentObject>(allStudentsList);
                tempStudentList = tempStudentList.OrderBy(x => x.Lastname).ToList();
                studentList = new LinkedList<StudentObject>(tempStudentList);
                string[] listStudent = new string[studentList.Count];
                for (int i = 0; i < studentList.Count; i++)
                    listStudent[i] = studentList.ElementAt(i).Firstname + " " + studentList.ElementAt(i).Lastname;
                cb_student.Items.AddRange(listStudent);
                cb_student2.Items.AddRange(listStudent);
                cb_student3.Items.AddRange(listStudent);
                cb_student2.Items.Add("");
                cb_student3.Items.Add("");
            }
            else
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<StudentObject> allStudents = database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i).Firstname + " " + allStudents.ElementAt(i).Lastname);
                autocomplete_student.AddRange(students);
                this.cb_student.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.cb_student2.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.cb_student3.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //
                cb_student.Items.Clear();
                cb_student2.Items.Clear();
                cb_student3.Items.Clear();
                LinkedList<StudentObject> studentList = new LinkedList<StudentObject>();
                List<StudentObject> tempStudentList = new List<StudentObject>(allStudents);
                tempStudentList = tempStudentList.OrderBy(x => x.Lastname).ToList();
                studentList = new LinkedList<StudentObject>(tempStudentList);
                string[] listStudent = new string[studentList.Count];
                for (int i = 0; i < studentList.Count; i++)
                    listStudent[i] = studentList.ElementAt(i).Firstname + " " + studentList.ElementAt(i).Lastname;
                cb_student.Items.AddRange(listStudent);
                cb_student2.Items.AddRange(listStudent);
                cb_student3.Items.AddRange(listStudent);
                cb_student2.Items.Add("");
                cb_student3.Items.Add("");
            }
        }
        private void cb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_show_subjectteacher.Checked)
                UpdateAutocompleteTeacher(database.GetTeacherBySubject(cb_subject.Text));
            else UpdateAutocompleteTeacher(database.GetAllTeachers());
        }
        private void cb_show_subjectteacher_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_show_subjectteacher.Checked)
                UpdateAutocompleteTeacher(database.GetTeacherBySubject(cb_subject.Text));
            else UpdateAutocompleteTeacher(database.GetAllTeachers());
        }
        private void dtp_timeline_date_ValueChanged(object sender, EventArgs e)
        {
            UpdateTimeline();
            Properties.Settings.Default.timeline_date = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }

        private void cb_exam_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEditPanel();
        }

        private void tsmi_import_export_Click(object sender, EventArgs e)
        {
            new FormImportExport().ShowDialog();
        }

        private void tsmi_settings_Click(object sender, EventArgs e)
        {
            FormSettings form = new FormSettings();
            form.UpdateColor += update_color_Event;
            form.ShowDialog(this);
        }
    }

}