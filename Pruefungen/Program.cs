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

            // draw or chart???

            // Student and Teacher Object
            // config add 45min when add new
            // duration time picker?
            // strg+enter -> add
            // fach dropdown
        }
    }
}
