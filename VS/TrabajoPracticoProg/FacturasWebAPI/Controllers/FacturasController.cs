﻿using FacturasBack.dominio;
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

        [HttpGet("formas_de_pago")]
        public IActionResult GetFormasPago()
        {
            return Ok(app.ConsultarFormaPago());
        }


        [HttpGet("proximo_nro_factura")]
        public IActionResult GetFacturaNro()
        {
            return Ok(app.ConsultarFacturaNro());
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

        [HttpPost("consultar")]
        public IActionResult GetFacturas(List<Parametro> lst)
        {
            if (lst == null || lst.Count == 0)
                return BadRequest("Se requiere una lista de parámetros!");

            return Ok(app.ConsultarFacturas(lst));
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



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return BadRequest("Id es requerido!");
            return Ok(app.ObtenerFacturaPorID(id));
        }

        //[HttpPut("facturas")]
        //public IActionResult Put(int id)
        //{
        //    if (id == 0)
        //        return BadRequest("Id es requerido!");
        //    return Ok(app.EditarFacturaPorID(id));
        //}
        [HttpPut("facturas")]
        public IActionResult PutFactura(Factura oFactura)
        {
            if (oFactura == null)
            {
                return BadRequest("Factura null");
            }

            if (app.EditarFactura(oFactura))
                return Ok("¡Se grabó exitosamente la factura!");
            else
                return BadRequest("¡No se pudo grabar la factura!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("Id es requerido!");
            return Ok(app.RegistrarBajaFactura(id));
        }

        [HttpPost("consultar_articulos")]
        public IActionResult GetArticulos(List<Parametro> lst)
        {
            if (lst == null || lst.Count == 0)
                return BadRequest("Se requiere una lista de parámetros!");

            return Ok(app.ConsultarArticulos(lst));
        }

        [HttpDelete("/api/articulos/{id}")]
        public IActionResult DeleteArticulo(int id)
        {
            if (id == 0)
                return BadRequest("Id es requerido!");
            return Ok(app.RegistrarBajaArticulo(id));
        }



    }
}
