using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;

using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoServiceController : ControllerBase, IArchivoServiceController
    {
        private readonly ILogger<ArchivoServiceController> _logger;
        private readonly IArchivoServiceFlujo _archivoServiceFlujo;

        public ArchivoServiceController(ILogger<ArchivoServiceController> logger, IArchivoServiceFlujo archivoServiceFlujo)
        {
            _logger = logger;
            _archivoServiceFlujo = archivoServiceFlujo;
        }

        [HttpPost("imagen")]
        public async Task<IActionResult> SubirImagenFirebaseAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("El archivo no es válido.");

            using var stream = file.OpenReadStream();
            var result = await _archivoServiceFlujo.SubirImagenFirebaseAsync(stream, file.FileName);

            if (string.IsNullOrEmpty(result))
                return StatusCode(500, "Error al subir la imagen.");

            return Ok(new { Url = result });
        }

        [HttpPost("documento")]
        public async Task<IActionResult> SubirDocumentoDriveAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("El archivo no es válido.");

            using var stream = file.OpenReadStream();
            var result = await _archivoServiceFlujo.SubirDocumentoDriveAsync(stream, file.FileName);

            if (string.IsNullOrEmpty(result))
                return StatusCode(500, "Error al subir el documento.");

            return Ok(new { Url = result });
        }

       
    }
}