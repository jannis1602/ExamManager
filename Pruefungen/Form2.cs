using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class Form_grid : Form
    {
        public Form_grid()
        {
            InitializeComponent();
        }

        private void Form_grid_Load(object sender, EventArgs e)
        {
            DataGridView dataGridView_students = new DataGridView();
            dataGridView_students.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            /*this.dataGridView.Columns.Add(
            new DataGridViewColumn[] 
            this.id,
            this.date,
            this.time,
            this.exam_room,
            this.preparation_room,
            this.student,
            this.teacher_vorsitz,
            this.teacher_pruefer,
            this.teacher_protokoll,
            this.subject,
            this.duration});*/
            dataGridView_students.Columns.Add("id", "id");
            dataGridView_students.Columns.Add("firstname", "Vorname");
            dataGridView_students.Columns.Add("lastname", "Nachname");
            dataGridView_students.Columns.Add("grade", "Klasse");
            dataGridView_students.Columns.Add("email", "Email");
            dataGridView_students.Columns.Add("phone_number", "Telefonnummer");
            //dataGridView_students.Dock = DockStyle.Fill;
            dataGridView_students.Location = new System.Drawing.Point(0, 300);
            dataGridView_students.Name = "dataGridView_students";
            dataGridView_students.Size = new System.Drawing.Size(1000, 300);
            this.Controls.Add(dataGridView_students);

            /*id.HeaderText = "ID";
            id.Name = "id";
            id.ReadOnly = true;
            id.Visible = false;
            date.HeaderText = "Datum";
            time.HeaderText = "Zeit";
            exam_room.HeaderText = "Prüfungsraum";
            preparation_room.HeaderText = "Vorbereitungsraum";
            student.HeaderText = "Schüler";
            teacher_vorsitz.HeaderText = "Lehrer Vorsitz";
            teacher_pruefer.HeaderText = "Lehrer Prüfer";
            teacher_protokoll.HeaderText = "Lehrer Protokoll";
            subject.HeaderText = "Fach";
            duration.HeaderText = "Dauer";*/

            List<string[]> data = new List<string[]>();
            foreach (string[] s in Program.database.GetAllExams())
                data.Add(s);
            //data.Add(new string[] { "2022-02-11", "08:00" });
            foreach (string[] s in data)
            {
                string[] student = Program.database.GetStudentByID(Int32.Parse(s[5]));
                this.dataGridView1.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5] + " -> " + student[1] + " " + student[2], s[6], s[7], s[8], s[9], s[10]);
            }

        }

        internal void Data_update()
        {
            dataGridView1.Rows.Clear();
            List<string[]> data = new List<string[]>();
            foreach (string[] s in Program.database.GetAllExams())
                data.Add(s);
            //data.Add(new string[] { "2022-02-11", "08:00" });
            foreach (string[] s in data)
            {
                string[] student = Program.database.GetStudentByID(Int32.Parse(s[5]));
                this.dataGridView1.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5] + " -> " + student[1] + " " + student[2], s[6], s[7], s[8], s[9], s[10]);
            }
        }
    }
}
