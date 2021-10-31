using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.dominio
{
   public class DetalleFactura
    {
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public int idDetalle { get; set; }

        public DetalleFactura(Articulo articulo, int cantidad)
        {
            Articulo = articulo;
            Cantidad = cantidad;
        }
        public DetalleFactura()
        {

        }


        public double CalcularSubtotal()
        {
            return Articulo.PrecioUnitario * Cantidad;
        }
    }
}
