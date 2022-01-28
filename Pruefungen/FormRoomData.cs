using System;
using System.Windows.Forms;

namespace Pruefungen
{
    public partial class FormRoomData : Form
    {
        public FormRoomData()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void tb_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                tb_add.Clear();
                e.Handled = true;
            }
        }
    }
}