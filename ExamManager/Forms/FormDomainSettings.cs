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
    public partial class FormDomainSettings : Form
    {
        public FormDomainSettings()
        {
            InitializeComponent();
            tb_maildomain.Text = Properties.Settings.Default.EmailDomain;
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EmailDomain = tb_maildomain.Text;
            Properties.Settings.Default.Save();
        }
    }
}
