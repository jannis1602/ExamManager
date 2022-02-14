using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormStudentData : Form
    {
        readonly Database database;
        readonly LinkedList<FlowLayoutPanel> student_entity_list;
        private int edit_id = 0;
        private string grade = null;
        readonly string[] add_mode = { "Schüler hinzufügen", "Schüler übernehmen" };
        readonly LinkedList<int> studentIdList;
        public enum Order { firstname, lastname }
        public Order listOrder = Order.lastname;
        public FormStudentData(LinkedList<int> studentIdList = null)
        {
            database = Program.database;
            student_entity_list = new LinkedList<FlowLayoutPanel>();
            this.studentIdList = studentIdList;
            InitializeComponent();
            UpdateStudentList();
            UpdateAutocomplete();
        }
        private void UpdateAutocomplete()
        {
            LinkedList<string[]> allStudents = database.GetAllStudents();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (string[] s in allStudents)
                if (!gradeList.Contains(s[3]))
                    gradeList.AddLast(s[3]);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList();
            gradeList = new LinkedList<string>(templist);
            string[] list = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                list[i] = gradeList.ElementAt(i);
            cb_grade.Items.AddRange(list);
            // grade tsmi
            ToolStripMenuItem tsmi_grade_entity_clear = new ToolStripMenuItem
            { Name = null, Size = new Size(188, 22), Text = "Alle" };
            tsmi_grade_entity_clear.Click += new EventHandler(tsmi_grade_entity_click);
            tsmi_grade.DropDownItems.Add(tsmi_grade_entity_clear);
            foreach (string s in gradeList)
            {
                ToolStripMenuItem tsmi_grade_entity = new ToolStripMenuItem()
                { Name = s, Size = new Size(188, 22), Text = s };
                tsmi_grade_entity.Click += new EventHandler(tsmi_grade_entity_click);
                tsmi_grade.DropDownItems.Add(tsmi_grade_entity);
            }
            void tsmi_grade_entity_click(object sender, EventArgs e)
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                grade = tsmi.Name;
                UpdateStudentList();
            }
        }

        private void UpdateStudentList()
        {
            flp_student_entitys.Controls.Clear();
            student_entity_list.Clear();
            LinkedList<string[]> studentList = null;
            if (listOrder == Order.lastname) studentList = database.GetAllStudents();
            else if (listOrder == Order.lastname) studentList = database.GetAllStudents(true);

            foreach (string[] s in studentList)
            {
                if (grade == null || s[3] == grade)
                    if ((studentIdList != null && studentIdList.Contains(Int32.Parse(s[0]))) || studentIdList == null)
                    {
                        FlowLayoutPanel panel_student = CreateEntityPanel(Int32.Parse(s[0]));
                        this.flp_student_entitys.HorizontalScroll.Value = 0;
                        student_entity_list.AddLast(panel_student);
                    }
            }

            /*List<FlowLayoutPanel> temp_panel_list = new List<FlowLayoutPanel>(student_entity_list);
            temp_panel_list = temp_panel_list.OrderBy(x => x.Name).ToList(); // .ThenBy( x => x.Bar)
            student_entity_list = new LinkedList<FlowLayoutPanel>(temp_panel_list);*/
            Control[] c = student_entity_list.ToArray();
            this.flp_student_entitys.Controls.AddRange(c);
            foreach (Panel p in student_entity_list)
                this.flp_student_entitys.SetFlowBreak(p, true);
        }

        private void AddStudentEntity(int id)
        {
            student_entity_list.AddLast(CreateEntityPanel(id));
            flp_student_entitys.Controls.Clear();
            foreach (Panel p in student_entity_list)
            {
                this.flp_student_entitys.Controls.Add(p);
                this.flp_student_entitys.SetFlowBreak(p, true);
            }
        }

        private FlowLayoutPanel CreateEntityPanel(int id)
        {
            string[] s = database.GetStudentByID(id);
            FlowLayoutPanel panel_student = new FlowLayoutPanel
            {
                Width = flp_student_entitys.Width - 28,
                Margin = new Padding(5),
                BackColor = Color.LightBlue,
                Name = s[0]
            };
            // -- NAME --
            Label lbl_student_name = new Label
            {
                Size = new Size(180, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s[1] + " " + s[2],
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel_student.Controls.Add(lbl_student_name);
            // -- grade --
            Label lbl_student_grade = new Label
            {
                Size = new Size(60, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s[3],
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel_student.Controls.Add(lbl_student_grade);
            // -- email --
            Label lbl_student_email = new Label
            {
                Size = new Size(220, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s[4]
            };
            if (s[4].Length < 1 || s[4] == null)
                lbl_student_email.Text = "-";
            lbl_student_email.TextAlign = ContentAlignment.MiddleLeft;
            panel_student.Controls.Add(lbl_student_email);
            // -- phone --
            Label lbl_student_phone = new Label
            {
                Size = new Size(140, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s[5]
            };
            if (s[5].Length < 1 || s[5] == null)
                lbl_student_phone.Text = "-";
            lbl_student_phone.TextAlign = ContentAlignment.MiddleLeft;
            panel_student.Controls.Add(lbl_student_phone);
            // -- BTN edit --
            Button btn_student_edit = new Button
            {
                Size = new Size(100, panel_student.Height - 40),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = "Bearbeiten",
                Name = s[0],
                Margin = new Padding(10, 20, 10, 20),
                BackColor = Color.LightGray
            };
            btn_student_edit.Click += btn_student_edit_Click;
            panel_student.Controls.Add(btn_student_edit);
            // -- BTN delete --
            Button btn_student_delete = new Button
            {
                Size = new Size(100, panel_student.Height - 40),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = "Löschen",
                Name = s[0],
                Margin = new Padding(10, 20, 10, 20),
                BackColor = Color.LightGray
            };
            btn_student_delete.Click += btn_student_delete_Click;
            panel_student.Controls.Add(btn_student_delete);
            return panel_student;
        }

        private void btn_student_delete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string[] t = database.GetStudentByID(Int32.Parse(btn.Name));
            string name = t[1] + " " + t[2];
            DialogResult result = MessageBox.Show("Schüler " + name + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                database.DeleteStudent(Int32.Parse(btn.Name));
                foreach (FlowLayoutPanel flp in student_entity_list)
                {
                    if (flp.Name == btn.Name)
                    {
                        flp.Dispose();
                        flp_student_entitys.Update();
                        break;
                    }
                }
            }
        }

        private void btn_student_edit_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            edit_id = Int32.Parse(btn.Name);
            btn_add_student.Text = add_mode[1];
            string[] t = database.GetStudentByID(Int32.Parse(btn.Name));
            tb_firstname.Text = t[1];
            tb_lastname.Text = t[2];
            cb_grade.Text = t[3];
            tb_email.Text = t[4];
            tb_phonenumber.Text = t[5];
        }

        private void btn_add_student_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string firstname = tb_firstname.Text;
            string lastname = tb_lastname.Text;
            string grade = cb_grade.Text;
            string email = tb_email.Text;
            string phonenumber = tb_phonenumber.Text;
            if (firstname.Length == 0 || lastname.Length == 0 || grade.Length == 0 || grade.Length == 0)
            { MessageBox.Show("Alle Felder mit * ausfüllen!", "Warnung"); return; }
            if (edit_id == 0) database.AddStudent(firstname, lastname, grade, email, phonenumber);
            else
            {
                database.EditStudent(edit_id, firstname, lastname, grade, email, phonenumber);
                foreach (FlowLayoutPanel flp in student_entity_list)
                {
                    if (Int32.Parse(flp.Name) == edit_id)
                    {
                        flp.Dispose();
                        flp_student_entitys.Update();
                        break;
                    }
                }
            }
            int id = Int32.Parse(database.GetStudent(firstname, lastname, grade)[0]);
            if (studentIdList != null) studentIdList.AddLast(id);
            UpdateAutocomplete();
            AddStudentEntity(id);
            tb_firstname.Clear();
            tb_lastname.Clear();
            tb_email.Clear();
            tb_phonenumber.Clear();

            edit_id = 0;
            btn_add_student.Text = add_mode[0];
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cb_grade.Text = null;
            tb_firstname.Clear();
            tb_lastname.Clear();
            tb_email.Clear();
            tb_phonenumber.Clear();

            edit_id = 0;
            btn_add_student.Text = add_mode[0];
        }

        private void btn_email_generate_Click(object sender, EventArgs e)
        {
            string domain = Properties.Settings.Default.email_domain;
            if (domain.Length < 2) MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung");
            tb_email.Text = tb_firstname.Text.ToLower().Replace(' ', '.').Replace('_', '.') + "." + tb_lastname.Text.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
        }

        private void flp_student_entitys_SizeChanged(object sender, EventArgs e)
        {
            foreach (Panel p in student_entity_list)
                p.Width = flp_student_entitys.Width - 28;
        }
        private void tb_firstname_TextChanged(object sender, EventArgs e)
        {
            if (tb_firstname.Text.Contains(' ') || tb_lastname.Text.Contains(' '))
                btn_hint.Visible = true;
            else btn_hint.Visible = false;
        }
        private void btn_hint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lehrzeichen vermeiden!\nDoppelnamen: name_name2", "Hinweis");
        }
        private void tsmi_sort_lastname_Click(object sender, EventArgs e)
        {
            listOrder = Order.lastname;
            UpdateStudentList();
        }
        private void tsmi_sort_firstname_Click(object sender, EventArgs e)
        {
            listOrder = Order.firstname;
            UpdateStudentList();
        }

        private void tstb_search_TextChanged(object sender, EventArgs e)
        {
            string search = tstb_search.Text;
            if (search.Length > 0)
            {
                foreach (FlowLayoutPanel flp in student_entity_list)
                {
                    string[] student = database.GetStudentByID(Int32.Parse(flp.Name));
                    if (!student[1].ToLower().Contains(search.ToLower()) && !student[2].ToLower().Contains(search.ToLower()))
                        flp.Hide();
                    else if (student[1].ToLower().Contains(search.ToLower()) || student[2].ToLower().Contains(search.ToLower()))
                        flp.Show();
                }
            }
            else foreach (FlowLayoutPanel flp in student_entity_list) flp.Show();

        }
    }
}