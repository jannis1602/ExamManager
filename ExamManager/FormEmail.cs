using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormEmail : Form
    {
        string[][] TextOptions = {
            new string[] { "Prüfungsanzahl", "{%examcount}" },
            new string[] { "Prüfungsliste", "{%examlist}" },
            new string[] { "Zeitraum", "{%timespan}" },
            new string[] { "Name", "{%teachername}" }};

        LinkedList<TeacherObject> teacherList;
        string date;

        public FormEmail()
        {
            InitializeComponent();
            foreach (string[] s in TextOptions)
            {
                Button btn = new Button();
                btn.Font = new Font("Microsoft Sans Serif", 10);
                btn.AutoSize = true;
                btn.Name = s[1];
                btn.Text = s[0];
                btn.Height = 30;
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(btn, s[1]);
                btn.Click += new EventHandler(delegate (object sender, EventArgs e) { InsertVariables(s[1]); });
                flp_var_btns.Controls.Add(btn);
            }
            tb_email_title.Text = Properties.Settings.Default.SMTP_email_title;


        }
        public void SetReceivers(LinkedList<TeacherObject> teacherList, string date)
        {
            this.teacherList = teacherList;
            this.date = date;
            string t = "";
            foreach (TeacherObject to in teacherList)
                t += to.Email + "; ";
            rtb_receivers.Text = t;
        }
        private void FormEmail_Load(object sender, EventArgs e)
        {
            rtb_email_text.AutoWordSelection = false;
        }
        private string ReplaceText(TeacherObject teacher)
        {
            string text = rtb_email_text.Text.Replace("{%examcount}", Program.database.GetAllExamsFromTeacherAtDate(date, teacher.Shortname).Count.ToString());
            string examstr = "";
            foreach (ExamObject eo in Program.database.GetAllExamsFromTeacherAtDate(date, teacher.Shortname))
                examstr += eo.Time + " " + eo.Subject + " " + eo.Examroom + " " + eo.Student.Fullname() + "\n";
            examstr = examstr.Remove(examstr.Length - 2, 2);
            text = text.Replace("{%examlist}", examstr);
            text = text.Replace("{%timespan}", "7 - 18 ");
            text = text.Replace("{%teachername}", teacher.Firstname + " " + teacher.Lastname);
            return text;
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            foreach (TeacherObject to in teacherList)
            {
                string preview = teacherList.First.Value.Email + "\n";
                preview += tb_email_title.Text + "\n";
                preview += ReplaceText(to);
                Console.WriteLine(preview);
                Console.WriteLine("-------------------");
            }
            //MessageBox.Show(ReplaceText(teacherList.First.Value), "Email text");
        }

        private void InsertVariables(string t)
        {
            rtb_email_text.Focus();
            int cursorPos = rtb_email_text.SelectionStart + t.Length;
            rtb_email_text.Text = rtb_email_text.Text.Insert(rtb_email_text.SelectionStart, t);
            rtb_email_text.SelectionStart = cursorPos;
        }

        private void CreateMenu()
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            foreach (string[] s in TextOptions)
            {
                ToolStripMenuItem mnu1 = new ToolStripMenuItem(s[0]);
                mnu1.Click += new EventHandler(delegate (object sender, EventArgs e)
                { InsertVariables(s[1]); });
                mnu.Items.Add(mnu1);
            }
            rtb_email_text.ContextMenuStrip = mnu;
        }

        private void rtb_email_text_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rtb_email_text.SelectionStart = rtb_email_text.GetCharIndexFromPosition(e.Location);
                rtb_email_text.SelectionLength = 0;
                CreateMenu();
            }
        }

        private void rtb_email_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 && e.Alt) InsertVariables(TextOptions[0][1]);
            if (e.KeyCode == Keys.D2 && e.Alt) InsertVariables(TextOptions[1][1]);
            if (e.KeyCode == Keys.D3 && e.Alt) InsertVariables(TextOptions[2][1]);
            if (e.KeyCode == Keys.D4 && e.Alt) InsertVariables(TextOptions[3][1]);
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            string preview = teacherList.First.Value.Email + "\n";
            preview += tb_email_title.Text + "\n";
            preview += ReplaceText(teacherList.First.Value);
            MessageBox.Show(preview, "Email text");
        }

        private void btn_replace_Click(object sender, EventArgs e)
        {
            rtb_email_text.Text = ReplaceText(teacherList.First.Value);
        }
    }
}
