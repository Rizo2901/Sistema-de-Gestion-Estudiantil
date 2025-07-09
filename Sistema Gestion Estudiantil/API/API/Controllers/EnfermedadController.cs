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
    public class EnfermedadController : ControllerBase, IEnfermedadController
    {
        private ILogger<EnfermedadController> _logger;
        private IEnfermedadFlujo _enfermedadFlujo;
        public EnfermedadController(ILogger<EnfermedadController> logger, IEnfermedadFlujo enfermedadFlujo)
        {
            _logger = logger;
            _enfermedadFlujo = enfermedadFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEnfermedad([FromBody] EnfermedadRequest enfermedad)
        {
            var result = await _enfermedadFlujo.CrearEnfermedad(enfermedad);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la enfermedad");
                return BadRequest("No se pudo crear la enfermedad");
            }
            return Ok(result);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarEnfermedad([FromRoute] Guid Id, [FromBody] EnfermedadRequest enfermedad)
        {
            var result = await VerificarEnfermedadExiste(Id);
            if (result == false)
            {
                return NotFound("La enfermedad no existe");
            }
            var resultado = await _enfermedadFlujo.EditarEnfermedad(Id, enfermedad);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarEnfermedad([FromRoute]Guid Id)
        {
            var result = await VerificarEnfermedadExiste(Id);
            if (result == false)
            {
                return NotFound("La enfermedad no existe");
            }
            var resultado = await _enfermedadFlujo.EliminarEnfermedad(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEnfermedades()
        {
            var result = await _enfermedadFlujo.ObtenerEnfermedades();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerEnfermedadPorId([FromRoute] Guid Id)
        {
            var result = await VerificarEnfermedadExiste(Id);
            if (result == false)
            {
                return NotFound("La enfermedad no existe");
            }
            var resultado = await _enfermedadFlujo.ObtenerEnfermedadPorId(Id);
            return Ok(resultado);
        }


        #region Helpers
        private async Task<bool> VerificarEnfermedadExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _enfermedadFlujo.ObtenerEnfermedadPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
