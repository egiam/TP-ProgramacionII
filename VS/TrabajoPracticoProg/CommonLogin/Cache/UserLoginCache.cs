using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLogin.Cache
{
    public class UserLoginCache
    {
        public int UserID { get; set; }
        public string Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

        public UserLoginCache() { }

        public UserLoginCache(int UserID, string Position, string FirstName, string LastName, string Email, string LoginName, string Password)
        {
            this.UserID = UserID;
            this.Position = Position;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.LoginName = LoginName;
            this.Password = Password;
        }
    }
}
