using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class FormChangeRoom : Form
    {
        string date;
        public FormChangeRoom(string date)
        {
            this.date = date;
            InitializeComponent();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (tb_oldroom.Text.Length > 0 && tb_newroom.Text.Length > 0)
            {
                Program.database.EditExamRoom(date, tb_oldroom.Text, tb_newroom.Text);
                this.Dispose();
            }
        }

        private void tb_newroom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tb_oldroom.Text.Length > 0 && tb_newroom.Text.Length > 0)
                {
                    Program.database.EditExamRoom(date, tb_oldroom.Text, tb_newroom.Text);
                    e.Handled = true;
                    this.Dispose();
                }
            }
        }
    }
}
