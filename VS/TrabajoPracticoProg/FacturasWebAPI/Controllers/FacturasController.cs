using FacturasBack.dominio;
using FacturasBack.negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturasWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private IAplicacion app;

        public FacturasController()
        {
            app = new Aplicacion();
        }

        [HttpGet("articulos")]
        public IActionResult GetArticulos()
        {
            return Ok(app.ConsultarArticulos());
        }



        [HttpPost("facturas")]
        public IActionResult PostFactura(Factura oFactura)
        {
            if (oFactura == null)
            {
                return BadRequest("Factura null");
            }

            if (app.CrearFactura(oFactura))
                return Ok("¡Se grabó exitosamente la factura!");
            else
                return BadRequest("¡No se pudo grabar la factura!");
        }
    }
}
