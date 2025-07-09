using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReunionController : ControllerBase, IReunionController
    {
        private ILogger<ReunionController> _logger;
        private IReunionFlujo _reunionFlujo;

        public ReunionController(ILogger<ReunionController> logger, IReunionFlujo reunionFlujo)
        {
            _logger = logger;
            _reunionFlujo = reunionFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReunion(ReunionesRequest reunion)
        {
            var result = await _reunionFlujo.CrearReunion(reunion);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Reunión creada correctamente", id = result });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo crear la reunión" });
            }

        }

        [HttpPut("{IdReunion}")]
        public async Task<IActionResult> EditarReunion([FromRoute]Guid IdReunion, [FromBody] ReunionesRequest reunion)
        {
            if (!VerificarReunionExiste(IdReunion).Result)
            {
                return NotFound("La reunion no existe");
            }
            var result = await _reunionFlujo.EditarReunion(IdReunion, reunion);
            return Ok(new { mensaje = "Reunion editada correctamente", id = result });
        }

        [HttpDelete("{IdReunion}")]
        public async Task<IActionResult> EliminarReunion([FromRoute]Guid IdReunion)
        {
            if (!VerificarReunionExiste(IdReunion).Result)
            {
                return NotFound("La reunion no existe");
            }
            var result = await _reunionFlujo.EliminarReunion(IdReunion);
            return Ok(new { mensaje = "Reunion eliminada correctamente", id = result });
        }


        [HttpGet("{IdReunion}")]
        public async Task<IActionResult> ObtenerReunion([FromRoute] Guid IdReunion)
        {
            if (!VerificarReunionExiste(IdReunion).Result)
            {
                return NotFound("La reunion no existe");
            }
            var result = await _reunionFlujo.ObtenerReunion(IdReunion);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerReuniones()
        {
            var result = await _reunionFlujo.ObtenerReuniones();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron reuniones");
            }
            return Ok(result);
        }


        [HttpGet("obtenerReunionesPorEstudiante/{Id}")]
        public async Task<IActionResult> ObtenerReunionesPorEstudiante([FromRoute]Guid IdEstudiante)
        {
            var result = await _reunionFlujo.ObtenerReunionesPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron reuniones para el estudiante");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarReunionExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoReunionExiste = await _reunionFlujo.ObtenerReunion(Id);
            if (resultadoReunionExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
