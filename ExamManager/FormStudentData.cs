﻿using System;
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
        public FormStudentData()
        {
            database = Program.database;
            LinkedList<string[]> allStudents = database.GetAllStudents();
            student_entity_list = new LinkedList<FlowLayoutPanel>();
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

        private void FormStudentData_Load(object sender, EventArgs e)
        {

        }

        private void UpdateStudentList()
        {
            foreach (FlowLayoutPanel p in student_entity_list) p.Dispose();
            student_entity_list.Clear();

            //TODO Order by lastname

            foreach (string[] s in database.GetAllStudents())
            {
                FlowLayoutPanel panel_student = new FlowLayoutPanel();
                //panel_student.Size = new Size(950, 80);
                //panel_student.Dock = DockStyle.Top;
                panel_student.Height = 80;
                panel_student.Width = flp_student_entitys.Width - 10;
                //panel_student.AutoSize = true;
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
                if (s[4] == "0" || s[4] == null)
                    lbl_student_email.Text = "-";
                lbl_student_email.TextAlign = ContentAlignment.MiddleLeft;
                panel_student.Controls.Add(lbl_student_email);
                // -- phone --
                Label lbl_student_phone = new Label();
                lbl_student_phone.Size = new Size(140, panel_student.Height);
                lbl_student_phone.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                lbl_student_phone.Text = s[5];
                if (s[5] == "0" || s[5] == null)
                    lbl_student_phone.Text = "-";
                lbl_student_phone.TextAlign = ContentAlignment.MiddleLeft;
                panel_student.Controls.Add(lbl_student_phone);
                /* // -- subjects --
                for (int i = 0; i < 3; i++)
                {
                    Label lbl_teacher_subject = new Label();
                    lbl_teacher_subject.Size = new Size(100, panel_student.Height);
                    lbl_teacher_subject.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    //lbl_teacher_phone.Location = new Point(0, 0);
                    //lbl_room.Margin = new Padding(3);
                    lbl_teacher_subject.Text = s[4 + i];
                    lbl_teacher_subject.TextAlign = ContentAlignment.MiddleLeft;
                    panel_student.Controls.Add(lbl_teacher_subject);
                }*/
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

            List<FlowLayoutPanel> temp_panel_list = new List<FlowLayoutPanel>(student_entity_list);
            temp_panel_list = temp_panel_list.OrderBy(x => x.Name).ToList(); // .ThenBy( x => x.Bar)
            student_entity_list = new LinkedList<FlowLayoutPanel>(temp_panel_list);

            foreach (Panel p in student_entity_list)
            {
                Console.WriteLine(p.Name);
                flp_student_entitys.Controls.Add(p);
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
            UpdateStudentList();
            //cb_grade.Text = null;
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
                if (domain == null)
                MessageBox.Show("Domain in den Einstellungen festlegen", "Warnung");
            tb_email.Text = tb_firstname.Text.Replace(' ','.') + "." + tb_lastname.Text.Replace(" ", ".") + "@"+domain;
        }

        private void flp_student_entitys_SizeChanged(object sender, EventArgs e)
        {
            foreach (Panel p in student_entity_list)
            {
                p.Width = flp_student_entitys.Width - 10;
            }
        }
    }
}
