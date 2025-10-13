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
    public class AtencionMedicaController : ControllerBase, IAtencionMedicaController
    {
        private ILogger<AtencionMedicaController> _logger;
        private IAtencionMedicaFlujo _atencionMedicaFlujo;

        public AtencionMedicaController(ILogger<AtencionMedicaController> logger, IAtencionMedicaFlujo atencionMedicaFlujo)
        {
            _logger = logger;
            _atencionMedicaFlujo = atencionMedicaFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearAtencionMedica([FromBody]AtencionMedicaRequest atencion)
        {
            var result = await _atencionMedicaFlujo.CrearAtencionMedica(atencion);
            if(result== Guid.Empty)
                return NotFound("Error al crear la atención médica");
            return Ok(result);
        }


        [HttpPut("{IdAtencionMedica}")]
        public async Task<IActionResult> EditarAtencionMedica([FromRoute]Guid IdAtencionMedica, [FromBody]AtencionMedicaRequest atencion)
        {
            if (!await VerificarAtencionMedicaExiste(IdAtencionMedica))
            {
                return BadRequest("La atención médica no existe");
            }
            var result = await _atencionMedicaFlujo.EditarAtencionMedica(IdAtencionMedica, atencion);
            if (result == Guid.Empty)
            {
                return NotFound("Error al editar la atención médica");
            }
            return Ok(result);
        }

        [HttpDelete("{IdAtencionMedica}")]
        public async Task<IActionResult> EliminarAtencionMedica([FromRoute]Guid IdAtencionMedica)
        {
            
            if (!await VerificarAtencionMedicaExiste(IdAtencionMedica))
            {
                return BadRequest("La atención médica no existe");
            }
            var result = await _atencionMedicaFlujo.EliminarAtencionMedica(IdAtencionMedica);
            
            return Ok(new { mensaje = "Atención médica eliminada correctamente", id = result });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAtencionesMedicas()
        {
            var result = await _atencionMedicaFlujo.ObtenerAtencionesMedicas();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron atenciones médicas");
            }
            return Ok(result);
        }

        [HttpGet("obtenerAtencionesAgrupadas")]
        public async Task<IActionResult> ObtenerAtencionesMedicasAgrupadas()
        {
            var result = await _atencionMedicaFlujo.ObtenerAtencionesMedicasAgrupadas();
            if(result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("obtenerPorEstudiante{IdEstudiante}")]
        public async Task<IActionResult> ObtenerAtencionesMedicasPorEstudiante([FromRoute]Guid IdEstudiante)
        {
            var result = await _atencionMedicaFlujo.ObtenerAtencionesMedicasPorEstudiante(IdEstudiante);
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron atenciones médicas para el estudiante");
            }
            return Ok(result);
        }

        [HttpGet("{IdAtencionMedica}")]
        public async Task<IActionResult> ObtenerAtencionMedicaPorId([FromRoute]Guid IdAtencionMedica)
        {

            if (!await VerificarAtencionMedicaExiste(IdAtencionMedica))
            {
                return BadRequest("La atención médica no existe");
            }
            var result = await _atencionMedicaFlujo.ObtenerAtencionMedicaPorId(IdAtencionMedica);

            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarAtencionMedicaExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoAtencionMedicaExiste = await _atencionMedicaFlujo.ObtenerAtencionMedicaPorId(Id);
            if (resultadoAtencionMedicaExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
