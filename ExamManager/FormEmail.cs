using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
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
                string preview = to.Email + "\n";
                preview += tb_email_title.Text + "\n";
                preview += ReplaceText(to);
                Console.WriteLine(preview);
                Console.WriteLine("-------------------");
            }
            //MessageBox.Show(ReplaceText(teacherList.First.Value), "Email text");
            DialogResult result = MessageBox.Show("Emails senden?", "Warnung", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) SendTeacherEmails();
        }

        private void SendTeacherEmails()
        {
            string dayExamListCSV = null;
            LinkedList<string> fullPNGList = null;
            if (cb_fullexamlist.Checked) dayExamListCSV = DayExamsCSV();
            if (cb_fulltimeline.Checked) fullPNGList = TeacherExamPNG();
            string missingMail = null;
            // check all teacher emails
            foreach (TeacherObject to in teacherList)
            {
                if (to.Email == null || to.Email.Length < 2)
                { missingMail = to.Fullname(); break; }
            }
            if (missingMail != null) { MessageBox.Show("fehlende Email bei " + missingMail, "Achtung"); return; }
            if (tb_email_title.Text.Length == 0) { MessageBox.Show("Titel fehlt", "Achtung"); return; }


            Console.WriteLine("sending " + teacherList.Count + " Emails");
            FormProgressBar bar = new FormProgressBar();
            bar.Show();
            bar.StartPrograssBar(1, teacherList.Count);
            foreach (TeacherObject to in teacherList)
            {
                LinkedList<string> files = new LinkedList<string>();
                if (cb_fullexamlist.Checked) files.AddLast(dayExamListCSV);
                if (cb_teacher_examlist.Checked) files.AddLast(TeacherExamsCSV(to));
                if (cb_fulltimeline.Checked) foreach (string f in fullPNGList) files.AddLast(f);
                if (cb_teacher_timeline.Checked) foreach (string f in TeacherExamPNG(to)) files.AddLast(f);

                SendEmail(to, tb_email_title.Text, ReplaceText(to), files);
                bar.AddOne();
                // TODO !!! remove !!!
                break;
            }
            bar.Exit();
            MessageBox.Show(teacherList.Count + " Emails gesendet!", "Mitteilung");
        }

        private bool SendEmail(TeacherObject teacher, string title, string text, LinkedList<string> files)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.SMTP_server)
                {
                    Port = int.Parse(Properties.Settings.Default.SMTP_port),
                    Credentials = new NetworkCredential(Properties.Settings.Default.SMTP_email, Properties.Settings.Default.SMTP_password),
                    EnableSsl = true,
                };
                string senderName = Properties.Settings.Default.SMTP_email_name;
                string mail_title = title;
                string mail_receiver = Properties.Settings.Default.SMTP_email;  // TODO: change to var ----------------------------------------------
                string mail_text = text; // TODO: change email text
                if (mail_receiver.Length == 0 || senderName.Length == 0 || mail_title.Length == 0 || mail_text.Length == 0) { MessageBox.Show("alle Daten ausfüllen!", "Achtung"); return false; }
                try
                {
                    MailAddress from = new MailAddress(Properties.Settings.Default.SMTP_email, senderName);
                    MailAddress receiver = new MailAddress(mail_receiver);
                    MailMessage message = new MailMessage(from, receiver)
                    { Subject = mail_title, Body = mail_text };
                    if (files.Count > 0)
                        foreach (string f in files)
                        {
                            Attachment data = new Attachment(f, MediaTypeNames.Application.Octet);
                            message.Attachments.Add(data);
                        }
                    smtpClient.Send(message);
                    Console.WriteLine("Email gesendet: " + teacher.Fullname());
                    Thread.Sleep(500);
                }
                catch (Exception) { return false; }
            }
            catch (Exception) { return false; }
            return true;
        }

        private string DayExamsCSV()
        {
            string fileExamDay = Path.GetTempPath() + "\\Prüfungstag_" + date + ".csv";
            var csv = new StringBuilder();
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach", "Dauer");
            csv.AppendLine(firstLine);
            foreach (TeacherObject teacher in teacherList)
                foreach (ExamObject exam in Program.database.GetAllExamsFromTeacherAtDate(date, teacher.Shortname))
                {
                    DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                    DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                    string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", teacher.Shortname.Replace("*", ""), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                    if (exam.Teacher1 != null)
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", exam.Teacher1.Fullname(), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                    csv.AppendLine(newLine);
                }
            File.WriteAllText(fileExamDay, csv.ToString());
            return fileExamDay;
        }

        private string TeacherExamsCSV(TeacherObject to)
        {
            string teacherExamFile = Path.GetTempPath() + "\\Prüfungstag_" + date + "_" + to.Shortname + ".csv";
            var csv1 = new StringBuilder();
            var firstLine1 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Lehrer", "Zeit", "Prüfungsraum", "Vorbereitungsraum", "Schüler", "Lehrer Vorsitz", "Lehrer Prüfer", "Lehrer Protokoll", "Fach", "Dauer");
            csv1.AppendLine(firstLine1);
            foreach (ExamObject exam in Program.database.GetAllExamsFromTeacherAtDate(date, to.Shortname))
            {
                DateTime start = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
                DateTime end = DateTime.ParseExact(exam.Time, "HH:mm", null, System.Globalization.DateTimeStyles.None).AddMinutes(exam.Duration);
                string time = start.ToString("HH:mm") + " - " + end.ToString("HH:mm");
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", to.Shortname.Replace("*", ""), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                if (exam.Teacher1 != null)
                    newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", exam.Teacher1.Fullname(), time, exam.Examroom, exam.Preparationroom, exam.Student.Fullname(), exam.Teacher1Id.Replace("*", ""), exam.Teacher2Id.Replace("*", ""), exam.Teacher3Id.Replace("*", ""), exam.Subject, exam.Duration);
                csv1.AppendLine(newLine);
            }
            File.WriteAllText(teacherExamFile, csv1.ToString());
            return teacherExamFile;
        }

        private LinkedList<string> TeacherExamPNG(TeacherObject to = null)
        {
            LinkedList<ExamObject> examList = null;
            string t = "";
            if (to != null) t = "_" + to.Shortname;
            if (to == null) examList = Program.database.GetAllExamsAtDate(date);
            else examList = Program.database.GetAllExamsFromTeacherAtDate(date, to.Shortname);
            TimeLineObject tlo = new TimeLineObject(date, examList);
            string file = Path.GetTempPath() + "\\Prüfungstag_" + date + t + ".png";
            tlo.ExportPNG(split: false, file: file);
            tlo = new TimeLineObject(date, examList);
            string fileP1 = Path.GetTempPath() + "\\Prüfungstag_P1_" + date + t + ".png";
            string fileP2 = Path.GetTempPath() + "\\Prüfungstag_P2_" + date + t + ".png";
            tlo.ExportPNG(split: true, fileP1: fileP1, fileP2: fileP2);
            LinkedList<string> list = new LinkedList<string>();
            list.AddLast(file);
            list.AddLast(fileP1);
            list.AddLast(fileP2);
            return list;
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
