using FacturasBack.dominio;
using FacturasBack.negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.datos
{
    public class FacturaDao:IFacturaDao
    {
        public List<Articulo> GetArticulos()
        {
            List<Articulo> lst = new List<Articulo>();

            DataTable t = HelperDao.GetInstance().ConsultaSQL("SP_CONSULTAR_ARTICULOS");
            foreach (DataRow row in t.Rows)
            {
                Articulo oArticulo = new Articulo();
                oArticulo.IdArticulo = Convert.ToInt32(row[0].ToString());
                oArticulo.Nombre = row[1].ToString();
                oArticulo.PrecioUnitario = Convert.ToDouble(row[2].ToString());

                lst.Add(oArticulo);
            }

            return lst;
        }

        public List<FormaPago> GetFormaPago()
        {
            List<FormaPago> lst = new List<FormaPago>();

            DataTable t = HelperDao.GetInstance().ConsultaSQL("SP_CONSULTAR_FORMAS_DE_PAGO");
            foreach (DataRow row in t.Rows)
            {
                FormaPago oFormaPago = new FormaPago();
                oFormaPago.IdFormaPago = Convert.ToInt32(row[0].ToString());
                oFormaPago.Nombre = row[1].ToString();

                lst.Add(oFormaPago);
            }

            return lst;
        }

        
        public int GetFacturaNro()
        {
            return HelperDao.GetInstance().EjecutarSQLConValorOUT("SP_PROXIMO_ID", "@next");
        }

        
        public int GetArticuloNro()
        {
            return HelperDao.GetInstance().EjecutarSQLConValorOUT("SP_PROXIMO_ID_ART", "@next");
        }

        public bool SaveFactura(Factura oFactura)
        {
            return HelperDao.GetInstance().EjecutarInsertFactura(oFactura, "SP_INSERTAR_FACTURA", "SP_ELIMINAR_DETALLES", "SP_INSERTAR_DETALLES");
        }

        public bool SaveArticulo(Articulo oArticulo)
        {
            return HelperDao.GetInstance().EjecutarInsertArticulo(oArticulo, "SP_INSERTAR_ARTICULO");
        }

        public List<Factura> GetByFilters(List<Parametro> criterios)
        {
            return HelperDao.GetInstance().ConsultarFacturas("SP_CONSULTAR_FACTURAS", criterios);
        }


        public Factura GetFacturaPorId(int id)
        {
            return HelperDao.GetInstance().GetFacturaPorId("SP_CONSULTAR_FACTURA_POR_ID", id);
        }

        public bool EditarFactura(Factura oFactura)
        {
            return HelperDao.GetInstance().EjecutarInsertFactura(oFactura, "SP_EDITAR_FACTURA", "SP_ELIMINAR_DETALLES", "SP_INSERTAR_DETALLES");
        }

        public bool Delete(int id)
        {
            return HelperDao.GetInstance().Delete("SP_REGISTRAR_BAJA_FACTURAS", id);
        }
        public bool DeleteArticulo(int id)
        {
            return HelperDao.GetInstance().Delete("SP_REGISTRAR_BAJA_ARTICULO", id);
        }

        public List<Articulo> GetArticulosByFilters(List<Parametro> criterios)
        {
            return HelperDao.GetInstance().ConsultarArticulos("SP_CONSULTAR_ARTICULOS_FILTROS", criterios);
        }

        public bool EditarArticulo(Articulo oArticulo)
        {
            return HelperDao.GetInstance().EjecutarInsertArticulo(oArticulo, "SP_EDITAR_ARTICULO");
        }


        public bool InsertarRegistro(Usuario oUsuario)
        {
            bool resultado = true;
            int filasAfectadas = 0;

            filasAfectadas = HelperDao.GetInstance().EjecutarInsertRegistro("pa_Registrar_Users", oUsuario);

            if (filasAfectadas == 0) resultado = false;

            return resultado;
        }

    }
}
