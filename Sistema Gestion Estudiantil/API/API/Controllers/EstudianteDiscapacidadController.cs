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
    public class EstudianteDiscapacidadController : ControllerBase, IEstudianteDiscapacidadController
    {
        private readonly ILogger<EstudianteDiscapacidadController> _logger;
        private readonly IEstudianteDiscapacidadFlujo _estudianteDiscapacidadFlujo;

        public EstudianteDiscapacidadController(ILogger<EstudianteDiscapacidadController> logger, IEstudianteDiscapacidadFlujo estudianteDiscapacidadFlujo)
        {
            _logger = logger;
            _estudianteDiscapacidadFlujo = estudianteDiscapacidadFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEstudianteDiscapacidad([FromBody] EstudianteDiscapacidadRequest estudianteDiscapacidad)
        {
            var result = await _estudianteDiscapacidadFlujo.CrearEstudianteDiscapacidad(estudianteDiscapacidad);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la discapacidad del estudiante");
                return BadRequest("No se pudo crear la discapacidad del estudiante");
            }
            return Ok(new { mensaje = "Discapacidad del estudiante creada correctamente", id = result });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarEstudianteDiscapacidad([FromRoute]Guid Id, [FromBody]EstudianteDiscapacidadRequest estudianteDiscapacidad)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La discapacidad del estudiante no existe");
            }
            var resultado = await _estudianteDiscapacidadFlujo.EditarEstudianteDiscapacidad(Id, estudianteDiscapacidad);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarEstudianteDiscapacidad(Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La discapacidad del estudiante no existe");
            }
            var resultado = await _estudianteDiscapacidadFlujo.EliminarEstudianteDiscapacidad(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDiscapacidadesConEstudiantes()
        {
            var result = await _estudianteDiscapacidadFlujo.ObtenerDiscapacidadesConEstudiantes();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron discapacidades con estudiantes asociados.");
            }
            return Ok(result);
        }

        [HttpGet("obtenerPorEstudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerDiscapacidadesPorEstudiante(Guid IdEstudiante)
        {
            var result = await _estudianteDiscapacidadFlujo.ObtenerDiscapacidadesPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron discapacidades para el estudiante especificado.");
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerEstudianteDiscapacidadPorId(Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La discapacidad del estudiante no existe");
            }
            var resultado = await _estudianteDiscapacidadFlujo.ObtenerEstudianteDiscapacidadPorId(Id);
            return Ok(resultado);
        }

        [HttpGet("discapacidadesPorEstudiantes")]
        public async Task<IActionResult> ObtenerResumenDiscapacidadesPorEstudiante()
        {
            var result = await _estudianteDiscapacidadFlujo.ObtenerResumenDiscapacidadesPorEstudiante();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron discapacidades por estudiante.");
            }
            return Ok(result);

        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _estudianteDiscapacidadFlujo.ObtenerEstudianteDiscapacidadPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
