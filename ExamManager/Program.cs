﻿using System;
using System.Windows.Forms;

namespace ExamManager
{
    static class Program
    {
        public static Database database = null;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            database = new Database();
            Application.Run(new Form1());

            //public event EventHandler UpdateList;
            //UpdateList.Invoke(this,null);

            // student/teacher object
            // tabindex
            // daten als liste exportieren
            // idea: Count exams per date in listviewexams
            // teacher subject dropdown
            // (lehrer doppelnamen)
            // editexam btn left top (1klick-> show exam(-> block boxes))
            // entertaste in textbox
            // doppelnamen filter -> fehlersuche
            // check lehrer und schüler nur in einem raum zur zeit
            // Lehrzeichen Plathalter
            // allstudents filter/order + better layout
            // delete old exams funktion
            // prep room can be null
            // color theme
            // export data to txt file
            // if get student > 1 -> error! + Message
            // connection status?
            // suche tooltipp -> more info
            // check if vorbereitungsraum is empty
            // remove addexam if id=null
            // unterschiedliche prüfungsarten (1 schueler, 2/3 schueler)
            // exam doppelclick -> open edit and mark exam in timeline
            // teacher email
            // exam gleicher lehrer
            // fächer lehrer filtern -> WARNUNG -> +ausnahmen
            // edit students form faster load
            // readteacherfile -> add subject
            // select teacher -> delete multiple
            // filter grade order abc
            // show filter
            // filter teacher
            // hide timelines without exams in filter
            // rerender timeline
            // editteacher = null etc. (teacher)
            // getallstudents order first grade then lastname
            // studentlist per grade
            // Tools - Extras: load exel file
            // formfilter -> action...
            // Search index -> enum
            // InsertTeacherFileIntoDB -> return idlist -> openform
            // (suche dropdown check)
            // Studentdata search string (tsmi_box?) -> hide others
            // teacherdata menubar
            // class for filereader + formart manager
            // data -> expand default
            // studentdata menu: load from file?

            // student/teacher object
            // namen mit _
            // studentdata max entities -> multiple sides

            // getstudentbyname: -> remove  " " and "_"
            // SELECT CONCAT('SQL', ' is', ' fun!');
            // Filter in einem Fenster grade etc.


            /*LinkedList<Item> list = new LinkedList<Item>();
            foreach (string[] s in Program.database.GetAllExams())
            {
                if (!list.Any(n => n.date == s[1]))
                {
                    DateTime dt = DateTime.ParseExact(s[1], "dd.MM.yyyy", null);
                    list.AddLast(new Item(s[1], s[1] + "  ->  " + Program.database.GetAllExamsAtDate(dt.ToString("yyyy-MM-dd")).Count().ToString() + " Prüfungen"));
                }
            }
            Item[] dates = new Item[list.Count];
            for (int i = 0; i < list.Count; i++)
                dates[i] = list.ElementAt(i);
            lb_exam_date.DisplayMember = nameof(Item.title);
            lb_exam_date.Items.Clear();
            lb_exam_date.Items.AddRange(dates);

            List<Item> item = new List<Item>();
        class Item
        {
            public string date { get; }
            public string title { get; }

            public Item(string date, string title)
            {
                this.date = date;
                this.title = title;
            }
        }*/
        }
    }
}
