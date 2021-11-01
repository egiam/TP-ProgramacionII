using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLogin.Cache
{
    public static class UserLoginCache
    {
        public static int UserID { get; set; }
        public static string Position { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Email { get; set; }
        public static string LoginName { get; set; }
        public static string Password { get; set; }
    }
}
