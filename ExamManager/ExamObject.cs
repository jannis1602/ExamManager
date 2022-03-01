using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManager
{
    class ExamObject
    {
        public int Id { get; private set; }
        public string Date { get; private set; }
        public string Time { get; private set; }
        public string Examroom { get; private set; }
        public string Preparationroom { get; private set; }
        public int StudentId { get; private set; }
        public StudentObject Student { get; private set; }
        public string Teacher1 { get; private set; }
        public string Teacher2 { get; private set; }
        public string Teacher3 { get; private set; }
        public string Subject { get; private set; }
        public int Duration { get; private set; }

        public ExamObject(int id, string date, string time, string examroom, string preparationroom, int student, string teacher1, string teacher2, string teacher3, string subject, int duration)
        {
            this.Id = id;
            this.Date = date;
            this.Time = time;
            this.Examroom = examroom;
            this.Preparationroom = preparationroom;
            this.StudentId = student;
            this.Teacher1 = teacher1;
            this.Teacher2 = teacher2;
            this.Teacher3 = teacher3;
            this.Subject = subject;
            this.Duration = duration;
            this.Student = Program.database.GetStudentByID(student);
            // TODO: check teacher in db
        }

        public void Edit(string date = null, string time = null, string examroom = null, string preparationroom = null, int student = 0, string teacher1 = null, string teacher2 = null, string teacher3 = null, string subject = null, int duration = 0)
        {
            if (date != null) this.Date = date;
            if (time != null) this.Time = time;
            if (examroom != null) this.Examroom = examroom;
            if (preparationroom != null) this.Preparationroom = preparationroom;
            if (student != 0) this.StudentId = student; this.Student = Program.database.GetStudentByID(student);
            if (teacher1 != null) this.Teacher1 = teacher1;
            if (teacher2 != null) this.Teacher2 = teacher2;
            if (teacher3 != null) this.Teacher3 = teacher3;
            if (subject != null) this.Subject = subject;
            if (duration != 0) this.Duration = duration;

            Program.database.EditExam(this.Id, this.Date, this.Time, this.Examroom, this.Preparationroom, this.StudentId, this.Teacher1, this.Teacher2, this.Teacher3, this.Subject, this.Duration);
        }

        public Panel GetTimelineEntity
    }
}
