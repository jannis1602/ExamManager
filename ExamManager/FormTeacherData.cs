using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormTeacherData : Form
    {
        Database database;
        LinkedList<FlowLayoutPanel> teacher_entity_list;
        string edit_id = null;
        string subject = null;
        string[] add_mode = { "Lehrer hinzufügen", "Lehrer übernehmen" };
        LinkedList<string> teacherIdList;
        public enum Order { firstname, lastname }
        public Order listOrder = Order.lastname;
        public FormTeacherData(LinkedList<string> teacherIdList = null)
        {
            database = Program.database;
            teacher_entity_list = new LinkedList<FlowLayoutPanel>();
            this.teacherIdList = teacherIdList;
            InitializeComponent();
            UpdateTeacherList();
            UpdateAutocomplete();
            cb_subject2.Items.Add("");
            cb_subject3.Items.Add("");
            LinkedList<string[]> subjectList = Program.database.GetAllSubjects();
            string[] subjects = new string[subjectList.Count];
            for (int i = 0; i < subjectList.Count; i++)
                subjects[i] = subjectList.ElementAt(i)[0];
            cb_subject1.Items.AddRange(subjects);
            cb_subject2.Items.AddRange(subjects);
            cb_subject3.Items.AddRange(subjects);
        }

        //private void UpdateAutocomplete()
        //{ }
        private void UpdateAutocomplete()
        {
            LinkedList<TeacherObject> allTeacher = database.GetAllTeachers();
            LinkedList<string> gradeList = new LinkedList<string>();
            foreach (TeacherObject s in allTeacher)
            {
                if (!gradeList.Contains(s.Subject1))
                    gradeList.AddLast(s.Subject1);
                if (s.Subject2.Length > 0 && !gradeList.Contains(s.Subject2))
                    gradeList.AddLast(s.Subject2);
                if (s.Subject3.Length > 0 && !gradeList.Contains(s.Subject3))
                    gradeList.AddLast(s.Subject3);
            }
            List<string> templist = new List<string>(gradeList);
            templist = templist.OrderBy(x => x).ToList();
            gradeList = new LinkedList<string>(templist);
            string[] list = new string[gradeList.Count];
            for (int i = 0; i < gradeList.Count; i++)
                list[i] = gradeList.ElementAt(i);
            // grade tsmi
            ToolStripMenuItem tsmi_grade_entity_clear = new ToolStripMenuItem
            { Name = null, Size = new Size(188, 22), Text = "Alle" };
            tsmi_grade_entity_clear.Click += new EventHandler(tsmi_grade_entity_click);
            tsmi_subject.DropDownItems.Add(tsmi_grade_entity_clear);
            foreach (string s in gradeList)
            {
                ToolStripMenuItem tsmi_grade_entity = new ToolStripMenuItem()
                { Name = s, Size = new Size(188, 22), Text = s };
                tsmi_grade_entity.Click += new EventHandler(tsmi_grade_entity_click);
                tsmi_subject.DropDownItems.Add(tsmi_grade_entity);
            }
            void tsmi_grade_entity_click(object sender, EventArgs e)
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                subject = tsmi.Name;
                UpdateTeacherList();
            }
        }




        private void UpdateTeacherList()
        {
            flp_teacher_entitys.Controls.Clear();
            teacher_entity_list.Clear();
            LinkedList<TeacherObject> teacherList = null;
            if (listOrder == Order.lastname) teacherList = database.GetAllTeachers();
            else if (listOrder == Order.lastname) teacherList = database.GetAllTeachers(true);

            foreach (TeacherObject s in teacherList)
            {
                if (subject == null || subject.Length < 1 || s.Subject1 == subject || s.Subject2 == subject || s.Subject3 == subject)
                    if ((teacherIdList != null && teacherIdList.Contains(s.Shortname)) || teacherIdList == null)
                    {
                        FlowLayoutPanel panel_teacher = CreateEntityPanel(s);
                        this.flp_teacher_entitys.HorizontalScroll.Value = 0;
                        teacher_entity_list.AddLast(panel_teacher);
                    }
            }

            foreach (Panel p in teacher_entity_list)
            {
                flp_teacher_entitys.Controls.Add(p);
                this.flp_teacher_entitys.SetFlowBreak(p, true);
            }
        }



        private FlowLayoutPanel CreateEntityPanel(TeacherObject t)
        {
            FlowLayoutPanel panel_teacher = new FlowLayoutPanel();
            panel_teacher.Width = flp_teacher_entitys.Width - 28;
            panel_teacher.Margin = new Padding(5);
            panel_teacher.BackColor = Color.LightBlue;
            panel_teacher.Name = t.Shortname;
            // -- NAME --
            Label lbl_teacher_name = new Label();
            lbl_teacher_name.Size = new Size(180, panel_teacher.Height);
            lbl_teacher_name.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_name.Text = t.Firstname + " " + t.Lastname;
            lbl_teacher_name.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_name);
            // -- shortname --
            Label lbl_teacher_shortname = new Label();
            lbl_teacher_shortname.Size = new Size(60, panel_teacher.Height);
            lbl_teacher_shortname.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_shortname.Text = t.Shortname;
            lbl_teacher_shortname.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_shortname);
            // -- email --
            Label lbl_teacher_email = new Label();
            lbl_teacher_email.Size = new Size(140, panel_teacher.Height);
            lbl_teacher_email.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_email.Text = t.Email;
            if (t.Email.Length < 1 || t.Email == null)
                lbl_teacher_email.Text = "-";
            lbl_teacher_email.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_email);
            // -- phone --
            Label lbl_teacher_phone = new Label();
            lbl_teacher_phone.Size = new Size(140, panel_teacher.Height);
            lbl_teacher_phone.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_phone.Text = t.Phonenumber;
            if (t.Phonenumber.Length < 1 || t.Phonenumber == null)
                lbl_teacher_phone.Text = "-";
            lbl_teacher_phone.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_phone);
            // -- subjects --
            for (int i = 0; i < 3; i++)
            {
                string subject = null;
                if (i == 0) subject = t.Subject1;
                if (i == 1) subject = t.Subject2;
                if (i == 2) subject = t.Subject3;
                Label lbl_teacher_subject = new Label();
                lbl_teacher_subject.Size = new Size(100, panel_teacher.Height);
                lbl_teacher_subject.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                lbl_teacher_subject.Text = subject;
                if (subject.Length < 1 || subject == null)
                    lbl_teacher_subject.Text = "-";
                lbl_teacher_subject.TextAlign = ContentAlignment.MiddleLeft;
                panel_teacher.Controls.Add(lbl_teacher_subject);
            }
            // -- BTN edit --
            Button btn_teacher_edit = new Button();
            btn_teacher_edit.Size = new Size(100, panel_teacher.Height - 40);
            btn_teacher_edit.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn_teacher_edit.Text = "Bearbeiten";
            btn_teacher_edit.Name = t.Shortname;
            btn_teacher_edit.Margin = new Padding(10, 20, 10, 20);
            btn_teacher_edit.BackColor = Color.LightGray;
            btn_teacher_edit.Click += btn_teacher_edit_Click;
            panel_teacher.Controls.Add(btn_teacher_edit);
            // -- BTN delete --
            Button btn_teacher_delete = new Button();
            btn_teacher_delete.Size = new Size(100, panel_teacher.Height - 40);
            btn_teacher_delete.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn_teacher_delete.Text = "Löschen";
            btn_teacher_delete.Name = t.Shortname;
            btn_teacher_delete.Margin = new Padding(10, 20, 10, 20);
            btn_teacher_delete.BackColor = Color.LightGray;
            btn_teacher_delete.Click += btn_teacher_delete_Click;
            panel_teacher.Controls.Add(btn_teacher_delete);
            teacher_entity_list.AddLast(panel_teacher);
            panel_teacher.Controls.Add(btn_teacher_delete);
            return panel_teacher;
        }

        private void AddTeacherEntity(string id)
        {
            TeacherObject t = database.GetTeacherByID(id.ToString());
            FlowLayoutPanel panel_teacher = new FlowLayoutPanel();
            panel_teacher.Width = flp_teacher_entitys.Width - 28;
            panel_teacher.Margin = new Padding(5);
            panel_teacher.BackColor = Color.LightBlue;
            panel_teacher.Name = t.Shortname;
            // -- NAME --
            Label lbl_teacher_name = new Label();
            lbl_teacher_name.Size = new Size(180, panel_teacher.Height);
            lbl_teacher_name.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_name.Text = t.Firstname + " " + t.Lastname;
            lbl_teacher_name.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_name);
            // -- shortname --
            Label lbl_teacher_shortname = new Label();
            lbl_teacher_shortname.Size = new Size(60, panel_teacher.Height);
            lbl_teacher_shortname.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_shortname.Text = t.Shortname;
            lbl_teacher_shortname.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_shortname);
            // -- email --
            Label lbl_teacher_email = new Label();
            lbl_teacher_email.Size = new Size(140, panel_teacher.Height);
            lbl_teacher_email.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_email.Text = t.Email;
            if (t.Email.Length < 1 || t.Email == null)
                lbl_teacher_email.Text = "-";
            lbl_teacher_email.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_email);
            // -- phone --
            Label lbl_teacher_phone = new Label();
            lbl_teacher_phone.Size = new Size(140, panel_teacher.Height);
            lbl_teacher_phone.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_phone.Text = t.Phonenumber;
            if (t.Phonenumber.Length < 1 || t.Phonenumber == null)
                lbl_teacher_phone.Text = "-";
            lbl_teacher_phone.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_phone);
            // -- subjects --
            for (int i = 0; i < 3; i++)
            {
                string subject = null;
                if (i == 0) subject = t.Subject1;
                if (i == 1) subject = t.Subject2;
                if (i == 2) subject = t.Subject3;
                Label lbl_teacher_subject = new Label();
                lbl_teacher_subject.Size = new Size(100, panel_teacher.Height);
                lbl_teacher_subject.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                lbl_teacher_subject.Text = subject;
                if (subject.Length < 1 || subject == null)
                    lbl_teacher_subject.Text = "-";
                lbl_teacher_subject.TextAlign = ContentAlignment.MiddleLeft;
                panel_teacher.Controls.Add(lbl_teacher_subject);
            }
            // -- BTN edit --
            Button btn_teacher_edit = new Button();
            btn_teacher_edit.Size = new Size(100, panel_teacher.Height - 40);
            btn_teacher_edit.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn_teacher_edit.Text = "Bearbeiten";
            btn_teacher_edit.Name = t.Shortname;
            btn_teacher_edit.Margin = new Padding(10, 20, 10, 20);
            btn_teacher_edit.BackColor = Color.LightGray;
            btn_teacher_edit.Click += btn_teacher_edit_Click;
            panel_teacher.Controls.Add(btn_teacher_edit);
            // -- BTN delete --
            Button btn_teacher_delete = new Button();
            btn_teacher_delete.Size = new Size(100, panel_teacher.Height - 40);
            btn_teacher_delete.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn_teacher_delete.Text = "Löschen";
            btn_teacher_delete.Name = t.Shortname;
            btn_teacher_delete.Margin = new Padding(10, 20, 10, 20);
            btn_teacher_delete.BackColor = Color.LightGray;
            btn_teacher_delete.Click += btn_teacher_delete_Click;
            panel_teacher.Controls.Add(btn_teacher_delete);
            teacher_entity_list.AddLast(panel_teacher);

            /*List<FlowLayoutPanel> temp_panel_list = new List<FlowLayoutPanel>(teacher_entity_list);
            temp_panel_list = temp_panel_list.OrderBy(x => x.Name).ToList();
            teacher_entity_list = new LinkedList<FlowLayoutPanel>(temp_panel_list);*/
            flp_teacher_entitys.Controls.Clear();
            foreach (Panel p in teacher_entity_list)
            {
                this.flp_teacher_entitys.Controls.Add(p);
                this.flp_teacher_entitys.SetFlowBreak(p, true);
            }
        }

        private void btn_teacher_delete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TeacherObject t = database.GetTeacherByID(btn.Name);
            string name = t.Firstname + " " + t.Lastname;
            DialogResult result = MessageBox.Show("Lehrer " + name + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                edit_id = null;
                database.DeleteTeacher(btn.Name);
                foreach (FlowLayoutPanel flp in teacher_entity_list)
                {
                    if (flp.Name == btn.Name)
                    {
                        flp.Dispose();
                        flp_teacher_entitys.Update();
                        break;
                    }
                }
            }
            UpdateTeacherList();
        }

        private void btn_teacher_edit_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            edit_id = btn.Name;
            btn_add_teacher.Text = add_mode[1];
            tb_shortname.ReadOnly = true;
            TeacherObject t = database.GetTeacherByID(btn.Name);
            tb_shortname.Text = t.Shortname;
            tb_firstname.Text = t.Firstname;
            tb_lastname.Text = t.Lastname;
            tb_email.Text = t.Email;
            tb_phonenumber.Text = t.Phonenumber;
            cb_subject1.Text = t.Subject1;
            cb_subject2.Text = t.Subject2;
            cb_subject3.Text = t.Subject3;
        }

        private void btn_add_teacher_Click(object sender, EventArgs e)
        {
            string shortname = tb_shortname.Text.Replace(" ", "");
            string firstname = tb_firstname.Text;
            string lastname = tb_lastname.Text;
            string email = tb_email.Text;
            string phonenumber = tb_phonenumber.Text;
            string subject1 = cb_subject1.Text;
            string subject2 = cb_subject2.Text;
            string subject3 = cb_subject3.Text;
            if (shortname.Length == 0 || firstname.Length == 0 || lastname.Length == 0 || subject1.Length == 0) // phonenumber.Length == 0 ||
            {
                MessageBox.Show("Alle Felder mit * ausfüllen!", "Warnung"); return;
            }
            if (edit_id == null)
            {
                if (database.GetTeacherByID(shortname) != null)
                { MessageBox.Show("Lehrer schon vorhanden", "Warnung"); return; }
                database.AddTeacher(new TeacherObject(shortname, firstname, lastname, email, phonenumber, subject1, subject2, subject3)); // TODO: email
            }
            else
            {
                if (database.GetTeacherByID(shortname) == null)
                { MessageBox.Show("Lehrer nicht vorhanden", "Warnung"); return; }
                database.EditTeacher(shortname, firstname, lastname, email, phonenumber, subject1, subject2, subject3);
                foreach (FlowLayoutPanel flp in teacher_entity_list)
                {
                    if (flp.Name == edit_id)
                    {
                        flp.Dispose();
                        flp_teacher_entitys.Update();
                        break;
                    }
                }
            }
            //UpdateTeacherList();
            AddTeacherEntity(shortname);
            tb_shortname.Clear();
            tb_firstname.Clear();
            tb_lastname.Clear();
            tb_email.Clear();
            tb_phonenumber.Clear();
            cb_subject1.SelectedItem = null;
            cb_subject2.SelectedItem = null;
            cb_subject3.SelectedItem = null;

            edit_id = null;
            btn_add_teacher.Text = add_mode[0];
            tb_shortname.ReadOnly = false;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            tb_shortname.Clear();
            tb_firstname.Clear();
            tb_lastname.Clear();
            tb_email.Clear();
            tb_phonenumber.Clear();
            cb_subject1.SelectedItem = null;
            cb_subject2.SelectedItem = null;
            cb_subject3.SelectedItem = null;

            edit_id = null;
            btn_add_teacher.Text = add_mode[0];
            tb_shortname.ReadOnly = false;
        }

        private void flp_teacher_entitys_SizeChanged(object sender, EventArgs e)
        {
            foreach (Panel p in teacher_entity_list)
                p.Width = flp_teacher_entitys.Width - 28;
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

        private void tstb_search_TextChanged(object sender, EventArgs e)
        {
            string search = tstb_search.Text;
            if (search.Length > 0)
            {
                foreach (FlowLayoutPanel flp in teacher_entity_list)
                {
                    TeacherObject teacher = database.GetTeacherByID(flp.Name);
                    if (!teacher.Shortname.ToLower().Contains(search.ToLower()) && !teacher.Firstname.ToLower().Contains(search.ToLower()) && !teacher.Lastname.ToLower().Contains(search.ToLower()))
                        flp.Hide();
                    else if (teacher.Shortname.ToLower().Contains(search.ToLower()) || teacher.Firstname.ToLower().Contains(search.ToLower()) || teacher.Lastname.ToLower().Contains(search.ToLower()))
                        flp.Show();
                }
            }
            else foreach (FlowLayoutPanel flp in teacher_entity_list) flp.Show();
        }

        private void tsmi_sort_lastname_Click(object sender, EventArgs e)
        {
            listOrder = Order.lastname;
            UpdateTeacherList();
        }

        private void tsmi_sort_firstname_Click(object sender, EventArgs e)
        {
            listOrder = Order.firstname;
            UpdateTeacherList();
        }
        private void tsmi_search_doublenames_Click(object sender, EventArgs e)
        {
            foreach (FlowLayoutPanel flp in teacher_entity_list)
            {
                TeacherObject teacher = database.GetTeacherByID(flp.Name);
                if (!teacher.Firstname.ToLower().Contains("_") && !teacher.Lastname.ToLower().Contains("_") && !teacher.Firstname.ToLower().Contains(" ") && !teacher.Lastname.ToLower().Contains(" "))
                    flp.Hide();
            }
        }

        private void tsmi_search_delete_Click(object sender, EventArgs e)
        {
            foreach (FlowLayoutPanel flp in teacher_entity_list)
            {
                flp.Show();
            }
        }

        private void btn_email_generate_Click(object sender, EventArgs e)
        {
            string domain = Properties.Settings.Default.email_domain;
            if (domain.Length < 2) MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung");
            tb_email.Text = tb_firstname.Text.ToLower().Replace(' ', '.').Replace('_', '.') + "." + tb_lastname.Text.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
        }

        private void tsmi_generate_email_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.email_domain.Length < 2) { MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung"); return; }
            DialogResult result = MessageBox.Show("Alle Lehrer-Emails generieren?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (TeacherObject to in database.GetAllTeachers())
                {
                    to.GenerateEmail(true);
                }
                UpdateTeacherList();
            }
        }
    }
}
