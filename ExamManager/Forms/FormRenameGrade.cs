using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormRenameGrade : Form
    {
        public FormRenameGrade()
        {
            InitializeComponent();
            LoadAutocomplete();
        }

        private void LoadAutocomplete()
        {
            cb_grade.Items.Clear();
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
            cb_grade.Items.AddRange(list);
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            string oldgrade = cb_grade.SelectedItem.ToString();
            string newgrade = tb_new.Text;
            if (oldgrade.Length > 0 && newgrade.Length > 0)
                if (Program.database.GetAllStudentsFromGrade(newgrade).Count > 1)
                {
                    DialogResult result = MessageBox.Show("Stufe " + newgrade + " exestiert!\nFortfahren?", "Warnung!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Program.database.ChangeGrade(oldgrade, newgrade); // .Remove(' ')
                        cb_grade.SelectedItem = null;
                        tb_new.Clear();
                        LoadAutocomplete();
                    }
                }
                else
                {
                    Program.database.ChangeGrade(oldgrade, newgrade); // .Remove(' ')
                    cb_grade.SelectedItem = null;
                    tb_new.Clear();
                    LoadAutocomplete();
                    // MessageBox.Show("Stufe " + oldgrade + " erfolgreich zu "+newgrade+" umbenannt!", "Warnung!");
                }
        }
    }
}
