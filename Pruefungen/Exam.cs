using System;


namespace Pruefungen
{
    class Exam
    {
        private int id { get; }
        private DateTime date;
        private DateTime time;
        private string exam_room;

        public Exam(int id, string date, string time, string exam_room, string preparation_room, string student, string t1, string t2, string t3, string subject, int duartion)
        {
            this.id = id;
            this.date = DateTime.ParseExact(date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None);
            this.time = DateTime.ParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            this.exam_room = exam_room;
            // ...
        }

        // get date and time as string

    }
}
