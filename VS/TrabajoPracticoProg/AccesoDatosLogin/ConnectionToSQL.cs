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

namespace AccesoDatosLogin
{
    public abstract class ConnectionToSQL
    {
        private readonly string ConnectionString;
        public ConnectionToSQL()
        {
            ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=facturacion;Integrated Security=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
