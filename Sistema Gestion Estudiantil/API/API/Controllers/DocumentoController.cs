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
    public class DocumentoController : ControllerBase , IDocumentoController
    {
        private readonly IDocumentoFlujo _docFlj;
        private readonly ILogger<DocumentoController> _logger;

        public DocumentoController(IDocumentoFlujo docFlj, ILogger<DocumentoController> logger)
        {
            _docFlj = docFlj;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDocumento([FromBody] DocumentosRequest documento)
        {
            var result = await _docFlj.CrearDocumento(documento);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Documento creado correctamente", id = result });
            }
            else
            {
                _logger.LogError("Error al crear el documento");
                return BadRequest("Error al crear el documento");
            }

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarDocumento([FromRoute]Guid Id, [FromBody]DocumentosRequest documento)
        {
            var result = await VerificarDocumentoExiste(Id);
            if (result == false)
                return NotFound("El documento no existe");
            var resultado = await _docFlj.EditarDocumento(Id, documento);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarDocumento([FromRoute] Guid Id)
        {

            if (!await VerificarDocumentoExiste(Id))
            {
                return NotFound("El documento no existe");
            }
            var result = await _docFlj.EliminarDocumento(Id);
            return Ok("Documento eliminado correctamente");
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> ObtenerDocumentoPorId([FromRoute]Guid Id)
        {
            var resultado = await _docFlj.ObtenerDocumentoPorId(Id);
            if (resultado == null)
            {
                _logger.LogError($"Documento con Id {Id} no encontrado");
                return NotFound($"Documento con Id {Id} no encontrado");
            }
            return Ok(resultado);
        }

        [HttpGet("Estudiante/{IdEstudiante}")]
        public async Task<IActionResult> ObtenerDocumentosPorEstudiante([FromRoute]Guid IdEstudiante)
        {
            var resultado = await _docFlj.ObtenerDocumentosPorEstudiante(IdEstudiante);
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning($"No se encontraron documentos para el estudiante con Id {IdEstudiante}");
                return NotFound($"No se encontraron documentos para el estudiante con Id {IdEstudiante}");
            }
            return Ok(resultado);
        }

        [HttpGet("estudianteConDocumentos")]
        public async Task<IActionResult> ObtenerEstudiantesConDocumentos()
        {
            var result = await _docFlj.ObtenerEstudiantesConDocumentos();
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No se encontraron documentos para el estudiante");
                return NotFound("No se encontraron documentos para el estudiante");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosDocumentos()
        {
            var resultado = await _docFlj.ObtenerTodosLosDocumentos();
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning("No se encontraron documentos");
                return NotFound("No se encontraron documentos");
            }
            return Ok(resultado);

        }



        #region Helpers
        private async Task<bool> VerificarDocumentoExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoEstudianteExiste = await _docFlj.ObtenerDocumentoPorId(Id);
            if (resultadoEstudianteExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
