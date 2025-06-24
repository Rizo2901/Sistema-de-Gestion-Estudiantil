using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioFlujo _horarioFlujo;

        public HorarioController(IHorarioFlujo horarioFlujo)
        {
            _horarioFlujo = horarioFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHorarios()
        {
            var result = await _horarioFlujo.ObtenerHorarios();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerHorarioPorId(Guid id)
        {
            var result = await _horarioFlujo.ObtenerHorarioPorId(id);
            if (result == null)
                return NotFound("El horario no existe");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearHorario([FromBody] HorarioRequest horario)
        {
            var result = await _horarioFlujo.CrearHorario(horario);
            if (result != Guid.Empty)
                return Ok(new { mensaje = "Horario creado correctamente", id = result });
            return BadRequest("No se pudo crear el horario");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarHorario(Guid id, [FromBody] HorarioRequest horario)
        {
            var result = await _horarioFlujo.EditarHorario(id, horario);
            return Ok("Horario editado correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarHorario(Guid id)
        {
            var result = await _horarioFlujo.EliminarHorario(id);
            return Ok("Horario eliminado correctamente");
        }
    }
}
