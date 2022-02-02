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
            database = new Database();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //public event EventHandler UpdateList;
            //UpdateList.Invoke(this,null);

            // show search string
            // tabindex
            // daten als liste exportieren
            // idea: Count exams per date in listviewexams
            // teacher subject dropdown
            // link autocomplete to id?
            // (lehrer doppelnamen)
            // insert file request grade
            // editexam btn left top (1klick-> show exam(-> block boxes))
            // entertaste in textbox
            // doppelnamen filter -> fehlersuche
            // check lehrer und schüler nur in einem raum zur zeit
            // Add Schüler Form nach loadlist nur mit neuen
            // Lehrzeichen plathalter
            // allstudents filter/order + better layout
            // delete old exams funktion
            // box always generate mail?
            // daten -> rename to "hinzufügen" (schueler/lehrer...)
            // prep room can be null
            // start -> date today
            // color theme
            // load teacher from file
            // export data to txt file
            // getStudent (grade=null)
            // if get student > 1 -> error! + Message
            // connection status?
            // suche tooltipp -> more info
            // settings keep last date after closing
        }
    }
}
