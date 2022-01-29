using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormExamDateListView : Form
    {
        Form1 form;
        public FormExamDateListView(Form1 form)
        {
            this.form = form;
            InitializeComponent();
            LinkedList<string> list = new LinkedList<string>();
            foreach (string[] s in Program.database.GetAllExams())
            {
                if (!list.Contains(s[1]))
                    list.AddLast(s[1]);
            }
            string[] dates = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                dates[i] = list.ElementAt(i);
            lb_exam_date.Items.Clear();
            lb_exam_date.Items.AddRange(dates);
        }

        private void lb_exam_date_DoubleClick(object sender, EventArgs e)
        {
            if (lb_exam_date.SelectedItem != null)
            {
                form.SetDate(DateTime.ParseExact(lb_exam_date.SelectedItem.ToString(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None));
                this.Dispose();
            }
        }
    }
}
