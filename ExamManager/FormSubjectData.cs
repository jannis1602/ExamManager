using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormSubjectData : Form
    {
        public FormSubjectData()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_add.Text.Length > 0)
            {
                Program.database.AddSubject(tb_add.Text);
                tb_add.Clear();
                LoadAllSubject();
            }
        }

        private void tb_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tb_add.Text.Length > 0)
                {
                    Program.database.AddSubject(tb_add.Text);
                    tb_add.Clear();
                    e.Handled = true;
                    LoadAllSubject();
                }
            }
        }

        private void FormSubjectData_Load(object sender, EventArgs e)
        {
            LoadAllSubject();
        }

        private void LoadAllSubject()
        {
            LinkedList<string[]> list = Program.database.GetAllSubjects();
            string[] subjects = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                subjects[i] = list.ElementAt(i)[0];
            }
            lb_subjectlist.Items.Clear();
            lb_subjectlist.Items.AddRange(subjects);
        }

        private void lb_subjectlist_DoubleClick(object sender, EventArgs e)
        {
            if (lb_subjectlist.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Fach " + lb_subjectlist.SelectedItem.ToString() + " Löschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Program.database.DeleteSubject(lb_subjectlist.SelectedItem.ToString());
                    LoadAllSubject();
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (lb_subjectlist.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Raum " + lb_subjectlist.SelectedItem.ToString() + " Löschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Program.database.DeleteSubject(lb_subjectlist.SelectedItem.ToString());
                    LoadAllSubject();
                }
            }
        }
    }
}
