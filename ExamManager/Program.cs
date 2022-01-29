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

            // strg+enter -> add
            // rename form1 
            // check if fach 1 null und fach 3 notnull
            // show search string
            // room dropdown
            // tabindex
            // liste exportieren
            // idea: Count exams per date in listviewexams
            // mailgenerator settings
            // teacher subject dropdown
            // link autocomplete to id?
            // static form sizes
            // lehrer doppelnamen 
            // insert file request grade
        }
    }
}
