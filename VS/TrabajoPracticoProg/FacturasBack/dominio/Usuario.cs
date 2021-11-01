using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.dominio
{
    public class Usuario
    {
        public  int UserID { get; set; }
        public  string Position { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public  string LoginName { get; set; }
        public  string Password { get; set; }

        public Usuario() { }

        public Usuario(int userid,string posit, string Fname, string Lname, string email, string Lgname, string pass)
        {
            UserID = userid;
            Position = posit;
            FirstName = Fname;
            LastName = Lname;
            Email = email;
            LoginName = Lgname;
            Password = pass;
        }

    }
}
