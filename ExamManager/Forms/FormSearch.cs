using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormSearch : Form
    {
        readonly ComboBox cb_search;
        readonly int searchmode = 0;
        public FormSearch(int mode)
        {
            InitializeComponent();
            this.searchmode = mode;
            if (mode == 0)
            {
                cb_search = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                    Location = new System.Drawing.Point(30, 20),
                    Margin = new Padding(30, 20, 30, 5),
                    Name = "tb_search",
                    Size = new System.Drawing.Size(324, 26),
                    DropDownStyle = ComboBoxStyle.DropDown,
                    TabIndex = 0
                };
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //
                LinkedList<string[]> allTeachers = Program.database.GetAllTeachers();
                string[] teacher = new string[allTeachers.Count];
                for (int i = 0; i < allTeachers.Count; i++)
                    teacher[i] = allTeachers.ElementAt(i)[1] + " " + allTeachers.ElementAt(i)[2];
                cb_search.Items.AddRange(teacher);
                //
                var autocomplete_teacher = new AutoCompleteStringCollection();
                string[] students = new string[allTeachers.Count];
                for (int i = 0; i < allTeachers.Count; i++)
                    students[i] = (allTeachers.ElementAt(i)[1] + " " + allTeachers.ElementAt(i)[2]);
                autocomplete_teacher.AddRange(students);
                cb_search.AutoCompleteCustomSource = autocomplete_teacher;
                cb_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb_search.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else if (mode == 1) // student
            {
                cb_search = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                    Location = new System.Drawing.Point(30, 20),
                    Margin = new Padding(30, 20, 30, 5),
                    Name = "tb_search",
                    Size = new System.Drawing.Size(324, 26),
                    DropDownStyle = ComboBoxStyle.DropDown,
                    TabIndex = 0
                };
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //
                LinkedList<string[]> allStudents = Program.database.GetAllStudents();
                string[] students = new string[allStudents.Count];
                for (int i = 0; i < allStudents.Count; i++)
                    students[i] = allStudents.ElementAt(i)[1] + " " + allStudents.ElementAt(i)[2];
                cb_search.Items.AddRange(students);
                //
                var autocomplete_student = new AutoCompleteStringCollection();
                autocomplete_student.AddRange(students);
                cb_search.AutoCompleteCustomSource = autocomplete_student;
                cb_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb_search.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else if (mode == 2) // subject
            {
                cb_search = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                    Location = new System.Drawing.Point(30, 20),
                    Margin = new Padding(30, 20, 30, 5),
                    Name = "tb_search",
                    Size = new System.Drawing.Size(324, 26),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    TabIndex = 0
                };
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //cb_search
                LinkedList<string[]> subjectList = Program.database.GetAllSubjects();
                string[] subjects = new string[subjectList.Count];
                for (int i = 0; i < subjectList.Count; i++)
                    subjects[i] = subjectList.ElementAt(i)[0];
                cb_search.Items.AddRange(subjects);
            }
            else if (mode == 3) // room
            {
                cb_search = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                    Location = new System.Drawing.Point(30, 20),
                    Margin = new Padding(30, 20, 30, 5),
                    Name = "tb_search",
                    Size = new System.Drawing.Size(324, 26),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    TabIndex = 0
                };
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //cb_search
                LinkedList<string[]> roomList = Program.database.GetAllRooms();
                string[] rooms = new string[roomList.Count];
                for (int i = 0; i < roomList.Count; i++)
                    rooms[i] = roomList.ElementAt(i)[0];
                cb_search.Items.AddRange(rooms);
            }
            else if (mode == 4) // grade
            {
                cb_search = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                    Location = new System.Drawing.Point(30, 20),
                    Margin = new Padding(30, 20, 30, 5),
                    Name = "tb_search",
                    Size = new System.Drawing.Size(324, 26),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    TabIndex = 0
                };
                cb_search.PreviewKeyDown += new PreviewKeyDownEventHandler(tb_search_PreviewKeyDown);
                tlp_main.Controls.Add(cb_search);
                //cb_search
                LinkedList<string[]> allStudents = Program.database.GetAllStudents();
                LinkedList<string> gradeList = new LinkedList<string>();
                foreach (string[] s in allStudents)
                    if (!gradeList.Contains(s[3]))
                        gradeList.AddLast(s[3]);
                List<string> templist = new List<string>(gradeList);
                templist = templist.OrderBy(x => x).ToList();
                gradeList = new LinkedList<string>(templist);
                string[] list = new string[gradeList.Count];
                for (int i = 0; i < gradeList.Count; i++)
                    list[i] = gradeList.ElementAt(i);
                cb_search.Items.AddRange(list);
            }
        }

        private void search()
        {
            if (cb_search != null)
                if (cb_search.Text.Length > 0)
                {
                    if (searchmode == 0)    // teacher
                    {
                        string s = cb_search.Text.First().ToString().ToUpper() + (cb_search.Text.Substring(1));
                        string tid = Program.database.GetTeacherByName(s.Split(' ')[0], s.Split(' ')[1])[0];
                        UpdateSearch.Invoke(tid, null);
                        this.Dispose();
                    }
                    else if (searchmode == 1)  // student
                    {
                        string s = cb_search.Text.First().ToString().ToUpper() + (cb_search.Text.Substring(1));
                        UpdateSearch.Invoke(s, null);
                        this.Dispose();
                    }
                    else
                    {
                        string s = cb_search.Text;
                        UpdateSearch.Invoke(s, null);
                        this.Dispose();
                    }
                }
        }
        public event EventHandler UpdateSearch;
        private void btn_search_Click(object sender, EventArgs e)
        { search(); }

        private void tb_search_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) search();
        }
    }
}
