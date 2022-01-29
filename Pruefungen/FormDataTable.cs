using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class Form_grid : Form
    {
        DataGridView dataGridView;
        public Form_grid(int table)
        {
            InitializeComponent();
            if (table == 0)
            {
                DataGridView dataGridView_exam = new DataGridView();
                dataGridView_exam.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView_exam.Columns.Add("id", "id");
                dataGridView_exam.Columns.Add("date", "Datum");
                dataGridView_exam.Columns.Add("time", "Zeit");
                dataGridView_exam.Columns.Add("exam_room", "Prüfungsraum");
                dataGridView_exam.Columns.Add("preparation_room", "Vorbereitungsraum");
                dataGridView_exam.Columns.Add("student", "Schüler");
                dataGridView_exam.Columns.Add("teacher_vorsitz", "Lehrer-vorsitz");
                dataGridView_exam.Columns.Add("teacher_pruefer", "Lehrer-pruefer");
                dataGridView_exam.Columns.Add("teacher_protokoll", "Lehrer-protokoll");
                dataGridView_exam.Columns.Add("subject", "Fach");
                dataGridView_exam.Columns.Add("duration", "Dauer");
                dataGridView_exam.Dock = DockStyle.Fill;
                dataGridView_exam.Location = new System.Drawing.Point(0, 0);
                dataGridView_exam.Name = "dataGridView_exam";
                dataGridView_exam.Size = new System.Drawing.Size(1000, 500);
                this.Controls.Add(dataGridView_exam);
                List<string[]> data = new List<string[]>();
                foreach (string[] s in Program.database.GetAllExams())
                    data.Add(s);
                foreach (string[] s in data)
                {
                    string[] student = Program.database.GetStudentByID(Int32.Parse(s[5]));
                    dataGridView_exam.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5] + " -> " + student[1] + " " + student[2], s[6], s[7], s[8], s[9], s[10]);
                }
                dataGridView = dataGridView_exam;
            }
            else if (table == 1)
            {
                DataGridView dataGridView_students = new DataGridView();
                dataGridView_students.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView_students.Columns.Add("short_name", "Kürzel");
                dataGridView_students.Columns.Add("firstname", "Vorname");
                dataGridView_students.Columns.Add("lastname", "Nachname");
                dataGridView_students.Columns.Add("phone_number", "Telefonnummer");
                dataGridView_students.Columns.Add("subject1", "Fach1");
                dataGridView_students.Columns.Add("subject2", "Fach2");
                dataGridView_students.Columns.Add("subject3", "Fach3");
                dataGridView_students.Dock = DockStyle.Fill;
                dataGridView_students.Location = new System.Drawing.Point(0, 0);
                dataGridView_students.Name = "dataGridView_students";
                dataGridView_students.Size = new System.Drawing.Size(1000, 500);
                this.Controls.Add(dataGridView_students);
                List<string[]> data = new List<string[]>();
                foreach (string[] s in Program.database.GetAllTeachers())
                    data.Add(s);
                foreach (string[] s in data)
                {
                    dataGridView_students.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5], s[6]);
                }
                dataGridView = dataGridView_students;
            }
            else if (table == 2)
            {
                DataGridView dataGridView_teachers = new DataGridView();
                dataGridView_teachers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView_teachers.Columns.Add("id", "id");
                dataGridView_teachers.Columns.Add("firstname", "Vorname");
                dataGridView_teachers.Columns.Add("lastname", "Nachname");
                dataGridView_teachers.Columns.Add("grade", "Klasse");
                dataGridView_teachers.Columns.Add("email", "Email");
                dataGridView_teachers.Columns.Add("phone_number", "Telefonnummer");
                dataGridView_teachers.Dock = DockStyle.Fill;
                dataGridView_teachers.Location = new System.Drawing.Point(0, 0);
                dataGridView_teachers.Name = "dataGridView_teachers";
                dataGridView_teachers.Size = new System.Drawing.Size(1000, 500);
                this.Controls.Add(dataGridView_teachers);
                List<string[]> data = new List<string[]>();
                foreach (string[] s in Program.database.GetAllStudents())
                    data.Add(s);
                foreach (string[] s in data)
                {
                    dataGridView_teachers.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5]);
                }
                dataGridView = dataGridView_teachers;
            }
        }

        private void Form_grid_Load(object sender, EventArgs e)
        {

        }

        internal void Data_update()
        {
            dataGridView.Rows.Clear();
            List<string[]> data = new List<string[]>();
            foreach (string[] s in Program.database.GetAllExams())
                data.Add(s);
            foreach (string[] s in data)
            {
                string[] student = Program.database.GetStudentByID(Int32.Parse(s[5]));
                this.dataGridView.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5] + " -> " + student[1] + " " + student[2], s[6], s[7], s[8], s[9], s[10]);
            }
        }
    }
}
