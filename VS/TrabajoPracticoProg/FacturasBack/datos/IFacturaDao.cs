using FacturasBack.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.datos
{
    interface IFacturaDao
    {
        List<Articulo> GetArticulos();
        bool Save(Factura oFactura);

        int ProximoNroFactura();
    }
}
