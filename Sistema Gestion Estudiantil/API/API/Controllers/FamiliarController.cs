using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliarController : ControllerBase
    {
        private readonly IFamiliarFlujo _familiarFlujo;

        public FamiliarController(IFamiliarFlujo familiarFlujo)
        {
            _familiarFlujo = familiarFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFamiliares()
        {
            var result = await _familiarFlujo.ObtenerFamiliares();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerFamiliarPorId(Guid id)
        {
            var result = await _familiarFlujo.ObtenerFamiliarPorId(id);
            if (result == null)
                return NotFound("El familiar no existe");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearFamiliar([FromBody] FamiliarRequest familiar)
        {
            var result = await _familiarFlujo.CrearFamiliar(familiar);
            if (result != Guid.Empty)
                return Ok(new { mensaje = "Familiar creado correctamente", id = result });
            return BadRequest("No se pudo crear el familiar");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFamiliar(Guid id, [FromBody] FamiliarRequest familiar)
        {
            var result = await _familiarFlujo.EditarFamiliar(id, familiar);
            return Ok("Familiar editado correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFamiliar(Guid id)
        {
            var result = await _familiarFlujo.EliminarFamiliar(id);
            return Ok("Familiar eliminado correctamente");
        }
    }
}
