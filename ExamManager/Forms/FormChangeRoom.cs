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
    public partial class FormChangeRoom : Form
    {
        string date;
        public FormChangeRoom(string date)
        {
            this.date = date;
            InitializeComponent();
            cb_oldroom.Items.Clear();
            cb_newroom.Items.Clear();
            LinkedList<string> roomList = new LinkedList<string>();
            LinkedList<string[]> rooms = Program.database.GetAllRooms();
            foreach (string[] s in rooms)
                if (!roomList.Contains(s[0])) roomList.AddLast(s[0]);
            List<string> temproomlist = new List<string>(roomList);
            temproomlist = temproomlist.OrderBy(x => x).ToList();
            roomList = new LinkedList<string>(temproomlist);
            string[] item_list = new string[roomList.Count];
            for (int i = 0; i < roomList.Count; i++)
                item_list[i] = roomList.ElementAt(i);
            cb_oldroom.Items.AddRange(item_list);
            cb_newroom.Items.AddRange(item_list);
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (cb_oldroom.SelectedItem.ToString().Length > 0 && cb_newroom.SelectedItem.ToString().Length > 0)
            {
                Program.database.EditExamRoom(date, cb_oldroom.SelectedItem.ToString(), cb_newroom.SelectedItem.ToString()); ;
                this.Dispose();
            }
        }

        private void tb_newroom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (cb_oldroom.SelectedItem.ToString().Length > 0 && cb_newroom.SelectedItem.ToString().Length > 0)
                {
                    Program.database.EditExamRoom(date, cb_oldroom.SelectedItem.ToString(), cb_newroom.SelectedItem.ToString());
                    e.Handled = true;
                    this.Dispose();
                }
            }
        }
    }
}
