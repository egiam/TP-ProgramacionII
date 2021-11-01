using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLogin.Cache;

namespace AccesoDatosLogin
{
    public class HelperDao
    {
		private static HelperDao instancia;
		//private string cadenaConexion;
		SqlConnection cnn;
		SqlCommand cmd;
		private HelperDao()
		{
			//cadenaConexion = Properties.Resources.strConexion;
			cnn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=facturacion;Integrated Security=True");
			cmd = new SqlCommand();
		}

		public static HelperDao ObtenerInstancia()
		{
			if (instancia == null)
			{
				instancia = new HelperDao();
			}
			return instancia;
		}


		public int EjecutarSQLMaestroDetalle(string spMaestro,  UserLoginCache cache)
		{
			int filasAfectadas = 0;
			SqlTransaction trans = null;

			try
			{
				cmd.Parameters.Clear();
				cnn.Open();
				trans = cnn.BeginTransaction();
				cmd.Connection = cnn;
				cmd.Transaction = trans;
				cmd.CommandText = spMaestro;
				cmd.CommandType = CommandType.StoredProcedure;

				//Parametros
				cmd.Parameters.AddWithValue("@Email", cache.Email);
				cmd.Parameters.AddWithValue("@LoginName", cache.LoginName);
				cmd.Parameters.AddWithValue("@Password", cache.Password);
				cmd.Parameters.AddWithValue("@FirstName", cache.FirstName);
				cmd.Parameters.AddWithValue("@LastName", cache.LastName);
				cmd.Parameters.AddWithValue("@Position", cache.Position);
				cmd.ExecuteNonQuery();

				trans.Commit();
			}
			catch (Exception e)
			{
				throw e;
				string mensaje = e.Message;
				trans.Rollback();
				filasAfectadas = 0;
			}
			finally
			{
				if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
			}

			return filasAfectadas;
		}

	}
}
