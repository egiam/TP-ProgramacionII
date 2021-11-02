using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CommonLogin.Cache;


namespace AccesoDatosLogin
{
    public class UserDao : ConnectionToSQL
    {
        public bool Login(string user, string pass)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Users where LoginName=@user and Password=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.UserID = reader.GetInt32(0);
                            UserLoginCache.LoginName = reader.GetString(1);
                            UserLoginCache.Password = reader.GetString(2);
                            UserLoginCache.FirstName = reader.GetString(3);
                            UserLoginCache.LastName = reader.GetString(4);
                            UserLoginCache.Email = reader.GetString(5);
                            UserLoginCache.Position = reader.GetString(6);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
        }


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
