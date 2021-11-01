using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.datos
{
    public abstract class AbstractDaoFactory
    {
        public abstract IFacturaDao CrearRecetaDao();
    }
}
