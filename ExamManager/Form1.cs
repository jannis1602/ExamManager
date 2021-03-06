using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly Database database;
        private LinkedList<Panel> time_line_room_list;
        private LinkedList<Panel> time_line_list;
        private LinkedList<ExamObject> tl_exam_entity_list;
        private readonly string[] edit_mode = { "neue Prüfung erstellen", "Prüfung bearbeiten", "mehrere Prüfungen bearbeiten" };
        private readonly string[] add_mode = { "Prüfung hinzufügen", "Prüfung übernehmen", "Prüfungen ändern" };
        private Point SyncScrollPos1 = new Point();
        private Point SyncScrollPos2 = new Point();
        private Panel panel_room_bottom;
        private ToolTip ttExamCounter = new ToolTip();
        // Search/Filter
        public enum Search { all, student, teacher, subject, room, grade }
        public enum Filter { all, grade, teacher, student, subject, room }
        public string search = null;
        public string filter = null;
        public Search searchMode = Search.all;
        public Filter filterMode = Filter.all;
        LinkedList<string> RoomNameFilterList;
        // EditMode
        private ExamObject EditExam = null;
        private ExamObject SwapExam = null;
        private Panel editPanel = null; // TODO: EditPanel in ExamObject
        private Point EditExamMovePanelOldPos;
        private Panel EditExamOldTL;
        private LinkedList<ExamObject> tl_entity_multiselect_list;

        // ---- TEMP ----
        public int StartTimeTL = Properties.Settings.Default.TLStartTime;
        public int LengthTL = Properties.Settings.Default.TLLength;
        public int PixelPerHour = Properties.Settings.Default.PixelPerHour;  // Check min length
        public Form1()
        {
            database = Program.database;
            time_line_list = new LinkedList<Panel>();
            tl_exam_entity_list = new LinkedList<ExamObject>();
            tl_entity_multiselect_list = new LinkedList<ExamObject>();
            time_line_room_list = new LinkedList<Panel>();
            InitializeComponent();
            dtp_time.MouseWheel += Dtp_time_MouseWheel;

            if (Properties.Settings.Default.TimelineDate.Length > 2)
                dtp_timeline_date.Value = DateTime.ParseExact(Properties.Settings.Default.TimelineDate, "dd.MM.yyyy", null);
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            if (database.GetAllExamsAtDate(date).Count < 1) dtp_timeline_date.Value = DateTime.Now;
            if (Properties.Settings.Default.TimelineDate.Length > 2)
                dtp_date.Value = DateTime.ParseExact(Properties.Settings.Default.TimelineDate, "dd.MM.yyyy", null);
            date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            if (database.GetAllExamsAtDate(date).Count < 1) dtp_date.Value = DateTime.Now;
            UpdateTimeline(); // render TL on startup
            UpdateAutocomplete();
            this.ActiveControl = panel_time_line;
        }

        #region -------- Methods --------
        /// <summary>reloads the autocomplete and dropdownlist
        /// singleUpdate: All(0), Subjects(1), Students(2), Grade(3), Teacher(4), Room(5) </summary>

        private void UpdateAutocomplete(int singleUpdate = 0)
        {
            // subjects
            if (singleUpdate == 0 || singleUpdate == 1)
            {
                cb_subject.Items.Clear();
                LinkedList<string[]> subjectList = Program.database.GetAllSubjects();
                string[] subjects = new string[subjectList.Count];
                for (int i = 0; i < subjectList.Count; i++)
                    subjects[i] = subjectList.ElementAt(i)[0];
                cb_subject.Items.AddRange(subjects);
            }
            // students
            if (singleUpdate == 0 || singleUpdate == 2)
            {
                if (cb_grade.SelectedItem != null && cb_grade.SelectedItem.ToString() != null)
                    UpdateAutocompleteStudent(database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString()));
                else UpdateAutocompleteStudent(database.GetAllStudents());
            }
            // grade
            if (singleUpdate == 0 || singleUpdate == 3)
            {
                cb_grade.Items.Clear();
                LinkedList<StudentObject> allStudents = database.GetAllStudents();
                LinkedList<string> gradeList = new LinkedList<string>();
                foreach (StudentObject s in allStudents)
                    if (!gradeList.Contains(s.Grade))
                        gradeList.AddLast(s.Grade);
                List<string> templist = new List<string>(gradeList);
                templist = templist.OrderBy(x => x).ToList(); // .ThenBy( x => x)
                gradeList = new LinkedList<string>(templist);
                string[] listGrade = new string[gradeList.Count];
                for (int i = 0; i < gradeList.Count; i++)
                    listGrade[i] = gradeList.ElementAt(i);
                cb_grade.Items.AddRange(listGrade);
            }
            // teacher
            if (singleUpdate == 0 || singleUpdate == 4)
            {
                if (cb_show_subjectteacher.Checked) UpdateAutocompleteTeacher(database.GetTeacherBySubject(cb_subject.Text));
                else UpdateAutocompleteTeacher(database.GetAllTeachers());
            }
            // exam_room & prep_room
            if (singleUpdate == 0 || singleUpdate == 5)
            {
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
            // -- timeline --
            this.panel_time_line.HorizontalScroll.Value = 0;
            Panel panel_tl = new Panel
            {
                Location = new Point(0, panel_top_time.Height + 5 + 85 * time_line_list.Count),
                Name = room,
                Size = new Size(2400, 80),
                BackColor = Colors.TL_TimeLineBg,
                // Dock = DockStyle.Top,  // TODO: TL Dock top ----------------------------------------------
            };
            panel_tl.Width = PixelPerHour * LengthTL; // TEST
            // panel_tl.Width = panel_top_time.Width;
            panel_tl.Paint += panel_time_line_Paint;
            panel_tl.MouseDown += panel_time_line_MouseDown;
            void panel_time_line_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left && editPanel != null)
                {
                    Panel p = sender as Panel;
                    if (EditExamOldTL == p) return;
                    EditExamOldTL = p;
                    cb_exam_room.SelectedItem = p.Name;
                    UpdatePreviewPanel();
                }
            }
            this.panel_time_line.Controls.Add(panel_tl);
            time_line_list.AddLast(panel_tl);
        }
        /// <summary>Checks the entered values ​​and adds an exam to the database</summary>
        private void AddExam()
        {
            if (cb_exam_room.Text.Length < 1) // || cb_preparation_room.Text.Length < 1)
            { MessageBox.Show("Raum fehlt!", "Warnung"); return; }
            string date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            string time = this.dtp_time.Value.ToString("HH:mm");
            string exam_room = cb_exam_room.Text;
            string preparation_room = cb_preparation_room.Text;
            string studentName = cb_student.Text;
            string student2Name = cb_student2.Text;
            string student3Name = cb_student3.Text;
            string grade = null;
            if (cb_grade.SelectedItem != null) grade = cb_grade.SelectedItem.ToString();
            string subject = cb_subject.Text;
            int duration = Int32.Parse(nud_duration.Text);
            // ---- teacher ----
            string teacher1 = null;
            string teacher2 = null;
            string teacher3 = null;
            try
            {
                if (Properties.Settings.Default.NameOrderTeacher)
                {
                    if (cb_teacher1.Text != null && cb_teacher1.Text.Length > 1) teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]).Shortname;
                    if (cb_teacher2.Text != null && cb_teacher2.Text.Length > 1) teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]).Shortname;
                    if (cb_teacher3.Text != null && cb_teacher3.Text.Length > 1) teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]).Shortname;
                }
                else
                {
                    if (cb_teacher1.Text != null && cb_teacher1.Text.Length > 1) teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[1], cb_teacher1.Text.Split(' ')[0]).Shortname;
                    if (cb_teacher2.Text != null && cb_teacher2.Text.Length > 1) teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[1], cb_teacher2.Text.Split(' ')[0]).Shortname;
                    if (cb_teacher3.Text != null && cb_teacher3.Text.Length > 1) teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[1], cb_teacher3.Text.Split(' ')[0]).Shortname;
                }
            }
            catch (Exception) { MessageBox.Show("Fehler beim Lehrernamen!", "Warnung"); return; }
            if (cb_teacher1.Text.Length > 1 && teacher1 == null) { MessageBox.Show("Lehrer 1 nicht gefunden!", "Warnung"); return; }
            if (cb_teacher2.Text.Length > 1 && teacher2 == null) { MessageBox.Show("Lehrer 2 nicht gefunden!", "Warnung"); return; }
            if (cb_teacher3.Text.Length > 1 && teacher3 == null) { MessageBox.Show("Lehrer 3 nicht gefunden!", "Warnung"); return; }
            if (teacher1 == null && teacher2 == null && teacher3 == null)
            {
                DialogResult res = MessageBox.Show("Kein Lehrer!\nPrüfung ohne Lehrer hinzufügen?", "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
            }
            // check if not empty
            if (exam_room.Length == 0)
            { MessageBox.Show("Prüfungsraum fehlt!", "Warnung"); return; }
            if (subject.Length == 0)
            { MessageBox.Show("Fach fehlt!", "Warnung"); return; }
            if (duration == 0)
            { MessageBox.Show("Prüfungsdauer fehlt!", "Warnung"); return; }
            // -------- student --------
            StudentObject student = null;
            StudentObject student2 = null;
            StudentObject student3 = null;
            try
            {
                if (Properties.Settings.Default.NameOrderStudent)
                {
                    if (studentName.Length > 1) student = database.GetStudentByName(studentName.Split(' ')[0], studentName.Split(' ')[1], grade);
                    if (student2Name.Length > 1) student2 = database.GetStudentByName(student2Name.Split(' ')[0], student2Name.Split(' ')[1], grade);
                    if (student3Name.Length > 1) student3 = database.GetStudentByName(student3Name.Split(' ')[0], student3Name.Split(' ')[1], grade);
                }
                else
                {
                    if (studentName.Length > 1) student = database.GetStudentByName(studentName.Split(' ')[1], studentName.Split(' ')[0], grade);
                    if (student2Name.Length > 1) student2 = database.GetStudentByName(student2Name.Split(' ')[1], student2Name.Split(' ')[0], grade);
                    if (student3Name.Length > 1) student3 = database.GetStudentByName(student3Name.Split(' ')[1], student3Name.Split(' ')[0], grade);
                }
            }
            catch (Exception) { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }
            if (studentName.Length > 1 && student == null) { MessageBox.Show("Schüler 1 nicht gefunden!", "Warnung"); return; }
            if (student2Name.Length > 1 && student2 == null) { MessageBox.Show("Schüler 2 nicht gefunden!", "Warnung"); return; }
            if (student3Name.Length > 1 && student3 == null) { MessageBox.Show("Schüler 3 nicht gefunden!", "Warnung"); return; }
            if (student == null && student2 == null && student3 == null)
            {
                DialogResult res = MessageBox.Show("Kein Schüler!\nPrüfung ohne Schüler hinzufügen?", "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
            }
            int[] sIDs = { 0, 0, 0 };
            if (student != null) sIDs[0] = student.Id;
            if (student2 != null) sIDs[1] = student2.Id;
            if (student3 != null) sIDs[2] = student3.Id;
            string msgError = null;
            ExamObject eo = new ExamObject(0, date, time, exam_room, preparation_room, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, subject, duration);
            if (EditExam != null)
            {
                if ((msgError = EditExam.Edit(date, time, exam_room, preparation_room, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, subject, duration)) != null)
                { MessageBox.Show(msgError, "Fehler"); return; }
            }
            else if (EditExam == null && !eo.EditDatabase()) { return; }
            if (EditExam == null) { eo.AddToDatabase(); UpdateTimeline(); }
            if (EditExam != null)
            {
                EditExam.RemoveBorder();
                EditExam.UpdatePanel();
                if (time_line_list.Count(x => x.Name == EditExam.Examroom) == 0) UpdateTimeline();
                else
                    foreach (Panel p in time_line_list)
                        if (p.Name == EditExam.Examroom)
                        {
                            p.Controls.Add(EditExam.GetTimelineEntity());
                            p.Update();
                        }
                EditExam.RemoveBorder();
                EditExam.UpdatePanel();
                editPanel.Dispose();
            }
            EditExam = null;
            // Add / Edit
            /*if (EditExam != null)
                database.EditExam(EditExam.Id, date, time, exam_room, preparation_room, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, subject, duration);
            if (EditExam == null)
                database.AddExam(date, time, exam_room, preparation_room, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, subject, duration);*/
            // Clear
            this.lbl_mode.Text = edit_mode[0];
            this.btn_add_exam.Text = add_mode[0];
            this.dtp_timeline_date.Value = this.dtp_date.Value;
            Properties.Settings.Default.TimelineDate = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
            //UpdateTimeline(); // TODO: ersetzen

            if (this.cb_add_next_time.Checked) { this.dtp_time.Value = this.dtp_time.Value.AddMinutes(Int32.Parse(nud_duration.Text)); }
            if (this.cb_keep_data.Checked)
            {
                if (!Properties.Settings.Default.KeepSubject) cb_subject.Text = null;
                if (!Properties.Settings.Default.KeepExamroom) cb_exam_room.Text = null;
                if (!Properties.Settings.Default.KeepPreparationroom) cb_preparation_room.Text = null;
                if (!Properties.Settings.Default.KeepTeacher) { cb_teacher1.Text = null; cb_teacher2.Text = null; cb_teacher3.Text = null; }
                if (!Properties.Settings.Default.KeepGrade) cb_grade.Text = null;
                if (!Properties.Settings.Default.KeepStudent) { cb_student.Text = null; cb_student2.Text = null; cb_student3.Text = null; }
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
        /// <summary>Updates the timeline for all rooms of the day</summary>
        private void AddMultieditExam()
        {
            int duration = int.Parse(nud_duration.Text);
            string subject = cb_subject.Text; if (subject.Length == 0) subject = null;
            string examRoom = cb_exam_room.Text; if (examRoom.Length == 0) examRoom = null;
            string preparationRoom = cb_preparation_room.Text; if (preparationRoom.Length == 0) preparationRoom = null;
            DateTime dtime = DateTime.ParseExact(tl_entity_multiselect_list.First.Value.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            foreach (ExamObject eo in tl_entity_multiselect_list)
                if (DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None) < dtime)
                    dtime = DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime editTime = DateTime.ParseExact(this.dtp_time.Value.ToString("HH:mm"), "HH:mm", null, System.Globalization.DateTimeStyles.None);
            TimeSpan timeDiff = editTime - dtime;

            foreach (ExamObject eo in tl_entity_multiselect_list)
            {
                DateTime eoTime = DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                if (eoTime.Add(timeDiff).Hour > StartTimeTL + LengthTL) { MessageBox.Show("Zeit nicht zwischen " + StartTimeTL + " - " + StartTimeTL + LengthTL + " Uhr", "Warnung!", MessageBoxButtons.OK); return; }
                if (eoTime.Add(timeDiff).Hour < StartTimeTL) { MessageBox.Show("Zeit nicht zwischen " + StartTimeTL + " - " + StartTimeTL + LengthTL + " Uhr", "Warnung!", MessageBoxButtons.OK); return; }
            }
            bool UpdateTL = false;
            foreach (ExamObject exam in tl_entity_multiselect_list) // TODO first check all then edit db
            {
                // ---- student ----
                int s1 = exam.StudentId;
                int s2 = exam.Student2Id;
                int s3 = exam.Student3Id;
                try
                {
                    if (Properties.Settings.Default.NameOrderTeacher)
                    {
                        if (cb_student.Text != null && cb_student.Text.Length > 1) s1 = database.GetStudentByName(cb_student.Text.Split(' ')[0], cb_student.Text.Split(' ')[1]).Id;
                        if (cb_student2.Text != null && cb_student2.Text.Length > 1) s2 = database.GetStudentByName(cb_student2.Text.Split(' ')[0], cb_student2.Text.Split(' ')[1]).Id;
                        if (cb_student3.Text != null && cb_student3.Text.Length > 1) s3 = database.GetStudentByName(cb_student3.Text.Split(' ')[0], cb_student3.Text.Split(' ')[1]).Id;
                    }
                    else
                    {
                        if (cb_student.Text != null && cb_student.Text.Length > 1) s1 = database.GetStudentByName(cb_student.Text.Split(' ')[1], cb_student.Text.Split(' ')[0]).Id;
                        if (cb_student2.Text != null && cb_student2.Text.Length > 1) s2 = database.GetStudentByName(cb_student2.Text.Split(' ')[1], cb_student2.Text.Split(' ')[0]).Id;
                        if (cb_student3.Text != null && cb_student3.Text.Length > 1) s3 = database.GetStudentByName(cb_student3.Text.Split(' ')[1], cb_student3.Text.Split(' ')[0]).Id;
                    }
                }
                catch (Exception) { MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return; }
                // ---- teacher ----
                string t1 = exam.Teacher1Id;
                string t2 = exam.Teacher2Id;
                string t3 = exam.Teacher3Id;
                try
                {
                    if (Properties.Settings.Default.NameOrderTeacher)
                    {
                        if (cb_teacher1.Text != null && cb_teacher1.Text.Length > 1) t1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]).Shortname;
                        if (cb_teacher2.Text != null && cb_teacher2.Text.Length > 1) t2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]).Shortname;
                        if (cb_teacher3.Text != null && cb_teacher3.Text.Length > 1) t3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]).Shortname;
                    }
                    else
                    {
                        if (cb_teacher1.Text != null && cb_teacher1.Text.Length > 1) t1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[1], cb_teacher1.Text.Split(' ')[0]).Shortname;
                        if (cb_teacher2.Text != null && cb_teacher2.Text.Length > 1) t2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[1], cb_teacher2.Text.Split(' ')[0]).Shortname;
                        if (cb_teacher3.Text != null && cb_teacher3.Text.Length > 1) t3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[1], cb_teacher3.Text.Split(' ')[0]).Shortname;
                    }
                }
                catch (Exception) { MessageBox.Show("Fehler beim Lehrernamen!", "Warnung"); return; }
                if (cb_teacher1.Text.Length > 1 && t1 == null) { MessageBox.Show("Lehrer 1 nicht gefunden!", "Warnung"); return; }
                if (cb_teacher2.Text.Length > 1 && t2 == null) { MessageBox.Show("Lehrer 2 nicht gefunden!", "Warnung"); return; }
                if (cb_teacher3.Text.Length > 1 && t3 == null) { MessageBox.Show("Lehrer 3 nicht gefunden!", "Warnung"); return; }
                if (t1 == null) t1 = exam.Teacher1Id;
                if (t2 == null) t2 = exam.Teacher2Id;
                if (t3 == null) t3 = exam.Teacher3Id;
                // ---- edit ----
                DateTime newTime = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).Add(timeDiff);
                string eTime = newTime.ToString("HH:mm");
                string msgError = exam.Edit(time: eTime, subject: subject, examroom: examRoom, preparationroom: preparationRoom, teacher1: t1, teacher2: t2, teacher3: t3, duration: duration, excludeList: tl_entity_multiselect_list);
                if (msgError != null) { MessageBox.Show(msgError, "Fehler"); break; }
                exam.RemoveBorder();
                exam.UpdatePanel();
                foreach (Panel p in time_line_list)
                    if (database.GetAllExamsAtDate(this.dtp_time.Value.ToString("HH:mm")).Count(x => x.Examroom == p.Name) == 0) UpdateTL = true;
                if (time_line_list.Count(x => x.Name == exam.Examroom) == 0) UpdateTL = true;
                else foreach (Panel p in this.time_line_list)
                        if (p.Name == exam.Examroom)
                        {
                            p.Controls.Add(exam.GetTimelineEntity());
                            p.Update();
                            break;
                        }
            }
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
            tl_entity_multiselect_list.Clear();
            if (UpdateTL) UpdateTimeline();
        }
        public void UpdateTimeline()
        {
            if ((filter == null || filter.Length == 0) && RoomNameFilterList == null) filterMode = Filter.all;
            if (panel_room_bottom != null) panel_side_room.Controls.Remove(panel_room_bottom);
            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_room_list) p.Dispose();
            time_line_list.Clear();
            time_line_room_list.Clear();
            // create new timeline
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            LinkedList<ExamObject> examList = null;
            if (filterMode == Filter.teacher) examList = database.GetAllExamsFromTeacherAtDate(date, filter);
            else if (filterMode == Filter.subject) examList = database.GetAllExamsFromSubjectAtDate(date, filter);
            else if (filterMode == Filter.grade) examList = database.GetAllExamsFromGradeAtDate(date, filter);
            else if (filterMode == Filter.room)
            {
                examList = database.GetAllExamsAtDate(date);
                LinkedList<ExamObject> tempList = new LinkedList<ExamObject>();
                foreach (ExamObject eo in examList)
                    if (RoomNameFilterList.Contains(eo.Examroom))
                        tempList.AddLast(eo);
                examList = tempList;
            }
            else examList = database.GetAllExamsAtDate(date);
            LinkedList<ExamObject> newExams = new LinkedList<ExamObject>();
            LinkedList<ExamObject> removeExams = new LinkedList<ExamObject>();
            LinkedList<ExamObject> updateExamList = new LinkedList<ExamObject>();
            tl_exam_entity_list = examList;
            // ---------- TimeLineEntities ----------
            foreach (ExamObject exam in tl_exam_entity_list)
            {
                // exam borders
                if (search != null)
                {
                    bool border = false;
                    if (searchMode == Search.teacher)
                    { // teacher
                        if (exam.Teacher1Id != null && exam.Teacher1Id.Length > 1 && search.Equals(exam.Teacher1Id)) border = true;
                        if (exam.Teacher2Id != null && exam.Teacher2Id.Length > 1 && search.Equals(exam.Teacher2Id)) border = true;
                        if (exam.Teacher3Id != null && exam.Teacher3Id.Length > 1 && search.Equals(exam.Teacher3Id)) border = true;
                    }
                    if (searchMode == Search.student)
                    { // student
                        if (exam.Student != null && search.Equals(exam.Student.Fullname())) border = true;
                        if (exam.Student2 != null && search.Equals(exam.Student2.Fullname())) border = true;
                        if (exam.Student3 != null && search.Equals(exam.Student3.Fullname())) border = true;
                    }
                    if (searchMode == Search.subject) if (search.Equals(exam.Subject)) border = true;
                    if (searchMode == Search.room) if (search.Equals(exam.Examroom) || search.Equals(exam.Preparationroom)) border = true;
                    if (searchMode == Search.grade) if (search.Equals(exam.Student.Grade)) border = true;
                    if (border) exam.SetBorder(Color.Red, true);
                }
                if (SwapExam != null && SwapExam.Id == exam.Id) exam.SetBorder(Color.Orange, false);
                if (EditExam != null && EditExam.Id == exam.Id) exam.SetBorder(Color.DarkRed, false);
                // exam panel
                exam.PixelPerHour = PixelPerHour;
                Panel panel_tl_entity = exam.GetTimelineEntity();
                panel_tl_entity.MouseClick += panel_tl_entity_click;
                panel_tl_entity.MouseDoubleClick += panel_tl_entity_double_click;
                // context menu
                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuEdit = new ToolStripMenuItem("Bearbeiten");
                ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Kopieren");
                ToolStripMenuItem mnuSwap = new ToolStripMenuItem("Tauschen");
                ToolStripMenuItem mnuDelete = new ToolStripMenuItem("Löschen");
                mnuEdit.Click += new EventHandler(panel_menu_click_edit);
                mnuCopy.Click += new EventHandler(panel_menu_click_copy);
                mnuSwap.Click += new EventHandler(panel_menu_click_swap);
                mnuDelete.Click += new EventHandler(panel_menu_click_delete);
                mnuEdit.Name = exam.Id.ToString();
                mnuCopy.Name = exam.Id.ToString();
                mnuSwap.Name = exam.Id.ToString();
                mnuDelete.Name = exam.Id.ToString();
                mnu.Items.AddRange(new ToolStripItem[] { mnuEdit, mnuCopy, mnuSwap, mnuDelete });
                panel_tl_entity.ContextMenuStrip = mnu;
            }
            UpdateTimeLineEntities();
            // ---- search/filter label ----
            if (search != null && search.Length >= 1) { lbl_search.Text = "Suche:\n" + search; panel_sidetop_empty.BackColor = Color.Yellow; }
            else if (filter != null && filter.Length >= 1) { lbl_search.Text = "Filter:\n" + filter; panel_sidetop_empty.BackColor = Color.Yellow; }
            else { lbl_search.Text = null; panel_sidetop_empty.BackColor = panel_side_room.BackColor; }
            if ((search != null && search.Length >= 1) && (filter != null && filter.Length >= 1)) tooltip_search_filter.SetToolTip(lbl_search, "Suche: " + search + "\nFilter: " + filter);
            else if (search != null && search.Length >= 1) tooltip_search_filter.SetToolTip(lbl_search, "Suche: " + search);
            else if (filter != null && filter.Length >= 1) tooltip_search_filter.SetToolTip(lbl_search, "Filter: " + filter);
        }

        private void UpdateTimeLineEntities()
        {
            LinkedList<string> room_list = new LinkedList<string>();
            foreach (ExamObject s in tl_exam_entity_list)
            { if (!room_list.Contains(s.Examroom)) room_list.AddLast(s.Examroom); }
            List<string> temp_room_list = new List<string>(room_list);
            temp_room_list.Sort();
            room_list = new LinkedList<string>(temp_room_list);
            // TODO: topTimePanel add panel? -> dock top
            foreach (string s in room_list) AddTimeline(s);
            // SideBottomPanel 
            if (panel_room_bottom == null) panel_room_bottom = new Panel();
            panel_room_bottom.Location = new Point(0, panel_top_time.Height + 5 + 85 * time_line_list.Count);
            panel_room_bottom.Size = new Size(panel_side_room.Width - 17, 12);
            panel_side_room.Controls.Add(panel_room_bottom);

            DateTime tlStartTime = DateTime.ParseExact("00:00", "HH:mm", null).AddHours(StartTimeTL);
            DateTime tlEndTime = DateTime.ParseExact("00:00", "HH:mm", null).AddHours(StartTimeTL + LengthTL);
            foreach (ExamObject exam in tl_exam_entity_list)
            {
                DateTime examStartTime = DateTime.ParseExact(exam.Time, "HH:mm", null);
                DateTime examEndTime = DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(exam.Duration);
                if (examStartTime >= tlStartTime && examEndTime <= tlEndTime)
                    foreach (Panel p in time_line_list)
                    {
                        if (p.Name.Equals(exam.Examroom))
                        { p.Controls.Add(exam.GetTimelineEntity()); break; }
                    }
            }
        }
        private void UpdatePreviewPanel()
        {
            if (tl_entity_multiselect_list.Count > 0 || nud_duration.Value < 10) return;
            if (!Properties.Settings.Default.ExamPreview)
            { if (editPanel != null) { editPanel.Dispose(); editPanel = null; } return; }
            if (editPanel != null) editPanel.Dispose();
            string teacher1 = null;
            string teacher2 = null;
            string teacher3 = null;
            try
            {
                if (Properties.Settings.Default.NameOrderStudent)
                {
                    if (cb_teacher1.Text.Length > 1) teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[0], cb_teacher1.Text.Split(' ')[1]).Shortname;
                    if (cb_teacher2.Text.Length > 1) teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[0], cb_teacher2.Text.Split(' ')[1]).Shortname;
                    if (cb_teacher3.Text.Length > 1) teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[0], cb_teacher3.Text.Split(' ')[1]).Shortname;
                }
                else
                {
                    if (cb_teacher1.Text.Length > 1) teacher1 = database.GetTeacherByName(cb_teacher1.Text.Split(' ')[1], cb_teacher1.Text.Split(' ')[0]).Shortname;
                    if (cb_teacher2.Text.Length > 1) teacher2 = database.GetTeacherByName(cb_teacher2.Text.Split(' ')[1], cb_teacher2.Text.Split(' ')[0]).Shortname;
                    if (cb_teacher3.Text.Length > 1) teacher3 = database.GetTeacherByName(cb_teacher3.Text.Split(' ')[1], cb_teacher3.Text.Split(' ')[0]).Shortname;
                }
            }
            catch (Exception) { }
            string studentName = cb_student.Text;
            string student2Name = cb_student2.Text;
            string student3Name = cb_student3.Text;
            string grade = null;
            if (cb_grade.SelectedItem != null) grade = cb_grade.SelectedItem.ToString();
            StudentObject student = null;
            StudentObject student2 = null;
            StudentObject student3 = null;
            try
            {
                if (Properties.Settings.Default.NameOrderStudent)
                {
                    if (studentName.Length > 1) student = database.GetStudentByName(studentName.Split(' ')[0], studentName.Split(' ')[1]);
                    if (student2Name.Length > 1) student2 = database.GetStudentByName(student2Name.Split(' ')[0], student2Name.Split(' ')[1]);
                    if (student3Name.Length > 1) student3 = database.GetStudentByName(student3Name.Split(' ')[0], student3Name.Split(' ')[1]);
                }
                else
                {
                    if (studentName.Length > 1) student = database.GetStudentByName(studentName.Split(' ')[1], studentName.Split(' ')[0]);
                    if (student2Name.Length > 1) student2 = database.GetStudentByName(student2Name.Split(' ')[1], student2Name.Split(' ')[0]);
                    if (student3Name.Length > 1) student3 = database.GetStudentByName(student3Name.Split(' ')[1], student3Name.Split(' ')[0]);
                }
            }
            catch (Exception) { return; }
            int[] sIDs = { 0, 0, 0 };
            if (student != null) sIDs[0] = student.Id;
            if (student2 != null) sIDs[1] = student2.Id;
            if (student3 != null) sIDs[2] = student3.Id;
            ExamObject exam = new ExamObject(0, dtp_date.Text, dtp_time.Text, cb_exam_room.Text, cb_preparation_room.Text, sIDs[0], sIDs[1], sIDs[2], teacher1, teacher2, teacher3, cb_subject.Text, int.Parse(nud_duration.Text));
            string room = cb_exam_room.Text;
            editPanel = exam.GetTimelineEntity(true);
            DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime examTime = DateTime.ParseExact(dtp_time.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
            float unit_per_minute = PixelPerHour / 60F;
            float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;

            foreach (Panel p in time_line_list) // TODO: if no room add new temp timeline
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
            editPanel.MouseDown += editPanel_MouseDown;
            void editPanel_MouseDown(object sender, MouseEventArgs e)
            { EditExamMovePanelOldPos = new Point(e.X, e.Y); }
            void editPanel_MouseMove(object sender, MouseEventArgs e)
            {
                DateTime oldTime = dtp_time.Value;
                if (EditExamMovePanelOldPos == null)
                    EditExamMovePanelOldPos = new Point(e.X, e.Y);
                Panel p = sender as Panel;
                if (e.Button == MouseButtons.Left)  // panel position relative to mouse position (mouse enter -> set start)
                {
                    int dragTime = 15;
                    if (Form.ModifierKeys == Keys.Control) dragTime = 10;
                    if (Form.ModifierKeys == Keys.Shift) dragTime = 5;
                    if (Form.ModifierKeys == Keys.Alt) dragTime = 1;
                    if (e.X - EditExamMovePanelOldPos.X > 2)
                    {
                        this.dtp_time.Value = this.dtp_time.Value.AddMinutes((e.X - EditExamMovePanelOldPos.X) / 4);
                        string time = dtp_time.Value.Hour + ":" + dtp_time.Value.Minute / 10 * 10;
                        this.dtp_time.Value = RoundUp(dtp_time.Value, TimeSpan.FromMinutes(dragTime));
                    }
                    else if (EditExamMovePanelOldPos.X - e.X > 2)
                    {
                        this.dtp_time.Value = this.dtp_time.Value.AddMinutes(-(EditExamMovePanelOldPos.X - e.X) / 4);
                        string time = dtp_time.Value.Hour + ":" + dtp_time.Value.Minute / 10 * 10;
                        this.dtp_time.Value = RoundUp(dtp_time.Value, TimeSpan.FromMinutes(dragTime));
                    }
                    else return;
                    // 2400 / panel_time_line.Width;
                    if (dtp_time.Value != oldTime)
                        UpdatePreviewPanel();
                }
            }

            DateTime RoundUp(DateTime dt, TimeSpan d)
            { return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind); }
        }
        private void UpdateAutocompleteTeacher(LinkedList<TeacherObject> list)
        {
            cb_teacher1.Items.Clear();
            cb_teacher2.Items.Clear();
            cb_teacher3.Items.Clear();
            var autocomplete_teacher = new AutoCompleteStringCollection();
            string[] teacher = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                teacher[i] = list.ElementAt(i).Fullname();
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
                listTeacher[i] = teacherList.ElementAt(i).Fullname();
            cb_teacher2.Items.Add("");
            cb_teacher3.Items.Add("");
            cb_teacher1.Items.AddRange(listTeacher);
            cb_teacher2.Items.AddRange(listTeacher);
            cb_teacher3.Items.AddRange(listTeacher);
        }
        private void UpdateAutocompleteStudent(LinkedList<StudentObject> list)
        {
            LinkedList<StudentObject> allStudentsList = list;
            var autocomplete_student = new AutoCompleteStringCollection();
            if (cb_student_onetime.Checked)
            {
                LinkedList<StudentObject> tempAllStudentsList = list;
                string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
                foreach (StudentObject s in database.GetAllStudents())
                    if (database.GetAllExamsFromStudentAtDate(date, s.Id).Count == 0)
                        tempAllStudentsList.AddLast(s);
                allStudentsList = tempAllStudentsList;
            }
            string[] students = new string[allStudentsList.Count];
            for (int i = 0; i < allStudentsList.Count; i++)
                students[i] = allStudentsList.ElementAt(i).Fullname();
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
                listStudent[i] = studentList.ElementAt(i).Fullname();
            cb_student.Items.Add("");  // TODO student null = "-"
            cb_student2.Items.Add("");
            cb_student3.Items.Add("");
            cb_student.Items.AddRange(listStudent);
            cb_student2.Items.AddRange(listStudent);
            cb_student3.Items.AddRange(listStudent);
        }
        public void SetDate(DateTime date)
        {
            dtp_timeline_date.Value = date;
            Properties.Settings.Default.TimelineDate = date.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }
        #endregion
        #region -------- panel click --------
        private void panel_tl_entity_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                if (Form.ModifierKeys == Keys.Control)
                {
                    lbl_mode.Text = edit_mode[2];
                    btn_add_exam.Text = add_mode[2];
                    ExamObject exam = database.GetExamById(Int32.Parse(p.Name));
                    nud_duration.Text = exam.Duration.ToString();
                    DateTime time = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    if (tl_entity_multiselect_list.Count > 0)
                    {
                        foreach (ExamObject eo in tl_entity_multiselect_list)
                            if (DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None) < time)
                                time = DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    }
                    this.dtp_time.Value = time;
                    foreach (ExamObject eo in tl_exam_entity_list)
                        if (eo.Id == exam.Id) { exam = eo; break; }
                    if (tl_entity_multiselect_list.Contains(exam))
                    {
                        exam.RemoveBorder();
                        exam.UpdatePanel();
                        tl_entity_multiselect_list.Remove(exam);
                        if (tl_entity_multiselect_list.Count == 0)
                        {
                            lbl_mode.Text = edit_mode[0];
                            btn_add_exam.Text = add_mode[0];
                        }
                    }
                    else
                    {
                        tl_entity_multiselect_list.AddLast(exam);
                        exam.SetBorder(Color.Yellow, false);
                        exam.UpdatePanel();
                        panel_time_line.Focus();
                    }
                    ttExamCounter.Show("Ausgewählte Prüfungen: " + tl_entity_multiselect_list.Count().ToString(), this, MousePosition.X, MousePosition.Y - 10, 1000);
                }
            }
        }
        private void panel_tl_entity_double_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                ExamObject texam = database.GetExamById(Int32.Parse(p.Name));
                DialogResult result = MessageBox.Show("Prüfung von Bearbeiten?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    foreach (ExamObject eo in tl_entity_multiselect_list)
                        eo.RemoveBorder();
                    tl_entity_multiselect_list.Clear();
                    ExamObject exam = null;
                    foreach (ExamObject eo in tl_exam_entity_list)
                        if (eo.Id == Int32.Parse(p.Name)) { exam = eo; break; }
                    if (EditExam != null)
                    {
                        EditExam.RemoveBorder();
                        EditExam.UpdatePanel();
                        if (EditExam.Id == exam.Id) { EditExam = null; editPanel.Dispose(); return; }
                    }
                    EditExam = exam;
                    // set texts
                    lbl_mode.Text = edit_mode[1];
                    btn_add_exam.Text = add_mode[1];
                    this.dtp_date.Value = DateTime.ParseExact(exam.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                    this.dtp_time.Value = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    this.cb_exam_room.SelectedItem = exam.Examroom;
                    this.cb_preparation_room.SelectedItem = exam.Preparationroom;
                    this.cb_subject.Text = exam.Subject;
                    this.nud_duration.Text = exam.Duration.ToString();
                    // student cbs
                    if (exam.Student == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
                    else { this.cb_student.Text = exam.Student.Fullname(); this.cb_grade.SelectedItem = exam.Student.Grade; }
                    if (exam.Student2 == null) { this.cb_student2.Text = null; }
                    else { this.cb_student2.Text = exam.Student2.Fullname(); }
                    if (exam.Student3 == null) { this.cb_student3.Text = null; }
                    else { this.cb_student3.Text = exam.Student3.Fullname(); }
                    // teacher cbs
                    if (database.GetTeacherByID(exam.Teacher1Id) == null) this.cb_teacher1.Text = "";
                    else this.cb_teacher1.Text = database.GetTeacherByID(exam.Teacher1Id).Fullname();
                    if (database.GetTeacherByID(exam.Teacher2Id) == null) this.cb_teacher2.Text = "";
                    else this.cb_teacher2.Text = database.GetTeacherByID(exam.Teacher2Id).Fullname();
                    if (database.GetTeacherByID(exam.Teacher3Id) == null) this.cb_teacher3.Text = "";
                    else this.cb_teacher3.Text = database.GetTeacherByID(exam.Teacher3Id).Fullname();

                    EditExam.SetBorder(Color.DarkRed, false);
                    UpdatePreviewPanel();
                    if (cb_show_subjectteacher.Checked)
                    { UpdateAutocompleteTeacher(database.GetTeacherBySubject(exam.Subject)); }
                    else { UpdateAutocompleteTeacher(database.GetAllTeachers()); }
                }
                UpdatePreviewPanel();
                //panel_time_line.Focus(); // TODO: test -------------
                lbl_mode.Focus();

            }
        }
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
            if (exam.Student == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
            else { this.cb_student.Text = exam.Student.Fullname(); this.cb_grade.SelectedItem = exam.Student.Grade; }
            if (exam.Student2 == null) this.cb_student2.Text = null;
            else this.cb_student2.Text = exam.Student2.Fullname();
            if (exam.Student3 == null) this.cb_student3.Text = null;
            else this.cb_student3.Text = exam.Student3.Fullname();
            if (database.GetTeacherByID(exam.Teacher1Id) == null) this.cb_teacher1.Text = "";
            else this.cb_teacher1.Text = database.GetTeacherByID(exam.Teacher1Id).Fullname();
            if (database.GetTeacherByID(exam.Teacher2Id) == null) this.cb_teacher2.Text = "";
            else this.cb_teacher2.Text = database.GetTeacherByID(exam.Teacher2Id).Fullname();
            if (database.GetTeacherByID(exam.Teacher3Id) == null) this.cb_teacher3.Text = "";
            else this.cb_teacher3.Text = database.GetTeacherByID(exam.Teacher3Id).Fullname();
            this.cb_subject.Text = exam.Subject;
            this.nud_duration.Text = exam.Duration.ToString();
            if (EditExam != null)
                EditExam.RemoveBorder();
            EditExam = null;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
            UpdatePreviewPanel();
        }
        private void panel_menu_click_edit(object sender, EventArgs e)
        {
            foreach (ExamObject eo in tl_entity_multiselect_list) eo.RemoveBorder();
            tl_entity_multiselect_list.Clear();
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            ExamObject exam = null;
            foreach (ExamObject eo in tl_exam_entity_list)
                if (eo.Id == Int32.Parse(tsmi.Name)) { exam = eo; break; }
            if (EditExam != null)
            {
                EditExam.RemoveBorder();
                EditExam.UpdatePanel();
                if (EditExam.Id == exam.Id) { EditExam = null; editPanel.Dispose(); return; }
            }
            EditExam = exam;
            // set texts
            lbl_mode.Text = edit_mode[1];
            btn_add_exam.Text = add_mode[1];
            this.dtp_date.Value = DateTime.ParseExact(exam.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
            this.dtp_time.Value = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            this.cb_exam_room.SelectedItem = exam.Examroom;
            this.cb_preparation_room.SelectedItem = exam.Preparationroom;
            this.cb_subject.Text = exam.Subject;
            this.nud_duration.Text = exam.Duration.ToString();
            // student cbs
            if (exam.Student == null) { this.cb_student.Text = null; this.cb_grade.Text = null; }
            else { this.cb_student.Text = exam.Student.Fullname(); this.cb_grade.SelectedItem = exam.Student.Grade; }
            if (exam.Student2 == null) { this.cb_student2.Text = null; }
            else { this.cb_student2.Text = exam.Student2.Fullname(); }
            if (exam.Student3 == null) { this.cb_student3.Text = null; }
            else { this.cb_student3.Text = exam.Student3.Fullname(); }
            // teacher cbs
            if (database.GetTeacherByID(exam.Teacher1Id) == null) this.cb_teacher1.Text = "";
            else this.cb_teacher1.Text = database.GetTeacherByID(exam.Teacher1Id).Fullname();
            if (database.GetTeacherByID(exam.Teacher2Id) == null) this.cb_teacher2.Text = "";
            else this.cb_teacher2.Text = database.GetTeacherByID(exam.Teacher2Id).Fullname();
            if (database.GetTeacherByID(exam.Teacher3Id) == null) this.cb_teacher3.Text = "";
            else this.cb_teacher3.Text = database.GetTeacherByID(exam.Teacher3Id).Fullname();

            EditExam.SetBorder(Color.DarkRed, false);
            UpdatePreviewPanel();
            if (cb_show_subjectteacher.Checked)
            { UpdateAutocompleteTeacher(database.GetTeacherBySubject(exam.Subject)); }
            else { UpdateAutocompleteTeacher(database.GetAllTeachers()); }
            lbl_mode.Focus();
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
                    { exam.SetBorder(Color.Orange, false); exam.Panel.Refresh(); break; }
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
                    DateTime dt1 = DateTime.ParseExact(e2.Date, "yyyy-MM-dd", null);
                    DateTime dt2 = DateTime.ParseExact(e1.Date, "yyyy-MM-dd", null);
                    database.EditExam(e1.Id, date: dt1.ToString("yyyy-MM-dd"), time: e2.Time, exam_room: e2.Examroom, preparation_room: e2.Preparationroom);
                    database.EditExam(e2.Id, date: dt2.ToString("yyyy-MM-dd"), time: e1.Time, exam_room: e1.Examroom, preparation_room: e1.Preparationroom);
                    SwapExam = null;
                    UpdateTimeline();// render on examswap
                }
                else { SwapExam.RemoveBorder(); SwapExam = null; }
            }
        }
        private void panel_menu_click_delete(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            ExamObject exam = database.GetExamById(Int32.Parse(tsmi.Name));
            DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //exam.Panel.Dispose();
                exam.Delete();
                UpdateTimeline(); //render on examDelete // -> remove empty TimeLine
            }
        }
        #endregion
        #region -------- PAINT --------
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void panel_side_room_Paint(object sender, PaintEventArgs e)
        {
            if (panel_time_line.AutoScrollPosition != SyncScrollPos1)
            {
                panel_side_room.AutoScrollPosition = new Point(-panel_time_line.AutoScrollPosition.X, -panel_time_line.AutoScrollPosition.Y);
                SyncScrollPos1 = panel_side_room.AutoScrollPosition;
            }
            else if (panel_side_room.AutoScrollPosition != SyncScrollPos2)
            {
                panel_time_line.AutoScrollPosition = new Point(-panel_side_room.AutoScrollPosition.X, -panel_side_room.AutoScrollPosition.Y);
                SyncScrollPos2 = panel_side_room.AutoScrollPosition;
            }
        }
        private void panel_time_line_master_Paint(object sender, PaintEventArgs e)
        {
            if (panel_time_line.AutoScrollPosition != SyncScrollPos1)
            {
                panel_side_room.AutoScrollPosition = new Point(-panel_time_line.AutoScrollPosition.X, -panel_time_line.AutoScrollPosition.Y);
                SyncScrollPos1 = panel_side_room.AutoScrollPosition;
            }
            else if (panel_side_room.AutoScrollPosition != SyncScrollPos2)
            {
                panel_time_line.AutoScrollPosition = new Point(-panel_side_room.AutoScrollPosition.X, -panel_side_room.AutoScrollPosition.Y);
                SyncScrollPos2 = panel_side_room.AutoScrollPosition;
            }
        }
        private void panel_top_time_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            Color c = Colors.TL_TimeBorder;
            byte b = 4; // border
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid,
            c, 3, ButtonBorderStyle.Solid);
            Font drawFont = new Font("Microsoft Sans Serif", 10);
            StringFormat drawFormat = new StringFormat();
            for (int i = 0; i < LengthTL; i++)
            {
                float[] dashValues = { 1, 1 };
                Pen pen = new Pen(Colors.TL_MinLine, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Colors.TL_MinLine, 2);
                pen2.DashPattern = dashValues;
                e.Graphics.DrawLine(new Pen(Colors.TL_MinLine, 2), b + panel_tl.Width / LengthTL * i, b, b + panel_tl.Width / LengthTL * i, panel_tl.Height - b);
                e.Graphics.DrawLine(pen2, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), b, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), panel_tl.Height - b);
                e.Graphics.DrawLine(pen, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), b, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), panel_tl.Height - b);
                e.Graphics.DrawLine(pen, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, b, b + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, panel_tl.Height - b);
                e.Graphics.DrawString(StartTimeTL + i + " Uhr", drawFont, new SolidBrush(Colors.TL_MinLine), 5 + panel_tl.Width / LengthTL * i, panel_tl.Height - 20, drawFormat);
            }
        }
        private void panel_time_line_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl = sender as Panel;
            for (int i = 0; i < LengthTL; i++)
            {
                float[] dashValues = { 2, 2 };
                float[] dashValues2 = { 1, 1 };
                Pen pen = new Pen(Colors.TL_MinLine, 1);
                pen.DashPattern = dashValues;
                Pen pen2 = new Pen(Colors.TL_MinLine, 2);
                pen2.DashPattern = dashValues2;
                e.Graphics.DrawLine(new Pen(Colors.TL_MinLine, 2), 4 + panel_tl.Width / LengthTL * i, 4, 4 + panel_tl.Width / LengthTL * i, panel_tl.Height - 4);
                e.Graphics.DrawLine(pen2, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), 4, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 2), panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), 4, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4), panel_tl.Height - 4);
                e.Graphics.DrawLine(pen, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, 4, 4 + panel_tl.Width / LengthTL * i + panel_tl.Width / (LengthTL * 4) * 3, panel_tl.Height - 4);
            }
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid,
            Colors.TL_TimeLineBorder, 4, ButtonBorderStyle.Solid);
        }
        #endregion
        #region -------- BTNs --------
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////    
        private void btn_add_exam_Click(object sender, EventArgs e)
        {
            if (tl_entity_multiselect_list.Count >= 1)
            {
                AddMultieditExam();
            }
            else
                AddExam();
        }
        private void btn_delete_exam_Click(object sender, EventArgs e)
        {
            if (EditExam.Id != 0)
            {
                DialogResult result = MessageBox.Show("Prüfung löschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    EditExam.Delete();
                    EditExam = null;
                    lbl_mode.Text = edit_mode[0];
                    btn_add_exam.Text = add_mode[0];
                    UpdateTimeline(); //render on ExamDelete // TODO no update -> delete only
                    if (this.cb_keep_data.Checked)
                    {
                        if (!Properties.Settings.Default.KeepSubject) cb_subject.Text = null;
                        if (!Properties.Settings.Default.KeepExamroom) cb_exam_room.Text = null;
                        if (!Properties.Settings.Default.KeepPreparationroom) cb_preparation_room.Text = null;
                        if (!Properties.Settings.Default.KeepTeacher) { cb_teacher1.Text = null; cb_teacher2.Text = null; cb_teacher3.Text = null; }
                        if (!Properties.Settings.Default.KeepGrade) cb_grade.Text = null;
                        if (!Properties.Settings.Default.KeepStudent) { cb_student.Text = null; cb_student2.Text = null; cb_student3.Text = null; }
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
                EditExam.RemoveBorder();
                EditExam = null;
                lbl_mode.Text = edit_mode[0];
                btn_add_exam.Text = add_mode[0];
            }
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            foreach (ExamObject exam in tl_entity_multiselect_list)
            {
                exam.RemoveBorder();
                exam.UpdatePanel();
            }
            tl_entity_multiselect_list.Clear();
            if (EditExam != null)
                EditExam.RemoveBorder();
            EditExam = null;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[1];
            this.cb_exam_room.Text = null;
            this.cb_preparation_room.Text = null;
            this.cb_student.Text = null;
            this.cb_grade.Text = null;
            this.cb_subject.Text = null;
            this.cb_teacher1.Text = null;
            this.cb_teacher2.Text = null;
            this.cb_teacher3.Text = null;
        }
        #endregion
        #region -------- TSMI --------
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                UpdateTimeline(); // render on editgrademove
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
                UpdateTimeline(); // render on editgradedelete
                UpdateAutocomplete();
            }
            form.FormClosed += update_Event;
            form.ShowDialog();
            // remove exams in grade?
        }
        // ----------------- tsmi search -----------------
        private void update_search_Event(object sender, EventArgs e)
        {
            string s = sender as string;
            search = s;
            UpdateTimeline(); // render on updatesearch
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
            UpdateTimeline(); // render on searchchange to all
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
            RoomNameFilterList = null;
            filter = null;
            UpdateTimeline();// render on filterchange to all
        }
        private void tsmi_filter_teacher_Click(object sender, EventArgs e)
        {
            filterMode = Filter.teacher;
            FormSearch form = new FormSearch(0);
            form.UpdateSearch += update_filter_Event;
            form.ShowDialog();
        }
        private void tsmi_filter_subject_Click(object sender, EventArgs e)
        {
            filterMode = Filter.subject;
            FormSearch form = new FormSearch(2);
            form.UpdateSearch += update_filter_Event;
            form.ShowDialog();
        }
        private void tsmi_filter_room_Click(object sender, EventArgs e)
        {
            LinkedList<string> roomNameList = new LinkedList<string>();
            foreach (Panel p in time_line_room_list)
                roomNameList.AddLast(p.Name);
            FormRoomFilter form = new FormRoomFilter(roomNameList);
            form.SelectedRoomList += roomList_Event;
            form.ShowDialog();

            void roomList_Event(object sender1, EventArgs a)
            {
                LinkedList<string> list = sender1 as LinkedList<string>;
                filterMode = Filter.room;
                RoomNameFilterList = list;
                UpdateTimeline();
            }
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
        private void tsmi_exam_delete_examday_Click(object sender, EventArgs e)
        {
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            DialogResult result = MessageBox.Show("Alle " + database.GetAllExamsAtDate(date).Count() + " Prüfungen am " + date + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) { foreach (ExamObject eo in database.GetAllExamsAtDate(date)) eo.Delete(); UpdateTimeline(); }
            else return;
        }
        private void tsmi_tools_deleteoedexams_Click(object sender, EventArgs e)
        {
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            DialogResult result = MessageBox.Show("Alle " + database.GetAllExamsBeforeDate(date).Count() + " Prüfungen vor dem " + date + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) database.DeleteOldExams(date);
            else return;
        }
        // ----------------- tsmi tools -----------------
        private void tsmi_tools_export_Click(object sender, EventArgs e)
        {
            bool split = false;
            DialogResult resultSplit = MessageBox.Show("Zeitachse teilen?", "Achtung", MessageBoxButtons.YesNoCancel);
            if (resultSplit == DialogResult.Yes)
            {
                split = true;
                if (time_line_room_list.Count > 10)
                {
                    MessageBox.Show("Räume auswählen", "Info");
                    LinkedList<string> roomNameList = new LinkedList<string>();
                    foreach (Panel p in time_line_room_list)
                        roomNameList.AddLast(p.Name);
                    FormRoomFilter form = new FormRoomFilter(roomNameList);
                    form.SelectedRoomList += roomList_Event;
                    form.ShowDialog();

                    void roomList_Event(object sender1, EventArgs a)
                    {
                        LinkedList<string> list = sender1 as LinkedList<string>;
                        while (list.Count > 10) list.RemoveLast();
                        filterMode = Filter.room;
                        RoomNameFilterList = list;
                        UpdateTimeline();
                    }
                }
            }
            if (resultSplit == DialogResult.Cancel) { return; }
            Colors.Theme tempTheme = Colors.theme;
            bool blackwhite = false;
            DialogResult result = MessageBox.Show("Zeitstrahl in schwarz-weiß exportieren?", "Achtung!", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes) { blackwhite = true; }
            if (result == DialogResult.Cancel) { return; }
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");

            DateTime lastTime = DateTime.ParseExact("07:00", "HH:mm", null);
            foreach (ExamObject exam in tl_exam_entity_list)
            {
                if (lastTime < DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(exam.Duration))
                    lastTime = DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(exam.Duration);
            }
            // TODO if Height<Width || Width<Height
            Double cutTime = (DateTime.ParseExact("18:30", "HH:mm", null) - lastTime).TotalMinutes;
            float unit_per_minute = PixelPerHour / 60F;
            int cutMinutes = Convert.ToInt32(cutTime * unit_per_minute);

            float fullWidth = panel_side_room.Width + panel_top_time.Width;
            float fullHeight = fullWidth / 297f * 210f;
            float BmpWidth = (panel_side_room.Width + panel_top_time.Width - cutMinutes);
            float BmpHeight = BmpWidth / 297f * 210f;
            if (blackwhite)
            {
                Colors.ColorTheme(Colors.Theme.bw);
                UpdateTimeline(); // render on export colorchange
                panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
                panel_time_line.BackColor = Colors.TL_Bg;
                panel_top_time.BackColor = Colors.TL_TimeBg;
                panel_side_room.BackColor = Colors.TL_RoomBg;
            }
            lbl_search.Text = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            Panel tempPanel = new Panel
            { Width = Convert.ToInt32(fullWidth), Height = Convert.ToInt32(fullHeight), BackColor = Color.White };
            tempPanel.Controls.Add(tlp_timeline_view);
            Bitmap bmp = new Bitmap(Convert.ToInt32(BmpWidth), Convert.ToInt32(BmpHeight)); // -20
            tempPanel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            // TODO: check if Height<Width || Width<Height

            Panel tPanelRoom = new Panel { Width = 120, Height = panel_side_room.Height, BackColor = Color.White };
            Panel tPanelTL = new Panel { Width = panel_top_time.Width, Height = panel_side_room.Height, BackColor = Color.White };
            tPanelRoom.Controls.Add(panel_side_room);
            tPanelTL.Controls.Add(panel_time_line);
            Bitmap bmpRoom = new Bitmap(120, panel_side_room.Height);
            Bitmap bmpTL = new Bitmap(LengthTL * PixelPerHour, panel_side_room.Height);
            tPanelRoom.DrawToBitmap(bmpRoom, new Rectangle(0, 0, bmpRoom.Width, bmpRoom.Height));
            tPanelTL.DrawToBitmap(bmpTL, new Rectangle(0, 0, bmpTL.Width, bmpTL.Height));

            if (!split)
            {
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
                if (sfd.ShowDialog() == DialogResult.OK)
                    bmp.Save(sfd.FileName, ImageFormat.Png);
            }
            if (split)
            {
                bmpTL = bmpTL.Clone(new Rectangle(0, 0, bmpTL.Width, bmpTL.Height), PixelFormat.DontCare);
                Bitmap newTLbmp = new Bitmap(120 + (LengthTL * PixelPerHour) / 2 + 5, panel_side_room.Height);
                Graphics g = Graphics.FromImage(newTLbmp);
                g.DrawImage(bmpRoom, new Rectangle(0, 0, 120, newTLbmp.Height));
                g.DrawImage(bmpTL, new Rectangle(120, 0, LengthTL * PixelPerHour, newTLbmp.Height));
                Bitmap bmpPart1 = ImageToDinA4(newTLbmp);

                bmpTL = bmpTL.Clone(new Rectangle((LengthTL * PixelPerHour) / 2 + 3, 0, bmpTL.Width - ((LengthTL * PixelPerHour) / 2 + 3), bmpTL.Height), PixelFormat.DontCare);
                newTLbmp = new Bitmap(120 + (LengthTL * PixelPerHour) / 2, panel_side_room.Height);
                g = Graphics.FromImage(newTLbmp);
                g.DrawImage(bmpRoom, new Rectangle(0, 0, 120, newTLbmp.Height));
                g.DrawImage(bmpTL, new Rectangle(120, 0, (LengthTL * PixelPerHour) / 2, newTLbmp.Height));
                Bitmap bmpPart2 = ImageToDinA4(newTLbmp);
                SaveFileDialog sfd = new SaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Title = "Save Timeline",
                    FileName = "Prüfungen-" + date + "_Part1.png",
                    DefaultExt = "png",
                    Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                    bmpPart1.Save(sfd.FileName, ImageFormat.Png);
                SaveFileDialog sfd2 = new SaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Title = "Save Timeline",
                    FileName = "Prüfungen-" + date + "_Part2.png",
                    DefaultExt = "png",
                    Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };
                if (sfd2.ShowDialog() == DialogResult.OK)
                    bmpPart2.Save(sfd2.FileName, ImageFormat.Png);
            }

            // restore 
            this.tlp_timeline_view.Controls.Add(panel_side_room);
            this.tlp_timeline_view.Controls.Add(panel_time_line);
            this.tlp_main.Controls.Add(this.tlp_timeline_view, 0, 1);
            lbl_search.Text = null;
            filterMode = Filter.all;
            RoomNameFilterList = null;
            Colors.ColorTheme(tempTheme);
            UpdateTimeline(); // render on export colorchange
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
            Bitmap ImageToDinA4(Bitmap bmp1)
            {
                float fullWidth1 = panel_side_room.Width + panel_top_time.Width / 2;
                float fullHeight1 = fullWidth1 / 297f * 210f;
                Bitmap newTLbmp1 = new Bitmap(Convert.ToInt32(fullWidth1), Convert.ToInt32(fullHeight1));
                Graphics g1 = Graphics.FromImage(newTLbmp1);
                g1.DrawImage(bmp1, new Rectangle(0, 0, Convert.ToInt32(fullWidth1), panel_side_room.Height));
                return newTLbmp1;
            }
        }
        private void tsmi_tools_deleteOldExams_Click(object sender, EventArgs e)
        {
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            DialogResult result = MessageBox.Show("Alle " + database.GetAllExamsBeforeDate(date).Count() + " Prüfungen vor dem " + date + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) { database.DeleteOldExams(date); UpdateTimeline(); }
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
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", teacher.Firstname + " " + teacher.Lastname, time, exam.Examroom, exam.Preparationroom, student.Firstname + " " + student.Lastname, exam.Teacher1Id, exam.Teacher2Id, exam.Teacher3Id, exam.Subject, exam.Duration);
                        csv.AppendLine(newLine); // TODO: fullname() ?
                    }
                File.WriteAllText(sfd.FileName, csv.ToString());
            }
        }
        private void tsmi_tools_import_export_Click(object sender, EventArgs e)
        {
            new FormImportExport().ShowDialog();
        }
        private void tsmi_tools_open_excel_Click(object sender, EventArgs e)
        {
            FormImportExport form = new FormImportExport(2);
            form.FormClosed += update_timeline_Event;
            form.ShowDialog();
            /*FormLoadTable form = new FormLoadTable();
            form.FormClosed += update_timeline_Event;
            form.ShowDialog();*/
        }
        private void tsmi_tools_sendemail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Testphase!\nEmails werden an den Absender gesendet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormEmail form = new FormEmail();
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");

            LinkedList<string> teacherList = new LinkedList<string>();
            foreach (ExamObject exam in database.GetAllExamsAtDate(date))
            {
                if (exam.Teacher1Id != null && !teacherList.Contains(exam.Teacher1Id)) teacherList.AddLast(exam.Teacher1Id);
                if (exam.Teacher2Id != null && !teacherList.Contains(exam.Teacher2Id)) teacherList.AddLast(exam.Teacher2Id);
                if (exam.Teacher3Id != null && !teacherList.Contains(exam.Teacher3Id)) teacherList.AddLast(exam.Teacher3Id);
            }
            LinkedList<TeacherObject> tList = new LinkedList<TeacherObject>();
            foreach (string s in teacherList)
                if (database.GetTeacherByID(s) != null) tList.AddLast(database.GetTeacherByID(s));
            form.SetReceivers(tList, date);
            form.ShowDialog();
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
        // ----------------- tsmi options -----------------
        private void tsmi_options_keepdata_Click(object sender, EventArgs e)
        {
            new KeepDataForm().ShowDialog();
        }
        private void tsmi_options_settings_Click(object sender, EventArgs e)
        {
            FormSettings form = new FormSettings();
            form.UpdateColor += update_tl_settings_Event;
            form.ShowDialog(this);
        }
        #endregion
        #region -------- events --------
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;

            panel_top_time.Width = PixelPerHour * LengthTL;
            PixelPerHour = panel_top_time.Width / LengthTL;
            UpdateTimeline();

            cb_keep_data.Checked = Properties.Settings.Default.AddOptionKeepData;
            cb_add_next_time.Checked = Properties.Settings.Default.AddOptionAddNextTime;
            cb_show_subjectteacher.Checked = Properties.Settings.Default.AddOptionShowSubjectteacher;
            cb_student_onetime.Checked = Properties.Settings.Default.AddOptionStudentOnetime;
        }
        private void update_autocomplete_Event(object sender, EventArgs a)
        {
            UpdateAutocomplete();
            Properties.Settings.Default.AddOptionKeepData = cb_keep_data.Checked;
            Properties.Settings.Default.Save();
        }
        private void update_timeline_Event(object sender, EventArgs a)
        {
            UpdateTimeline(); // render on UpdateEvent(changeExamRoom)
        }
        private void update_filter_Event(object sender, EventArgs e)
        {
            string s = sender as string;
            filter = s;
            UpdateTimeline(); // render on filterchange
        }
        private void update_tl_settings_Event(object sender, EventArgs a)
        {
            PixelPerHour = Properties.Settings.Default.PixelPerHour;
            StartTimeTL = Properties.Settings.Default.TLStartTime;
            LengthTL = Properties.Settings.Default.TLLength;
            panel_top_time.Width = Properties.Settings.Default.PixelPerHour * LengthTL;
            panel_time_line.Refresh();
            while (LengthTL * PixelPerHour < panel_time_line.Width)
            { PixelPerHour += 10; }
            Properties.Settings.Default.PixelPerHour = PixelPerHour;
            UpdateTimeline();
            panel_sidetop_empty.BackColor = Colors.TL_RoomBg;
            panel_time_line.BackColor = Colors.TL_Bg;
            panel_top_time.BackColor = Colors.TL_TimeBg;
            panel_side_room.BackColor = Colors.TL_RoomBg;
            tlp_edit.BackColor = Colors.Edit_Bg;
            lbl_mode.BackColor = Colors.Edit_ModeBg;
            UpdateAutocomplete();
        }
        private void nud_duration_SizeChanged(object sender, EventArgs e)
        {
            cb_add_next_time.Text = "Nächste + " + this.nud_duration.Text + "min";
        }
        private void cb_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_grade.SelectedItem != null && cb_grade.SelectedItem.ToString() != null)
                UpdateAutocompleteStudent(database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString()));
            else UpdateAutocompleteStudent(database.GetAllStudents());
        }
        private void cb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_show_subjectteacher.Checked)
                UpdateAutocompleteTeacher(database.GetTeacherBySubject(cb_subject.Text));
        }
        private void cb_show_subjectteacher_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_show_subjectteacher.Checked)
                UpdateAutocompleteTeacher(database.GetTeacherBySubject(cb_subject.Text));
            else UpdateAutocompleteTeacher(database.GetAllTeachers());
            Properties.Settings.Default.AddOptionShowSubjectteacher = cb_show_subjectteacher.Checked;
            Properties.Settings.Default.Save();
        }
        private void dtp_timeline_date_ValueChanged(object sender, EventArgs e)
        {
            UpdateTimeline();// render on daychange
            Properties.Settings.Default.TimelineDate = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy");
            Properties.Settings.Default.Save();
        }
        private void cb_exam_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewPanel();
        }
        private void dtp_time_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreviewPanel();
        }
        #endregion
        private void panel_time_line_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // TODO: Single Edit keys !!!
            //Console.WriteLine(e.KeyData);
            if (tl_entity_multiselect_list.Count == 0) return;
            if (e.KeyData == Keys.Enter)
            { if (tl_entity_multiselect_list.Count >= 1) AddMultieditExam(); else AddExam(); }

            else if (e.KeyData == Keys.Right || e.KeyData == Keys.Left || e.KeyData == Keys.Up || e.KeyData == Keys.Down)
            {
                bool updateTL = false;
                LinkedList<ExamObject> selectedExams = new LinkedList<ExamObject>(tl_entity_multiselect_list);
                List<ExamObject> tempList = new List<ExamObject>(selectedExams);
                tempList = tempList.OrderBy(x => x.Time).ToList();
                selectedExams = new LinkedList<ExamObject>(tempList);
                foreach (ExamObject exam in selectedExams)
                {
                    DateTime dtime = DateTime.ParseExact(exam.Time, "HH:mm", null);
                    if (e.KeyData == Keys.Right) dtime = DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(15);
                    if (e.KeyData == Keys.Left) dtime = DateTime.ParseExact(exam.Time, "HH:mm", null).AddMinutes(-15);
                    if (e.KeyData == Keys.Up)
                    {
                        if (time_line_room_list.First().Name == exam.Subject) return;
                        string room_up = null;
                        foreach (Panel room in time_line_room_list)
                        {
                            if (room.Name == exam.Examroom) break;
                            room_up = room.Name;
                        }
                        if (room_up != null)
                        {
                            exam.Edit(examroom: room_up);
                            updateTL = true;
                        }
                    }
                    if (e.KeyData == Keys.Down)
                    {
                        if (time_line_room_list.First().Name == exam.Subject) return;
                        string room_down = null;
                        for (int i = 0; i < time_line_room_list.Count - 1; i++) // not last
                        {
                            if (time_line_room_list.ElementAt(i).Name == exam.Examroom) { room_down = time_line_room_list.ElementAt(i + 1).Name; break; }
                        }
                        if (room_down != null)
                        {
                            exam.Edit(examroom: room_down);
                            updateTL = true;
                        }
                    }
                    string time = dtime.ToString("HH:mm");
                    if (updateTL) UpdateTimeline(); // render on multimove up/down
                    if (exam.Edit(time: dtime.ToString("HH:mm"), excludeList: tl_entity_multiselect_list) != null) break;
                    exam.UpdatePanel();
                }
                if (tl_entity_multiselect_list.Count > 0)
                {
                    DateTime ttime = DateTime.ParseExact(tl_entity_multiselect_list.ElementAt(0).Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    foreach (ExamObject eo in tl_entity_multiselect_list)
                        if (DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None) < ttime)
                            ttime = DateTime.ParseExact(eo.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    this.dtp_time.Value = ttime;
                }
            }
        }

        private bool ExportCsvFile(string file, string date, LinkedList<string> teacherShortnameList)
        {
            FormProgressBar bar = new FormProgressBar();
            bar.Show();
            bar.StartPrograssBar(1, database.GetAllExamsAtDate(date).Count);
            var csv = new StringBuilder();
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach", "Dauer");
            csv.AppendLine(firstLine);
            if (teacherShortnameList == null || teacherShortnameList.Count == 0) return false;
            foreach (string teacher in teacherShortnameList)
                foreach (ExamObject exam in database.GetAllExamsFromTeacherAtDate(date, teacher))
                {
                    bar.AddOne();
                    StudentObject student = exam.Student;
                    DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                    string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", teacher.Replace("*", ""), time, exam.Examroom, exam.Preparationroom, student.Firstname + " " + student.Lastname, exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                    if (exam.Teacher1 != null)
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", exam.Teacher1.Fullname(), time, exam.Examroom, exam.Preparationroom, student.Firstname + " " + student.Lastname, exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                    csv.AppendLine(newLine);
                }
            File.WriteAllText(file, csv.ToString());
            return true;
        }

        /*private void SendEmail()
        {
            LinkedList<string> files = new LinkedList<string>();
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            string fileExamDay = " Prüfungstag_" + date + ".csv";
            LinkedList<string> list = new LinkedList<string>();
            foreach (ExamObject exam in database.GetAllExamsAtDate(date))
            {
                if (exam.Teacher1Id != null && !list.Contains(exam.Teacher1Id)) list.AddLast(exam.Teacher1Id);
                if (exam.Teacher2Id != null && !list.Contains(exam.Teacher2Id)) list.AddLast(exam.Teacher2Id);
                if (exam.Teacher3Id != null && !list.Contains(exam.Teacher3Id)) list.AddLast(exam.Teacher3Id);
            }
            ExportCsvFile(fileExamDay, date, list);
            files.AddLast(fileExamDay);

            // email text
            string examstring = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy") + "\n";
            foreach (ExamObject exam in database.GetAllExamsFromTeacherAtDate(date, list.First.Value))
            {
                DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                examstring += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                examstring += "\n";
            }
            // ---- send email ----
            try
            {
                SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.SMTP_server)
                {
                    Port = int.Parse(Properties.Settings.Default.SMTP_port),
                    Credentials = new NetworkCredential(Properties.Settings.Default.SMTP_email, Properties.Settings.Default.SMTP_password),
                    EnableSsl = true,
                };
                string senderName = Properties.Settings.Default.SMTP_email_name;
                string mail_title = Properties.Settings.Default.SMTP_email_title;
                string mail_receiver = Properties.Settings.Default.SMTP_email; // TODO: change to teacheremail 
                string mail_text = "Prüfungen " + examstring; // TODO: change email text
                if (mail_receiver.Length == 0 || senderName.Length == 0 || mail_title.Length == 0 || mail_text.Length == 0) { MessageBox.Show("alle Daten ausfüllen!", "Achtung"); return; }
                try
                {
                    MailAddress from = new MailAddress(Properties.Settings.Default.SMTP_email, senderName);
                    MailAddress to = new MailAddress(mail_receiver);
                    MailMessage message = new MailMessage(from, to)
                    { Subject = mail_title, Body = mail_text };
                    if (files.Count > 0)
                        foreach (string f in files)
                        {
                            Attachment data = new Attachment(f, MediaTypeNames.Application.Octet);
                            message.Attachments.Add(data);
                        }
                    smtpClient.Send(message);
                    MessageBox.Show("Email gesendet!", "Mitteilung");
                }
                catch (Exception) { MessageBox.Show("Fehler beim Senden", "Fehler"); return; }
            }
            catch (Exception) { MessageBox.Show("Fehler bei der Email", "Fehler"); }
        }
        private void SendTeacherEmails()
        {
            int errorCounter = 0;
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            LinkedList<string> files = new LinkedList<string>();
            string fileExamDay = Path.GetTempPath() + "\\Prüfungstag_" + date + ".csv";

            FormProgressBar bar = new FormProgressBar();
            bar.Show();
            // ---- CSV ----
            var csv = new StringBuilder();
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach", "Dauer");
            csv.AppendLine(firstLine);
            LinkedList<string> teacherList = new LinkedList<string>();
            foreach (ExamObject exam in database.GetAllExamsAtDate(date))
            {
                if (exam.Teacher1Id != null && !teacherList.Contains(exam.Teacher1Id)) teacherList.AddLast(exam.Teacher1Id);
                if (exam.Teacher2Id != null && !teacherList.Contains(exam.Teacher2Id)) teacherList.AddLast(exam.Teacher2Id);
                if (exam.Teacher3Id != null && !teacherList.Contains(exam.Teacher3Id)) teacherList.AddLast(exam.Teacher3Id);
            }
            bar.StartPrograssBar(1, teacherList.Count * 2);
            foreach (string teacher in teacherList)
            {
                foreach (ExamObject exam in database.GetAllExamsFromTeacherAtDate(date, teacher))
                {
                    DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                    string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", teacher.Replace("*", ""), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                    if (exam.Teacher1 != null)
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", exam.Teacher1.Fullname(), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                    csv.AppendLine(newLine);
                }
                bar.AddOne();
            }
            File.WriteAllText(fileExamDay, csv.ToString());
            // ---- Email ----
            files.AddLast(fileExamDay);
            bool emails = true;
            foreach (string to in teacherList)
            {
                TeacherObject teacher = database.GetTeacherByID(to);
                if (teacher.Email == null || teacher.Email.Length < 2)
                    emails = false;
            }
            if (!emails) { MessageBox.Show("fehlende Email", "Achtung"); return; }

            Console.WriteLine("sending " + teacherList.Count + " Emails");
            foreach (string to in teacherList)
            {
                files.Clear();
                files.AddLast(fileExamDay);
                bar.AddOne();
                if (database.GetTeacherByID(to) != null)
                {
                    //////////////////////////////////

                    var csv1 = new StringBuilder();
                    var firstLine1 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach", "Dauer");
                    csv1.AppendLine(firstLine1);
                    foreach (ExamObject exam in database.GetAllExamsFromTeacherAtDate(date, to))
                    {
                        DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                        DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                        string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", to.Replace("*", ""), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                        if (exam.Teacher1 != null)
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", exam.Teacher1.Fullname(), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                        csv1.AppendLine(newLine);
                    }
                    string tempTeacherFile = Path.GetTempPath() + "\\Prüfungstag_" + date + "_" + to + ".csv"; ;
                    File.WriteAllText(tempTeacherFile, csv1.ToString());
                    files.AddLast(tempTeacherFile);

                    //////////////////////////////////

                    TeacherObject teacher = database.GetTeacherByID(to);
                    // email text
                    string examstring = this.dtp_timeline_date.Value.ToString("dd.MM.yyyy") + "\n";
                    foreach (ExamObject exam in database.GetAllExamsFromTeacherAtDate(date, teacher.Shortname))
                    {
                        DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                        DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                        string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                        examstring += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                        examstring += "\n";
                    }
                    // ---- send email ----
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.SMTP_server)
                        {
                            Port = int.Parse(Properties.Settings.Default.SMTP_port),
                            Credentials = new NetworkCredential(Properties.Settings.Default.SMTP_email, Properties.Settings.Default.SMTP_password),
                            EnableSsl = true,
                        };
                        string senderName = Properties.Settings.Default.SMTP_email_name;
                        string mail_title = Properties.Settings.Default.SMTP_email_title;
                        string mail_receiver = Properties.Settings.Default.SMTP_email;  // TODO: change to var ----------------------------------------------
                        string mail_text = "Prüfungen " + examstring; // TODO: change email text
                        if (mail_receiver.Length == 0 || senderName.Length == 0 || mail_title.Length == 0 || mail_text.Length == 0) { MessageBox.Show("alle Daten ausfüllen!", "Achtung"); return; }
                        try
                        {
                            MailAddress from = new MailAddress(Properties.Settings.Default.SMTP_email, senderName);
                            MailAddress receiver = new MailAddress(mail_receiver);
                            MailMessage message = new MailMessage(from, receiver)
                            { Subject = mail_title, Body = mail_text };
                            if (files.Count > 0)
                                foreach (string f in files)
                                {
                                    Attachment data = new Attachment(f, MediaTypeNames.Application.Octet);
                                    message.Attachments.Add(data);
                                }
                            smtpClient.Send(message);
                            Console.WriteLine("Email gesendet: " + to);
                            Thread.Sleep(500);
                            //MessageBox.Show("Email gesendet!", "Mitteilung");
                        }
                        catch (Exception) { MessageBox.Show("Fehler beim Senden", "Fehler"); errorCounter++; }
                    }
                    catch (Exception) { MessageBox.Show("Fehler bei der Email", "Fehler"); errorCounter++; }
                    if (errorCounter > 1) { bar.Exit(); MessageBox.Show("Senden wurde abgebrochen", "Achtung"); return; }
                }
            }
            MessageBox.Show(teacherList.Count + " Emails gesendet!", "Mitteilung");
        }*/

        private void dtp_timeline_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) lbl_mode.Focus();
            if (e.KeyData == Keys.Enter) lbl_mode.Focus();

        }
        private void cb_escape_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyData == Keys.Escape) lbl_mode.Focus();
        }
        private void lbl_mode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) // TODO: up/down/left/right // TODO:-> keys
        {
            Console.WriteLine("lbl_mode: ");
            if (e.KeyData == Keys.Enter)
            { if (tl_entity_multiselect_list.Count >= 1) AddMultieditExam(); else AddExam(); }
            else if (e.KeyData == Keys.Left) dtp_time.Value = dtp_time.Value.AddMinutes(-15); 
            else if (e.KeyData == Keys.Right) dtp_time.Value = dtp_time.Value.AddMinutes(15);
            UpdatePreviewPanel();
            // -- default focused panel --
        }

        private void Dtp_time_MouseWheel(object sender, MouseEventArgs e)
        {
            dtp_time.Value = dtp_time.Value.AddMinutes(15 * (-e.Delta / 120));
            UpdatePreviewPanel();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            panel_top_time.Width = PixelPerHour * LengthTL;
            PixelPerHour = panel_top_time.Width / LengthTL;
            UpdateTimeline();
        }

        private void cb_keep_data_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AddOptionKeepData = cb_keep_data.Checked;
            Properties.Settings.Default.Save();
        }

        private void cb_add_next_time_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AddOptionAddNextTime = cb_add_next_time.Checked;
            Properties.Settings.Default.Save();
        }

        private void cb_student_onetime_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutocomplete(2);
            Properties.Settings.Default.AddOptionStudentOnetime = cb_student_onetime.Checked;
            Properties.Settings.Default.Save();
        }

    }
}