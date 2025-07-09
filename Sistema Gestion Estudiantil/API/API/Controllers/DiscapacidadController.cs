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
    public class DiscapacidadController : ControllerBase, IDiscapacidadController
    {
        private ILogger<DiscapacidadController> _logger;
        private IDiscapacidadFlujo _discapacidadFlujo;

        public DiscapacidadController(ILogger<DiscapacidadController> logger, IDiscapacidadFlujo discapacidadFlujo)
        {
            _logger = logger;
            _discapacidadFlujo = discapacidadFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDiscapacidad([FromBody]DiscapacidadRequest discapacidad)
        {
            var result = await _discapacidadFlujo.CrearDiscapacidad(discapacidad);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la discapacidad");
                return BadRequest("No se pudo crear la discapacidad");
            }
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarDiscapacidad([FromRoute]Guid Id, [FromBody]DiscapacidadRequest discapacidad)
        {
            if (!await VerificarExiste(Id))
            {
                _logger.LogWarning($"La discapacidad con ID {Id} no existe.");
                return NotFound(new { mensaje = "Discapacidad no encontrada" });
            }
            var result = await _discapacidadFlujo.EditarDiscapacidad(Id, discapacidad);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarDiscapacidad([FromRoute]Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                _logger.LogWarning($"La discapacidad con ID {Id} no existe.");
                return NotFound(new { mensaje = "Discapacidad no encontrada" });
            }
            var result = await _discapacidadFlujo.EliminarDiscapacidad(Id);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDiscapacidades()
        {
            var result = await _discapacidadFlujo.ObtenerDiscapacidades();
            if (result == null || !result.Any())
            {
                _logger.LogInformation("No se encontraron discapacidades.");
                return NotFound("No se encontraron discapacidades.");
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerDiscapacidadPorId([FromRoute]Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                _logger.LogWarning($"La discapacidad con ID {Id} no existe.");
                return NotFound(new { mensaje = "Discapacidad no encontrada" });
            }
            var result = await _discapacidadFlujo.ObtenerDiscapacidadPorId(Id);
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _discapacidadFlujo.ObtenerDiscapacidadPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
