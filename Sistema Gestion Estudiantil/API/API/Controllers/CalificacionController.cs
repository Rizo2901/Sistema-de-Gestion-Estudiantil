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
    public class CalificacionController : ControllerBase, ICalificacionController
    {
        private ICalificacionFlujo _calificacionFlujo;
        private ILogger<CalificacionController> _logger;
        public CalificacionController(ICalificacionFlujo calificacionFlujo, ILogger<CalificacionController> logger)
        {
            _calificacionFlujo = calificacionFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCalificacion([FromBody]CalificacionRequest calificacion)
        {
            var result = await _calificacionFlujo.CrearCalificacion(calificacion);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la calificación");
                return BadRequest("Error al crear la calificación");
            }   
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarCalificacion([FromRoute]Guid IdCalificacion, [FromBody]CalificacionRequest calificacion)
        {
            if (!VerificarCalificacionExiste(IdCalificacion).Result)
            {
                return NotFound("La calificacion no existe");
            }
            var result = await _calificacionFlujo.EditarCalificacion(IdCalificacion, calificacion);
            return Ok(new { mensaje = "Calificación eliminada correctamente", id = result });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarCalificacion([FromRoute]Guid IdCalificacion)
        {
            if (!VerificarCalificacionExiste(IdCalificacion).Result)
            {
                return NotFound("La calificación no existe");
            }
            var result = await _calificacionFlujo.EliminarCalificacion(IdCalificacion);
            return Ok(new { mensaje = "Calificación eliminada correctamente", id = result });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerCalificacion([FromRoute]Guid IdCalificacion)
        {
            if (!VerificarCalificacionExiste(IdCalificacion).Result)
            {
                return NotFound("La calificación no existe");
            }
            var result = await _calificacionFlujo.ObtenerCalificacion(IdCalificacion);
            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> ObtenerCalificaciones()
        {
           var result = await _calificacionFlujo.ObtenerCalificaciones();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron calificaciones");
            }
            return Ok(result);
        }


        [HttpGet("calificacionesPorEstudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerCalificacionesPorEstudiante([FromRoute]Guid IdEstudiante)
        {
            var result = await _calificacionFlujo.ObtenerCalificacionesPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron calificaciones para el estudiante especificado");
            }
            return Ok(result);
        }


        #region Helpers
        private async Task<bool> VerificarCalificacionExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoCalificacionExiste = await _calificacionFlujo.ObtenerCalificacion(Id);
            if (resultadoCalificacionExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
