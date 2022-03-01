using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormExamDateListView : Form
    {
        readonly Form1 form;
        public FormExamDateListView(Form1 form)
        {
            this.form = form;
            InitializeComponent();
            /*LinkedList<string> list = new LinkedList<string>();
            foreach (string[] s in Program.database.GetAllExams())
            {
                if (!list.Contains(s[1]))
                    list.AddLast(s[1]);
            }
            string[] dates = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                dates[i] = list.ElementAt(i);
            lb_exam_date.Items.Clear();
            lb_exam_date.Items.AddRange(dates);*/

            LinkedList<Item> list = new LinkedList<Item>();
            foreach (ExamObject s in Program.database.GetAllExams(true))
            {
                if (!list.Any(n => n.date == s.Date))
                {
                    DateTime dt = DateTime.ParseExact(s.Date, "dd.MM.yyyy", null);
                    list.AddLast(new Item(s.Date, s.Date + "  ->  " + Program.database.GetAllExamsAtDate(dt.ToString("yyyy-MM-dd")).Count().ToString() + " Prüfungen"));
                }
            }
            Item[] dates = new Item[list.Count];
            for (int i = 0; i < list.Count; i++)
                dates[i] = list.ElementAt(i);
            lb_exam_date.DisplayMember = nameof(Item.title);
            lb_exam_date.Items.Clear();
            lb_exam_date.Items.AddRange(dates);

            List<Item> item = new List<Item>();
        }
        class Item
        {
            public string date { get; }
            public string title { get; }

            public Item(string date, string title)
            {
                this.date = date;
                this.title = title;
            }
        }

        private void lb_exam_date_DoubleClick(object sender, EventArgs e)
        {
            if (lb_exam_date.SelectedItem != null)
            {
                Item itm = lb_exam_date.SelectedItem as Item;  // lb_exam_date.SelectedItem.ToString()
                form.SetDate(DateTime.ParseExact(itm.date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None));
            }
        }
    }
}
