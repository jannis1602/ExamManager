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
            // box always generate mail?
            // daten -> rename to "hinzufügen" (schueler/lehrer...)
            // prep room can be null
            // color theme
            // load teacher from file
            // export data to txt file
            // if get student > 1 -> error! + Message
            // connection status?
            // suche tooltipp -> more info
            // settings keep last date after closing
            // check if vorbereitungsraum is empty
            // remove addexam if id=null
            // unterschiedliche prüfungsarten (1 schueler, 2/3 schueler)
            // exam doppelclick -> open edit and mark exam in timeline

            // getstudentbyname: -> remove  " " and "_"
            // SELECT CONCAT('SQL', ' is', ' fun!');
            // Filter in einem Fenster grade etc.
        }
    }
}
