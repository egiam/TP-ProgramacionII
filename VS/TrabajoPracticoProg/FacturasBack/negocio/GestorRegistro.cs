using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturasBack.datos;
using FacturasBack.dominio;

namespace FacturasBack.negocio
{
    public class GestorRegistro
    {
        private IFacturaDao dao;
        public GestorRegistro(AbstractDaoFactory factory)
        {
            dao = factory.CrearRecetaDao();
        }

        public bool NuevoRegistro(Usuario oUsuario)
        {
            return dao.InsertarRegistro(oUsuario);
        }
    }
}
