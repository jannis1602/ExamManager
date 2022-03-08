using System;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class KeepDataForm : Form
    {
        public KeepDataForm()
        {
            InitializeComponent();
            if (Properties.Settings.Default.KeepSubject) clb1.SetItemChecked(0, true);
            if (Properties.Settings.Default.KeepExamroom) clb1.SetItemChecked(1, true);
            if (Properties.Settings.Default.KeepPreparationroom) clb1.SetItemChecked(2, true);
            if (Properties.Settings.Default.KeepTeacher) clb1.SetItemChecked(3, true);
            if (Properties.Settings.Default.KeepGrade) clb1.SetItemChecked(4, true);
            if (Properties.Settings.Default.KeepStudent) clb1.SetItemChecked(5, true);

        }

        private void clb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeepSubject = clb1.GetItemChecked(0);
            Properties.Settings.Default.KeepExamroom = clb1.GetItemChecked(1);
            Properties.Settings.Default.KeepPreparationroom = clb1.GetItemChecked(2);
            Properties.Settings.Default.KeepTeacher = clb1.GetItemChecked(3);
            Properties.Settings.Default.KeepGrade = clb1.GetItemChecked(4);
            Properties.Settings.Default.KeepStudent = clb1.GetItemChecked(5);
            Properties.Settings.Default.Save();
        }

        private void clb1_DoubleClick(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeepSubject = clb1.GetItemChecked(0);
            Properties.Settings.Default.KeepExamroom = clb1.GetItemChecked(1);
            Properties.Settings.Default.KeepPreparationroom = clb1.GetItemChecked(2);
            Properties.Settings.Default.KeepTeacher = clb1.GetItemChecked(3);
            Properties.Settings.Default.KeepGrade = clb1.GetItemChecked(4);
            Properties.Settings.Default.KeepStudent = clb1.GetItemChecked(5);
            Properties.Settings.Default.Save();
        }
    }
}
