using System;
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

            // strg+enter -> add
            // rename form1 
            // test if fach 1 null und fach 3 notnull
            // show search string
            // room dropdown
            // tabindex
        }
    }
}
