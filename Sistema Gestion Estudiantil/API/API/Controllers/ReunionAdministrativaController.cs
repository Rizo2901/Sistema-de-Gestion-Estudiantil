using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReunionAdministrativaController : ControllerBase, IReunionAdministrativaController
    {
        private ILogger<ReunionAdministrativaController> _logger;
        private IReunionAdministrativaFlujo _reunionAdministrativaFlujo;
        public ReunionAdministrativaController(ILogger<ReunionAdministrativaController> logger, IReunionAdministrativaFlujo reunionAdministrativaFlujo)
        {
            _logger = logger;
            _reunionAdministrativaFlujo = reunionAdministrativaFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReunionAdministrativa([FromBody]ReunionAdministrativaRequest reunion)
        {
            var result = await _reunionAdministrativaFlujo.CrearReunionAdministrativa(reunion);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Reunión administrativa creada correctamente", id = result });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo crear la reunión administrativa" });
            }
        }

        [HttpPut("{IdReunionAdmin}")]
        public async Task<IActionResult> EditarReunionAdministrativa([FromRoute]Guid IdReunionAdmin, [FromBody]ReunionAdministrativaRequest reunion)
        {
            var result = await VerificarReunionAdministrativasExiste(IdReunionAdmin);
            if (result == false)
            {
                return NotFound("La reunion administrativa no existe");
            }
            var resultado = await _reunionAdministrativaFlujo.EditarReunionAdministrativa(IdReunionAdmin, reunion);
            return Ok(resultado);

            
        }

        [HttpDelete("{IdReunionAdmin}")]
        public async Task<IActionResult> EliminarReunionAdministrativa([FromRoute]Guid IdReunionAdmin)
        {
            var result = await VerificarReunionAdministrativasExiste(IdReunionAdmin);
            if (result == false)
            {
                return NotFound("La reunion administrativa no existe");
            }
            var resultado = await _reunionAdministrativaFlujo.EliminarReunionAdministrativa(IdReunionAdmin);
            return Ok(resultado);

        }

        [HttpGet("{IdReunionAdmin}")]
        public async Task<IActionResult> ObtenerReunionAdministrativa([FromRoute]Guid IdReunionAdmin)
        {
            var result = await VerificarReunionAdministrativasExiste(IdReunionAdmin);
            if (result == false)
            {
                return NotFound("La reunion administrativa no existe");
            }
            var resultado = await _reunionAdministrativaFlujo.ObtenerReunionAdministrativa(IdReunionAdmin);
            return Ok(resultado);


           
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerReunionesAdministrativas()
        {
            var result = await _reunionAdministrativaFlujo.ObtenerReunionesAdministrativas();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron reuniones administrativas");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarReunionAdministrativasExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoReunionAdminExiste = await _reunionAdministrativaFlujo.ObtenerReunionAdministrativa(Id);
            if (resultadoReunionAdminExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
