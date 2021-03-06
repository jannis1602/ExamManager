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
        FlowLayoutPanel panel_page;
        LinkedList<LinkedList<StudentObject>> chunkList;
        readonly LinkedList<FlowLayoutPanel> student_entity_list;
        private int edit_id = 0;
        private string grade = null;
        readonly string[] add_mode = { "Schüler hinzufügen", "Schüler übernehmen" };
        readonly LinkedList<int> studentIdList;
        int page = 1;
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
            LinkedList<StudentObject> allStudents = database.GetAllStudents();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (StudentObject s in allStudents)
                if (!gradeList.Contains(s.Grade))
                    gradeList.AddLast(s.Grade);
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList();
            gradeList = new LinkedList<string>(templist);
            string[] list = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                list[i] = gradeList.ElementAt(i);
            cb_grade.Items.AddRange(list);
            // grade tsmi
            tsmi_grade.DropDownItems.Clear();
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
                page = 1;
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                grade = tsmi.Name;
                UpdateStudentList();
            }
        }

        private void UpdateStudentList()
        {
            flp_student_entitys.Controls.Clear();
            student_entity_list.Clear();
            LinkedList<StudentObject> studentList = null;
            if (listOrder == Order.lastname) studentList = database.GetAllStudents();
            else if (listOrder == Order.firstname) studentList = database.GetAllStudents(true);

            int chunkLength = Properties.Settings.Default.EntitiesPerPage;
            chunkList = new LinkedList<LinkedList<StudentObject>>();
            chunkList.AddLast(new LinkedList<StudentObject>());
            foreach (StudentObject s in studentList)
            {
                string search = tstb_search.Text;
                if (grade == null || grade.Length < 1 || s.Grade == grade)
                    if (search.Length > 0 && (s.Firstname.ToLower().Contains(search.ToLower()) || s.Lastname.ToLower().Contains(search.ToLower())))
                    {
                        if (chunkList.Last.Value.Count() > chunkLength)
                            chunkList.AddLast(new LinkedList<StudentObject>());
                        chunkList.Last.Value.AddLast(s);
                    }
                    else if (search.Length == 0)
                    {
                        if (chunkList.Last.Value.Count() > chunkLength)
                            chunkList.AddLast(new LinkedList<StudentObject>());
                        chunkList.Last.Value.AddLast(s);
                    }
            }
            foreach (StudentObject s in chunkList.ElementAt(page - 1))
            {
                //if (grade == null || grade.Length < 1 || s.Grade == grade)
                if ((studentIdList != null && studentIdList.Contains(s.Id)) || studentIdList == null)
                {
                    FlowLayoutPanel panel_student = CreateEntityPanel(s.Id);
                    this.flp_student_entitys.HorizontalScroll.Value = 0;
                    student_entity_list.AddLast(panel_student);
                }
            }

            Control[] c = student_entity_list.ToArray();
            this.flp_student_entitys.Controls.AddRange(c);
            foreach (Panel p in student_entity_list)
                this.flp_student_entitys.SetFlowBreak(p, true);

            panel_page = new FlowLayoutPanel();
            panel_page.Width = flp_student_entitys.Width - 28;
            panel_page.Height = 50;
            panel_page.Margin = new Padding(5);
            panel_page.BackColor = Color.LightBlue;
            panel_page.Name = "pages";
            // --  lbl --
            Label lbl_page = new Label();
            lbl_page.Size = new Size(60, panel_page.Height);
            lbl_page.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular);
            lbl_page.Text = "Seite ";
            lbl_page.TextAlign = ContentAlignment.MiddleLeft;
            panel_page.Controls.Add(lbl_page);
            // -- BTN --
            for (int i = 1; i <= chunkList.Count; i++)
            {
                if (i != page)
                {
                    Button btn_page = new Button();
                    btn_page.Size = new Size(40, 30);
                    btn_page.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular);
                    btn_page.Text = i.ToString();
                    btn_page.Name = i.ToString();
                    btn_page.Margin = new Padding(4, 10, 4, 10);
                    btn_page.BackColor = Color.LightGray;
                    btn_page.Click += page_button_event;
                    panel_page.Controls.Add(btn_page);
                }
            }
            flp_student_entitys.Controls.Add(panel_page);

            void page_button_event(object sender, EventArgs e)
            {
                Button btn = sender as Button;
                page = int.Parse(btn.Name);
                UpdateStudentList();
            }
        }
        private FlowLayoutPanel CreateEntityPanel(int id)
        {
            StudentObject s = database.GetStudentByID(id);
            FlowLayoutPanel panel_student = new FlowLayoutPanel
            {
                Width = flp_student_entitys.Width - 28,
                Margin = new Padding(5),
                BackColor = Color.LightBlue,
                Name = s.Id.ToString()
            };
            // -- NAME --
            Label lbl_student_name = new Label
            {
                Size = new Size(180, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s.Firstname + " " + s.Lastname,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel_student.Controls.Add(lbl_student_name);
            // -- grade --
            Label lbl_student_grade = new Label
            {
                Size = new Size(60, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s.Grade,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel_student.Controls.Add(lbl_student_grade);
            // -- email --
            Label lbl_student_email = new Label
            {
                Size = new Size(220, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s.Email
            };
            if (s.Email.Length < 1 || s.Email == null)
                lbl_student_email.Text = "-";
            lbl_student_email.TextAlign = ContentAlignment.MiddleLeft;
            panel_student.Controls.Add(lbl_student_email);
            // -- phone --
            Label lbl_student_phone = new Label
            {
                Size = new Size(140, panel_student.Height),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = s.Phonenumber
            };
            if (s.Phonenumber.Length < 1 || s.Phonenumber == null)
                lbl_student_phone.Text = "-";
            lbl_student_phone.TextAlign = ContentAlignment.MiddleLeft;
            panel_student.Controls.Add(lbl_student_phone);
            // -- BTN edit --
            Button btn_student_edit = new Button
            {
                Size = new Size(100, panel_student.Height - 40),
                Font = new Font("Microsoft Sans Serif", 10F),
                Text = "Bearbeiten",
                Name = s.Id.ToString(),
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
                Name = s.Id.ToString(),
                Margin = new Padding(10, 20, 10, 20),
                BackColor = Color.LightGray
            };
            btn_student_delete.Click += btn_student_delete_Click;
            panel_student.Controls.Add(btn_student_delete);
            return panel_student;
        }
        #region Button
        private void btn_add_student_Click(object sender, EventArgs e)
        {
            string firstname = tb_firstname.Text;
            string lastname = tb_lastname.Text;
            string grade = cb_grade.Text;
            string email = tb_email.Text;
            string phonenumber = tb_phonenumber.Text;
            if (firstname.Length == 0 || lastname.Length == 0 || grade.Length == 0 || grade.Length == 0)
            { MessageBox.Show("Alle Felder mit * ausfüllen!", "Warnung"); return; }
            if (edit_id == 0) database.AddStudent(new StudentObject(0, firstname, lastname, grade, email, phonenumber));
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
            int id = database.GetStudentByName(firstname, lastname, grade).Id;
            if (studentIdList != null) studentIdList.AddLast(id);
            UpdateAutocomplete();
            UpdateStudentList();
            tb_firstname.Clear();
            tb_lastname.Clear();
            tb_email.Clear();
            tb_phonenumber.Clear();

            edit_id = 0;
            btn_add_student.Text = add_mode[0];
        }
        private void btn_student_delete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            StudentObject s = database.GetStudentByID(Int32.Parse(btn.Name));
            string name = s.Firstname + " " + s.Lastname;
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
            StudentObject s = database.GetStudentByID(Int32.Parse(btn.Name));
            tb_firstname.Text = s.Firstname;
            tb_lastname.Text = s.Lastname;
            cb_grade.Text = s.Grade;
            tb_email.Text = s.Email;
            tb_phonenumber.Text = s.Phonenumber;
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
            string domain = Properties.Settings.Default.EmailDomain;
            if (domain.Length < 2) MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung");
            else tb_email.Text = tb_firstname.Text.ToLower().Replace(' ', '.').Replace('_', '.') + "." + tb_lastname.Text.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
        }
        private void btn_hint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lehrzeichen vermeiden!\nDoppelnamen: name_name2", "Hinweis");
        }
        #endregion

        private void flp_student_entitys_SizeChanged(object sender, EventArgs e)
        {
            foreach (Panel p in student_entity_list)
                p.Width = flp_student_entitys.Width - 28;
            panel_page.Width = flp_student_entitys.Width - 28;
        }
        private void tb_firstname_TextChanged(object sender, EventArgs e)
        {
            if (tb_firstname.Text.Contains(' ') || tb_lastname.Text.Contains(' '))
                btn_hint.Visible = true;
            else btn_hint.Visible = false;
        }
        #region tsmi
        private void tsmi_tools_editgrade_Click(object sender, EventArgs e)
        {
            FormRenameGrade form = new FormRenameGrade();
            void update_Event(object se, EventArgs ea)
            { UpdateAutocomplete(); UpdateStudentList(); }
            form.FormClosed += update_Event;
            form.ShowDialog();
        }
        private void tsmi_tools_delete_grade_Click(object sender, EventArgs e)
        {
            FormDeleteGrade form = new FormDeleteGrade();
            void update_Event(object se, EventArgs ea)
            { UpdateAutocomplete(); UpdateStudentList(); }
            form.FormClosed += update_Event;
            form.ShowDialog();
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
        private void tsmi_search_doublenames_Click(object sender, EventArgs e)
        {
            foreach (FlowLayoutPanel flp in student_entity_list)
            {
                StudentObject student = database.GetStudentByID(Int32.Parse(flp.Name));
                if (!student.Firstname.ToLower().Contains("_") && !student.Lastname.ToLower().Contains("_") && !student.Firstname.ToLower().Contains(" ") && !student.Lastname.ToLower().Contains(" "))
                    flp.Hide();
            }
        }
        private void tsmi_search_delete_Click(object sender, EventArgs e)
        {
            foreach (FlowLayoutPanel flp in student_entity_list)
            {
                flp.Show();
            }
        }
        private void tsmi_generate_email_click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.EmailDomain.Length < 2) { MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung"); return; }
            DialogResult result = MessageBox.Show("Alle Schüler-Emails generieren?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (StudentObject so in database.GetAllStudents())
                {
                    so.GenerateEmail(true);
                }
                UpdateStudentList();
            }
        }
        #endregion

        private void tstb_search_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyData == Keys.Enter || (tstb_search.Text.Length == 1 && e.KeyData == Keys.Back))
            {
                UpdateStudentList();
            }
        }
    }
}