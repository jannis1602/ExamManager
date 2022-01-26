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
            // timeline sort

            // room side panel scroll with time line
        }
    }
}
