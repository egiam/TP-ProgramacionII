using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public abstract class ConnectionToSQL
    {
        private readonly string connectionString;
        public ConnectionToSQL()
        {
            connectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=facturacion;Integrated Security=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
