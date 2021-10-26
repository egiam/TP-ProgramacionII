using FacturasBack.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.datos
{
    class FacturaDao:IFacturaDao
    {
        public List<Articulo> GetIngredientes()
        {
            List<Articulo> lst = new List<Articulo>();

            DataTable t = HelperDao.GetInstance().ConsultaSQL("SP_CONSULTAR_ARTICULOS");
            foreach (DataRow row in t.Rows)
            {
                //Articulo oArticulo = new Articulo(); 
                //oArticulo.IdArticulo = Convert.ToInt32(row[0].ToString());
                //oArticulo.Nombre = row[1].ToString();
                //oArticulo.PrecioUnitario = Convert.ToDouble(row[2].ToString());

                //lst.Add(oArticulo);
            }

            return lst;
        }

        public int ProximoNroFactura()
        {
            return HelperDao.GetInstance().EjecutarSQLConValorOUT("SP_PROXIMO_ID", "@next");
        }

        public int ProximoNroReceta()
        {
            throw new NotImplementedException();
        }

        public bool Save(Factura oFactura)
        {
            return HelperDao.GetInstance().EjecutarInsert(oFactura, "SP_INSERTAR_FACTURA", "SP_INSERTAR_DETALLES");
        }
    }
}
