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
    public class AsistenciaProfesorController : ControllerBase, IAsistenciaProfesorController
    {
        private  IAsistenciaProfesorFlujo _asistenciaProfesorFlujo;
        private readonly ILogger<AsistenciaProfesorController> _logger;

        public AsistenciaProfesorController(IAsistenciaProfesorFlujo asistenciaProfesorFlujo, ILogger<AsistenciaProfesorController> logger)
        {
            _asistenciaProfesorFlujo = asistenciaProfesorFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearAsitenciaProfesor([FromBody]AsistenciaProfesorRequest asistenciaProfesor)
        {
            var result = await _asistenciaProfesorFlujo.CrearAsitenciaProfesor(asistenciaProfesor);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la asistencia del profesor");
                return BadRequest("No se pudo crear la asistencia del profesor");
            }
            return Ok(new { mensaje = "Asistencia del profesor creada correctamente", id = result });
        }

        [HttpPut("{IdAsistenciaProfesor}")]
        public async Task<IActionResult> EditarAsitenciaProfesor([FromRoute]Guid IdAsistenciaProfesor,[FromBody] AsistenciaProfesorRequest asistenciaProfesor)
        {
            var result = await VerificarExiste(IdAsistenciaProfesor);
            if (result == false)
            {
                return NotFound("La asistencia del profesor no existe");
            }
            var resultado = await _asistenciaProfesorFlujo.EditarAsitenciaProfesor(IdAsistenciaProfesor, asistenciaProfesor);
            return Ok(resultado);
        }

        [HttpDelete("{IdAsistenciaProfesor}")]
        public async Task<IActionResult> EliminarAsitenciaProfesor([FromRoute]Guid IdAsistenciaProfesor)
        {
            var result = await VerificarExiste(IdAsistenciaProfesor);
            if (result == false)
            {
                return NotFound("La asistencia del profesor no existe");
            }
            var resultado = await _asistenciaProfesorFlujo.EliminarAsitenciaProfesor(IdAsistenciaProfesor);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAsitenciaProfesores()
        {
            var result = await _asistenciaProfesorFlujo.ObtenerAsitenciaProfesores();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron asistencias de profesores");
            }
            return Ok(result);
        }

        [HttpGet("obtenerPorProfsor/{IdProfesor}")]
        public async Task<IActionResult> ObtenerAsitenciaProfesoresPorIdProfesor([FromRoute]Guid IdProfesor)
        {
            var result = await _asistenciaProfesorFlujo.ObtenerAsitenciaProfesoresPorIdProfesor(IdProfesor);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron asistencias de profesores para el ID proporcionado");
            }
            return Ok(result);
        }

        [HttpGet("{IdAsistenciaProfesor}")]
        public async Task<IActionResult> ObtenerAsitenciaProfesorPorId([FromRoute]Guid IdAsistenciaProfesor)
        {
            var result = await VerificarExiste(IdAsistenciaProfesor);
            if (result == false)
            {
                return NotFound("La asistencia del profesor no existe");
            }
            var resultado = await _asistenciaProfesorFlujo.ObtenerAsitenciaProfesorPorId(IdAsistenciaProfesor);
            return Ok(resultado);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid IdAsistenciaProfesor)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _asistenciaProfesorFlujo.ObtenerAsitenciaProfesorPorId(IdAsistenciaProfesor);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
