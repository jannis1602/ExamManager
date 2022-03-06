using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // TODO: get fullname
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
            string domain = Properties.Settings.Default.email_domain;
            if (domain.Length < 2) return null;
            string email = Firstname.ToLower().Replace(' ', '.').Replace('_', '.') + "." + Lastname.ToLower().Replace(" ", ".").Replace('_', '.') + "@" + domain;
            if (setEmail) { this.Email = email; this.Edit(email: email); }
            return email;
        }

        public string Fullname()
        {
            return Firstname + " " + Lastname;
        }

        public void Edit(string firstname = null, string lastname = null, string grade = null, string email = null, string phonenumber = null)
        {
            if (firstname != null) this.Firstname = firstname;
            if (lastname != null) this.Lastname = lastname;
            if (grade != null) this.Grade = Grade;
            if (email != null) this.Email = email;
            if (phonenumber != null) this.Phonenumber = phonenumber;
            Program.database.EditStudent(this.Id, this.Firstname, this.Lastname, this.Grade, this.Email, this.Phonenumber);
        }

        public void Delete()
        {
            Program.database.DeleteStudent(this.Id);
        }
    }
}
