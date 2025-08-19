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
    public class CondicionEspecialController : ControllerBase , ICondicionEspecialController
    {
        private readonly ILogger<CondicionEspecialController> _logger;
        private readonly ICondicionEspecialFlujo _condicionEspecialFlujo;
        public CondicionEspecialController(ICondicionEspecialFlujo condicionEspecialFlujo, ILogger<CondicionEspecialController> logger)
        {
            _condicionEspecialFlujo = condicionEspecialFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial)
        {
           var result = await _condicionEspecialFlujo.CrearCondicionEspecial(condicionEspecial);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Condición especial creada correctamente", id = result });
            }
            else
            {
                _logger.LogError("Error al crear la condición especial");
                return BadRequest("Error al crear la condición especial");
            }

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarCondicionEspecial([FromRoute]Guid Id, [FromBody]CondicionesEspecialesRequest condicionEspecial)
        {
            var result = await VerificarCondicionExiste(Id);
            if (result == false)
                return NotFound("La condicion no existe");
            var resultado = await _condicionEspecialFlujo.EditarCondicionEspecial(Id, condicionEspecial);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarCondicionEspecial([FromRoute]Guid Id)
        {

            if (!await VerificarCondicionExiste(Id))
            {
                return NotFound("La condicion no existe");
            }
            var result = await _condicionEspecialFlujo.EliminarCondicionEspecial(Id);
            return Ok("Condicion eliminada correctamente");

        }
        [HttpGet("obtenerCondicionesEstudiantes")]
        public async Task<IActionResult> ObtenerCondicionesAgrupadas()
        {
            var result =  await _condicionEspecialFlujo.ObtenerCondicionesAgrupadas();
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No se encontraron condiciones especiales");
                return NotFound("No se encontraron condiciones especiales");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCondicionesEspeciales()
        {
           var result = await _condicionEspecialFlujo.ObtenerCondicionesEspeciales();
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No se encontraron condiciones especiales");
                return NotFound("No se encontraron condiciones especiales");
            }
            return Ok(result);
        }

        

        

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerCondicionEspecial([FromRoute] Guid Id)
        {
            var resultado = await _condicionEspecialFlujo.ObtenerCondicionEspecial(Id);

            if (resultado == null)
            {
                _logger.LogWarning($"No se encontró la condición especial con ID {Id}");
                return NotFound($"No se encontró la condición especial con ID {Id}");
            }

            return Ok(resultado);
        }

       



        #region Helpers
        private async Task<bool> VerificarCondicionExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _condicionEspecialFlujo.ObtenerCondicionEspecial(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
