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

        public void anyMethod()
        {
            if (UserLoginCache.Position == Positions.Empleado)
            {
                //Lineas o métodos que quieras ejecutar para el cargo recepcionita
            }
            if (UserLoginCache.Position == Positions.Gerente)
            {
                //Lineas o métodos que quieras ejecutar para el cargo contador
            }
            if (UserLoginCache.Position == Positions.Administrador)
            {
                //Lineas o métodos que quieras ejecutar para el cargo contador
            }
            if (UserLoginCache.Position == Positions.Cliente)
            {
                //Lineas o métodos que quieras ejecutar para el cargo contador
            }
        }
    }
}
