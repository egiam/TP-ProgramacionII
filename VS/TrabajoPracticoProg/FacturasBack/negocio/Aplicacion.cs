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
            dao = new FacturaDao(); 
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
            return dao.GetByFilters(criterios);
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
        public bool RegistrarBajaFactura(int id)
        {
            return dao.Delete(id);
        }

        public bool RegistrarBajaArticulo(int id)
        {
            return dao.DeleteArticulo(id);
        }

        public List<Articulo> ConsultarArticulos(List<Parametro> criterios)
        {
            return dao.GetArticulosByFilters(criterios);//
        }

        public bool EditarArticulo(Articulo oArticulo)
        {
            return dao.EditarArticulo(oArticulo);
        }

        //public bool NuevoRegistro(Usuario oUsuario)
        //{
        //    return dao.InsertarRegistro(oUsuario);
        //}
    }
}
