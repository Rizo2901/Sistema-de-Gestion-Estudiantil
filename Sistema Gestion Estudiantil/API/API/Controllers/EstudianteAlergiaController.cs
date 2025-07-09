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
    public class EstudianteAlergiaController : ControllerBase, IEstudianteAlergiaController
    {
        private readonly ILogger<EstudianteAlergiaController> _logger;
        private readonly IEstudianteAlergiaFlujo _estudianteAlergiaFlujo;
        public EstudianteAlergiaController(ILogger<EstudianteAlergiaController> logger, IEstudianteAlergiaFlujo estudianteAlergiaFlujo)
        {
            _logger = logger;
            _estudianteAlergiaFlujo = estudianteAlergiaFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEstudianteAlergia([FromBody]EstudianteAlergiaRequest estudianteAlergia)
        {
            var result = await _estudianteAlergiaFlujo.CrearEstudianteAlergia(estudianteAlergia);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la relacion estudiante-alergia");
                return BadRequest("No se pudo crear la relacion estudiante-alergia");
            }
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarEstudianteAlergia([FromRoute]Guid Id, EstudianteAlergiaRequest estudianteAlergia)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("El registro no existe");
            }
            var resultado = await _estudianteAlergiaFlujo.EditarEstudianteAlergia(Id, estudianteAlergia);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarEstudianteAlergia([FromRoute]Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("El registro no existe");
            }
            var resultado = await _estudianteAlergiaFlujo.EliminarEstudianteAlergia(Id);
            return Ok(resultado);
        }

        [HttpGet("obtenerPorEstudiantes")]   
        public async Task<IActionResult> ObtenerAlergiasAgrupadasPorEstudiantes()
        {
            var result = await _estudianteAlergiaFlujo.ObtenerAlergiasAgrupadasPorEstudiantes();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron alergias para el estudiante especificado.");
            }
            return Ok(result);
        }

        [HttpGet("obtenerPorEstudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerAlergiasPorEstudiante([FromRoute]Guid IdEstudiante)
        {
            var result = await _estudianteAlergiaFlujo.ObtenerAlergiasPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron alergias para el estudiante especificado.");
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerEstudianteAlergiaPorId([FromRoute]Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("El registro no existe");
            }
            var resultado = await _estudianteAlergiaFlujo.ObtenerEstudianteAlergiaPorId(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasAlergiasEstudiantes()
        {
            var result = await _estudianteAlergiaFlujo.ObtenerTodasLasAlergiasEstudiantes();
            if(result == null || !result.Any())
            {
                return NotFound("No se encontraron registros de alergias para estudiantes.");
            }
            return Ok(result);
        }


        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _estudianteAlergiaFlujo.ObtenerEstudianteAlergiaPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
