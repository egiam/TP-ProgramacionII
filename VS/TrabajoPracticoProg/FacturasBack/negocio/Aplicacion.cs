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
        public List<FormaPago> ConsultarFormaPago()
        {
            return dao.GetFormaPago();
        }

        public int ConsultarFacturaNro()
        {
            return dao.GetFacturaNro();
        }

        public int ConsultarArticuloNro()
        {
            return dao.GetArticuloNro();
        }

        public List<Factura> ConsultarFacturas(List<Parametro> criterios)
        {
            return dao.GetByFilters(criterios);//
        }

        public bool CrearFactura(Factura oFactura)
        {
            return dao.SaveFactura(oFactura);
        }
        public bool CrearArticulo(Articulo oArticulo)
        {
            return dao.SaveArticulo(oArticulo);
        }

        public Factura ObtenerFacturaPorID(int id)
        {
            return dao.GetFacturaPorId(id);
        }

        public bool EditarFactura(Factura oFactura)
        {
            return dao.EditarFactura(oFactura);
        }


    }
}
