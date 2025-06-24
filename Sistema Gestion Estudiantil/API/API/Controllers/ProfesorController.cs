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
    public class ProfesorController : ControllerBase, IProfesorController
    {
        private ILogger<ProfesorController> _logger;
        private IProfesorFlujo _profFlujo;

        public ProfesorController(ILogger<ProfesorController> logger, IProfesorFlujo profFlujo)
        {
            _logger = logger;
            _profFlujo = profFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProfesor([FromBody]ProfesorRequest profesor)
        {
           var result = await _profFlujo.CrearProfesor(profesor);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Profesor creado correctamente", id = result });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo crear el profesor" });
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarProfesor([FromRoute]Guid Id,[FromBody] ProfesorRequest profesor)
        {
            
            var result = await VerificarProfesorExiste(Id);
            if (result == false)
                return NotFound("El profesor no existe");
            var resultado = await _profFlujo.EditarProfesor(Id, profesor);
            return Ok(resultado);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarProfesor([FromRoute] Guid Id)
        {
            if (!await VerificarProfesorExiste(Id))
            {
                return NotFound("El profesor no existe");
            }
            var result = await _profFlujo.EliminarProfesor(Id);
            return Ok("Profesor eliminado correctamente");
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerProfesor(Guid Id)
        {
            var result = await VerificarProfesorExiste(Id);
            if (!result)
            {
                return NotFound("El profesor no existe");
            }
            var resultadoProfesor = await _profFlujo.ObtenerProfesor(Id);
            if (resultadoProfesor == null)
            {
                return NotFound("Profesor no encontrado");
            }
            return Ok(resultadoProfesor);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProfesores()
        {
            var result = await _profFlujo.ObtenerProfesores();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron profesores");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarProfesorExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoProfesorExiste = await _profFlujo.ObtenerProfesor(Id);
            if (resultadoProfesorExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
