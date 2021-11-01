using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLogin.Cache;

namespace AccesoDatosLogin
{
    public class RegistroDao
    {


		public bool InsertarRegistro(UserLoginCache cache)
		{
			bool resultado = true;
			int filasAfectadas = 0;

			filasAfectadas = HelperDao.ObtenerInstancia().EjecutarSQLMaestroDetalle("pa_Registrar_Users", cache);

			if (filasAfectadas == 0) resultado = false;

			return resultado;
		}
	}
}
