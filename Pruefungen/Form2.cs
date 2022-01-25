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
    public partial class Form_grid : Form
    {
        public Form_grid()
        {
            InitializeComponent();
        }

        private void Form_grid_Load(object sender, EventArgs e)
        {
            List<string[]> data = new List<string[]>();
            foreach (string[] s in Program.database.GetAllExams())
                data.Add(s);
            //data.Add(new string[] { "2022-02-11", "08:00" });
            foreach (string[] s in data)
            {
                string[] student = Program.database.GetStudentByID(Int32.Parse(s[5]));
                this.dataGridView1.Rows.Add(s[0], s[1], s[2], s[3], s[4],s[5]+" -> "+ student[1] + " " + student[2], s[6], s[7], s[8], s[9], s[10]);
            }

        }

        internal void Data_update()
        {
            dataGridView1.Rows.Clear();
            List<string[]> data = new List<string[]>();
            foreach (string[] s in Program.database.GetAllExams())
                data.Add(s);
            //data.Add(new string[] { "2022-02-11", "08:00" });
            foreach (string[] s in data)
            {
                string[] student = Program.database.GetStudentByID(Int32.Parse(s[5]));
                this.dataGridView1.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5] + " -> " + student[1] + " " + student[2], s[6], s[7], s[8], s[9], s[10]);
            }
        }
    }
}
