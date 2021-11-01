using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.datos
{
    public class DaoFactory : AbstractDaoFactory 
    {
        public override IFacturaDao CrearRecetaDao()
        {
            return new FacturaDao();
        }
    }
}
