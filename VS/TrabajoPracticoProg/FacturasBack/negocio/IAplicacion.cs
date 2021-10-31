﻿using FacturasBack.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasBack.negocio
{
    public interface IAplicacion
    {
        public List<Articulo> ConsultarArticulos();

        public List<FormaPago> ConsultarFormaPago();

        public List<Factura> ConsultarFacturas(List<Parametro> criterios);

        public int ConsultarFacturaNro();

        public int ConsultarArticuloNro();

        public bool CrearFactura(Factura oFactura);

        public bool CrearArticulo(Articulo oArticulo);

        public Factura ObtenerFacturaPorID(int id);

        public bool EditarFactura(Factura oFactura);

    }
}
