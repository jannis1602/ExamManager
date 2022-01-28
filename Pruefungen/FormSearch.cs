using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class FormSearch : Form
    {
        Form1 form;
        public FormSearch(int mode, Form1 form)
        {
            this.form = form;
            InitializeComponent();
            if (mode == 0)
            {
                // ## [DEV] ##
                // TODO TEACHER
                var autocomplete_teacher = new AutoCompleteStringCollection();
                LinkedList<string[]> allTeachers = Program.database.GetAllTeachers();
                string[] students = new string[allTeachers.Count];
                for (int i = 0; i < allTeachers.Count; i++)
                    students[i] = (allTeachers.ElementAt(i)[1] + " " + allTeachers.ElementAt(i)[2]);
                autocomplete_teacher.AddRange(students);
                this.tb_search_string.AutoCompleteCustomSource = autocomplete_teacher;
                this.tb_search_string.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.tb_search_string.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else if (mode == 1)
            {
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = Program.database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                this.tb_search_string.AutoCompleteCustomSource = autocomplete_student;
                this.tb_search_string.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.tb_search_string.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else if (mode == 1)
            {
                // TODO: autocomplete subject
                // oder dropdown?

                /*var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = Program.database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                this.tb_search_string.AutoCompleteCustomSource = autocomplete_student;
                this.tb_search_string.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.tb_search_string.AutoCompleteSource = AutoCompleteSource.CustomSource;*/
            }
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_search_string.Text.Length > 0)
            {
                this.tb_search_string.Select(this.tb_search_string.Text.Length - 1, 0);
                form.search = tb_search_string.Text.First().ToString().ToUpper() + (tb_search_string.Text.Substring(1));
                form.update_timeline();
                this.Dispose();
            }
        }

        private void tb_search_string_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tb_search_string.Text.Length > 0)
                {
                    this.tb_search_string.Select(this.tb_search_string.Text.Length - 1, 0);
                    Console.WriteLine("SUCHE: " + tb_search_string.Text.First().ToString().ToUpper() + (tb_search_string.Text.Substring(1)));
                    form.search = tb_search_string.Text.First().ToString().ToUpper() + (tb_search_string.Text.Substring(1));
                    Console.WriteLine("#" + form.search + "#");
                    form.update_timeline();
                    this.Dispose();
                }
            }
        }
    }
}
