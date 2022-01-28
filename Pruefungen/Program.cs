using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pruefungen
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

            // Student and Teacher Object
            // config add 45min when add new
            // strg+enter -> add
            // subject dropdown

            // database subjects and rooms
            // rename form1 and form2

            // room side panel scroll with time line

            // delete panelside paint

            // rename database students to student
            // test if fach 1 null und fach 3 notnull

            // change exam Room
            // show search string
        }
    }
}
