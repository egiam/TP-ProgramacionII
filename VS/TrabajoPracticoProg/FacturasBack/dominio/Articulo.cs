using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.dominio
{
   public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public string DadoBaja { get; set; }

        public Articulo(int idArticulo, string nombre, double precioUnitario)
        {
            IdArticulo = idArticulo;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
        }
        public Articulo()
        {
                
        }


        public override string ToString()
        {
            return Nombre;
        }
    }
}
