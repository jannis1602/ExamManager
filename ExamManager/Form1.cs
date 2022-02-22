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
        private Panel editPanel = null;
        Point oldPoint;


        public string search = null;
        public int search_index = 0; // 0-student; 1-teacher; 2-subject; 3-room // TODO: ENUM
        //public enum Search { all, student, teacher, subject, room }
        //public Search searchMode = Search.all;
        public string filter = null;
        public enum Filter { all, grade, teacher, student }
        public Filter filterMode = Filter.all;
        private int swapExam = 0;

        private readonly Database database;
        private int id = 0;
        private readonly LinkedList<Panel> time_line_list;
        private readonly LinkedList<Panel> time_line_entity_list;
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
            time_line_entity_list = new LinkedList<Panel>();
            time_line_room_list = new LinkedList<Panel>();
            InitializeComponent();
            dtp_date.Value = DateTime.Now;
            if (Properties.Settings.Default.timeline_date.Length > 2)
                dtp_timeline_date.Value = DateTime.ParseExact(Properties.Settings.Default.timeline_date, "dd.MM.yyyy", null);
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            if (database.GetAllExamsAtDate(date).Count < 1) dtp_timeline_date.Value = DateTime.Now;
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
            LinkedList<string[]> allStudentsList = database.GetAllStudents();
            if (cb_student_onetime.Checked)
            {
                LinkedList<string[]> tempAllStudentsList = new LinkedList<string[]>();
                string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
                foreach (string[] s in database.GetAllStudents())
                    if (database.GetAllExamsFromStudentAtDate(date, s[0]).Count == 0)
                        tempAllStudentsList.AddLast(s);
                allStudentsList = tempAllStudentsList;
            }
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
            //if (cb_student.SelectedItem == null) return;
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
            //if (database.CheckTimeAndRoom(date, time, exam_room))
            //{ MessageBox.Show("Raum besetzt", "Warnung"); return; }
            if (id != 0)
            {
                if (time != database.GetExamById(id)[2] || date != database.GetExamById(id)[1])
                    foreach (string[] s in database.GetAllExamsAtDateAndRoom(date, exam_room))
                        if (id != Int32.Parse(s[0]))
                        {
                            DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                            DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                            DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                            DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);
                            if ((start < timestart && timestart < end) || (timestart < start && start < timeend))
                            { MessageBox.Show("Raum besetzt!", "Warnung"); return; }
                        }
            }
            else
                foreach (string[] s in database.GetAllExamsAtDateAndRoom(date, exam_room))
                {
                    DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
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
                for (int i = 0; i < student.Split(' ').Length - 1; i++)
                    tempfirstname += student.Split(' ')[i] += " ";
                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                templastname += student.Split(' ')[student.Split(' ').Length - 1];
            }
            catch (NullReferenceException)
            { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }
            if (database.GetStudent(tempfirstname, templastname, grade) == null)
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
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(database.GetTeacherByID(teacher1)[1] + " " + database.GetTeacherByID(teacher1)[2] + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }
            foreach (string[] s in database.GetAllExamsFromTeacherAtDate(date, teacher2))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(database.GetTeacherByID(teacher2)[1] + " " + database.GetTeacherByID(teacher2)[2] + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }
            foreach (string[] s in database.GetAllExamsFromTeacherAtDate(date, teacher3))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
                    if (!checkTimeIsFree(s[2], s[10]))
                    { MessageBox.Show(database.GetTeacherByID(teacher3)[1] + " " + database.GetTeacherByID(teacher3)[2] + " befindet sich in einem anderem Raum: " + s[3], "Warnung"); return; }
                }
            }
            // check student in other rooms
            foreach (string[] s in database.GetAllExamsFromStudentAtDate(date, database.GetStudent(tempfirstname, templastname, grade)[0]))
            {
                if (id == 0 || (exam_room != s[3] && s[3] != database.GetExamById(id)[3]))
                {
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
            this.lbl_mode.Text = edit_mode[0];
            this.btn_add_exam.Text = add_mode[0];
            this.dtp_timeline_date.Value = this.dtp_date.Value;
            Properties.Settings.Default.timeline_date = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
            UpdateTimeline();
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
            this.panel_time_line.Controls.Add(panel_tl);
            time_line_list.AddLast(panel_tl);
        }
        /// <summary>Updates the timeline for all rooms of the day</summary>
        public void UpdateTimeline()
        {
            if (filter == null || filter.Length == 0) filterMode = Filter.all;
            if (panel_empty != null) panel_side_room.Controls.Remove(panel_empty);
            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_entity_list) p.Dispose();
            foreach (Panel p in time_line_room_list) p.Dispose();
            time_line_list.Clear();
            time_line_entity_list.Clear();
            time_line_room_list.Clear();

            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            LinkedList<string[]> examList = null;
            if (filterMode == Filter.teacher) examList = database.GetAllExamsFromTeacherAtDate(date, filter);
            else if (filterMode == Filter.grade) examList = database.GetAllExamsFromGradeAtDate(date, filter);
            else examList = database.GetAllExamsAtDate(date);
            LinkedList<string> room_list = new LinkedList<string>();
            // ---------- Rooms ----------
            foreach (string[] s in examList)
            { if (!room_list.Contains(s[3])) room_list.AddLast(s[3]); }
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
            // TODO: Filter ?
            // ---------- TimeLineEntities ----------
            foreach (string[] s in examList)
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
                panel_tl_entity.BackColor = Colors.TL_Entity;
                panel_tl_entity.Paint += panel_time_line_entity_Paint;
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
                foreach (Panel p in time_line_list)
                {
                    if (p.Name.Equals(s[3]))
                    {
                        p.Controls.Add(panel_tl_entity);
                        time_line_entity_list.AddLast(panel_tl_entity);
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
            string[] exam = database.GetExamById(Int32.Parse(itm.Name));
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
            id = 0;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
        }
        private void panel_menu_click_edit(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            string[] exam = database.GetExamById(Int32.Parse(itm.Name));
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
            { UpdateAutocompleteTeacher(database.GetTeacherBySubject(exam[9])); }
            else { UpdateAutocompleteTeacher(database.GetAllTeachers()); }
        }
        private void panel_menu_click_swap(object sender, EventArgs e)
        {
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
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
                    DateTime dt1 = DateTime.ParseExact(e2[1], "dd.MM.yyyy", null);
                    DateTime dt2 = DateTime.ParseExact(e1[1], "dd.MM.yyyy", null);
                    database.EditExam(Int32.Parse(e1[0]), date: dt1.ToString("yyyy-MM-dd"), time: e2[2], exam_room: e2[3], preparation_room: e2[4]);
                    database.EditExam(Int32.Parse(e2[0]), date: dt2.ToString("yyyy-MM-dd"), time: e1[2], exam_room: e1[3], preparation_room: e1[4]);
                    swapExam = 0;
                    UpdateTimeline();
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
                UpdateTimeline();
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// ---- BTN ---- ////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////    
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
                    UpdateTimeline();
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
            search_index = 0;
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_student_Click(object sender, EventArgs e)
        {
            search_index = 1;
            FormSearch form = new FormSearch(1);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_subject_Click(object sender, EventArgs e)
        {
            search_index = 2;
            FormSearch form = new FormSearch(2);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();

        }
        private void tsmi_search_room_Click(object sender, EventArgs e)
        {
            search_index = 3;
            FormSearch form = new FormSearch(3);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_grade_Click(object sender, EventArgs e)
        {
            search_index = 4;
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_search_Event;
            form.ShowDialog();
        }
        private void tsmi_search_delete_Click(object sender, EventArgs e)
        {
            search = null;
            search_index = 0;
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
            // TODO: update all? ##############################################################################

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
        private void tsmi_settings_db_default_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.databasePath = "default";
            Properties.Settings.Default.Save();
        }
        private void tsmi_settings_db_localdb_Click(object sender, EventArgs e)
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
                    Properties.Settings.Default.databasePath = filePath;
                    Properties.Settings.Default.Save();
                    Program.database = new Database();
                    UpdateTimeline();
                    UpdateAutocomplete();
                }
            }
        }
        private void tsmi_settings_mailgenerator_Click(object sender, EventArgs e)
        {
            FormSettings form = new FormSettings();
            form.ShowDialog(this);
        }
        private void tsmi_color_dark_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.color_theme = 0;
            Properties.Settings.Default.Save();
            Colors.ColorTheme(Colors.Theme.dark);
            UpdateTimeline();
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
            //MessageBox.Show("Einstellungen werden beim nächsten start übernommen.", "Info!", MessageBoxButtons.OK);
        }
        private void tsmi_color_light_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.color_theme = 1;
            Properties.Settings.Default.Save();
            Colors.ColorTheme(Colors.Theme.light);
            UpdateTimeline();
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
            //MessageBox.Show("Einstellungen werden beim nächsten start übernommen.", "Info!", MessageBoxButtons.OK);
        }
        private void tsmi_color_bw_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.color_theme = 2;
            Properties.Settings.Default.Save();
            Colors.ColorTheme(Colors.Theme.bw);
            UpdateTimeline();
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
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
            tempPanel.Width = panel_side_room.Width + 2400;
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
                var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach");
                csv.AppendLine(firstLine);
                foreach (string[] teacher in database.GetAllTeachers())
                    foreach (string[] exam in database.GetAllExamsFromTeacherAtDate(date, teacher[0]))
                    {
                        string[] student = database.GetStudentByID(Int32.Parse(exam[5]));
                        DateTime start = DateTime.ParseExact(exam[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                        DateTime end = DateTime.ParseExact(exam[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(exam[10]));
                        string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", teacher[1] + " " + teacher[2], time, exam[3], exam[4], student[1] + " " + student[2], exam[6], exam[7], exam[8], exam[9]);
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
            //editPanel.MouseEnter
            void editPanel_MouseEnter(object sender, MouseEventArgs e)
            {
                oldPoint = new Point(e.X, e.Y); // new Point(Cursor.Position.X, Cursor.Position.Y);
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
                        //Console.WriteLine(this.dtp_time.Value.Minute / 10);
                        string time = dtp_time.Value.Hour + ":" + dtp_time.Value.Minute / 10 * 10;
                        //Console.WriteLine(Convert.ToDateTime(time).ToString("HH:mm"));
                        //this.dtp_time.Value = DateTime.ParseExact(Convert.ToDateTime(time).ToString("HH:mm"), "HH:mm", null);
                        this.dtp_time.Value = RoundUp(dtp_time.Value, TimeSpan.FromMinutes(15));

                    }
                    else if (oldPoint.X - e.X > 10)
                    {
                        this.dtp_time.Value = this.dtp_time.Value.AddMinutes(-(oldPoint.X - e.X) / 4);
                        //Console.WriteLine(this.dtp_time.Value.Minute / 10);
                        string time = dtp_time.Value.Hour + ":" + dtp_time.Value.Minute / 10 * 10;
                        //Console.WriteLine(Convert.ToDateTime(time).ToString("HH:mm"));
                        //this.dtp_time.Value = DateTime.ParseExact(Convert.ToDateTime(time).ToString("HH:mm"), "HH:mm", null);
                        this.dtp_time.Value = RoundUp(dtp_time.Value, TimeSpan.FromMinutes(15));
                    }

                    else return;
                    //Console.WriteLine(oldPoint.X - e.X);
                    //Console.WriteLine(new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y));
                    // this.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
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
                if (cb_teacher1.Text.Length < 1) teacher1 = " - "; else teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1])[0];
                if (cb_teacher2.Text.Length < 1) teacher2 = " - "; else teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1])[0];
                if (cb_teacher3.Text.Length < 1) teacher3 = " - "; else teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1])[0];
                string student_name = cb_student.Text; //= new string[] { "0", dtp_date.Text, dtp_time.Text, cb_grade.Text, " - ", " - " };
                string tempfirstname = null;
                string templastname = null;
                if (student_name.Length > 1 && student_name.Contains(' '))
                {
                    for (int i = 0; i < student_name.Split(' ').Length - 1; i++)
                        tempfirstname += student_name.Split(' ')[i] += " ";
                    tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                    templastname += student_name.Split(' ')[student_name.Split(' ').Length - 1];
                }
                string[] student = database.GetStudent(tempfirstname, templastname);

                if (student == null)
                    student = new string[] { "0", "Schüler nicht gefunden!", " ", " - ", " - ", " - " };
                string[] exam = new string[] { "0", tempfirstname, templastname, cb_exam_room.Text, cb_preparation_room.Text, " p-room - ", " - ", teacher1, teacher2, teacher3, tb_duration.Text };
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
            }
        }
        public void SetDate(DateTime date)
        {
            dtp_timeline_date.Value = date;
            Properties.Settings.Default.timeline_date = date.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }
        private void UpdateAutocompleteTeacher(LinkedList<string[]> list)
        {
            cb_teacher1.Items.Clear();
            cb_teacher2.Items.Clear();
            cb_teacher3.Items.Clear();
            var autocomplete_teacher = new AutoCompleteStringCollection();
            string[] teacher = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                teacher[i] = list.ElementAt(i)[1] + " " + list.ElementAt(i)[2];
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
            LinkedList<string[]> teacherList = new LinkedList<string[]>(list);
            List<string[]> tempTeacherList = new List<string[]>(list);
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
    }
}