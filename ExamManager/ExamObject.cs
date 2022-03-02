﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    class ExamObject
    {
        private bool Border;
        private ButtonBorderStyle BorderStyle;
        private Color BorderColor;
        public Panel Panel { get; private set; }

        public int Id { get; private set; }
        public string Date { get; private set; } // DateTime?
        public string Time { get; private set; }
        public string Examroom { get; private set; }
        public string Preparationroom { get; private set; }
        public int StudentId { get; private set; }      
        public int Student2Id { get; private set; }
        public int Student3Id { get; private set; }
        public StudentObject Student { get; private set; }
        public StudentObject Student2 { get; private set; }
        public StudentObject Student3 { get; private set; }

        public string Teacher1 { get; private set; }
        public string Teacher2 { get; private set; }
        public string Teacher3 { get; private set; }
        public string Subject { get; private set; }
        public int Duration { get; private set; }

        public ExamObject(int id, string date, string time, string examroom, string preparationroom, int student, int student2, int student3, string teacher1, string teacher2, string teacher3, string subject, int duration)
        {
            this.Id = id;
            this.Date = date;
            DateTime dt = DateTime.ParseExact(date, "dd.MM.yyyy", null);
            this.Date = dt.ToString("yyyy-MM-dd");
            this.Time = time;
            this.Examroom = examroom;
            this.Preparationroom = preparationroom;
            this.StudentId = student;
            if (student2 != 0) { this.Student2Id = student2; this.Student2 = Program.database.GetStudentByID(student2); }
            if (student3 != 0) { this.Student3Id = student3; this.Student3 = Program.database.GetStudentByID(student3); }
            this.Teacher1 = teacher1;
            this.Teacher2 = teacher2;
            this.Teacher3 = teacher3;
            this.Subject = subject;
            this.Duration = duration;
            this.Student = Program.database.GetStudentByID(student);
            BorderColor = Colors.TL_EntityBorder;
            // TODO: check teacher in db
        }

        public void Edit(string date = null, string time = null, string examroom = null, string preparationroom = null, int student = 0, int student2 = 0, int student3 = 0, string teacher1 = null, string teacher2 = null, string teacher3 = null, string subject = null, int duration = 0)
        {
            if (date != null) this.Date = date;
            if (time != null) this.Time = time;
            if (examroom != null) this.Examroom = examroom;
            if (preparationroom != null) this.Preparationroom = preparationroom;
            if (student != 0) this.StudentId = student; this.Student = Program.database.GetStudentByID(student);
            if (student2 != 0) this.Student2Id = student2; this.Student2 = Program.database.GetStudentByID(student2);
            if (student3 != 0) this.Student3Id = student3; this.Student3 = Program.database.GetStudentByID(student3);
            if (teacher1 != null) this.Teacher1 = teacher1;
            if (teacher2 != null) this.Teacher2 = teacher2;
            if (teacher3 != null) this.Teacher3 = teacher3;
            if (subject != null) this.Subject = subject;
            if (duration != 0) this.Duration = duration;
            Program.database.EditExam(this.Id, this.Date, this.Time, this.Examroom, this.Preparationroom, this.StudentId, this.Student2Id, this.Student3Id, this.Teacher1, this.Teacher2, this.Teacher3, this.Subject, this.Duration);
        }

        public void Delete()
        {
            Program.database.DeleteExam(this.Id);
        }

        public void CreatePanel()
        {
            this.Panel = new Panel();
            DateTime startTime = DateTime.ParseExact("07:00", "HH:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime examTime = DateTime.ParseExact(Time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            int totalMins = Convert.ToInt32(examTime.Subtract(startTime).TotalMinutes);
            float unit_per_minute = 200F / 60F;
            float startpoint = (float)Convert.ToDouble(totalMins) * unit_per_minute + 4;
            this.Panel.Location = new Point(Convert.ToInt32(startpoint), 10);
            this.Panel.Size = new Size(Convert.ToInt32(unit_per_minute * Duration), 60);
            this.Panel.Name = Id.ToString();
            this.Panel.BackColor = Colors.TL_Entity;
            this.Panel.Paint += panel_time_line_entity_Paint;
        }

        public void UpdatePanel()
        {
            this.Panel.Refresh();
        }

        public Panel GetTimelineEntity()
        {
            if (Panel == null) CreatePanel();
            return this.Panel;
        }

        private void panel_time_line_entity_Paint(object sender, PaintEventArgs e)
        {
            Panel panel_tl_entity = sender as Panel;
            Font drawFont = new Font("Microsoft Sans Serif", 8);  // Arial
            ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid,
            Colors.TL_EntityBorder, 2, ButtonBorderStyle.Solid);
            ExamObject exam = Program.database.GetExamById(Int32.Parse(panel_tl_entity.Name));
            StudentObject student = exam.Student;
            if (student == null)
                student = new StudentObject(0, "Schüler nicht gefunden!", " ", "-");
            if (Border)
            {
                ControlPaint.DrawBorder(e.Graphics, panel_tl_entity.ClientRectangle,
                BorderColor, 2, BorderStyle,
                BorderColor, 2, BorderStyle,
                BorderColor, 2, BorderStyle,
                BorderColor, 2, BorderStyle);
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rectL1 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 0, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL2 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 1, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL3 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 2, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            Rectangle rectL4 = new Rectangle(1, 1 + (panel_tl_entity.Height - 4) / 4 * 3, panel_tl_entity.Width, (panel_tl_entity.Height - 4) / 4);
            e.Graphics.DrawString(student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]", drawFont, Brushes.Black, rectL1, stringFormat);
            e.Graphics.DrawString(exam.Time + "     " + exam.Duration + "min", drawFont, Brushes.Black, rectL2, stringFormat);
            e.Graphics.DrawString(exam.Teacher1 + "  " + exam.Teacher2 + "  " + exam.Teacher3, drawFont, Brushes.Black, rectL3, stringFormat);
            e.Graphics.DrawString(exam.Subject + "  " + exam.Examroom + "  [" + exam.Preparationroom + "]", drawFont, Brushes.Black, rectL4, stringFormat);
            string line1 = student.Firstname + " " + student.Lastname + "  [" + student.Grade + "]\n";
            string line2 = exam.Time + "     " + exam.Duration + "min\n";
            string line3 = exam.Teacher1 + "  " + exam.Teacher2 + "  " + exam.Teacher3 + "\n";
            string line4 = exam.Subject + "  " + exam.Examroom + "  [" + exam.Preparationroom + "]";
            ToolTip sfToolTip1 = new ToolTip();
            sfToolTip1.SetToolTip(panel_tl_entity, line1 + line2 + line3 + line4);
        }

        public void SetBorder(Color borderColor, bool solidBorder)
        {
            this.Border = true;
            if (solidBorder) BorderStyle = ButtonBorderStyle.Solid;
            else BorderStyle = ButtonBorderStyle.Dashed;
            this.BorderColor = borderColor;
            Panel.Refresh();
        }

        public void RemoveBorder()
        {
            Border = false;
            Panel.Refresh();
        }
    }
}