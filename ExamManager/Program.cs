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

            // new FormTLPreview().ShowDialog();
            Application.Run(new Form1());
            //}
        }

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

        // tl_entity mouse up -> move timeline up?
        // nur 1/2 teacher 
        // Doc: Data etc. unterpunkte erklären 
        // student/teacher obj -> panel...
        // database change summary
        // Database: int StudentId
        // editpanel cursor: move
        // cb teacher 2+3 add "-"
        // editpanel preview change if data change

        // info vorsitz prüfer protokoll
        // tabindex
        // exam gleicher lehrer
        // class for filereader + formart manager
        // studentdata menu: load from file?
        // namen immer mit mit _
        // list clear thread: create new pannels add...
        // DB getStudent(grade=null) if>1 -> error
        // EditStudent(id,fn=null,ln=null,...)
        // roompanel empty size y+1?
        // FileReaderClass (#grade, #format,...)
        // Student/teacher Data paintPanel
        // Student Data default grade = all
        // + ReadOnlyMode [-> if(readonly)return;] enum mode{all,read,write} disable write/edit
        // + export exam with student and teacher -> keep id / change to same
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
