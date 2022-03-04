using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManager
{
    class TeacherObject
    {
        public string Shortname { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Phonenumber { get; private set; }
        public string Subject1 { get; private set; }
        public string Subject2 { get; private set; }
        public string Subject3 { get; private set; }
        // TODO: get fullname

        public TeacherObject(string shortname, string firstname, string lastname, string email, string phonenumber, string subject1, string subject2 , string subject3)
        {
            this.Shortname = shortname;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Phonenumber = phonenumber;
            this.Subject1 = subject1;
            this.Subject2 = subject2;
            this.Subject3 = subject3;
        }

        public void Edit(string shortname, string firstname = null, string lastname = null, string email = null, string phonenumber = null, string subject1 = null, string subject2 = null, string subject3 = null)
        {
            if (shortname != null) this.Shortname = shortname;
            if (firstname != null) this.Firstname = firstname;
            if (lastname != null) this.Lastname = lastname;
            if (email != null) this.Email = email;
            if (phonenumber != null) this.Phonenumber = phonenumber;
            if (subject1 != null) this.Subject1 = subject1;
            if (subject2 != null) this.Subject2 = subject2;
            if (subject3 != null) this.Subject3 = subject3;
            Program.database.EditTeacher(this.Shortname, this.Firstname, this.Lastname, this.Email, this.Phonenumber, this.Subject1, this.Subject2, this.Subject3);
        }

        public void Delete()
        {
            Program.database.DeleteTeacher(this.Shortname);
        }

    }
}
