using FacturasBack.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.negocio
{
    public interface IAplicacion
    {
        public List<Articulo> ConsultarArticulos();

        public bool CrearFactura(Factura oFactura);

    }
}
