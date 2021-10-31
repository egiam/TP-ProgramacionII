using FacturasBack.dominio;
using FacturasBack.negocio;
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

        List<FormaPago> GetFormaPago();
        bool SaveFactura(Factura oFactura);
        bool SaveArticulo(Articulo oArticulo);

        int GetFacturaNro();

        int GetArticuloNro();

        List<Factura> GetByFilters(List<Parametro> criterios);
    }
}
