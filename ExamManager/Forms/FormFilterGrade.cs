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
    public partial class FormFilterGrade : Form
    {
        Form1 form;
        public FormFilterGrade(Form1 form)
        {
            this.form = form;
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
            cb_grade.Items.AddRange(list);
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            if (cb_grade.SelectedItem.ToString() != null)
            {
                Console.WriteLine("FILTER: "+cb_grade.SelectedItem.ToString());
                form.filterMode = Form1.Filter.grade;
                form.filter = cb_grade.SelectedItem.ToString();

                /*cb_grade.SelectedItem = null;
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
                cb_grade.Items.AddRange(list);*/
                this.Dispose();
            }
        }
    }
}
