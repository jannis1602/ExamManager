using System;
using System.Collections.Generic;
using System.Linq;
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
            if (tb_add.Text.Length > 0)
            {
                Program.database.AddRoom(tb_add.Text);
                tb_add.Clear();
                LoadAllRooms();
            }
        }

        private void tb_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (tb_add.Text.Length > 0)
                {
                    Program.database.AddRoom(tb_add.Text);
                    tb_add.Clear();
                    e.Handled = true;
                    LoadAllRooms();
                }
            }
        }

        private void FormRoomData_Load(object sender, EventArgs e)
        {
            LoadAllRooms();
        }

        private void LoadAllRooms()
        {
            LinkedList<string[]> list = Program.database.GetAllRooms();

            string[] rooms = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                rooms[i] = list.ElementAt(i)[0];
            }
            lb_roomlist.Items.Clear();
            lb_roomlist.Items.AddRange(rooms);
        }

        private void lb_roomlist_DoubleClick(object sender, EventArgs e)
        {
            if (lb_roomlist.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Raum " + lb_roomlist.SelectedItem.ToString() + " Löschen?", "Warnung!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Program.database.DeleteRoom(lb_roomlist.SelectedItem.ToString());
                    LoadAllRooms();
                }
            }
        }
    }
}