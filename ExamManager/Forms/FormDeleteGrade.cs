using System;
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
    public partial class FormDeleteGrade : Form
    {
        public FormDeleteGrade()
        {
            InitializeComponent();
        }

        private void FormDeleteGrade_Load(object sender, EventArgs e)
        {
            LinkedList<StudentObject> allStudents = Program.database.GetAllStudents();
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
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (cb_grade.SelectedItem.ToString() != null)
            {
                Program.database.DeleteGrade(cb_grade.SelectedItem.ToString());
                MessageBox.Show("Stufe " + cb_grade.SelectedItem.ToString() + " gelöscht!", "Warnung!");
                cb_grade.SelectedItem = null;
                cb_grade.Items.Clear();
                LinkedList<StudentObject> allStudents = Program.database.GetAllStudents();
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
            }
        }
    }
}
