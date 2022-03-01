using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class KeepDataForm : Form
    {
        public KeepDataForm()
        {
            InitializeComponent();
        }

        private void clb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (string s in clb1.SelectedItems) {
                Console.WriteLine(s);
            }
        }
    }
}
