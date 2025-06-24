using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase, IRolController
    {
        private ILogger<RolController> _logger;
        private IRolFlujo _rolFlujo;
        public RolController(ILogger<RolController> logger, IRolFlujo rolFlujo)
        {
            _logger = logger;
            _rolFlujo = rolFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRole([FromBody]RolesRequest rol)
        {
           var result = await _rolFlujo.CrearRole(rol);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Rol creado correctamente", id = result });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo crear el rol" });
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarRole([FromRoute]Guid Id, [FromBody]RolesRequest rol)
        {
           var result = await VerificarRolExiste(Id);
            if (result==false)
            {
                return NotFound("El rol no existe");
            }
            var resultado = await _rolFlujo.EditarRole(Id, rol);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarRole([FromRoute]Guid Id)
        {
            if(!VerificarRolExiste(Id).Result)
            {
              return NotFound("El rol no existe");
            }
            var result = await _rolFlujo.EliminarRole(Id);
            return Ok(new { mensaje = "Rol eliminado correctamente", id = result });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerRole([FromRoute]Guid Id)
        {
            if (!VerificarRolExiste(Id).Result)
            {
                return NotFound("El rol no existe");
            }
            var result = await _rolFlujo.ObtenerRolePorId(Id);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRoles()
        {
            var result = await _rolFlujo.ObtenerRoles();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron roles");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarRolExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoRolExiste = await _rolFlujo.ObtenerRolePorId(Id);
            if (resultadoRolExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
