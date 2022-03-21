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
    public partial class FormRoomFilter : Form
    {
        public LinkedList<string> RoomList;
        public FormRoomFilter(LinkedList<string> roomList)
        {
            this.RoomList = roomList;
            InitializeComponent();
            clb_rooms.Items.AddRange(roomList.ToArray());
            for (int i = 0; i < clb_rooms.Items.Count; i++)
                clb_rooms.SetItemChecked(i, true);

        }
        public event EventHandler SelectedRoomList;
        private void FormRoomFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            LinkedList<string> list = new LinkedList<string>();
            foreach (string s in clb_rooms.CheckedItems)
                list.AddLast(s);
            SelectedRoomList.Invoke(list, null);
        }
    }
}
