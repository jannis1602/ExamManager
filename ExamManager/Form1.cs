using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class Form1 : Form
    {
        public string search = null;
        public int search_index = 0; // 0-student; 1-teacher; 2-subject; 3-room
        public string filter = null;
        public enum Filter { all, grade, teacher, student }
        public Filter filterMode = Filter.all;
        private int swapExam = 0;

        private Database database;
        private int id = 0;
        private LinkedList<Panel> time_line_list;
        private LinkedList<Panel> time_line_entity_list;
        private LinkedList<Panel> time_line_room_list;
        private string[] edit_mode = { "neue Prüfung erstellen", "Prüfung bearbeiten" };
        private string[] add_mode = { "Prüfung hinzufügen", "Prüfung übernehmen" };
        private Point panelScrollPos1 = new Point();
        private Point panelScrollPos2 = new Point();
        private Panel panel_empty, panel_top;
        public Form1()
        {
            database = Program.database;
            time_line_list = new LinkedList<Panel>();
            time_line_entity_list = new LinkedList<Panel>();
            time_line_room_list = new LinkedList<Panel>();
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            dtp_date.Value = DateTime.Now;
            if (Properties.Settings.Default.timeline_date.Length > 2)
                dtp_timeline_date.Value = DateTime.ParseExact(Properties.Settings.Default.timeline_date, "dd.MM.yyyy", null);
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            if (database.GetAllExamsAtDate(date).Count < 1) dtp_timeline_date.Value = DateTime.Now;
            update_timeline();
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
            LinkedList<string[]> allStudentsList = database.GetAllStudents();
            string[] students = new string[allStudentsList.Count];
            for (int i = 0; i < allStudentsList.Count; i++)
                students[i] = (allStudentsList.ElementAt(i)[1] + " " + allStudentsList.ElementAt(i)[2]);
            autocomplete_student.AddRange(students);
            this.cb_student.AutoCompleteCustomSource = autocomplete_student;
            this.cb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //
            cb_student.Items.Clear();
            LinkedList<string[]> studentList = new LinkedList<string[]>();
            List<string[]> tempStudentList = new List<string[]>(allStudentsList);
            tempStudentList = tempStudentList.OrderBy(x => x[2]).ToList();
            studentList = new LinkedList<string[]>(tempStudentList);
            string[] listStudent = new string[studentList.Count];
            for (int i = 0; i < studentList.Count; i++)
                listStudent[i] = studentList.ElementAt(i)[1] + " " + studentList.ElementAt(i)[2];
            cb_student.Items.AddRange(listStudent);
            // grade
            cb_grade.Items.Clear();
            LinkedList<string[]> allStudents = database.GetAllStudents();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (string[] s in allStudents)
                if (!gradeList.Contains(s[3]))
                    gradeList.AddLast(s[3]);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList(); // .ThenBy( x => x.Bar)
            gradeList = new LinkedList<string>(templist);
            string[] listGrade = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                listGrade[i] = gradeList.ElementAt(i);
            cb_grade.Items.AddRange(listGrade);
            // teacher
            var autocomplete_teacher = new AutoCompleteStringCollection();
            LinkedList<string[]> teacherList1 = database.GetAllTeachers();
            string[] teacher = new string[teacherList1.Count];
            for (int i = 0; i < teacherList1.Count; i++)
                teacher[i] = teacherList1.ElementAt(i)[1] + " " + teacherList1.ElementAt(i)[2];
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
            LinkedList<string[]> allTeachers = database.GetAllTeachers();
            LinkedList<string[]> teacherList = new LinkedList<string[]>();
            List<string[]> tempTeacherList = new List<string[]>(allTeachers);
            tempTeacherList = tempTeacherList.OrderBy(x => x[2]).ToList();
            teacherList = new LinkedList<string[]>(tempTeacherList);
            string[] listTeacher = new string[teacherList.Count];
            for (int i = 0; i < teacherList.Count; i++)
                listTeacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
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
            if (cb_exam_room.SelectedItem == null || cb_preparation_room.SelectedItem == null)
            { MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return; }
            string date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            string time = this.dtp_time.Value.ToString("HH:mm");
            string exam_room = cb_exam_room.SelectedItem.ToString();
            string preparation_room = cb_preparation_room.SelectedItem.ToString();
            string student = cb_student.Text;
            string grade = null;
            if (cb_grade.SelectedItem != null) grade = cb_grade.SelectedItem.ToString();
            if (cb_teacher1.Text.Length < 1 || cb_teacher2.Text.Length < 1 || cb_teacher3.Text.Length < 1)
            { MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return; }
            string teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1])[0];
            string teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1])[0];
            string teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1])[0];
            string subject = cb_subject.Text;
            int duration = Int32.Parse(tb_duration.Text);
            // check if not empty
            if (exam_room.Length == 0 || preparation_room.Length == 0 || student.Length == 0 || teacher1.Length == 0 || teacher2.Length == 0 || teacher3.Length == 0 || subject.Length == 0 || duration == 0)
            { MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return; }
            // check room
            //if (id == 0)
            //{
            if (database.CheckTimeAndRoom(date, time, exam_room))
            { MessageBox.Show("Raum besetzt!", "Warnung"); return; }
            foreach (string[] s in database.GetAllExamsAtDateAndRoom(date, exam_room))
            {
                DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                if ((start < timestart && timestart < end) || (timestart < start && start < timeend))
                { MessageBox.Show("Raum besetzt!", "Warnung"); return; }
            }
            // }
            // check names in database
            string tempfirstname = null;
            string templastname = null;
            try
            {
                for (int i = 0; i < student.Split(' ').Length - 1; i++)
                    tempfirstname += student.Split(' ')[i] += " ";
                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                templastname += student.Split(' ')[student.Split(' ').Length - 1];
            }
            catch (NullReferenceException)
            { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }
            if (database.GetStudent(tempfirstname, templastname, grade)[0] == null)
            { MessageBox.Show("Schüler nicht gefunden!", "Warnung"); return; }
            if (database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]) == null)
            { MessageBox.Show("Lehrer 1 nicht gefunden!", "Warnung"); return; }
            if (database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]) == null)
            { MessageBox.Show("Lehrer 2 nicht gefunden!", "Warnung"); return; }
            if (database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]) == null)
            { MessageBox.Show("Lehrer 3 nicht gefunden!", "Warnung"); return; }

            bool checkTimeIsFree(string t, string d)
            {
                DateTime start = DateTime.ParseExact(t, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime end = DateTime.ParseExact(t, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(d));
                DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))
                    return false;
                return true;
            }
            // check teacher in other rooms
            foreach (string[] s in database.GetAllExamsFromTeacherAtDate(date, teacher1))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
                    /*DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                    DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                    if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))*/
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(database.GetTeacherByID(teacher1)[1] + " " + database.GetTeacherByID(teacher1)[2] + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }
            foreach (string[] s in database.GetAllExamsFromTeacherAtDate(date, teacher2))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
                    /*DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                    DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                    if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))*/
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(database.GetTeacherByID(teacher2)[1] + " " + database.GetTeacherByID(teacher2)[2] + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }
            foreach (string[] s in database.GetAllExamsFromTeacherAtDate(date, teacher3))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
                    /*DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                    DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                    if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))*/
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(database.GetTeacherByID(teacher3)[1] + " " + database.GetTeacherByID(teacher3)[2] + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }
            // check student in other rooms
            foreach (string[] s in database.GetAllExamsFromStudentAtDate(date, database.GetStudent(tempfirstname, templastname, grade)[0]))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
                    /*DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                    DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                    if ((start <= timestart && timestart < end) || (timestart <= start && start < timeend))*/
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(student + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }

            // Add / Edit / Clear
            if (id != 0)
                database.EditExam(id, date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, grade)[0], teacher1, teacher2, teacher3, subject, duration);
            if (id == 0)
                database.AddExam(date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, grade)[0], teacher1, teacher2, teacher3, subject, duration);
            id = 0;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
            this.dtp_timeline_date.Value = this.dtp_date.Value;
            Properties.Settings.Default.timeline_date = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
            update_timeline();
            if (this.cb_add_next_time.Checked) { this.dtp_time.Value = this.dtp_time.Value.AddMinutes(Int32.Parse(tb_duration.Text)); }
            if (this.cb_keep_data.Checked) { this.cb_student.Text = null; }
            else
            {
                this.cb_exam_room.Text = null;
                this.cb_preparation_room.Text = null;
                this.cb_student.Text = null;
                this.cb_grade.Text = null;
                this.cb_subject.Text = null;
                this.cb_teacher1.Text = null;
                this.cb_teacher2.Text = null;
                this.cb_teacher3.Text = null;
            }
        }
        /// <summary>Adds a timeline for a room</summary>
        public void AddTimeline(string room)
        {
            // -- roomlist --
            Panel panel_room = new Panel();
            panel_room.Location = new Point(0, panel_side_time.Height + 5 + 85 * time_line_list.Count);
            panel_room.Size = new Size(panel_side_room.Width - 17, 80);
            panel_room.BackColor = Color.LightBlue;
            panel_room.Name = room;
            Label lbl_room = new Label();
            lbl_room.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_room.Location = new Point(0, 0);
            lbl_room.Dock = DockStyle.Fill;
            lbl_room.Name = "lbl_room";
            lbl_room.Text = room;
            lbl_room.TextAlign = ContentAlignment.MiddleCenter;
            panel_room.Controls.Add(lbl_room);
            this.panel_side_room.HorizontalScroll.Value = 0;
            panel_side_room.Controls.Add(panel_room);
            time_line_room_list.AddLast(panel_room);
            panel_side_room.Refresh();
            // -- timeline --
            this.panel_time_line.HorizontalScroll.Value = 0;
            Panel panel_tl = new Panel();
            panel_tl.Location = new Point(0, panel_side_time.Height + 5 + 85 * time_line_list.Count);
            panel_tl.Name = room;
            panel_tl.Size = new Size(2400, 80);
            panel_tl.BackColor = Color.Gray;
            panel_tl.Paint += panel_time_line_Paint;
            this.panel_time_line.Controls.Add(panel_tl);
            time_line_list.AddLast(panel_tl);
        }
        /// <summary>Updates the timeline for all rooms on the day</summary>
        public void update_timeline()
        {
            if (panel_empty != null) panel_side_room.Controls.Remove(panel_empty);
            if (panel_top == null) panel_top = new Panel();
            else panel_side_room.Controls.Remove(panel_top);

            panel_top.Location = new Point(0, 0);
            panel_top.Size = new Size(panel_side_room.Width - 17, panel_side_time.Height);
            panel_top.Name = "top";
            panel_side_room.Controls.Add(panel_top);

            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_entity_list) p.Dispose();
            foreach (Panel p in time_line_room_list) p.Dispose();
            time_line_list.Clear();
            time_line_entity_list.Clear();
            time_line_room_list.Clear();

            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            LinkedList<string> room_list = new LinkedList<string>();
            foreach (string[] s in database.GetAllExamsAtDate(date))
            { if (!room_list.Contains(s[3])) room_list.AddLast(s[3]); }
            List<string> temp_room_list = new List<string>(room_list);
            temp_room_list.Sort();
            room_list = new LinkedList<string>(temp_room_list);
            foreach (string s in room_list)
                AddTimeline(s);
            // SideBottomPanel 
            if (panel_empty == null)
                panel_empty = new Panel();
            panel_empty.Location = new Point(0, panel_side_time.Height + 5 + 85 * time_line_list.Count);
            panel_empty.Size = new Size(panel_side_room.Width - 17, 12);
            panel_empty.Name = "empty";
            panel_side_room.Controls.Add(panel_empty);
            foreach (string[] s in database.GetAllExamsAtDate(date))
            {
                Panel panel_tl_entity = new Panel();
                DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime examTime = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
                float unit_per_minute = 200F / 60F;
                float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;
                panel_tl_entity.Location = new Point(Convert.ToInt32(startpoint), 10);
                panel_tl_entity.Size = new Size(Convert.ToInt32(unit_per_minute * Int32.Parse(s[10])), 60);
                panel_tl_entity.Name = s[0];
                panel_tl_entity.BackColor = Color.LightBlue;
                panel_tl_entity.Paint += panel_time_line_entity_Paint;
                //panel_tl_entity.MouseClick += panel_tl_entity_click;
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
                mnuEdit.Name = s[0];
                mnuCopy.Name = s[0];
                mnuSwap.Name = s[0];
                mnuDelete.Name = s[0];
                mnu.Items.AddRange(new ToolStripItem[] { mnuEdit, mnuCopy, mnuSwap, mnuDelete });
                panel_tl_entity.ContextMenuStrip = mnu;
                LinkedList<string> tempRoomFilterList = new LinkedList<string>();
                foreach (Panel p in time_line_list)
                {
                    if (p.Name.Equals(s[3]))
                    {
                        string[] exam = database.GetExamById(Int32.Parse(panel_tl_entity.Name));
                        string[] student = database.GetStudentByID(Int32.Parse(exam[5]));
                        if (filterMode == Filter.all)
                        {
                            p.Controls.Add(panel_tl_entity);
                            time_line_entity_list.AddLast(panel_tl_entity);
                        }
                        else if (filterMode == Filter.grade && student[3].Equals(filter))
                        {
                            tempRoomFilterList.AddLast(exam[3]);
                            p.Controls.Add(panel_tl_entity);
                            time_line_entity_list.AddLast(panel_tl_entity);
                        }
                    }
                }
            }
            if (search != null && search.Length >= 1) { lbl_search.Text = "Suche:\n" + search; panel_side_empty.BackColor = Color.Yellow; }
            else if (filter != null && filter.Length >= 1) { lbl_search.Text = "Filter:\n" + filter; panel_side_empty.BackColor = Color.Yellow; }
            else { lbl_search.Text = null; panel_side_empty.BackColor = panel_side_room.BackColor; }
        }
        // ----------------- panel menu -----------------
        private void panel_menu_click_copy(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            string[] exam = database.GetExamById(Int32.Parse(itm.Name));
            string[] student = database.GetStudentByID(Int32.Parse(exam[5]));

            lbl_mode.Text = edit_mode[1];
            btn_add_exam.Text = add_mode[1];
            this.dtp_date.Value = DateTime.ParseExact(exam[1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None);
            this.dtp_time.Value = DateTime.ParseExact(exam[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
            this.cb_exam_room.SelectedItem = exam[3];
            this.cb_preparation_room.SelectedItem = exam[4];
            string[] st = database.GetStudentByID(Int32.Parse(exam[5]));
            if (st == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
            else { this.cb_student.Text = st[1] + " " + st[2]; this.cb_grade.SelectedItem = st[3]; }
            if (database.GetTeacherByID(exam[6]) == null) this.cb_teacher1.Text = "";
            else this.cb_teacher1.Text = database.GetTeacherByID(exam[6])[1] + " " + database.GetTeacherByID(exam[6])[2];
            if (database.GetTeacherByID(exam[7]) == null) this.cb_teacher2.Text = "";
            else this.cb_teacher2.Text = database.GetTeacherByID(exam[7])[1] + " " + database.GetTeacherByID(exam[7])[2];
            if (database.GetTeacherByID(exam[8]) == null) this.cb_teacher3.Text = "";
            else this.cb_teacher3.Text = database.GetTeacherByID(exam[8])[1] + " " + database.GetTeacherByID(exam[8])[2];
            /*this.cb_student.Text = st[1] + " " + st[2];
            this.cb_grade.SelectedItem = st[3];
            this.cb_teacher1.Text = database.GetTeacherByID(exam[6])[1] + " " + database.GetTeacherByID(exam[6])[2];
            this.cb_teacher2.Text = database.GetTeacherByID(exam[7])[1] + " " + database.GetTeacherByID(exam[7])[2];
            this.cb_teacher3.Text = database.GetTeacherByID(exam[8])[1] + " " + database.GetTeacherByID(exam[8])[2];*/
            this.cb_subject.Text = exam[9];
            this.tb_duration.Text = exam[10];
            id = 0;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
        }
        private void panel_menu_click_edit(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            string[] exam = database.GetExamById(Int32.Parse(itm.Name));
            string[] student = database.GetStudentByID(Int32.Parse(exam[5]));
            id = Int32.Parse(exam[0]);
            lbl_mode.Text = edit_mode[1];
            btn_add_exam.Text = add_mode[1];
            this.dtp_date.Value = DateTime.ParseExact(exam[1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None);
            this.dtp_time.Value = DateTime.ParseExact(exam[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
            this.cb_exam_room.SelectedItem = exam[3];
            this.cb_preparation_room.SelectedItem = exam[4];
            string[] st = database.GetStudentByID(Int32.Parse(exam[5]));
            if (st == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
            else { this.cb_student.Text = st[1] + " " + st[2]; this.cb_grade.SelectedItem = st[3]; }
            if (database.GetTeacherByID(exam[6]) == null) this.cb_teacher1.Text = "";
            else this.cb_teacher1.Text = database.GetTeacherByID(exam[6])[1] + " " + database.GetTeacherByID(exam[6])[2];
            if (database.GetTeacherByID(exam[7]) == null) this.cb_teacher2.Text = "";
            else this.cb_teacher2.Text = database.GetTeacherByID(exam[7])[1] + " " + database.GetTeacherByID(exam[7])[2];
            if (database.GetTeacherByID(exam[8]) == null) this.cb_teacher3.Text = "";
            else this.cb_teacher3.Text = database.GetTeacherByID(exam[8])[1] + " " + database.GetTeacherByID(exam[8])[2];
            this.cb_subject.Text = exam[9];
            this.tb_duration.Text = exam[10];

            foreach (Panel p in time_line_entity_list) { p.Refresh(); }
            if (cb_show_subjectteacher.Checked)
            {
                var autocomplete_teacher = new AutoCompleteStringCollection();
                LinkedList<string[]> teacherList1 = database.GetTeacherBySubject(exam[9]);
                string[] teacher = new string[teacherList1.Count];
                for (int i = 0; i < teacherList1.Count; i++)
                    teacher[i] = teacherList1.ElementAt(i)[1] + " " + teacherList1.ElementAt(i)[2];
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

                cb_teacher1.Items.Clear();
                cb_teacher2.Items.Clear();
                cb_teacher3.Items.Clear();
                LinkedList<string[]> allTeachers = database.GetTeacherBySubject(cb_subject.Text);
                LinkedList<string[]> teacherList = new LinkedList<string[]>();
                List<string[]> tempTeacherList = new List<string[]>(allTeachers);
                tempTeacherList = tempTeacherList.OrderBy(x => x[2]).ToList();
                teacherList = new LinkedList<string[]>(tempTeacherList);
                string[] listTeacher = new string[teacherList.Count];
                for (int i = 0; i < teacherList.Count; i++)
                    listTeacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
                cb_teacher1.Items.AddRange(listTeacher);
                cb_teacher2.Items.AddRange(listTeacher);
                cb_teacher3.Items.AddRange(listTeacher);
                cb_teacher2.Items.Add("");
                cb_teacher3.Items.Add("");
            }
            else
            {
                var autocomplete_teacher = new AutoCompleteStringCollection();
                LinkedList<string[]> teacherList1 = database.GetAllTeachers();
                string[] teacher = new string[teacherList1.Count];
                for (int i = 0; i < teacherList1.Count; i++)
                    teacher[i] = teacherList1.ElementAt(i)[1] + " " + teacherList1.ElementAt(i)[2];
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

                cb_teacher1.Items.Clear();
                cb_teacher2.Items.Clear();
                cb_teacher3.Items.Clear();
                LinkedList<string[]> allTeachers = database.GetTeacherBySubject(cb_subject.Text);
                LinkedList<string[]> teacherList = new LinkedList<string[]>();
                List<string[]> tempTeacherList = new List<string[]>(allTeachers);
                tempTeacherList = tempTeacherList.OrderBy(x => x[2]).ToList();
                teacherList = new LinkedList<string[]>(tempTeacherList);
                string[] listTeacher = new string[teacherList.Count];
                for (int i = 0; i < teacherList.Count; i++)
                    listTeacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
                cb_teacher1.Items.AddRange(listTeacher);
                cb_teacher2.Items.AddRange(listTeacher);
                cb_teacher3.Items.AddRange(listTeacher);
                cb_teacher2.Items.Add("");
                cb_teacher3.Items.Add("");
            }
        }
        private void panel_menu_click_swap(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            string[] exam = database.GetExamById(Int32.Parse(itm.Name));
            string[] student = database.GetStudentByID(Int32.Parse(exam[5]));
            if (swapExam == 0)
            {
                swapExam = Int32.Parse(itm.Name);
                foreach (Panel p in time_line_entity_list)
                {
                    if (Int32.Parse(p.Name) == swapExam)
                    { p.Refresh(); break; }
                }
            }
            else if (swapExam != 0)
            {
                string[] e1 = database.GetExamById(swapExam);
                string[] e2 = database.GetExamById(Int32.Parse(itm.Name));
                if (e1[0] == e2[0]) { swapExam = 0; return; }
                foreach (Panel p in time_line_entity_list)
                {
                    if (p.Name == e1[0]) p.Refresh();
                }
                DialogResult result = MessageBox.Show("Prüfung Tauschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    database.EditExam(Int32.Parse(e1[0]), date: DateTime.ParseExact(e2[1], "dd.MM.yyyy", null).ToString("yyyy-MM-dd"), time: e2[2], exam_room: e2[3], preparation_room: e2[4]);
                    database.EditExam(Int32.Parse(e2[0]), date: DateTime.ParseExact(e1[1], "dd.MM.yyyy", null).ToString("yyyy-MM-dd"), time: e1[2], exam_room: e1[3], preparation_room: e1[4]);
                    swapExam = 0;
                    update_timeline();
                }
                else
                {
                    swapExam = 0;
                    foreach (Panel p in time_line_entity_list) { p.Refresh(); }
                }
            }
        }
        private void panel_menu_click_delete(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            string[] exam = database.GetExamById(Int32.Parse(itm.Name));
            DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                database.DeleteExam(Int32.Parse(exam[0]));
                update_timeline();
            }
        }
        // ----------------- panel click -----------------
        private void panel_tl_entity_double_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                string[] exam = database.GetExamById(Int32.Parse(p.Name));
                string[] student = database.GetStudentByID(Int32.Parse(exam[5]));

                DialogResult result = MessageBox.Show("Prüfung von " + student[1] + " " + student[2] + " Bearbeiten?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    id = Int32.Parse(exam[0]);
                    lbl_mode.Text = edit_mode[1];
                    btn_add_exam.Text = add_mode[1];
                    this.dtp_date.Value = DateTime.ParseExact(exam[1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None);
                    this.dtp_time.Value = DateTime.ParseExact(exam[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    this.cb_exam_room.SelectedItem = exam[3];
                    this.cb_preparation_room.SelectedItem = exam[4];
                    string[] st = database.GetStudentByID(Int32.Parse(exam[5]));
                    this.cb_student.Text = st[1] + " " + st[2];
                    this.cb_grade.SelectedItem = st[3];
                    this.cb_teacher1.Text = database.GetTeacherByID(exam[6])[1] + " " + database.GetTeacherByID(exam[6])[2];
                    this.cb_teacher2.Text = database.GetTeacherByID(exam[7])[1] + " " + database.GetTeacherByID(exam[7])[2];
                    this.cb_teacher3.Text = database.GetTeacherByID(exam[8])[1] + " " + database.GetTeacherByID(exam[8])[2];
                    this.cb_subject.Text = exam[9];
                    this.tb_duration.Text = exam[10];
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
        // ----------------- paint -----------------
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
        private void panel_side_time_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid);
            Font drawFont = new Font("Microsoft Sans Serif", 10);
            StringFormat drawFormat = new StringFormat();
            for (int i = 0; i < 12; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Blue, 2), 4 + panel_tl.Width / 12 * i, 4, 4 + panel_tl.Width / 12 * i, panel_tl.Height - 4);
                float[] dashValues = { 1, 1 };
                Pen pen = new Pen(Color.Blue, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Color.Blue, 2);
                pen2.DashPattern = dashValues;
                e.Graphics.DrawLine(pen2, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 24, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 24, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, panel_tl.Height - 4);
                e.Graphics.DrawString(7 + i + " Uhr", drawFont, Brushes.Blue, 5 + panel_tl.Width / 12 * i, panel_tl.Height - 20, drawFormat);
            }
        }
        private void panel_time_line_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid);
            for (int i = 0; i < 12; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Blue, 2), 4 + panel_tl.Width / 12 * i, 4, 4 + panel_tl.Width / 12 * i, panel_tl.Height - 4);
                float[] dashValues = { 2, 2 };
                float[] dashValues2 = { 1, 1 };
                Pen pen = new Pen(Color.Blue, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Color.Blue, 2);
                pen2.DashPattern = dashValues2;
                e.Graphics.DrawLine(pen2, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 24, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 24, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, 4, 4 + panel_tl.Width / 12 * i + panel_tl.Width / 48 * 3, panel_tl.Height - 4);
            }
        }
        private void panel_time_line_entity_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl_entity = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid);
            Font drawFont = new Font("Microsoft Sans Serif", 8);  // Arial
            string[] exam = database.GetExamById(Int32.Parse(panel_tl_entity.Name));
            string[] student = database.GetStudentByID(Int32.Parse(exam[5]));
            if (student == null)
                student = new string[] { "0", "Schüler nicht gefunden!", "  ", " - ", " - ", " - " };
            if (search != null)
            {
                if (search_index == 0) // teacher
                    if (search.Equals(exam[6]) || search.Equals(exam[7]) || search.Equals(exam[8]))
                    {
                        ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid);
                    }
                if (search_index == 1) // student
                    if (search.Equals(student[1] + " " + student[2]))
                    {
                        ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid);
                    }
                if (search_index == 2) // subject
                    if (search.Equals(exam[9]))
                    {
                        ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid);
                    }
                if (search_index == 3) // room
                    if (search.Equals(exam[3]) || search.Equals(exam[4]))
                    {
                        ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid);
                    }
                if (search_index == 4) // grade
                    if (search.Equals(student[3]))
                    {
                        ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid);
                    }
            }
            if (swapExam == Int32.Parse(panel_tl_entity.Name))
            {
                ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                Color.Orange, 3, ButtonBorderStyle.Dashed,
                Color.Orange, 3, ButtonBorderStyle.Dashed,
                Color.Orange, 3, ButtonBorderStyle.Dashed,
                Color.Orange, 3, ButtonBorderStyle.Dashed);
            }
            if (id == Int32.Parse(panel_tl_entity.Name))
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
            e.Graphics.DrawString(student[1] + " " + student[2] + "  [" + student[3] + "]", drawFont, Brushes.Black, rectL1, stringFormat);
            e.Graphics.DrawString(exam[2] + "     " + exam[10] + "min", drawFont, Brushes.Black, rectL2, stringFormat);
            e.Graphics.DrawString(exam[6] + "  " + exam[7] + "  " + exam[8], drawFont, Brushes.Black, rectL3, stringFormat);
            e.Graphics.DrawString(exam[9] + "  " + exam[3] + "  [" + exam[4] + "]", drawFont, Brushes.Black, rectL4, stringFormat);
            string line1 = student[1] + " " + student[2] + "  [" + student[3] + "]\n";
            string line2 = exam[2] + "     " + exam[10] + "min\n";
            string line3 = exam[6] + "  " + exam[7] + "  " + exam[8] + "\n";
            string line4 = exam[9] + "  " + exam[3] + "  [" + exam[4] + "]";
            ToolTip sfToolTip1 = new ToolTip();
            sfToolTip1.SetToolTip(panel_tl_entity, line1 + line2 + line3 + line4);
        }
        // ----------------- methods -----------------
        private void panel_time_line_Move(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            Console.WriteLine(p.AutoScrollPosition.Y);
        }
        private void panel_time_line_master_Paint(object sender, PaintEventArgs e)
        {
            if (panel_time_line.AutoScrollPosition.Y == 0)
                this.panel_side_room.HorizontalScroll.Value = 0;
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
        private void tb_duration_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tb_duration.Text, "[^0-9]"))
                tb_duration.Text = tb_duration.Text.Remove(tb_duration.Text.Length - 1);
            cb_add_next_time.Text = "Nächste + " + this.tb_duration.Text + "min";
        }
        private void UpdateAutocomplete_Event(object sender, EventArgs a)
        {
            UpdateAutocomplete();
        }
        public void SetDate(DateTime date)
        {
            dtp_timeline_date.Value = date;
            Properties.Settings.Default.timeline_date = date.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }
        private void update_timeline_Event(object sender, EventArgs a)
        {
            update_timeline();
        }
        // ----------------- BTNs -----------------
        private void btn_add_exam_Click(object sender, EventArgs e)
        {
            AddExam();
        }
        private void btn_delete_exam_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    database.DeleteExam(id);
                    id = 0;
                    lbl_mode.Text = edit_mode[0];
                    btn_add_exam.Text = add_mode[0];
                    update_timeline();
                    if (this.cb_keep_data.Checked)
                    {
                        this.cb_student.Text = null;
                    }
                    else
                    {
                        this.cb_exam_room.Text = null;
                        this.cb_preparation_room.Text = null;
                        this.cb_student.Text = null;
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
            if (id != 0)
            {
                id = 0;
                lbl_mode.Text = edit_mode[0];
                btn_add_exam.Text = add_mode[0];
            }
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            id = 0;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[1];
            foreach (Panel p in time_line_entity_list) { p.Refresh(); }
            /*if (this.cb_keep_data.Checked)
        { this.cb_student.SelectedItem = ""; }
        else
        {*/
            this.cb_exam_room.Text = null;
            this.cb_preparation_room.Text = null;
            this.cb_student.Text = null;
            this.cb_grade.Text = null;
            this.cb_subject.Text = null;
            this.cb_teacher1.Text = null;
            this.cb_teacher2.Text = null;
            this.cb_teacher3.Text = null;
            /*this.cb_exam_room.SelectedItem = "";
        this.cb_preparation_room.SelectedItem = "";
        this.cb_student.SelectedItem = "";
        this.cb_grade.SelectedItem = "";
        this.cb_subject.SelectedItem = "";
        this.cb_teacher1.SelectedItem = "";
        this.cb_teacher2.SelectedItem = "";
        this.cb_teacher3.SelectedItem = "";*/

        }
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
            update_timeline();
        }
        private void tsmi_search_teacher_Click(object sender, EventArgs e)
        {
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
            search_index = 0;
        }
        private void tsmi_search_student_Click(object sender, EventArgs e)
        {
            FormSearch form = new FormSearch(1);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog(); search_index = 1;
        }
        private void tsmi_search_subject_Click(object sender, EventArgs e)
        {
            FormSearch form = new FormSearch(2);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog(); search_index = 2;

        }
        private void tsmi_search_room_Click(object sender, EventArgs e)
        {
            FormSearch form = new FormSearch(3);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog(); search_index = 3;
        }
        private void tsmi_search_grade_Click(object sender, EventArgs e)
        {
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
            search_index = 4;
        }
        private void tsmi_search_delete_Click(object sender, EventArgs e)
        {
            search = null;
            search_index = 0;
            update_timeline();
        }
        // ----------------- tsmi data -----------------
        private void tsmi_data_students_Click(object sender, EventArgs e)
        {
            FormStudentData form = new FormStudentData();
            form.FormClosed += UpdateAutocomplete_Event;
            form.ShowDialog();
        }
        private void tsmi_data_rooms_Click(object sender, EventArgs e)
        {
            FormRoomData form = new FormRoomData();
            form.FormClosed += UpdateAutocomplete_Event;
            form.ShowDialog();
        }
        private void tsmi_data_subjects_Click(object sender, EventArgs e)
        {
            FormSubjectData form = new FormSubjectData();
            form.FormClosed += UpdateAutocomplete_Event;
            form.ShowDialog();
        }
        private void tsmi_data_teachers_Click(object sender, EventArgs e)
        {
            FormTeacherData formTeacherData = new FormTeacherData();
            formTeacherData.FormClosed += UpdateAutocomplete_Event;
            formTeacherData.ShowDialog();
        }
        private void tsmi_data_editgrade_move_Click(object sender, EventArgs e)
        {
            FormRenameGrade form = new FormRenameGrade();
            form.FormClosed += UpdateAutocomplete_Event;
            form.ShowDialog();
            // TODO: update all? ##############################################################################

        }
        private void tsmi_data_editgrade_delete_Click(object sender, EventArgs e)
        {
            FormDeleteGrade form = new FormDeleteGrade();
            form.FormClosed += UpdateAutocomplete_Event;
            form.ShowDialog();
            // yesno dialog
            // TODO: update all ############################################################################################################################
            // remove exams in grade
            // database.DeleteGrade();
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
                    Console.WriteLine(filePath);
                    Program.database.InsertTeacherFileIntoDB(filePath, false);
                }
            }
        }
        private void tsmi_data_loadstudents_Click(object sender, EventArgs e)
        {
            FormLoadStudents form = new FormLoadStudents();
            form.FormClosed += UpdateAutocomplete_Event;
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
        private void tsmi_settings_db_default_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.databasePath = "default";
            Properties.Settings.Default.Save();
        }
        private void tsmi_settings_db_localdb_Click(object sender, EventArgs e)
        {
            string filePath = Properties.Settings.Default.databasePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "database files (*.db)|*.db|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Lokale Datenbank auswählen";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    Properties.Settings.Default.databasePath = filePath;
                    Properties.Settings.Default.Save();
                    Program.database = new Database();
                    update_timeline();
                    UpdateAutocomplete();
                }
            }
        }
        private void tsmi_settings_mailgenerator_Click(object sender, EventArgs e)
        {
            FormSettings form = new FormSettings();
            form.ShowDialog(this);
        }
        // ----------------- tsmi filter -----------------
        private void tsmi_filter_grade_Click(object sender, EventArgs e)
        {
            FormFilterGrade form = new FormFilterGrade(this);
            form.FormClosed += update_timeline_Event;
            form.ShowDialog();
        }
        private void tsmi_filter_all_Click(object sender, EventArgs e)
        {
            filterMode = Filter.all;
            filter = null;
            update_timeline();
        }
        private void tsmi_filter_teacher_Click(object sender, EventArgs e)
        {
            // TODO
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cb_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_grade.SelectedItem != null && cb_grade.SelectedItem.ToString() != null)
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString());
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                this.cb_student.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //
                cb_student.Items.Clear();
                LinkedList<string[]> studentList = new LinkedList<string[]>();
                LinkedList<string[]> allStudentsList = database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString());
                List<string[]> tempStudentList = new List<string[]>(allStudentsList);
                tempStudentList = tempStudentList.OrderBy(x => x[2]).ToList();
                studentList = new LinkedList<string[]>(tempStudentList);
                string[] listStudent = new string[studentList.Count];
                for (int i = 0; i < studentList.Count; i++)
                    listStudent[i] = studentList.ElementAt(i)[1] + " " + studentList.ElementAt(i)[2];
                cb_student.Items.AddRange(listStudent);
            }
            else
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                this.cb_student.AutoCompleteCustomSource = autocomplete_student;
                this.cb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //
                cb_student.Items.Clear();
                LinkedList<string[]> studentList = new LinkedList<string[]>();
                List<string[]> tempStudentList = new List<string[]>(allStudents);
                tempStudentList = tempStudentList.OrderBy(x => x[2]).ToList();
                studentList = new LinkedList<string[]>(tempStudentList);
                string[] listStudent = new string[studentList.Count];
                for (int i = 0; i < studentList.Count; i++)
                    listStudent[i] = studentList.ElementAt(i)[1] + " " + studentList.ElementAt(i)[2];
                cb_student.Items.AddRange(listStudent);
            }
        }
        private void cb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_show_subjectteacher.Checked)
            {
                var autocomplete_teacher = new AutoCompleteStringCollection();
                LinkedList<string[]> teacherList1 = database.GetTeacherBySubject(cb_subject.Text);
                string[] teacher = new string[teacherList1.Count];
                for (int i = 0; i < teacherList1.Count; i++)
                    teacher[i] = teacherList1.ElementAt(i)[1] + " " + teacherList1.ElementAt(i)[2];
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


                cb_teacher1.Items.Clear();
                cb_teacher2.Items.Clear();
                cb_teacher3.Items.Clear();
                LinkedList<string[]> allTeachers = database.GetTeacherBySubject(cb_subject.Text);
                LinkedList<string[]> teacherList = new LinkedList<string[]>();
                List<string[]> tempTeacherList = new List<string[]>(allTeachers);
                tempTeacherList = tempTeacherList.OrderBy(x => x[2]).ToList();
                teacherList = new LinkedList<string[]>(tempTeacherList);
                string[] listTeacher = new string[teacherList.Count];
                for (int i = 0; i < teacherList.Count; i++)
                    listTeacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
                cb_teacher1.Items.AddRange(listTeacher);
                cb_teacher2.Items.AddRange(listTeacher);
                cb_teacher3.Items.AddRange(listTeacher);
                cb_teacher2.Items.Add("");
                cb_teacher3.Items.Add("");
            }
            else
            {
                var autocomplete_teacher = new AutoCompleteStringCollection();
                LinkedList<string[]> teacherList1 = database.GetAllTeachers();
                string[] teacher = new string[teacherList1.Count];
                for (int i = 0; i < teacherList1.Count; i++)
                    teacher[i] = teacherList1.ElementAt(i)[1] + " " + teacherList1.ElementAt(i)[2];
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


                cb_teacher1.Items.Clear();
                cb_teacher2.Items.Clear();
                cb_teacher3.Items.Clear();
                LinkedList<string[]> allTeachers = database.GetTeacherBySubject(cb_subject.Text);
                LinkedList<string[]> teacherList = new LinkedList<string[]>();
                List<string[]> tempTeacherList = new List<string[]>(allTeachers);
                tempTeacherList = tempTeacherList.OrderBy(x => x[2]).ToList();
                teacherList = new LinkedList<string[]>(tempTeacherList);
                string[] listTeacher = new string[teacherList.Count];
                for (int i = 0; i < teacherList.Count; i++)
                    listTeacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
                cb_teacher1.Items.AddRange(listTeacher);
                cb_teacher2.Items.AddRange(listTeacher);
                cb_teacher3.Items.AddRange(listTeacher);
                cb_teacher2.Items.Add("");
                cb_teacher3.Items.Add("");
            }
        }
        private void cb_show_subjectteacher_CheckedChanged(object sender, EventArgs e)
        {
            var autocomplete_teacher = new AutoCompleteStringCollection();
            LinkedList<string[]> teacherList1 = database.GetAllTeachers();
            string[] teacher = new string[teacherList1.Count];
            for (int i = 0; i < teacherList1.Count; i++)
                teacher[i] = teacherList1.ElementAt(i)[1] + " " + teacherList1.ElementAt(i)[2];
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
            LinkedList<string[]> allTeachers = database.GetAllTeachers();
            LinkedList<string[]> teacherList = new LinkedList<string[]>();
            List<string[]> tempTeacherList = new List<string[]>(allTeachers);
            tempTeacherList = tempTeacherList.OrderBy(x => x[2]).ToList();
            teacherList = new LinkedList<string[]>(tempTeacherList);
            string[] listTeacher = new string[teacherList.Count];
            for (int i = 0; i < teacherList.Count; i++)
                listTeacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
            cb_teacher1.Items.AddRange(listTeacher);
            cb_teacher2.Items.AddRange(listTeacher);
            cb_teacher3.Items.AddRange(listTeacher);
            cb_teacher2.Items.Add("");
            cb_teacher3.Items.Add("");
        }
        private void dtp_timeline_date_ValueChanged(object sender, EventArgs e)
        {
            update_timeline();
            Properties.Settings.Default.timeline_date = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }

    }
}