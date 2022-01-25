using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class Form1 : Form
    {
        Database database;
        Form_grid form_grid;
        public Form1()
        {
            database = Program.database;
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
            Panel panel_time_line1;
            for (int i = 0; i < 3; i++)
            {
                panel_time_line1 = new Panel();
                panel_time_line1.Location = new Point(12, 12 + 85 * i);
                panel_time_line1.Name = "panel_time_line1";
                panel_time_line1.Size = new Size(2400, 80);
                panel_time_line1.Margin = new Padding(5);
                panel_time_line1.TabIndex = 0;
                panel_time_line1.BackColor = Color.Red;
                panel_time_line1.Paint += panel1_Paint;
                this.panel_time_line.Controls.Add(panel_time_line1);

            }
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
            }

            /*var autocomplete_subject = new AutoCompleteStringCollection();
            autocomplete_subject.AddRange(new string[] { "Mathe", "Physik", "Deutsch", "Geschichte", "Englisch" });
            this.tb_subject.AutoCompleteCustomSource = autocomplete_subject;
            this.tb_subject.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tb_subject.AutoCompleteSource = AutoCompleteSource.CustomSource;*/
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
            int duration = Int16.Parse(tb_duration.Text);  // check for number

            if (exam_room != null && preparation_room != null && student != null && teacher1 != null && teacher2 != null && teacher3 != null && subject != null && duration != null)
            {
                // check db
            }
            else { MessageBox.Show("Alle Felder ausfüllen!"); return; }

            string name = student;
            string tempfirstname = null;
            string templastname = null;
            for (int i = 0; i < name.Split(' ').Length - 1; i++)
                tempfirstname += name.Split(' ')[i] += " ";
            tempfirstname = tempfirstname.Remove(tempfirstname.Length - 1, 1);
            templastname += name.Split(' ')[name.Split(' ').Length - 1];
            Console.WriteLine(" ===>>> " + database.GetStudent(tempfirstname, templastname, "Q2")[1]);

            database.AddExam(date, time, exam_room, preparation_room, database.GetStudent(tempfirstname, templastname, "Q2")[0], teacher1, teacher2, teacher3, subject, duration);
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
        Panel panel_tl;
        Panel panel_tl_entity;
        public void AddTimeline()
        {
            panel_tl = new Panel();
            panel_tl.Location = new Point(12, 12 + 85 * 4);
            panel_tl.Name = "panel_time_line";
            panel_tl.Size = new Size(2400, 80);
            panel_tl.BackColor = Color.Red;
            panel_tl.Paint += panel_timeline_Paint;
            this.panel_time_line.Controls.Add(panel_tl);
        }

        private void dtp_timeline_date_ValueChanged(object sender, EventArgs e)
        {
            update_timeline();
        }
        public void update_timeline()
        {
            if (panel_tl != null)
                panel_tl.Dispose();
            AddTimeline();
            string date = this.dtp_timeline_date.Value.ToString("yyyy-MM-dd");
            foreach (string[] s in Program.database.GetAllExamsAtDate(date))
            {
                Console.WriteLine(s[0] + s[1] + s[2] + s[3] + s[4] + s[5] + s[6] + s[7] + s[8] + s[9] + s[10]);
                panel_tl_entity = new Panel();
                DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime examTime = DateTime.ParseExact(s[2], "HH:mm", null, System.Globalization.DateTimeStyles.None);
                int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
                Console.WriteLine(totalMins);
                Console.WriteLine(12 + 200 / 60 * totalMins);
                panel_tl_entity.Location = new Point(12 + 200 / 60 * totalMins, 10);
                panel_tl_entity.Name = s[0];
                panel_tl_entity.Size = new Size(200 / 60 * Int32.Parse(s[10]), 60);
                panel_tl_entity.BackColor = Color.LightBlue;
                panel_tl_entity.Paint += panel_timeline_entity_Paint;
                panel_tl_entity.MouseClick += panel_tl_entity_click;
                panel_tl_entity.MouseDoubleClick += panel_tl_entity_double_click;
                this.panel_tl.Controls.Add(panel_tl_entity);
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
            {
                //ContextMenuStrip cm = sender as ContextMenuStrip;
            }
            else
            {
                //Codes Here
            }
        }*/

        private void panel_timeline_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel_tl.ClientRectangle,
            Color.DarkGreen, 4, ButtonBorderStyle.Solid, // left
            Color.DarkGreen, 4, ButtonBorderStyle.Solid, // top
            Color.DarkGreen, 4, ButtonBorderStyle.Solid, // right
            Color.DarkGreen, 4, ButtonBorderStyle.Solid);// bottom
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            // drawFormat.FormatFlags = StringFormatFlags.
            for (int i = 0; i < 12; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Blue, 2), panel_tl.Width / 12 * i, 10, panel_tl.Width / 12 * i, panel_tl.Height - 20);
                e.Graphics.DrawString(7 + i + " Uhr", drawFont, drawBrush, 10 + panel_tl.Width / 12 * i, panel_tl.Height - 30, drawFormat);
            }
        }
        private void panel_timeline_entity_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid,
            Color.DarkGreen, 2, ButtonBorderStyle.Solid);
            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            string[] student = Program.database.GetStudentByID(Int32.Parse(Program.database.GetExamById(Int32.Parse(p.Name))[5]));
            e.Graphics.DrawString(student[1] + " " + student[2], drawFont, drawBrush, 5, panel_tl_entity.Height / 8, drawFormat);
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
