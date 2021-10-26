using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using AccesoDatosLogin;
using CommonLogin.Cache;

namespace DominoLogin
{
    public class UserModel
    {
        UserDao userDao = new UserDao();
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }
        //public bool EditPassword(int user, string pass)
        //{
        //    if (user == UserLoginCache.UserID)
        //    {

        //    }
        //    return true;
        //}
    }
}
