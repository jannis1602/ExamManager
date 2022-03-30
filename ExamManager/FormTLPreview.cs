using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormTLPreview : Form
    {
        TimeLineObject tl;
        public FormTLPreview()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            DateTime Value = DateTime.ParseExact(Properties.Settings.Default.TimelineDate, "dd.MM.yyyy", null);
            string date = Value.ToString("yyyy-MM-dd");
            //tl = new TimeLineObject(date, Program.database.GetAllExamsAtDate(date));
            tl = new TimeLineObject(date, Program.database.GetAllExamsFromTeacherAtDate(date, "BRE"));
            tl.ExportPNG();
            panel_main.Controls.Add(tl.Panel);
        }

    }
}
