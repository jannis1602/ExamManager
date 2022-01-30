﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormLoadStudents : Form
    {
        public FormLoadStudents()
        {
            InitializeComponent();
            LinkedList<string[]> allStudents = Program.database.GetAllStudents();
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
            cb_existinggrade.Items.Add("neue Stufe");
            cb_existinggrade.Items.AddRange(list);
            cb_existinggrade.SelectedIndex = 0;

            if (Properties.Settings.Default.email_domain.Length < 2)
            {
                cb_mailgenerator.Text += " (nicht festgelegt!)";
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string filePath;
            string grade = null;
            if (cb_existinggrade.SelectedIndex == 0)
                grade = tb_newgrade.Text;
            else grade = cb_existinggrade.SelectedItem.ToString();
            Console.WriteLine(grade);
            if (grade.Length < 1)
                return;
            this.Dispose();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    Program.database.InsertStudentFileIntoDB(filePath, grade, cb_mailgenerator.Checked);
                }
            }
        }

        private void cb_existinggrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_existinggrade.SelectedIndex == 0)
                tb_newgrade.ReadOnly = false;
            else tb_newgrade.ReadOnly = true;
        }
    }
}
