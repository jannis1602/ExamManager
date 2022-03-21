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
    public partial class FormProgressBar : Form
    {
        public FormProgressBar()
        {
            InitializeComponent();
        }

        public void StartPrograssBar(int min, int max)
        {
            progressBar.Minimum = min;
            progressBar.Maximum = max;
            progressBar.Step = 1;
            progressBar.Value = 1;
        }
        public void Update(int value)
        {
            progressBar.Value = value;
        }
        public void AddOne()
        {
            progressBar.PerformStep();
            if (progressBar.Value >= progressBar.Maximum)
                this.Dispose();
        }
        public void Exit()
        {
            this.Dispose();
        }
    }
}
