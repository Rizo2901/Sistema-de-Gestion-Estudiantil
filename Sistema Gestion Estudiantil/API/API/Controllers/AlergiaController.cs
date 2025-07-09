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
    public class AlergiaController : ControllerBase, IAlergiaController
    {
        private readonly ILogger<AlergiaController> _logger;
        private readonly IAlergiaFlujo _alergiaFlujo;
        public AlergiaController(ILogger<AlergiaController> logger, IAlergiaFlujo alergiaFlujo)
        {
            _logger = logger;
            _alergiaFlujo = alergiaFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearAlergia([FromBody]AlergiaRequest alergia)
        {
            var result = await _alergiaFlujo.CrearAlergia(alergia);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la alergia");
                return BadRequest("No se pudo crear la alergia");
            }
            return Ok(result);
        }

        [HttpPut("{IdAlergia}")]
        public async Task<IActionResult> EditarAlergia([FromRoute]Guid IdAlergia, [FromBody]AlergiaRequest alergia)
        {
            var result = await VerificarExiste(IdAlergia);
            if (result == false)
            {
                return NotFound("La alergia no existe");
            }
            var resultado = await _alergiaFlujo.EditarAlergia(IdAlergia, alergia);
            return Ok(resultado);
        }

        [HttpDelete("{IdAlergia}")]
        public async Task<IActionResult> EliminarAlergia([FromRoute]Guid IdAlergia)
        {
            var result = await VerificarExiste(IdAlergia);
            if (result == false)
            {
                return NotFound("La alergia no existe");
            }
            var resultado = await _alergiaFlujo.EliminarAlergia(IdAlergia);
            return Ok(resultado);
        }

        [HttpGet("{IdAlergia}")]
        public async Task<IActionResult> ObtenerAlergiaPorId([FromRoute]Guid IdAlergia)
        {
            var result = await VerificarExiste(IdAlergia);
            if (result == false)
            {
                return NotFound("La alergia no existe");
            }
            var resultado = await _alergiaFlujo.ObtenerAlergiaPorId(IdAlergia);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAlergias()
        {
            var result = await _alergiaFlujo.ObtenerAlergias();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron alergias");
            }
            return Ok(result);
        }


        #region Helpers
        private async Task<bool> VerificarExiste(Guid IdAlergia)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _alergiaFlujo.ObtenerAlergiaPorId(IdAlergia);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
