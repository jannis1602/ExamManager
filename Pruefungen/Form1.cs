using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class Form1 : Form
    {
        Database database;
        Form_grid form_grid;
        int id = 0;
        LinkedList<Panel> time_line_list;
        LinkedList<Panel> time_line_entity_list;
        LinkedList<Label> time_line_room_list;

        public Form1()
        {
            database = Program.database;
            time_line_list = new LinkedList<Panel>();
            time_line_entity_list = new LinkedList<Panel>();
            time_line_room_list = new LinkedList<Label>();



            /*var source = new AutoCompleteStringCollection();
            source.AddRange(new string[] {"January","February"});
            var textBox = new TextBox
            {
                AutoCompleteCustomSource = source,
                AutoCompleteMode =
                      AutoCompleteMode.SuggestAppend,
                AutoCompleteSource =
                      AutoCompleteSource.CustomSource,
                Location = new Point(20, 20),
                Width = ClientRectangle.Width - 40,
                Visible = true
            };*/
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            // ## TODO: get next Exam date ## #######################################################
            update_timeline();
            /*for (int i = 0; i < 10; i++)
            {
                AddTimeline("O-20" + i);
            }*/
            /*Panel panel_time_line1;
            void panel1_Paint(object sender, PaintEventArgs e)
            {
                ControlPaint.DrawBorder(e.Graphics, panel_time_line1.ClientRectangle,
                Color.DarkGreen, 4, ButtonBorderStyle.Solid, // left
                Color.DarkGreen, 4, ButtonBorderStyle.Solid, // top
                Color.DarkGreen, 4, ButtonBorderStyle.Solid, // right
                Color.DarkGreen, 4, ButtonBorderStyle.Solid);// bottom
                Font drawFont = new Font("Arial", 10);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                StringFormat drawFormat = new StringFormat();
                // drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                for (int i = 0; i < 12; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Blue, 2), panel_time_line1.Width / 12 * i, 10, panel_time_line1.Width / 12 * i, panel_time_line1.Height - 20);
                    e.Graphics.DrawString(7 + i + " Uhr", drawFont, drawBrush, 10 + panel_time_line1.Width / 12 * i, panel_time_line1.Height - 30, drawFormat);
                }
            }*/

            string[] subjectlist = new string[] { "Mathe", "Physik", "Deutsch", "Geschichte", "Englisch" };
            cb_subject.Items.AddRange(subjectlist);
            var autocomplete_student = new AutoCompleteStringCollection();
            LinkedList<string[]> allStudents = database.GetAllStudents();
            string[] students = new string[allStudents.Count];
            for (int i = 0; i < allStudents.Count; i++)
                students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
            autocomplete_student.AddRange(students);
            this.tb_student.AutoCompleteCustomSource = autocomplete_student;
            this.tb_student.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_student.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var autocomplete_exam_room = new AutoCompleteStringCollection();
            autocomplete_exam_room.AddRange(new string[] { "O-201", "O-202", "O-203", "O-204", "O-205" });
            this.tb_exam_room.AutoCompleteCustomSource = autocomplete_exam_room;
            this.tb_exam_room.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_exam_room.AutoCompleteSource = AutoCompleteSource.CustomSource;

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
        // ## [DEV] ##
        private void AddExam()
        {
            string date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            string time = this.dtp_time.Value.ToString("HH:mm");
            string exam_room = tb_exam_room.Text.ToUpper();
            string preparation_room = tb_preparation_room.Text.ToUpper();
            string student = tb_student.Text;  // check in db
            string teacher1 = tb_teacher1.Text;  // check in db
            string teacher2 = tb_teacher2.Text;  // check in db
            string teacher3 = tb_teacher3.Text;  // check in db
            string subject = cb_subject.Text;       // uppercase
            int duration = Int32.Parse(tb_duration.Text);  // check for number

            if (exam_room.Length == 0 || preparation_room.Length == 0 || student.Length == 0 || teacher1.Length == 0 || teacher2.Length == 0 || teacher3.Length == 0 || subject.Length == 0 || duration == 0)
            {
                MessageBox.Show("Alle Felder ausfüllen!", "Warnung"); return;
            }

            // check db ##################################################

            string name = student;
            string tempfirstname = null;
            string templastname = null;
            try
            {
                for (int i = 0; i < name.Split(' ').Length - 1; i++)
                    tempfirstname += name.Split(' ')[i] += " ";
                tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
                templastname += name.Split(' ')[name.Split(' ').Length - 1];
                Console.WriteLine(" ===>>> " + database.GetStudent(tempfirstname, templastname, "Q2")[1]);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Fehler beim Schülernamen!", "Warnung"); return;
            }
            if (database.GetStudent(tempfirstname, templastname, "Q2")[0] == null)
            {
                MessageBox.Show("Schüler nicht gefunden!", "Warnung"); return;
            }
            if (id != 0)
                database.EditExam(id, date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, "Q2")[0], teacher1, teacher2, teacher3, subject, duration);
            if (id == 0)
                database.AddExam(date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, "Q2")[0], teacher1, teacher2, teacher3, subject, duration);
            id = 0;
            update_timeline();
            if (this.cb_add_next_time.Checked) { this.dtp_time.Value = this.dtp_time.Value.AddMinutes(45); }
            if (this.cb_keep_data.Checked)
            {
                this.tb_student.Clear();
            }
            else
            {
                this.tb_exam_room.Clear();
                this.tb_preparation_room.Clear();
                this.tb_student.Clear();
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
                form_grid = new Form_grid();
                form_grid.Show();
            }

            if (form_grid.IsDisposed)
            {
                form_grid = new Form_grid();
                form_grid.Show();
            }
        }
        public void AddTimeline(string room)
        {
            Label lbl_room = new Label();
            lbl_room.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_room.Location = new Point(20, panel_side_time.Height + 12 + 85 * time_line_list.Count);
            lbl_room.Margin = new Padding(3);
            lbl_room.Name = "lbl_room";
            lbl_room.Size = new Size(80, 80);
            lbl_room.Text = room;
            lbl_room.TextAlign = ContentAlignment.MiddleLeft;
            //panel_side_room.Controls.Add(lbl_room);
            time_line_room_list.AddLast(lbl_room);

            this.panel_time_line.HorizontalScroll.Value = 0;
            Panel panel_tl = new Panel();
            panel_tl.Location = new Point(panel_side_room.Width, panel_side_time.Height + 12 + 85 * time_line_list.Count);
            panel_tl.Name = room;
            panel_tl.Size = new Size(2400, 80);
            panel_tl.BackColor = Color.Gray;
            panel_tl.Paint += panel_time_line_Paint;
            this.panel_time_line.Controls.Add(panel_tl);
            time_line_list.AddLast(panel_tl);
        }

        private void dtp_timeline_date_ValueChanged(object sender, EventArgs e)
        {
            update_timeline();
        }
        public void update_timeline()
        {
            foreach (Panel p in time_line_list) p.Dispose();
            foreach (Panel p in time_line_entity_list) p.Dispose();
            foreach (Label p in time_line_room_list) p.Dispose();
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
            foreach (string s in room_list)
                Console.WriteLine(s);
            foreach (string s in room_list)
                AddTimeline(s);
            foreach (string[] s in database.GetAllExamsAtDate(date))
            {
                //Console.WriteLine(s[0] + s[1] + s[2] + s[3] + s[4] + s[5] + s[6] + s[7] + s[8] + s[9] + s[10]);
                Panel panel_tl_entity = new Panel();
                DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime examTime = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
                panel_tl_entity.Location = new Point(12 + 200 / 60 * totalMins, 10);
                panel_tl_entity.Size = new Size(200 / 60 * Int32.Parse(s[10]), 60);
                panel_tl_entity.Name = s[0];
                panel_tl_entity.BackColor = Color.LightBlue;
                panel_tl_entity.Paint += panel_time_line_entity_Paint;
                panel_tl_entity.MouseClick += panel_tl_entity_click;
                panel_tl_entity.MouseDoubleClick += panel_tl_entity_double_click;
                foreach (Panel p in time_line_list)
                    if (p.Name.Equals(s[3]))
                        p.Controls.Add(panel_tl_entity);
            }
        }
        private void panel_tl_entity_double_click(object sender, MouseEventArgs e) // TODO delete old if edited
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                string[] exam = Program.database.GetExamById(Int32.Parse(p.Name));
                string[] student = Program.database.GetStudentByID(Int32.Parse(exam[5]));

                DialogResult result = MessageBox.Show("Prüfung von " + student[1] + " " + student[2] + " Bearbeiten?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    id = Int32.Parse(exam[0]);
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
                }
            }
        }
        private void panel_tl_entity_click(object sender, MouseEventArgs e) // TODO delete old if edited
        {
            Console.WriteLine("Click");

            if (e.Button == MouseButtons.Left)
            {
                Panel p = sender as Panel;
                string[] exam = Program.database.GetExamById(Int32.Parse(p.Name));
                string[] student = Program.database.GetStudentByID(Int32.Parse(exam[5]));

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
                // add 30min line ######################
                e.Graphics.DrawString(7 + i + " Uhr", drawFont, Brushes.Blue, 5 + panel_tl.Width / 12 * i, panel_tl.Height - 20, drawFormat);
            }
        }
        private void panel_side_room_Paint(object sender, PaintEventArgs e)
        {
            // ## [DEV] ##

            Font drawFont = new Font("Arial", 10);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rect = new Rectangle(5, 5, panel_side_room.Width - 10, panel_side_time.Height - 10);
            e.Graphics.FillRectangle(Brushes.LightGreen, rect);
            ControlPaint.DrawBorder(e.Graphics, rect,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
            e.Graphics.DrawString("Q2", drawFont, Brushes.Black, rect, stringFormat);

            int i = 0;
            foreach (Label l in time_line_room_list)
            {
                Rectangle rect_room = new Rectangle(10, panel_side_time.Height + 20 + 85 * i, 80, 60);
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.FillRectangle(Brushes.LightGreen, rect_room);
                ControlPaint.DrawBorder(e.Graphics, rect_room,
                Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid,
                Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
                e.Graphics.DrawString(l.Text, drawFont, Brushes.Black, rect_room, stringFormat);
                i++;
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
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            string[] student = Program.database.GetStudentByID(Int32.Parse(Program.database.GetExamById(Int32.Parse(panel_tl_entity.Name))[5]));
            e.Graphics.DrawString(student[1] + " " + student[2], drawFont, drawBrush, 5, panel_tl_entity.Height / 8, drawFormat);
        }

        private void btn_delete_exam_Click(object sender, EventArgs e)
        {
            database.DeleteExam(id);
            id = 0;
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
                this.cb_subject.SelectedItem = null;
                this.tb_teacher1.Clear();
                this.tb_teacher2.Clear();
                this.tb_teacher3.Clear();
            }
        }

        private void btn_reuse_exam_Click(object sender, EventArgs e)
        { id = 0; }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            id = 0;
            if (this.cb_keep_data.Checked)
            { this.tb_student.Clear(); }
            else
            {
                this.tb_exam_room.Clear();
                this.tb_preparation_room.Clear();
                this.tb_student.Clear();
                this.cb_subject.SelectedItem = null;
                this.tb_teacher1.Clear();
                this.tb_teacher2.Clear();
                this.tb_teacher3.Clear();
            }
        }

        /*private void tb_duration_TextChanged(object sender, EventArgs e)
{
if (System.Text.RegularExpressions.Regex.IsMatch(tb_duration.Text, "[^0-9]"))
{
MessageBox.Show("Nur Zahlen!");
tb_duration.Text = tb_duration.Text.Remove(tb_duration.Text.Length - 1);
}
}*/
    }
}
