using System;
using System.Windows.Forms;

namespace ExamManager
{
    static class Program
    {
        public static Database database = null;
        [STAThread]
        static void Main()
        {
            if (Properties.Settings.Default.color_theme == 0)
                Colors.ColorTheme(Colors.Theme.dark);
            else if (Properties.Settings.Default.color_theme == 1)
                Colors.ColorTheme(Colors.Theme.light);
            else if (Properties.Settings.Default.color_theme == 2)
                Colors.ColorTheme(Colors.Theme.bw);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            database = new Database();
            Application.Run(new Form1());

            //public event EventHandler UpdateList;
            //UpdateList.Invoke(this,null);

            // if (e.KeyData == (Keys.Control | Keys.Enter))

            // info vorsitz prüfer protokoll
            // student/teacher + Exam object
            // - string[] fn,ln,grade,...
            // - Panel obj.
            // tabindex
            // export data to txt file
            // color theme
            // connection status?
            // unterschiedliche prüfungsarten (1 schueler, 2/3 schueler)
            // teacher email ?
            // exam gleicher lehrer
            // edit_students_form faster load
            // teacherData filter subject
            // hide timelines without exams in filter...
            // editteacher = null etc. (teacher)
            // Search index -> enum
            // class for filereader + formart manager
            // data -> expand default
            // studentdata menu: load from file?
            // studentdata order firstname
            // student/teacherdata -> tableview edit/delet
            // readonly mode
            // autocomplete lastname
            // namen immer mit mit _
            // studentdata max entities -> multiple pages
            // list clear thread: create new pannels add...
            // search etc. in taskbar(top)
            // DB getStudent(grade=null) if>1 -> error
            // EditStudent(id,fn=null,ln=null,...)
            // roompanel size y+1?
            // Data filter doppelnamen
            // Settings last/firstname || first/lastname -> [student,teacher]
            // FileReaderClass (#grade, #formart,...)
            // Student/teacher Data paintPanel
            // Student/teacher Data multiple pages
            // Student Data default grade = all
            // ReadOnlyMode [-> if(readonly)return;] enum mode{all,read,write} disable write/edit
            // export data as csv (student/teacher/exam) 
            // import data from csv
            // *Extra load Exel file
            // Settings Form
            // TimeLine & tlEntity Object
            // load exams -> check if teacher exists
            // AddExam Vorschau -> dragPanel move with mouse (blocks=15min shift->1min)
            // custom autocomplete? if string.contains...-> show...



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
