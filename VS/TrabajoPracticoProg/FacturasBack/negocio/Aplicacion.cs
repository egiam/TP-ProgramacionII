using FacturasBack.datos;
using FacturasBack.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.negocio
{
     public class Aplicacion : IAplicacion
    {
        private IFacturaDao dao;

        public Aplicacion()
        {
            dao = new FacturaDao(); //cambiar esto por un factory
        }


        public List<Articulo> ConsultarArticulos()
        {
            return dao.GetArticulos();
        }

        public bool CrearFactura(Factura oFactura)
        {
            return dao.Save(oFactura);
        }


    }
}
