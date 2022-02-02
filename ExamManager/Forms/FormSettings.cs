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
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            tb_maildomain.Text = Properties.Settings.Default.email_domain;
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.email_domain = tb_maildomain.Text;
            Properties.Settings.Default.Save();
        }
    }
}
