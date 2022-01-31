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

            // strg+enter -> add
            // rename form1 
            // check if fach 1 null und fach 3 notnull
            // show search string
            // tabindex
            // liste exportieren
            // idea: Count exams per date in listviewexams
            // teacher subject dropdown
            // link autocomplete to id?
            // (lehrer doppelnamen)
            // insert file request grade
            // open forms only once
            // editexam btn left top (1klick-> show exam(-> block boxes))
            // entertaste in textbox
            // doppelnamen filter -> fehlersuche
            // lehrer und schüler nur in einem raum zur zeit
            // Add Schüler Form nach loadlist nur mit neuen
            // Lehrzeichen plathalter
            // allstudents filter + better layout
            // teacher exam dropdown
            // exam students dropdown abc sortiert
            // switch grade and student 
            // delete old funktion
            // form showdialog
        }
    }
}
