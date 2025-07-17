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
    public class EstudianteMedicamentoController : ControllerBase, IEstudianteMedicamentoController
    {
        private ILogger<EstudianteMedicamentoController> _logger;
        private IEstudianteMedicamentoFlujo _estudianteMedicamentoFlujo;
        public EstudianteMedicamentoController(ILogger<EstudianteMedicamentoController> logger, IEstudianteMedicamentoFlujo estudianteMedicamentoFlujo)
        {
            _logger = logger;
            _estudianteMedicamentoFlujo = estudianteMedicamentoFlujo;
        }
        [HttpPost]
        public async Task<IActionResult> CrearEstudianteMedicamento([FromBody] EstudianteMedicamentoResponse estudianteMedicamento)
        {
            var result = await _estudianteMedicamentoFlujo.CrearEstudianteMedicamento(estudianteMedicamento);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear el estudiante medicamento");
                return BadRequest("No se pudo crear el estudiante medicamento");
            }
            return Ok(result);
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarEstudianteMedicamento([FromRoute] Guid Id, [FromBody] EstudianteMedicamentoResponse estudianteMedicamento)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("El estudiante medicamento no existe");
            }
            var resultado = await _estudianteMedicamentoFlujo.EditarEstudianteMedicamento(Id, estudianteMedicamento);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarEstudianteMedicamento([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("El estudiante medicamento no existe");
            }
            var resultado = await _estudianteMedicamentoFlujo.EliminarEstudianteMedicamento(Id);
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerEstudianteMedicamentoPorId([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("El estudiante medicamento no existe");
            }
            var resultado = await _estudianteMedicamentoFlujo.ObtenerEstudianteMedicamentoPorId(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstudianteMedicamentos()
        {
            var result = await _estudianteMedicamentoFlujo.ObtenerEstudianteMedicamentos();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron registros de estudiante medicamentos");
            }
            return Ok(result);
        }

        [HttpGet("ObtenerPorEstudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerEstudianteMedicamentosPorEstudiante([FromRoute] Guid IdEstudiante)
        {
            var result = await _estudianteMedicamentoFlujo.ObtenerEstudianteMedicamentosPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron registros de estudiante medicamentos para el estudiante especificado");
            }
            return Ok(result);
        }

        [HttpGet("ObtenerPorEstudiantes")]
        public async Task<IActionResult> ObtenerEstudianteMedicamentosPorEstudiantes()
        {
            var result = await _estudianteMedicamentoFlujo.ObtenerEstudianteMedicamentosPorEstudiantes();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron registros de estudiante medicamentos para los estudiantes");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _estudianteMedicamentoFlujo.ObtenerEstudianteMedicamentoPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
