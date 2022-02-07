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
        string[] add_mode = { "Lehrer hinzufügen", "Lehrer übernehmen" };
        LinkedList<string> teacherIdList;
        public FormTeacherData(LinkedList<string> teacherIdList = null)
        {
            database = Program.database;
            teacher_entity_list = new LinkedList<FlowLayoutPanel>();
            this.teacherIdList = teacherIdList;
            InitializeComponent();
            UpdateTeacherList();
            //cb_subject1.Items.Add("");
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

        private void UpdateTeacherList()
        {
            //foreach (FlowLayoutPanel p in teacher_entity_list) p.Dispose();
            flp_teacher_entitys.Controls.Clear();

            teacher_entity_list.Clear();
            foreach (string[] s in database.GetAllTeachers())
            {
                if ((teacherIdList != null && teacherIdList.Contains(s[0])) || teacherIdList == null)
                {
                    FlowLayoutPanel panel_teacher = new FlowLayoutPanel();
                    //panel_teacher.Height = 80;
                    panel_teacher.Width = flp_teacher_entitys.Width - 28;
                    panel_teacher.Margin = new Padding(5);
                    panel_teacher.BackColor = Color.LightBlue;
                    panel_teacher.Name = s[0];
                    // -- NAME --
                    Label lbl_teacher_name = new Label();
                    lbl_teacher_name.Size = new Size(180, panel_teacher.Height);
                    lbl_teacher_name.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_teacher_name.Text = s[1] + " " + s[2];
                    lbl_teacher_name.TextAlign = ContentAlignment.MiddleLeft;
                    panel_teacher.Controls.Add(lbl_teacher_name);
                    // -- shortname --
                    Label lbl_teacher_shortname = new Label();
                    lbl_teacher_shortname.Size = new Size(60, panel_teacher.Height);
                    lbl_teacher_shortname.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_teacher_shortname.Text = s[0];
                    lbl_teacher_shortname.TextAlign = ContentAlignment.MiddleLeft;
                    panel_teacher.Controls.Add(lbl_teacher_shortname);
                    // -- phone --
                    Label lbl_teacher_phone = new Label();
                    lbl_teacher_phone.Size = new Size(140, panel_teacher.Height);
                    lbl_teacher_phone.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_teacher_phone.Text = s[3];
                    if (s[3].Length < 1 || s[3] == null)
                        lbl_teacher_phone.Text = "-";
                    lbl_teacher_phone.TextAlign = ContentAlignment.MiddleLeft;
                    panel_teacher.Controls.Add(lbl_teacher_phone);
                    // -- subjects --
                    for (int i = 0; i < 3; i++)
                    {
                        Label lbl_teacher_subject = new Label();
                        lbl_teacher_subject.Size = new Size(100, panel_teacher.Height);
                        lbl_teacher_subject.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        lbl_teacher_subject.Text = s[4 + i];
                        if (s[4 + i].Length < 1 || s[4 + i] == null)
                            lbl_teacher_subject.Text = "-";
                        lbl_teacher_subject.TextAlign = ContentAlignment.MiddleLeft;
                        panel_teacher.Controls.Add(lbl_teacher_subject);
                    }
                    // -- BTN edit --
                    Button btn_teacher_edit = new Button();
                    btn_teacher_edit.Size = new Size(100, panel_teacher.Height - 40);
                    btn_teacher_edit.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    btn_teacher_edit.Text = "Bearbeiten";
                    btn_teacher_edit.Name = s[0];
                    btn_teacher_edit.Margin = new Padding(10, 20, 10, 20);
                    btn_teacher_edit.BackColor = Color.LightGray;
                    btn_teacher_edit.Click += btn_teacher_edit_Click;
                    panel_teacher.Controls.Add(btn_teacher_edit);
                    // -- BTN delete --
                    Button btn_teacher_delete = new Button();
                    btn_teacher_delete.Size = new Size(100, panel_teacher.Height - 40);
                    btn_teacher_delete.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    btn_teacher_delete.Text = "Löschen";
                    btn_teacher_delete.Name = s[0];
                    btn_teacher_delete.Margin = new Padding(10, 20, 10, 20);
                    btn_teacher_delete.BackColor = Color.LightGray;
                    btn_teacher_delete.Click += btn_teacher_delete_Click;
                    panel_teacher.Controls.Add(btn_teacher_delete);

                    //this.flp_teacher_entitys.HorizontalScroll.Value = 0;
                    teacher_entity_list.AddLast(panel_teacher);
                }
            }
            /*List<FlowLayoutPanel> temp_panel_list = new List<FlowLayoutPanel>(teacher_entity_list);
            temp_panel_list = temp_panel_list.OrderBy(x => x.Name).ToList();
            teacher_entity_list = new LinkedList<FlowLayoutPanel>(temp_panel_list);*/

            foreach (Panel p in teacher_entity_list)
            {
                flp_teacher_entitys.Controls.Add(p);
                this.flp_teacher_entitys.SetFlowBreak(p, true);
            }
        }

        private void AddTeacherEntity(string id) //TODO ################################################################
        {
            string[] s = database.GetTeacherByID(id.ToString());
            FlowLayoutPanel panel_teacher = new FlowLayoutPanel();
            panel_teacher.Width = flp_teacher_entitys.Width - 28;
            panel_teacher.Margin = new Padding(5);
            panel_teacher.BackColor = Color.LightBlue;
            panel_teacher.Name = s[0];
            // -- NAME --
            Label lbl_teacher_name = new Label();
            lbl_teacher_name.Size = new Size(180, panel_teacher.Height);
            lbl_teacher_name.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_name.Text = s[1] + " " + s[2];
            lbl_teacher_name.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_name);
            // -- shortname --
            Label lbl_teacher_shortname = new Label();
            lbl_teacher_shortname.Size = new Size(60, panel_teacher.Height);
            lbl_teacher_shortname.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_shortname.Text = s[0];
            lbl_teacher_shortname.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_shortname);
            // -- phone --
            Label lbl_teacher_phone = new Label();
            lbl_teacher_phone.Size = new Size(140, panel_teacher.Height);
            lbl_teacher_phone.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbl_teacher_phone.Text = s[3];
            if (s[3].Length < 1 || s[3] == null)
                lbl_teacher_phone.Text = "-";
            lbl_teacher_phone.TextAlign = ContentAlignment.MiddleLeft;
            panel_teacher.Controls.Add(lbl_teacher_phone);
            // -- subjects --
            for (int i = 0; i < 3; i++)
            {
                Label lbl_teacher_subject = new Label();
                lbl_teacher_subject.Size = new Size(100, panel_teacher.Height);
                lbl_teacher_subject.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                lbl_teacher_subject.Text = s[4 + i];
                if (s[4 + i].Length < 1 || s[4 + i] == null)
                    lbl_teacher_subject.Text = "-";
                lbl_teacher_subject.TextAlign = ContentAlignment.MiddleLeft;
                panel_teacher.Controls.Add(lbl_teacher_subject);
            }
            // -- BTN edit --
            Button btn_teacher_edit = new Button();
            btn_teacher_edit.Size = new Size(100, panel_teacher.Height - 40);
            btn_teacher_edit.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn_teacher_edit.Text = "Bearbeiten";
            btn_teacher_edit.Name = s[0];
            btn_teacher_edit.Margin = new Padding(10, 20, 10, 20);
            btn_teacher_edit.BackColor = Color.LightGray;
            btn_teacher_edit.Click += btn_teacher_edit_Click;
            panel_teacher.Controls.Add(btn_teacher_edit);
            // -- BTN delete --
            Button btn_teacher_delete = new Button();
            btn_teacher_delete.Size = new Size(100, panel_teacher.Height - 40);
            btn_teacher_delete.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn_teacher_delete.Text = "Löschen";
            btn_teacher_delete.Name = s[0];
            btn_teacher_delete.Margin = new Padding(10, 20, 10, 20);
            btn_teacher_delete.BackColor = Color.LightGray;
            btn_teacher_delete.Click += btn_teacher_delete_Click;
            panel_teacher.Controls.Add(btn_teacher_delete);
            teacher_entity_list.AddLast(panel_teacher);

            /*List<FlowLayoutPanel> temp_panel_list = new List<FlowLayoutPanel>(teacher_entity_list);
            temp_panel_list = temp_panel_list.OrderBy(x => x.Name).ToList();
            teacher_entity_list = new LinkedList<FlowLayoutPanel>(temp_panel_list);*/
            flp_teacher_entitys.Controls.Clear();
            //foreach (FlowLayoutPanel p in teacher_entity_list) p.Dispose();
            foreach (Panel p in teacher_entity_list)
            {
                this.flp_teacher_entitys.Controls.Add(p);
                this.flp_teacher_entitys.SetFlowBreak(p, true);
            }
        }

        private void btn_teacher_delete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string[] t = database.GetTeacherByID(btn.Name);
            string name = t[1] + " " + t[2];
            DialogResult result = MessageBox.Show("Lehrer " + name + " löschen?", "Warnung!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                database.DeleteTeacher(btn.Name);
                foreach (FlowLayoutPanel flp in teacher_entity_list)
                {
                    if (flp.Name == btn.Name)
                    {
                        flp.Dispose();
                        //flp_teacher_entitys.Controls.Remove(flp);
                        flp_teacher_entitys.Update();
                        break;
                    }
                }
                //UpdateTeacherList();
            }
        }

        private void btn_teacher_edit_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            edit_id = btn.Name;
            btn_add_teacher.Text = add_mode[1];
            tb_shortname.ReadOnly = true;
            string[] t = database.GetTeacherByID(btn.Name);
            tb_shortname.Text = t[0];
            tb_firstname.Text = t[1];
            tb_lastname.Text = t[2];
            tb_phonenumber.Text = t[3];
            cb_subject1.Text = t[4];
            cb_subject2.Text = t[5];
            cb_subject3.Text = t[6];
        }

        private void btn_add_teacher_Click(object sender, EventArgs e)
        {
            string shortname = tb_shortname.Text.Replace(" ", "");
            string firstname = tb_firstname.Text;
            string lastname = tb_lastname.Text;
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
                database.AddTeacher(shortname, firstname, lastname, phonenumber, subject1, subject2, subject3);
            }
            else
            {
                if (database.GetTeacherByID(shortname) == null)
                { MessageBox.Show("Lehrer nicht vorhanden", "Warnung"); return; }
                database.EditTeacher(shortname, firstname, lastname, phonenumber, subject1, subject2, subject3);
                foreach (FlowLayoutPanel flp in teacher_entity_list)
                {
                    if (flp.Name == edit_id)
                    {
                        flp.Dispose();
                        // flp_teacher_entitys.Controls.Remove(flp);
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
            {
                p.Width = flp_teacher_entitys.Width - 28;
            }
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
    }
}
