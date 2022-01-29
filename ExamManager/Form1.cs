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

        Database database;
        Form_grid form_grid;
        int id = 0;
        LinkedList<Panel> time_line_list;
        LinkedList<Panel> time_line_entity_list;
        LinkedList<Panel> time_line_room_list;
        string[] edit_mode = { "neue Prüfung erstellen", "Prüfung bearbeiten" };
        string[] add_mode = { "Prüfung hinzufügen", "Prüfung übernehmen" };
        Point panelScrollPos1 = new Point();
        Point panelScrollPos2 = new Point();
        Panel panel_empty, panel_top;
        public Form1()
        {
            database = Program.database;
            time_line_list = new LinkedList<Panel>();
            time_line_entity_list = new LinkedList<Panel>();
            time_line_room_list = new LinkedList<Panel>();
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            update_timeline();
            LoadAutocomplete();
        }

        private void LoadAutocomplete()
        {
            //TODO: autocomplete rooms (cb_room)
            // subjects
            cb_subject.Items.Clear();
            LinkedList<string[]> subjectList = Program.database.GetAllSubjects();
            string[] subjects = new string[subjectList.Count];
            for (int i = 0; i < subjectList.Count; i++)
                subjects[i] = subjectList.ElementAt(i)[0];
            cb_subject.Items.AddRange(subjects);
            // students
            var autocomplete_student = new AutoCompleteStringCollection();
            LinkedList<string[]> allStudents = database.GetAllStudents();
            string[] students = new string[allStudents.Count];
            for (int i = 0; i < allStudents.Count; i++)
                students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
            autocomplete_student.AddRange(students);
            this.tb_student.AutoCompleteCustomSource = autocomplete_student;
            this.tb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // grade
            cb_grade.Items.Clear();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (string[] s in allStudents)
                if (!gradeList.Contains(s[3]))
                    gradeList.AddLast(s[3]);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList(); // .ThenBy( x => x.Bar)
            gradeList = new LinkedList<string>(templist);
            string[] list = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                list[i] = gradeList.ElementAt(i);
            cb_grade.Items.AddRange(list);
            // teacher
            var autocomplete_teacher = new AutoCompleteStringCollection();
            LinkedList<string[]> teacherList = database.GetAllTeachers();
            string[] teacher = new string[teacherList.Count];
            for (int i = 0; i < teacherList.Count; i++)
            {
                teacher[i] = teacherList.ElementAt(i)[1] + " " + teacherList.ElementAt(i)[2];
                Console.WriteLine(teacher[i]);
            }
            autocomplete_teacher.AddRange(teacher);
            this.tb_teacher1.AutoCompleteCustomSource = autocomplete_teacher;
            this.tb_teacher1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_teacher1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.tb_teacher2.AutoCompleteCustomSource = autocomplete_teacher;
            this.tb_teacher2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_teacher2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.tb_teacher3.AutoCompleteCustomSource = autocomplete_teacher;
            this.tb_teacher3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_teacher3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            /*LinkedList<string[]> teacherList = Program.database.GetAllSubjects();
            string[] teacher = new string[teacherList.Count];
            for (int i = 0; i < teacherList.Count; i++)
                teacher[i] = teacherList.ElementAt(i)[0];
            cb_.Items.AddRange(teacher);*/
            // exam_room
            var autocomplete_exam_room = new AutoCompleteStringCollection();
            LinkedList<string[]> examList = Program.database.GetAllRooms();
            string[] rooms = new string[examList.Count];
            for (int i = 0; i < examList.Count; i++)
                rooms[i] = examList.ElementAt(i)[0];
            autocomplete_exam_room.AddRange(rooms);
            this.tb_exam_room.AutoCompleteCustomSource = autocomplete_exam_room;
            this.tb_exam_room.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_exam_room.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // prep_room
            this.tb_preparation_room.AutoCompleteCustomSource = autocomplete_exam_room;
            this.tb_preparation_room.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_preparation_room.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btn_add_exam_Click(object sender, EventArgs e)
        {
            AddExam();
            if (form_grid != null && !form_grid.IsDisposed)
                form_grid.Data_update();
        }
        private void tb_student_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                AddExam();
                if (form_grid != null && !form_grid.IsDisposed)
                    form_grid.Data_update();
                e.Handled = true;
            }
        }

        // ## [DEV] ## ################################################################################
        private void AddExam()
        {
            string date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            string time = this.dtp_time.Value.ToString("HH:mm");
            string exam_room = tb_exam_room.Text.ToUpper();
            string preparation_room = tb_preparation_room.Text.ToUpper();
            string student = tb_student.Text;  // check in db
            string grade = cb_grade.SelectedItem.ToString();  // check in db
            string teacher1 = tb_teacher1.Text;  // check in db
            string teacher2 = tb_teacher2.Text;  // check in db
            string teacher3 = tb_teacher3.Text;  // check in db
            string subject = cb_subject.Text;       // uppercase
            int duration = Int32.Parse(tb_duration.Text);  // check for number

            if (exam_room.Length == 0 || preparation_room.Length == 0 || student.Length == 0 || grade.Length == 0 || teacher1.Length == 0 || teacher2.Length == 0 || teacher3.Length == 0 || subject.Length == 0 || duration == 0)
            {
                MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return;
            }

            /*if (database.CheckTimeAndRoom(date, time, exam_room))
            {
                MessageBox.Show("Raum besetzt!", "Warnung"); return;
            }*/
            if (id == 0)
                foreach (string[] s in database.GetAllExamsAtDateAndRoom(date, exam_room))
                {
                    DateTime start = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(Int32.Parse(s[10]));
                    DateTime timestart = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime timeend = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(duration);

                    if ((start < timestart && timestart < end) || (timestart < start && start < timeend))
                    {
                        MessageBox.Show("Raum besetzt!", "Warnung");
                        return;
                    }
                }


            // check db ######################################################################################################

            string name = student;
            string tempfirstname = null;
            string templastname = null;

            try
            {
                for (int i = 0; i < name.Split(' ').Length - 1; i++)
                    tempfirstname += name.Split(' ')[i] += " ";
                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                templastname += name.Split(' ')[name.Split(' ').Length - 1];

                Console.WriteLine(" ===>>> " + database.GetStudent(tempfirstname, templastname, grade)[1]);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return;
            }
            if (database.GetStudent(tempfirstname, templastname, grade)[0] == null)
            {
                MessageBox.Show("Schüler nicht gefunden!", "Warnung"); return;
            }
            if (id != 0)
                database.EditExam(id, date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, grade)[0], teacher1, teacher2, teacher3, subject, duration);
            if (id == 0)
                database.AddExam(date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, grade)[0], teacher1, teacher2, teacher3, subject, duration);
            id = 0;
            lbl_mode.Text = edit_mode[0];
            btn_add_exam.Text = add_mode[0];
            update_timeline();
            if (this.cb_add_next_time.Checked) { this.dtp_time.Value = this.dtp_time.Value.AddMinutes(Int32.Parse(tb_duration.Text)); }
            if (this.cb_keep_data.Checked)
            {
                this.tb_student.Clear();
            }
            else
            {
                this.tb_exam_room.Clear();
                this.tb_preparation_room.Clear();
                this.tb_student.Clear();
                this.cb_grade.SelectedItem = null;
                this.cb_subject.SelectedItem = null;
                this.tb_teacher1.Clear();
                this.tb_teacher2.Clear();
                this.tb_teacher3.Clear();
            }
        }

        private void btn_grid_view_Click(object sender, EventArgs e)
        {
            if (form_grid == null)
            {
                form_grid = new Form_grid(1);
                form_grid.Show();
            }

            if (form_grid.IsDisposed)
            {
                form_grid = new Form_grid(1);
                form_grid.Show();
            }
        }
        public void AddTimeline(string room)
        {
            /*Label lbl_room = new Label();
            lbl_room.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_room.Location = new Point(20, panel_side_time.Height + 12 + 85 * time_line_list.Count);
            lbl_room.Margin = new Padding(3);
            lbl_room.Name = "lbl_room";
            lbl_room.Size = new Size(80, 80);
            lbl_room.Text = room;
            lbl_room.TextAlign = ContentAlignment.MiddleLeft;*/
            Panel panel_room = new Panel();
            //panel_room.Location = new Point(10, panel_side_time.Height + 20 + 85 * time_line_list.Count);
            //panel_room.Size = new Size(80, panel_side_time.Height + 12);
            panel_room.Location = new Point(0, panel_side_time.Height + 5 + 85 * time_line_list.Count);
            panel_room.Size = new Size(panel_side_room.Width - 17, 80);
            //panel_room.Margin = new Padding(5);
            panel_room.BackColor = Color.LightBlue;
            panel_room.Name = room;
            Label lbl_room = new Label();
            lbl_room.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_room.Location = new Point(0, 0);
            //lbl_room.Margin = new Padding(3);
            lbl_room.Dock = DockStyle.Fill;
            //lbl_room.Size = new Size(80, 80);
            lbl_room.Name = "lbl_room";
            lbl_room.Text = room;
            lbl_room.TextAlign = ContentAlignment.MiddleCenter;
            panel_room.Controls.Add(lbl_room);
            this.panel_side_room.HorizontalScroll.Value = 0;
            panel_side_room.Controls.Add(panel_room);
            time_line_room_list.AddLast(panel_room);
            //
            /*if (panel_empty == null)
                panel_empty = new Panel();
            else panel_side_room.Controls.Remove(panel_empty);
            panel_empty.Location = new Point(0, panel_side_time.Height + 12 + 85 * (time_line_list.Count + 1));
            panel_empty.Size = new Size(panel_side_room.Width - 17, 12);
            panel_empty.BackColor = Color.Red;
            panel_empty.Name = "empty";
            panel_side_room.Controls.Add(panel_empty);*/
            //
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

        private void dtp_time_line_date_ValueChanged(object sender, EventArgs e)
        {
            update_timeline();
        }
        public void update_timeline()
        {
            if (panel_empty != null)
                panel_side_room.Controls.Remove(panel_empty);

            if (panel_top == null)
                panel_top = new Panel();
            else panel_side_room.Controls.Remove(panel_top);

            panel_top.Location = new Point(0, 0);
            panel_top.Size = new Size(panel_side_room.Width - 17, panel_side_time.Height);
            //panel_top.BackColor = Color.Gray;
            panel_top.Name = "top";
            panel_side_room.Controls.Add(panel_top);

            //TODO: add top panel and text ####################################

            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_entity_list) p.Dispose();
            foreach (Panel p in time_line_room_list) p.Dispose();
            time_line_list.Clear();
            time_line_entity_list.Clear();
            time_line_room_list.Clear();

            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            LinkedList<string> room_list = new LinkedList<string>();
            foreach (string[] s in database.GetAllExamsAtDate(date))
            {
                if (!room_list.Contains(s[3]))
                    room_list.AddLast(s[3]);
            }
            List<string> temp_room_list = new List<string>(room_list);
            temp_room_list.Sort();
            room_list = new LinkedList<string>(temp_room_list);
            foreach (string s in room_list)
                AddTimeline(s);
            // ## [DEV] ## 
            if (panel_empty == null)
                panel_empty = new Panel();
            panel_empty.Location = new Point(0, panel_side_time.Height + 5 + 85 * time_line_list.Count);
            panel_empty.Size = new Size(panel_side_room.Width - 17, 12);
            //panel_empty.BackColor = Color.Red;
            panel_empty.Name = "empty";
            panel_side_room.Controls.Add(panel_empty);
            foreach (string[] s in database.GetAllExamsAtDate(date))
            {
                //Console.WriteLine(s[0] + s[1] + s[2] + s[3] + s[4] + s[5] + s[6] + s[7] + s[8] + s[9] + s[10]);
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
                panel_tl_entity.MouseClick += panel_tl_entity_click;
                panel_tl_entity.MouseDoubleClick += panel_tl_entity_double_click;
                foreach (Panel p in time_line_list)
                    if (p.Name.Equals(s[3]))
                    {
                        p.Controls.Add(panel_tl_entity);
                    }
            }
        }
        private void panel_tl_entity_double_click(object sender, MouseEventArgs e) // TODO delete old if edited
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
                    this.tb_exam_room.Text = exam[3];
                    this.tb_preparation_room.Text = exam[4];
                    string[] st = database.GetStudentByID(Int32.Parse(exam[5]));
                    this.tb_student.Text = st[1] + " " + st[2];
                    this.cb_grade.SelectedItem = st[3];
                    this.tb_teacher1.Text = exam[6];
                    this.tb_teacher2.Text = exam[7];
                    this.tb_teacher3.Text = exam[8];
                    this.cb_subject.Text = exam[9];
                    this.tb_duration.Text = exam[10];
                }
            }
        }
        private void panel_tl_entity_click(object sender, MouseEventArgs e) // TODO delete old if edited
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                string[] exam = database.GetExamById(Int32.Parse(p.Name));
                string[] student = database.GetStudentByID(Int32.Parse(exam[5]));

                /*DialogResult result = MessageBox.Show("Prüfung von " + student[1] + " " + student[2] + " Bearbeiten?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Console.WriteLine(exam[1]);
                    this.dtp_date.Value = DateTime.ParseExact(exam[1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None);
                    this.dtp_time.Value = DateTime.ParseExact(exam[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    this.tb_exam_room.Text = exam[3];
                    this.tb_preparation_room.Text = exam[4];
                    string[] st = Program.database.GetStudentByID(Int32.Parse(exam[5]));
                    this.tb_student.Text = st[1] + " " + st[2];
                    this.tb_teacher1.Text = exam[6];
                    this.tb_teacher2.Text = exam[7];
                    this.tb_teacher3.Text = exam[8];
                    this.cb_subject.Text = exam[9];
                    this.tb_duration.Text = exam[10];
                }*/
            }
            else if (e.Button == MouseButtons.Right)
            {
                //Panel p = sender as Panel;
                //int exam_id = Int32.Parse(Program.database.GetExamById(Int32.Parse(p.Name))[0]);
                //string[] student = Program.database.GetStudentByID(Int32.Parse(Program.database.GetExamById(Int32.Parse(p.Name))[5]));
                //DialogResult result = MessageBox.Show("Prüfung von " + student[1] + " " + student[2] + " Löschen?", "Warnung!", MessageBoxButtons.YesNo);
                //if (result == DialogResult.Yes) { Program.database.DeleteExam(exam_id); p.Dispose(); }

                /*ContextMenuStrip cm = new ContextMenuStrip();
                cm.Name = Program.database.GetExamById(Int32.Parse(p.Name))[0];
                cm.Items.Add("Item 1");
                cm.Items.Add("Item 2");
                cm.Show(p, new Point(e.X, e.Y));
                cm.ItemClicked += new ToolStripItemClickedEventHandler(Item_Click);*/
            }
        }
        /*private void Item_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Delete")
            {//ContextMenuStrip cm = sender as ContextMenuStrip;s}
        }*/

        private void panel_side_room_Paint(object sender, PaintEventArgs e)
        {
            // ## [DEV] ##

            /*Panel p = sender as Panel;
            // p.Height = panel_time_line.Height; // - panel_side_time.Height;
            Font drawFont = new Font("Arial", 10);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rect = new Rectangle(5, 5, panel_side_room.Width - 10 - 17, panel_side_time.Height - 10);
            e.Graphics.FillRectangle(Brushes.LightGreen, rect);
            ControlPaint.DrawBorder(e.Graphics, rect,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
            e.Graphics.DrawString("Q2", drawFont, Brushes.Black, rect, stringFormat);
            e.Graphics.DrawLine(new Pen(Color.Black, 4), p.Width, 0, p.Width, p.Height);
            int i = 0;*/

            /*foreach (Panel pr in time_line_room_list)
            {
                Panel panel_tl_room = pr;
                panel_tl_room.Location = new Point(10, panel_side_time.Height + 20);
                panel_tl_room.Size = new Size(80, 60);
                panel_tl_room.BackColor = Color.LightBlue;
                panel_tl_room.Paint += panel_time_line_entity_Paint;
                panel_tl_room.MouseClick += panel_tl_entity_click;
                panel_tl_room.MouseDoubleClick += panel_tl_entity_double_click;*/
            /*                foreach (Panel p in time_line_list)
                                if (p.Name.Equals(s[3]))
                                    p.Controls.Add(panel_tl_entity);*/

            /*Rectangle rect_room = new Rectangle(10, panel_side_time.Height + 20 + 85 * i, 80, 60);
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.FillRectangle(Brushes.LightGreen, rect_room);
            ControlPaint.DrawBorder(e.Graphics, rect_room,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
            e.Graphics.DrawString(pr.Text, drawFont, Brushes.Black, rect_room, stringFormat);
            i++;*/
            //}
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
            Font drawFont = new Font("Arial", 10);
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
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            StringFormat drawFormat = new StringFormat();
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
                //e.Graphics.DrawString(7 + i + " Uhr", drawFont, drawBrush, 10 + panel_tl.Width / 12 * i, panel_tl.Height - 20, drawFormat);
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
            Font drawFont = new Font("Arial", 8);
            StringFormat drawFormat = new StringFormat();
            string[] exam = database.GetExamById(Int32.Parse(panel_tl_entity.Name));
            string[] student = database.GetStudentByID(Int32.Parse(exam[5]));
            Console.WriteLine("############################## " + student);
            if (student == null)
                student = new string[] { "0", "Name  not  found!", "  ", " - ", " - ", " - " };
            if (search != null)
                if (search_index == 0)
                    if (search.Equals(student[1] + " " + student[2]))
                    {
                        ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid,
                        Color.Red, 2, ButtonBorderStyle.Solid);
                    }
            if (search_index == 1)
                if (search.Equals(exam[6]) || search.Equals(exam[7]) || search.Equals(exam[8]))
                {
                    ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid);
                }
            if (search_index == 2)
                if (search.Equals(exam[9]))
                {
                    ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid);
                }
            if (search_index == 3)
                if (search.Equals(exam[3]) || search.Equals(exam[4]))
                {
                    ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid,
                    Color.Red, 2, ButtonBorderStyle.Solid);
                }

            //e.Graphics.DrawString(student[1] + " " + student[2], drawFont, drawBrush, 2, panel_tl_entity.Height / 16*1, drawFormat);
            //e.Graphics.DrawString(exam[2], drawFont, drawBrush, 2, panel_tl_entity.Height / 16 * 6, drawFormat);
            //e.Graphics.DrawString(exam[6] + " " + exam[7] + " " + exam[8], drawFont, drawBrush, 2, panel_tl_entity.Height / 16 * 10, drawFormat);

            //ControlPaint.DrawBorder(e.Graphics, rect,
            //Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            //Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
            //Rectangle rect = new Rectangle(1, 1, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rectL1 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 0, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL2 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 1, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL3 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 2, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL4 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 3, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            e.Graphics.DrawString(student[1] + " " + student[2], drawFont, Brushes.Black, rectL1, stringFormat);
            e.Graphics.DrawString(exam[2] + "     " + exam[10] + "min", drawFont, Brushes.Black, rectL2, stringFormat);
            e.Graphics.DrawString(exam[6] + "  " + exam[7] + "  " + exam[8], drawFont, Brushes.Black, rectL3, stringFormat);
            e.Graphics.DrawString(exam[9] + " " + exam[3] + "  prep: " + exam[4], drawFont, Brushes.Black, rectL4, stringFormat);
            // ## [DEV] ## TODO: startTime - end Time

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
                        this.tb_student.Clear();
                    }
                    else
                    {
                        this.tb_exam_room.Clear();
                        this.tb_preparation_room.Clear();
                        this.tb_student.Clear();
                        this.cb_grade.SelectedItem = null;
                        this.cb_subject.SelectedItem = null;
                        this.tb_teacher1.Clear();
                        this.tb_teacher2.Clear();
                        this.tb_teacher3.Clear();
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
            btn_add_exam.Text = add_mode[0];
            if (this.cb_keep_data.Checked)
            { this.tb_student.Clear(); }
            else
            {
                this.tb_exam_room.Clear();
                this.tb_preparation_room.Clear();
                this.tb_student.Clear();
                this.cb_grade.SelectedItem = null;
                this.cb_subject.SelectedItem = null;
                this.tb_teacher1.Clear();
                this.tb_teacher2.Clear();
                this.tb_teacher3.Clear();
            }
        }

        private void panel_time_line_Move(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            Console.WriteLine(p.AutoScrollPosition.Y);
        }

        private void panel_time_line_master_Paint(object sender, PaintEventArgs e)
        {
            //Console.WriteLine(panel_time_line.AutoScrollPosition.Y);
            if (panel_time_line.AutoScrollPosition.Y == 0)
                this.panel_side_room.HorizontalScroll.Value = 0;
            //panel_side_room.AutoScrollPosition = new Point(0, 0);
            // für unten auch
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
            {
                tb_duration.Text = tb_duration.Text.Remove(tb_duration.Text.Length - 1);
            }
            cb_add_next_time.Text = "Nächste + " + this.tb_duration.Text + "min";
        }

        public void SetDate(DateTime date)
        {
            dtp_timeline_date.Value = date;
        }

        private void tsmi_table_exams_Click(object sender, EventArgs e)
        {
            new Form_grid(0).Show();

            /*if (form_grid == null)
            {
                form_grid = new Form_grid(0);
                form_grid.Show();
            }

            if (form_grid.IsDisposed)
            {
                form_grid = new Form_grid(0);
                form_grid.Show();
            }*/
        }

        private void tsmi_table_students_Click(object sender, EventArgs e)
        {
            new Form_grid(2).Show();
        }

        private void tsmi_table_teacher_Click(object sender, EventArgs e)
        {
            new Form_grid(1).Show();
        }

        private void tsmi_search_teacher_Click(object sender, EventArgs e)
        {
            new FormSearch(0, this).Show();
        }

        private void tsmi_search_student_Click(object sender, EventArgs e)
        {
            new FormSearch(1, this).Show();
        }
        private void tsmi_search_subject_Click(object sender, EventArgs e)
        {
            new FormSearch(2, this).Show();

        }
        private void raumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSearch(3, this).Show();

        }
        private void tsmi_search_delete_Click(object sender, EventArgs e)
        {
            search = null;
            search_index = 0;
            update_timeline();
        }

        private void tsmi_data_students_Click(object sender, EventArgs e)
        {
            FormStudentData formStudentData = new FormStudentData();
            formStudentData.Disposed += UpdateAutocomplete_Event;
            formStudentData.Show();
        }

        private void tsmi_data_rooms_Click(object sender, EventArgs e)
        {
            FormRoomData form = new FormRoomData();
            form.Disposed += UpdateAutocomplete_Event;
            form.Show();
        }

        private void tsmi_exam_changeroom_Click(object sender, EventArgs e)
        {
            FormChangeRoom form = new FormChangeRoom(this.dtp_date.Value.ToString("yyyy-MM-dd"));
            form.Disposed += changeroom_Event;
            form.Show();
        }

        void changeroom_Event(object sender, EventArgs a)
        {
            update_timeline();
        }

        private void tsmi_data_subjects_Click(object sender, EventArgs e)
        {
            FormSubjectData form = new FormSubjectData();
            form.Disposed += UpdateAutocomplete_Event;
            form.Show();
        }
        void UpdateAutocomplete_Event(object sender, EventArgs a)
        {
            LoadAutocomplete();
        }

        private void tsmi_data_editgrade_move_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[DEVELOPMENT]", "Warnung!");
        }
        private void tsmi_data_editgrade_delete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[DEVELOPMENT]", "Warnung!");
            //database.DeleteGrade();
        }

        private void tsmi_exam_examdates_Click(object sender, EventArgs e)
        {
            new FormExamDateListView(this).Show();
        }

        private void tsmi_data_loadstudents_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    database.InsertStudentFileIntoDB(filePath);
                }
            }
            LoadAutocomplete();
        }

        private void cb_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_grade.SelectedItem.ToString() != null)
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = database.GetAllStudentsFromGrade(cb_grade.SelectedItem.ToString());
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                this.tb_student.AutoCompleteCustomSource = autocomplete_student;
                this.tb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.tb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                this.tb_student.AutoCompleteCustomSource = autocomplete_student;
                this.tb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.tb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void tsmi_data_teachers_Click(object sender, EventArgs e)
        {
            FormTeacherData formTeacherData = new FormTeacherData();
            formTeacherData.Disposed += UpdateAutocomplete_Event;
            formTeacherData.Show();

        }
    }
}
