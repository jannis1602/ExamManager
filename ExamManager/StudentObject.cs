using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamManager
{
    class StudentObject
    {
        public int Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Grade { get; private set; }
        public string Email { get; private set; }
        public string Phonenumber { get; private set; }
        public StudentObject(int id, string firstname, string lastname, string grade, string email = null, string phone_number = null)
        {
            this.Id = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Grade = grade;
            this.Email = email;
            this.Phonenumber = phone_number;
        }

        public string GenerateEmail(bool setEmail = false)
        {
            string domain = Properties.Settings.Default.EmailDomain;
            if (domain.Length < 2) return null;
            string email = Firstname.ToLower().Replace(' ', '.').Replace('_', '.') + "." + Lastname.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
            if (setEmail) { this.Email = email; this.Edit(email: email); }
            return email;
        }

        public string Fullname()
        {
            if (Properties.Settings.Default.NameOrderStudent)
                return Firstname + " " + Lastname;
            else return Lastname + " " + Firstname;
        }

        public void Edit(string firstname = null, string lastname = null, string grade = null, string email = null, string phonenumber = null)
        {
            if (firstname != null) this.Firstname = lastname;
            if (lastname != null) this.Lastname = firstname;
            if (grade != null) this.Grade = Grade;
            if (email != null) this.Email = email;
            if (phonenumber != null) this.Phonenumber = phonenumber;
            Program.database.EditStudent(this.Id, this.Firstname, this.Lastname, this.Grade, this.Email, this.Phonenumber);
            /*if (!Properties.Settings.Default.NameOrderStudent)
                Program.database.EditStudent(this.Id, this.Firstname, this.Lastname, this.Grade, this.Email, this.Phonenumber);
            else Program.database.EditStudent(this.Id, this.Lastname, this.Firstname, this.Grade, this.Email, this.Phonenumber);*/
        }

        public bool AddToDatabase()
        {
            string check = null;
            if (Program.database.GetStudentByName(Firstname, Lastname) != null) check = "Schüler mit gleichem Namen exestiert bereits: " + Fullname();
            if (check == null)
            {
                Console.WriteLine("check=null " + Fullname());
                Program.database.AddStudent(this);
                UpdateDBData();
                return true;
            }
            else
            {
                Console.WriteLine("!!! check!=null " + Fullname());
                MessageBox.Show(check, "Fehler");
            } // UpdateDBData(); }
            return false;
        }
        public void UpdateDBData(StudentObject so = null)
        {
            if (Id == 0 && so == null) so = Program.database.GetStudentByName(Firstname, Lastname);
            if (so == null) so = Program.database.GetStudentByID(Id);
            if (so == null) return;
            this.Id = so.Id;
            this.Firstname = so.Firstname;
            this.Lastname = so.Lastname;
            this.Grade = so.Grade;
            this.Email = so.Email;
            this.Phonenumber = so.Phonenumber;
        }

        public void SetGrade(string grade)
        {
            this.Grade = grade;
        }

        public void Delete()
        {
            Program.database.DeleteStudent(this.Id);
        }
    }
}
