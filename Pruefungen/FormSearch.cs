using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class FormSearch : Form
    {
        Form1 form;
        TextBox tb_search;
        ComboBox cb_search;
        public FormSearch(int mode, Form1 form)
        {
            this.form = form;
            InitializeComponent();
            if (mode == 0)
            {
                // ## [DEV] ##
                // TODO TEACHER
                //
                tb_search = new TextBox();
                tb_search.Dock = DockStyle.Fill;
                tb_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                tb_search.Location = new System.Drawing.Point(30, 20);
                tb_search.Margin = new Padding(30, 20, 30, 5);
                tb_search.Name = "tb_search";
                tb_search.Size = new System.Drawing.Size(324, 26);
                tb_search.TabIndex = 0;
                tb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(tb_search);
                //
                var autocomplete_teacher = new AutoCompleteStringCollection();
                LinkedList<string[]> allTeachers = Program.database.GetAllTeachers();
                string[] students = new string[allTeachers.Count];
                for (int i = 0; i < allTeachers.Count; i++)
                    students[i] = (allTeachers.ElementAt(i)[1] + " " + allTeachers.ElementAt(i)[2]);
                autocomplete_teacher.AddRange(students);
                tb_search.AutoCompleteCustomSource = autocomplete_teacher;
                tb_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tb_search.AutoCompleteSource = AutoCompleteSource.CustomSource;
                form.search_index = 0;
            }
            else if (mode == 1) // student
            {
                tb_search = new TextBox();
                tb_search.Dock = DockStyle.Fill;
                tb_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                tb_search.Location = new System.Drawing.Point(30, 20);
                tb_search.Margin = new Padding(30, 20, 30, 5);
                tb_search.Name = "tb_search";
                tb_search.Size = new System.Drawing.Size(324, 26);
                tb_search.TabIndex = 0;
                tb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(tb_search);
                //
                var autocomplete_student = new AutoCompleteStringCollection();
                LinkedList<string[]> allStudents = Program.database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = (allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2]);
                autocomplete_student.AddRange(students);
                tb_search.AutoCompleteCustomSource = autocomplete_student;
                tb_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tb_search.AutoCompleteSource = AutoCompleteSource.CustomSource;
                form.search_index = 1;
            }
            else if (mode == 2) // subject
            {
                cb_search = new ComboBox();
                cb_search.Dock = DockStyle.Fill;
                cb_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                cb_search.Location = new System.Drawing.Point(30, 20);
                cb_search.Margin = new Padding(30, 20, 30, 5);
                cb_search.Name = "tb_search";
                cb_search.Size = new System.Drawing.Size(324, 26);
                cb_search.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_search.TabIndex = 0;
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //cb_search
                LinkedList<string[]> subjectList = Program.database.GetAllSubjects();
                string[] subjects = new string[subjectList.Count];
                for (int i = 0; i < subjectList.Count; i++)
                    subjects[i] = subjectList.ElementAt(i)[0];
                cb_search.Items.AddRange(subjects);
                form.search_index = 2;
            }
            else if (mode == 3) // room
            {
                cb_search = new ComboBox();
                cb_search.Dock = DockStyle.Fill;
                cb_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                cb_search.Location = new System.Drawing.Point(30, 20);
                cb_search.Margin = new Padding(30, 20, 30, 5);
                cb_search.Name = "tb_search";
                cb_search.Size = new System.Drawing.Size(324, 26);
                cb_search.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_search.TabIndex = 0;
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //cb_search
                LinkedList<string[]> roomList = Program.database.GetAllRooms();
                string[] rooms = new string[roomList.Count];
                for (int i = 0; i < roomList.Count; i++)
                    rooms[i] = roomList.ElementAt(i)[0];
                cb_search.Items.AddRange(rooms);
                form.search_index = 3;
            }
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_search != null)
                if (tb_search.Text.Length > 0)
                {
                    this.tb_search.Select(this.tb_search.Text.Length - 1, 0);
                    form.search = tb_search.Text.First().ToString().ToUpper() + (tb_search.Text.Substring(1));
                    form.update_timeline();
                    this.Dispose();
                }
            if (cb_search != null)
                if (cb_search.Text.Length > 0)
                {
                    this.cb_search.Select(this.cb_search.Text.Length - 1, 0);
                    form.search = cb_search.Text;
                    form.update_timeline();
                    this.Dispose();
                }
        }

        private void tb_search_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tb_search != null)
                    if (tb_search.Text.Length > 0)
                    {
                        tb_search.Select(tb_search.Text.Length - 1, 0);
                        form.search = tb_search.Text.First().ToString().ToUpper() + (tb_search.Text.Substring(1));
                        form.update_timeline();
                        this.Dispose();
                    }
                if (cb_search != null)
                    if (cb_search.Text.Length > 0)
                    {
                        cb_search.Select(cb_search.Text.Length - 1, 0);
                        form.search = cb_search.Text.First().ToString().ToUpper() + (cb_search.Text.Substring(1));
                        form.update_timeline();
                        this.Dispose();
                    }
            }
        }
    }
}
