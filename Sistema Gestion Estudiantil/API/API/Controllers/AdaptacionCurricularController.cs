using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdaptacionCurricularController : ControllerBase, IAdaptacionCurricularController
    {
        private readonly ILogger<AdaptacionCurricularController> _logger;
        private readonly IAdaptacionCurricularFlujo _adapCurricular;

        public AdaptacionCurricularController(ILogger<AdaptacionCurricularController> logger, IAdaptacionCurricularFlujo adapCurricular)
        {
            _logger = logger;
            _adapCurricular = adapCurricular;
        }

        // Crear
        [HttpPost]
        public async Task<IActionResult> CrearAdaptacionCurricular([FromBody] AdaptacionCurricularRequest adaptacionCurricular)
        {
            var result = await _adapCurricular.CrearAdaptacionCurricular(adaptacionCurricular);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Adaptación curricular creada correctamente", id = result });
            }
            return BadRequest(new { mensaje = "No se pudo crear la adaptación curricular" });
        }

        // Editar
        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarAdaptacionCurricular([FromRoute] Guid Id, [FromBody] AdaptacionCurricularRequest adaptacionCurricular)
        {
            if (!await VerificarAdaptacionExiste(Id))
            {
                return NotFound("La adaptación curricular no existe");
            }

            var result = await _adapCurricular.EditarAdaptacionCurricular(Id, adaptacionCurricular);
            return Ok(new { mensaje = "Adaptación curricular editada correctamente", id = result });
        }

        // Eliminar
        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarAdaptacionCurricular([FromRoute] Guid Id)
        {
            if (!await VerificarAdaptacionExiste(Id))
            {
                return NotFound("La adaptación curricular no existe");
            }

            var result = await _adapCurricular.EliminarAdaptacionCurricular(Id);
            return Ok(new { mensaje = "Adaptación curricular eliminada correctamente", id = result });
        }

        // Obtener por ID
        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerAdaptacionCurricularPorId([FromRoute] Guid Id)
        {
            var result = await _adapCurricular.ObtenerAdaptacionCurricularPorId(Id);
            if (result == null)
            {
                return NotFound("No se encontró la adaptación curricular");
            }
            return Ok(result);
        }

        // Obtener todas
        [HttpGet]
        public async Task<IActionResult> ObtenerAdaptacionesCurriculares()
        {
            var result = await _adapCurricular.ObtenerAdaptacionesCurriculares();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron adaptaciones curriculares");
            }
            return Ok(result);
        }

        [HttpGet("PorEstudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerAdaptacionesPorEstudiante([FromRoute] Guid IdEstudiante)
        {
            var result = await _adapCurricular.ObtenerAdaptacionesPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron adaptaciones curriculares para el estudiante");
            }
            return Ok(result);
        }

        [HttpGet("PorEstudiantes")]
        public async Task<IActionResult> ObtenerAdaptacionesPorEstudiantes()
        {
            var result = await _adapCurricular.ObtenerAdaptacionesPorEstudiantes();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron adaptaciones curriculares por estudiantes");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarAdaptacionExiste(Guid Id)
        {
            var adaptacion = await _adapCurricular.ObtenerAdaptacionCurricularPorId(Id);
            return adaptacion != null;
        }
        #endregion
    }
}
