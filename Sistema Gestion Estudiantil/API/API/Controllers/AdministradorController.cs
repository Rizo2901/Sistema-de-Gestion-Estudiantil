using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase, IAdministradorController
    {
        private IAdministradorFlujo _administradorFlujo;
        private readonly ILogger<AdministradorController> _logger;
        public AdministradorController(IAdministradorFlujo administradorFlujo, ILogger<AdministradorController> logger)
        {
            _administradorFlujo = administradorFlujo;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CrearAdministrador([FromBody]AdministradorRequest administradorRequest)
        {
           var result = await _administradorFlujo.CrearAdministrador(administradorRequest);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear el administrador");
                return BadRequest("No se pudo crear el administrador");
            }
            return Ok(new { mensaje = "Administrador creado correctamente", id = result });
        }

        [HttpPut("{IdAdministrador}")] 
        public async Task<IActionResult> EditarAdministrador([FromRoute]Guid IdAdministrador, [FromBody]AdministradorRequest administradorRequest)
        {
            var result = await VerificarExiste(IdAdministrador);
            if (result == false)
            {
                return NotFound("El Administrador no existe");
            }
            var resultado = await _administradorFlujo.EditarAdministrador(IdAdministrador, administradorRequest);
            return Ok(resultado);
        }

        [HttpDelete("{IdAdministrador}")]
        public async Task<IActionResult> EliminarAdministrador([FromRoute]Guid IdAdministrador)
        {
            var result = await VerificarExiste(IdAdministrador);
            if (result == false)
            {
                return NotFound("El Administrador no existe");
            }
            var resultado = await _administradorFlujo.EliminarAdministrador(IdAdministrador);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAdministradores()
        {
            var result = await _administradorFlujo.ObtenerAdministradores();
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No se encontraron administradores");
                return NotFound("No se encontraron administradores");
            }
            return Ok(result);
        }

        [HttpGet("{IdAdministrador}")]
        public async Task<IActionResult> ObtenerAdministradorPorId([FromRoute]Guid IdAdministrador)
        {
            var result = await VerificarExiste(IdAdministrador);
            if (result == false)
            {
                return NotFound("El Administrador no existe");
            }
            var resultado = await _administradorFlujo.ObtenerAdministradorPorId(IdAdministrador);
            return Ok(resultado);
        }


        #region Helpers
        private async Task<bool> VerificarExiste(Guid IdAdministrador)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _administradorFlujo.ObtenerAdministradorPorId(IdAdministrador);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
