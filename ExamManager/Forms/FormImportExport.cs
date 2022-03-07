using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    public partial class FormImportExport : Form
    {
        public FormImportExport()
        {
            InitializeComponent();
        }

        private void btn_exam_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Daten speichern",
                FileName = "ExamData.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var json = JsonConvert.SerializeObject(Program.database.GetAllExams(), Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }

        private void btn_exam_import_Click(object sender, EventArgs e) // TODO: import exams and check if student/teacher exists else create
        {
            string filePath;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    List<ExamObject> items;
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string jsonString = r.ReadToEnd();
                        items = JsonConvert.DeserializeObject<List<ExamObject>>(jsonString);
                    }
                    foreach (ExamObject so in items)
                        Console.WriteLine(so.Id);
                }
            }
        }

        private void btn_student_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Schülerdaten speichern",
                FileName = "StudentData.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                CheckFileExists = true,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var json = JsonConvert.SerializeObject(Program.database.GetAllStudents(), Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }

        private void btn_student_import_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    List<StudentObject> items;
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string jsonString = r.ReadToEnd();
                        items = JsonConvert.DeserializeObject<List<StudentObject>>(jsonString);
                    }
                }
            }
        }

        private void btn_teacher_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Lehrerdaten speichern",
                FileName = "TeachertData.json",
                DefaultExt = "json",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 2,
                CheckFileExists = true,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var json = JsonConvert.SerializeObject(Program.database.GetAllTeachers(), Formatting.Indented);
            File.WriteAllText(sfd.FileName, json);
        }

        private void btn_teacher_import_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    List<TeacherObject> items;
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string jsonString = r.ReadToEnd();
                        items = JsonConvert.DeserializeObject<List<TeacherObject>>(jsonString);
                    }
                }
            }
        }
    }
}
