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
    public class SeccionesController : ControllerBase , ISeccionController
    {
        private ISeccionFlujo _seccionFlujo;
        private ILogger<SeccionesController> _logg;

        public SeccionesController(ISeccionFlujo seccionFlujo, ILogger<SeccionesController> logg)
        {
            _seccionFlujo = seccionFlujo;
            _logg = logg;
        }

        [HttpPost]
        public async Task<IActionResult> CrearSeccion([FromBody]SeccionesRequest seccion)
        {
            var resultado = await _seccionFlujo.CrearSeccion(seccion);
            if (resultado != Guid.Empty)
            {
                return Ok(new { mensaje = "Sección creada correctamente", id = resultado });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo crear la sección" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSecciones()
        {
            var resultado = await _seccionFlujo.ObtenerSecciones();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerSeccion([FromRoute] Guid Id)
        {
            var result = await _seccionFlujo.ObtenerSeccionPorId(Id);
            if(result == null)
            {
                return NotFound("La seccion no existe");
            }
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task <IActionResult> EditarSeccion([FromRoute] Guid Id, [FromBody] SeccionesRequest seccion)
        {
            var result = await VerificarSeccionExiste(Id);
            if (result == false)
                return NotFound("La seccion no existe");
            var resultado = await _seccionFlujo.EditarSeccion(Id, seccion);
            return Ok("La seccion se edito correctamente");
        }

        [HttpDelete("{Id}")]
        public async Task <IActionResult> EliminarSeccion([FromRoute] Guid Id)
        {
            if(!await VerificarSeccionExiste(Id))
            {
                return NotFound("La seccion no existe");

            }
            var result = await _seccionFlujo.EliminarSeccion(Id);
            return Ok("La seccion se elimino correctamente");
        }

        #region Helpers
        private async Task<bool> VerificarSeccionExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoSeccionExiste = await _seccionFlujo.ObtenerSeccionPorId(Id);
            if (resultadoSeccionExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion

    }
}
