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
    public class SeccionProfesorController : ControllerBase, ISeccionProfesorController
    {
        private  ISeccionProfesorFlujo _seccionProfesorFlujo;
        private ILogger<SeccionProfesorController> _logger;
        public SeccionProfesorController(ISeccionProfesorFlujo seccionProfesorFlujo, ILogger<SeccionProfesorController> logger)
        {
            _seccionProfesorFlujo = seccionProfesorFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearSeccionProfesor([FromBody]SeccionProfesorRequest request)
        {
            var result = await _seccionProfesorFlujo.CrearSeccionProfesor(request);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la sección del profesor.");
                return BadRequest("No se pudo crear la sección del profesor.");
            }
            return Ok("Se creo correctamente "+ result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarSeccionProfesor([FromRoute]Guid Id, [FromBody]SeccionProfesorRequest request)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("El registro no existe");
            }
            var resultado = await _seccionProfesorFlujo.EditarSeccionProfesor(Id, request);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarSeccionProfesor([FromRoute]Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("El registro no existe");
            }
            var resultado = await _seccionProfesorFlujo.EliminarSeccionProfesor(Id);
            return Ok(resultado);
        }

        [HttpGet("profesoresPorSeccion/{IdSeccion}")]
        public async Task<IActionResult> ObtenerProfesoresPorSeccion(Guid IdSeccion)
        {
            var result = await _seccionProfesorFlujo.ObtenerProfesoresPorSeccion(IdSeccion);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron profesores para la sección especificada.");
            }
            return Ok(result);
        }

        [HttpGet("seccionesPorProfesor/{IdProfesor}")]
        public async Task<IActionResult> ObtenerSeccionesPorProfesor(Guid IdProfesor)
        {
            var result = await _seccionProfesorFlujo.ObtenerSeccionesPorProfesor(IdProfesor);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron secciones para el profesor especificado.");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSeccionesProfesores()
        {
            var result = await _seccionProfesorFlujo.ObtenerSeccionesProfesores();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron secciones de profesores.");
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerSeccionProfesorPorId(Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("El registro no existe");
            }
            var resultado = await _seccionProfesorFlujo.ObtenerSeccionProfesorPorId(Id);
            return Ok(resultado);
        }


        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _seccionProfesorFlujo.ObtenerSeccionProfesorPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
