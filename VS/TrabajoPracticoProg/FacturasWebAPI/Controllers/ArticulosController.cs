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
    public class ArticulosController : ControllerBase
    {
        private IAplicacion app;

        public ArticulosController()
        {
            app = new Aplicacion();
        }


        [HttpGet("proximo_nro_articulo")]
        public IActionResult GetArticuloNro()
        {
            return Ok(app.ConsultarArticuloNro());
        }

        [HttpGet("articulos")]
        public IActionResult GetArticulos()
        {
            return Ok(app.ConsultarArticulos());
        }

        [HttpPost("consultar")]
        public IActionResult GetArticulos(List<Parametro> lst)
        {
            if (lst == null || lst.Count == 0)
                return BadRequest("Se requiere una lista de parámetros!");

            return Ok(app.ConsultarArticulos(lst));
        }


        [HttpPost("articulos")]
        public IActionResult PostArticulo(Articulo oArticulo)
        {
            if (oArticulo == null)
            {
                return BadRequest("Articulo null");
            }

            if (app.CrearArticulo(oArticulo))
                return Ok("¡Se grabó exitosamente el artículo!");
            else
                return BadRequest("¡No se pudo grabar el artículo!");
        }


        [HttpPut("articulo")]
        public IActionResult PutArticulo(Articulo oArticulo)
        {
            if (oArticulo == null)
            {
                return BadRequest("Artículo null!");
            }

            if (app.EditarArticulo(oArticulo))
                return Ok("¡Se grabó exitosamente el artículo!");
            else
                return BadRequest("¡No se pudo grabar el artículo!");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteArticulo(int id)
        {
            if (id == 0)
                return BadRequest("Id es requerido!");
            return Ok(app.RegistrarBajaArticulo(id));
        }


    }
}
