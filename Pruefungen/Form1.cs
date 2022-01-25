using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                float x = 10.0F;
                StringFormat drawFormat = new StringFormat();
                // drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                // Draw string to screen.
                for (int i = 0; i < 24; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Blue, 2), panel_time_line1.Width / 24 * i, 10, panel_time_line1.Width / 24 * i, panel_time_line1.Height - 20);
                    e.Graphics.DrawString(i + " Uhr", drawFont, drawBrush, 10 + panel_time_line1.Width / 24 * i, panel_time_line1.Height - 30, drawFormat);
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

            /*chart1.Titles.Add("Stacked BAR Chart!");
            chart1.Series["RUN"].Points.AddXY("MACHINE 1", "50");
            chart1.Series["ALARM"].Points.AddXY("MACHINE 1", "30");
            chart1.Series["WAIT"].Points.AddXY("MACHINE 1", "10");
            chart1.Series["OFF"].Points.AddXY("MACHINE 1", "10");

            chart1.Series["RUN"].Points.AddXY("MACHINE 2", "250");
            chart1.Series["ALARM"].Points.AddXY("MACHINE 2", "150");
            chart1.Series["WAIT"].Points.AddXY("MACHINE 2", "70");
            chart1.Series["OFF"].Points.AddXY("MACHINE 2", "200");

            chart1.Series["RUN"].Points.AddXY("MACHINE 3", "50");
            chart1.Series["ALARM"].Points.AddXY("MACHINE 3", "150");
            chart1.Series["WAIT"].Points.AddXY("MACHINE 3", "150");
            chart1.Series["OFF"].Points.AddXY("MACHINE 3", "400");

            chart1.Series["RUN"].Points.AddXY("MACHINE 4", "250");
            chart1.Series["ALARM"].Points.AddXY("MACHINE 4", "150");
            chart1.Series["WAIT"].Points.AddXY("MACHINE 4", "750");
            chart1.Series["OFF"].Points.AddXY("MACHINE 4", "200");


            chart1.Series["RUN"].Points.AddXY("MACHINE 5", "250");
            chart1.Series["ALARM"].Points.AddXY("MACHINE 5", "50");
            chart1.Series["WAIT"].Points.AddXY("MACHINE 5", "170");
            chart1.Series["OFF"].Points.AddXY("MACHINE 5", "20"); */
        }

        private void btn_add_exam_Click(object sender, EventArgs e)
        {
            AddExam();
            form_grid.Data_update();
        }
        private void AddExam()
        {
            //AddExam("2022-01-28", "09:00:00", "O-201", "O-202", "student1", "abc", "def", "ghi", "ma", 45);
            //string date = tb_date.Text;
            //IWebElement txtBxDatePicker = driver.FindElement(By.Id("TextBoxOfDatePicker"));
            //txtBxDatePicker.SendKeys("dd/mm/yyy");
            //DateTime datetime;
            //DateTime.TryParseExact(tb_date.Text, "yyyy-mm-dd", null, DateTimeStyles.None, out datetime);
            //date = datetime.ToString();
            //string time = tb_time.Text;
            string date = this.dtp_date.Value.ToString("yyyy-MM-dd");
            string time = this.dtp_time.Value.ToString("HH:mm");
            string exam_room = tb_exam_room.Text.ToUpper();
            string preparation_room = tb_preparation_room.Text.ToUpper();
            string student = tb_student.Text;  // check in db
            string teacher1 = tb_teacher1.Text;  // check in db
            string teacher2 = tb_teacher2.Text;  // check in db
            string teacher3 = tb_teacher3.Text;  // check in db
            string subject = cb_subject.Text;       // upper
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
