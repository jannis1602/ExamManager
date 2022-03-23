using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            /*FormRegistration form = new FormRegistration();
            if (form.locked)
                form.ShowDialog();
            if (form.locked) Application.Exit();
            if (!form.locked)
            {*/
            if (Properties.Settings.Default.ColorTheme == 0) Colors.ColorTheme(Colors.Theme.light);
            else if (Properties.Settings.Default.ColorTheme == 1) Colors.ColorTheme(Colors.Theme.dark);
            else if (Properties.Settings.Default.ColorTheme == 2) Colors.ColorTheme(Colors.Theme.bw);

            database = new Database();
            Application.Run(new Form1());
            //}


            // InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),


            /*Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Console.WriteLine(Properties.strings.test);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
            Console.WriteLine(Properties.strings.test);*/

            //MessageBox.Show("Test", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //DialogResult result = MessageBox.Show("Zeitstrahl in schwarz-weiß exportieren?", "Achtung!", MessageBoxButtons.YesNo);
            //if (result == DialogResult.Yes)

            //public event EventHandler UpdateList;
            //UpdateList.Invoke(this,null);
            // if (e.KeyData == (Keys.Control | Keys.Enter))

            // exam preview on/off
            // tl_entity mouse up -> move timeline up?
            // im/export data form
            // nur 1/2 teacher 
            // update exampreview on change

            // Doc: Data etc. unterpunkte erklären 

            // student/teacher obj -> panel...
            // database change summary
            // Database: int StudentId
            // student/Teacher Object.Delete()
            // editpanel cursor: move
            // cb teacher 2+3 add "-"
            // editExam.RemoveBorder bug
            // move panel with mouse add 15min per move (new+5min))
            // editpanel preview change if data change
            // search filter student: filter s2+s3! etc.
            // exam / student edit in db ifx!=null

            // student/teacher data loading window
            // info vorsitz prüfer protokoll
            // tabindex
            // unterschiedliche prüfungsarten (1 schueler, 2/3 schueler)
            // teacher email ?
            // exam gleicher lehrer
            // edit_students_form faster load
            // teacherData filter subject
            // editteacher = null etc. (teacher)
            // class for filereader + formart manager
            // data -> expand default
            // studentdata menu: load from file?
            // student/teacherdata -> tableview edit/delet
            // namen immer mit mit _
            // studentdata max entities -> multiple pages
            // list clear thread: create new pannels add...
            // search etc. in taskbar(top)
            // DB getStudent(grade=null) if>1 -> error
            // EditStudent(id,fn=null,ln=null,...)
            // roompanel empty size y+1?
            // + Settings last/firstname || first/lastname -> [student,teacher]
            // - autocomplete lastname
            // FileReaderClass (#grade, #format,...)
            // Student/teacher Data paintPanel
            // Student/teacher Data multiple pages
            // Student Data default grade = all
            // + ReadOnlyMode [-> if(readonly)return;] enum mode{all,read,write} disable write/edit
            // + export data as csv / json (student/teacher/exam) 
            // + export exam with student and teacher -> keep id / change to same
            // import data from csv
            // *Extra load Exel file
            // Settings Form
            // + load exams -> check if teacher exists
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
