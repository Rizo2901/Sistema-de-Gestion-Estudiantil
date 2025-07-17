using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComportamientoController : ControllerBase
    {
        private readonly IComportamientoFlujo _comportamientoFlujo;

        public ComportamientoController(IComportamientoFlujo comportamientoFlujo)
        {
            _comportamientoFlujo = comportamientoFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerComportamientos()
        {
            var result = await _comportamientoFlujo.ObtenerComportamientos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerComportamientoPorId(Guid id)
        {
            var result = await _comportamientoFlujo.ObtenerComportamientoPorId(id);
            if (result == null)
                return NotFound("El comportamiento no existe");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearComportamiento([FromBody] ComportamientoRequest comportamiento)
        {
            var result = await _comportamientoFlujo.CrearComportamiento(comportamiento);
            if (result != Guid.Empty)
                return Ok(new { mensaje = "Comportamiento creado correctamente", id = result });
            return BadRequest("No se pudo crear el comportamiento");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarComportamiento(Guid id, [FromBody] ComportamientoRequest comportamiento)
        {
            var result = await _comportamientoFlujo.EditarComportamiento(id, comportamiento);
            return Ok("Comportamiento editado correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarComportamiento(Guid id)
        {
            var result = await _comportamientoFlujo.EliminarComportamiento(id);
            return Ok("Comportamiento eliminado correctamente");
        }
    }
}
