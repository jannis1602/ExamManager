using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExamManager
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
                dataGridView_exam.Columns.Add("student2", "Schüler2");
                dataGridView_exam.Columns.Add("student3", "Schüler3");
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
                List<ExamObject> data = new List<ExamObject>();
                foreach (ExamObject s in Program.database.GetAllExams())
                    data.Add(s);
                foreach (ExamObject s in data)
                {
                    StudentObject s1 = s.Student;
                    StudentObject s2 = s.Student2;
                    StudentObject s3 = s.Student3;
                    if (s1 == null) s1 = new StudentObject(0, "ID " + s.StudentId + " nicht gefunden", "nicht gefunden", "-");
                    if (s2 == null) s2 = new StudentObject(0, "-", "-", "-");
                    if (s3 == null) s3 = new StudentObject(0, "-", "-", "-");
                    /*TeacherObject t1 = s.Teacher1;
                    TeacherObject t2 = s.Teacher2;
                    TeacherObject t3 = s.Teacher3;
                    if (s1 == null) t1 = new TeacherObject("-", "nicht gefunden", "nicht gefunden", "-", "-", "-", "-", "-");
                    if (s2 == null) t2 = new TeacherObject("-", "nicht gefunden", "nicht gefunden", "-", "-", "-", "-", "-");
                    if (s3 == null) t3 = new TeacherObject("-", "nicht gefunden", "nicht gefunden", "-", "-", "-", "-", "-");*/
                    dataGridView_exam.Rows.Add(s.Id, s.Date, s.Time, s.Examroom, s.Preparationroom, s1.Fullname(), s2.Fullname(), s3.Fullname(), s.Teacher1, s.Teacher2, s.Teacher3, s.Subject, s.Duration);
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
                dataGridView_students.Columns.Add("email", "Email");
                dataGridView_students.Columns.Add("phone_number", "Telefonnummer");
                dataGridView_students.Columns.Add("subject1", "Fach1");
                dataGridView_students.Columns.Add("subject2", "Fach2");
                dataGridView_students.Columns.Add("subject3", "Fach3");
                dataGridView_students.Dock = DockStyle.Fill;
                dataGridView_students.Location = new System.Drawing.Point(0, 0);
                dataGridView_students.Name = "dataGridView_students";
                dataGridView_students.Size = new System.Drawing.Size(1000, 500);
                this.Controls.Add(dataGridView_students);
                List<TeacherObject> data = new List<TeacherObject>();
                foreach (TeacherObject s in Program.database.GetAllTeachers())
                    data.Add(s);
                foreach (TeacherObject t in data)
                {
                    dataGridView_students.Rows.Add(t.Shortname, t.Firstname, t.Lastname, t.Email, t.Phonenumber, t.Subject1, t.Subject2, t.Subject3);
                }
                dataGridView = dataGridView_students;
            }
            else if (table == 2)
            {
                DataGridView dataGridView_teachers = new DataGridView();
                dataGridView_teachers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView_teachers.Columns.Add("id", "Id");
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
                List<StudentObject> data = new List<StudentObject>();
                foreach (StudentObject s in Program.database.GetAllStudents())
                    data.Add(s);
                foreach (StudentObject s in data)
                {
                    dataGridView_teachers.Rows.Add(s.Id, s.Firstname, s.Lastname, s.Grade, s.Email, s.Phonenumber);
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
            List<ExamObject> data = new List<ExamObject>();
            foreach (ExamObject s in Program.database.GetAllExams())
                data.Add(s);
            foreach (ExamObject s in data)
            {
                StudentObject student = s.Student;
                this.dataGridView.Rows.Add(s.Id, s.Date, s.Time, s.Examroom, s.Preparationroom, s.StudentId + " -> " + student.Firstname + " " + student.Lastname, s.Teacher1, s.Teacher2, s.Teacher3, s.Subject, s.Duration);
            }
        }
    }
}
