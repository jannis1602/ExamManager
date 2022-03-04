using System;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class KeepDataForm : Form
    {
        public KeepDataForm()
        {
            InitializeComponent();
            if (Properties.Settings.Default.keep_subject) clb1.SetItemChecked(0, true);
            if (Properties.Settings.Default.keep_examroom) clb1.SetItemChecked(1, true);
            if (Properties.Settings.Default.keep_preparationroom) clb1.SetItemChecked(2, true);
            if (Properties.Settings.Default.keep_teacher) clb1.SetItemChecked(3, true);
            if (Properties.Settings.Default.keep_grade) clb1.SetItemChecked(4, true);
            if (Properties.Settings.Default.keep_student) clb1.SetItemChecked(5, true);

        }

        private void clb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.keep_subject = clb1.GetItemChecked(0);
            Properties.Settings.Default.keep_examroom = clb1.GetItemChecked(1);
            Properties.Settings.Default.keep_preparationroom = clb1.GetItemChecked(2);
            Properties.Settings.Default.keep_teacher = clb1.GetItemChecked(3);
            Properties.Settings.Default.keep_grade = clb1.GetItemChecked(4);
            Properties.Settings.Default.keep_student = clb1.GetItemChecked(5);
            Properties.Settings.Default.Save();
        }

        private void clb1_DoubleClick(object sender, EventArgs e)
        {
            Properties.Settings.Default.keep_subject = clb1.GetItemChecked(0);
            Properties.Settings.Default.keep_examroom = clb1.GetItemChecked(1);
            Properties.Settings.Default.keep_preparationroom = clb1.GetItemChecked(2);
            Properties.Settings.Default.keep_teacher = clb1.GetItemChecked(3);
            Properties.Settings.Default.keep_grade = clb1.GetItemChecked(4);
            Properties.Settings.Default.keep_student = clb1.GetItemChecked(5);
            Properties.Settings.Default.Save();
        }
    }
}
