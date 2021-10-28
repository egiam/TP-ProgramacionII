using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.dominio
{
    public class FormaPago
    {
        public int IdFormaPago { get; set; }
        public string Nombre { get; set; }

        public FormaPago(int idFormaPago, string nombre)
        {
            IdFormaPago = idFormaPago;
            Nombre = nombre;
        }
        public FormaPago()
        {

        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
