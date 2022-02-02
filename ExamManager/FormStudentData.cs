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
        Database database;
        LinkedList<FlowLayoutPanel> student_entity_list;
        int edit_id = 0;
        string[] add_mode = { "Schüler hinzufügen", "Schüler übernehmen" };
        LinkedList<int> studentIdList;
        public FormStudentData(LinkedList<int> studentIdList = null)
        {
            database = Program.database;
            LinkedList<string[]> allStudents = database.GetAllStudents();
            student_entity_list = new LinkedList<FlowLayoutPanel>();
            this.studentIdList = studentIdList;
            InitializeComponent();
            UpdateStudentList();
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
        }

        private void UpdateStudentList()
        {
            foreach (FlowLayoutPanel p in student_entity_list) p.Dispose();
            student_entity_list.Clear();

            foreach (string[] s in database.GetAllStudents())
            {
                if ((studentIdList != null && studentIdList.Contains(Int32.Parse(s[0]))) || studentIdList == null)
                {
                    FlowLayoutPanel panel_student = new FlowLayoutPanel();
                    //panel_student.Size = new Size(950, 80);
                    panel_student.Height = 80;
                    panel_student.Width = flp_student_entitys.Width - 28;
                    panel_student.Margin = new Padding(5);
                    panel_student.BackColor = Color.LightBlue;
                    panel_student.Name = s[2];
                    // -- NAME --
                    Label lbl_student_name = new Label();
                    lbl_student_name.Size = new Size(140, panel_student.Height);
                    lbl_student_name.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_student_name.Text = s[1] + " " + s[2];
                    lbl_student_name.TextAlign = ContentAlignment.MiddleLeft;
                    panel_student.Controls.Add(lbl_student_name);
                    // -- grade --
                    Label lbl_student_grade = new Label();
                    lbl_student_grade.Size = new Size(60, panel_student.Height);
                    lbl_student_grade.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_student_grade.Text = s[3];
                    lbl_student_grade.TextAlign = ContentAlignment.MiddleLeft;
                    panel_student.Controls.Add(lbl_student_grade);
                    // -- email --
                    Label lbl_student_email = new Label();
                    lbl_student_email.Size = new Size(220, panel_student.Height);
                    lbl_student_email.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_student_email.Text = s[4];
                    if (s[4].Length < 1 || s[4] == null)
                        lbl_student_email.Text = "-";
                    lbl_student_email.TextAlign = ContentAlignment.MiddleLeft;
                    panel_student.Controls.Add(lbl_student_email);
                    // -- phone --
                    Label lbl_student_phone = new Label();
                    lbl_student_phone.Size = new Size(140, panel_student.Height);
                    lbl_student_phone.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lbl_student_phone.Text = s[5];
                    if (s[5].Length < 1 || s[5] == null)
                        lbl_student_phone.Text = "-";
                    lbl_student_phone.TextAlign = ContentAlignment.MiddleLeft;
                    panel_student.Controls.Add(lbl_student_phone);
                    // -- BTN edit --
                    Button btn_student_edit = new Button();
                    btn_student_edit.Size = new Size(100, panel_student.Height - 40);
                    btn_student_edit.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    btn_student_edit.Text = "Bearbeiten";
                    btn_student_edit.Name = s[0];
                    btn_student_edit.Margin = new Padding(10, 20, 10, 20);
                    //btn_student_edit.Size= new Size(panel_student);
                    btn_student_edit.BackColor = Color.LightGray;
                    btn_student_edit.Click += btn_student_edit_Click;
                    panel_student.Controls.Add(btn_student_edit);
                    // -- BTN delete --
                    Button btn_student_delete = new Button();
                    btn_student_delete.Size = new Size(100, panel_student.Height - 40);
                    btn_student_delete.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    btn_student_delete.Text = "Löschen";
                    btn_student_delete.Name = s[0];
                    btn_student_delete.Margin = new Padding(10, 20, 10, 20);
                    //btn_student_edit.Size= new Size(panel_student);
                    btn_student_delete.BackColor = Color.LightGray;
                    btn_student_delete.Click += btn_student_delete_Click;
                    panel_student.Controls.Add(btn_student_delete);

                    //
                    this.flp_student_entitys.HorizontalScroll.Value = 0;
                    //flp_teacher_entitys.Controls.Add(panel_student);
                    panel_student.Name = s[2];
                    student_entity_list.AddLast(panel_student);
                }
            }

            List<FlowLayoutPanel> temp_panel_list = new List<FlowLayoutPanel>(student_entity_list);
            temp_panel_list = temp_panel_list.OrderBy(x => x.Name).ToList(); // .ThenBy( x => x.Bar)
            student_entity_list = new LinkedList<FlowLayoutPanel>(temp_panel_list);

            foreach (Panel p in student_entity_list)
            {
                this.flp_student_entitys.Controls.Add(p);
                this.flp_student_entitys.SetFlowBreak(p, true);
            }
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
                UpdateStudentList();
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
            // TODO: check name?
            // TODO: Check if null #########################################################################################

            string firstname = tb_firstname.Text;
            string lastname = tb_lastname.Text;
            string grade = cb_grade.Text;
            string email = tb_email.Text;
            string phonenumber = tb_phonenumber.Text;
            if (firstname.Length == 0 || lastname.Length == 0 || grade.Length == 0 || grade.Length == 0)
            { MessageBox.Show("Alle Felder mit * ausfüllen!", "Warnung"); return; }
            if (edit_id == 0)
            {
                /*if (database.GetTeacherByID(shortname) != null)
                { MessageBox.Show("Schüler schon vorhanden", "Warnung"); return; }*/
                database.AddStudent(firstname, lastname, grade, email, phonenumber);
            }
            else
            {
                /*if (database.GetTeacherByID(shortname) == null)
                { MessageBox.Show("LehrSchülerer nicht vorhanden", "Warnung"); return; }*/
                database.EditStudent(edit_id, firstname, lastname, grade, email, phonenumber);
            }
            if (studentIdList != null) studentIdList.AddLast(Int32.Parse(database.GetStudent(firstname, lastname, grade)[0]));
            UpdateStudentList();
            tb_firstname.Clear();
            tb_lastname.Clear();
            tb_email.Clear();
            tb_phonenumber.Clear();

            edit_id = 0;
            btn_add_student.Text = add_mode[0];
            //cb_grade.ReadOnly = false;
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
            //cb_grade.ReadOnly = false;
        }

        private void btn_email_generate_Click(object sender, EventArgs e)
        {
            string domain = Properties.Settings.Default.email_domain;
            if (domain.Length < 2)
                MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung");
            tb_email.Text = tb_firstname.Text.Replace(' ', '.').Replace('_', '.') + "." + tb_lastname.Text.Replace(" ", ".").Replace('_', '.') + "@" + domain;
        }

        private void flp_student_entitys_SizeChanged(object sender, EventArgs e)
        {
            foreach (Panel p in student_entity_list)
            {
                p.Width = flp_student_entitys.Width - 28;
            }
        }

        private void tb_firstname_TextChanged(object sender, EventArgs e)
        {
            if (tb_firstname.Text.Contains(' ') || tb_lastname.Text.Contains(' '))
                btn_hint.Visible = true;
        }

        private void btn_hint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lehrzeichen vermeiden!\nDoppelnamen: name_name2", "Hinweis");
        }
    }
}
