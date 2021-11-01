using FacturasBack.dominio;
using FacturasBack.negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.datos
{
    public interface IFacturaDao
    {
        List<Articulo> GetArticulos();

        List<FormaPago> GetFormaPago();
        bool SaveFactura(Factura oFactura);
        bool SaveArticulo(Articulo oArticulo);

        int GetFacturaNro();

        int GetArticuloNro();

        List<Factura> GetByFilters(List<Parametro> criterios);


        Factura GetFacturaPorId(int id);
        bool EditarFactura(Factura oFactura);

        public bool Delete(int id);
        public bool DeleteArticulo(int id);

        List<Articulo> GetArticulosByFilters(List<Parametro> criterios);

        bool EditarArticulo(Articulo oArticulo);

        bool InsertarRegistro(Usuario oUsuario);
    }
}
