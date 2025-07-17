using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Azure.Core;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SituacionFamiliarController : ControllerBase, ISituacionFamiliarController
    {
        private ISituacionFamiliarFlujo _situacionFamiliarFlujo;
        private ILogger<SituacionFamiliarController> _logger;
        public SituacionFamiliarController(ISituacionFamiliarFlujo situacionFamiliarFlujo, ILogger<SituacionFamiliarController> logger)
        {
            _situacionFamiliarFlujo = situacionFamiliarFlujo;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CrearSituacionFamiliar([FromBody] SituacionFamiliarRequest request)
        {
            var result = await _situacionFamiliarFlujo.CrearSituacionFamiliar(request);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la situación familiar.");
                return BadRequest("No se pudo crear la situación familiar.");
            }
            return Ok("Situacion familiar creada correctamente" + result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarSituacionFamiliar([FromRoute] Guid Id, [FromBody] SituacionFamiliarRequest request)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("Situación familiar con ID  no encontrada.");
            }
            var result = await _situacionFamiliarFlujo.EditarSituacionFamiliar(Id, request);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarSituacionFamiliar([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("Situación familiar con ID  no encontrada.");
            }
            var result = await _situacionFamiliarFlujo.EliminarSituacionFamiliar(Id);
            return Ok(result);
        }

        [HttpGet("ObtenerEstudiantesConSituacionesFamiliar")]
        public async Task<IActionResult> ObtenerEstudiantesConSituacionesFamiliar()
        {
            var result = await _situacionFamiliarFlujo.ObtenerEstudiantesConSituacionesFamiliar();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron estudiantes con situaciones familiares.");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSituacionesFamiliares()
        {
            var result = await _situacionFamiliarFlujo.ObtenerSituacionesFamiliares();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron registros de situaciones familiares");
            }
            return Ok(result);
        }

        [HttpGet("ObtenerSituacionesFamiliaresPorEstudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerSituacionesFamiliaresPorEstudiante([FromRoute] Guid IdEstudiante)
        {
            var result = await _situacionFamiliarFlujo.ObtenerSituacionesFamiliaresPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron situaciones familiares para el estudiante con ID " + IdEstudiante);
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerSituacionFamiliarPorId([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("Situación familiar con no encontrada.");
            }
            var result = await _situacionFamiliarFlujo.ObtenerSituacionFamiliarPorId(Id);
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _situacionFamiliarFlujo.ObtenerSituacionFamiliarPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
