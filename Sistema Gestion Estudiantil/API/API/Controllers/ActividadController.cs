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
    public class ActividadController : ControllerBase, IActividadController
    {
        private ILogger<ActividadController> _logger;
        private IActividadFlujo _actividadFlujo;

        public ActividadController(ILogger<ActividadController> logger, IActividadFlujo actividadFlujo)
        {
            _logger = logger;
            _actividadFlujo = actividadFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearActividad([FromBody]ActividadRequest actividad)
        {
            var result = await _actividadFlujo.CrearActividad(actividad);
            if (result != Guid.Empty)
            {
                return Ok(new { Id = result, mensaje = "Actividad creada correctamente" });
            }
            else
            {
                _logger.LogError("Error al crear la actividad");
                return BadRequest("Error al crear la actividad");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarActividad([FromRoute]Guid Id,[FromBody] ActividadRequest actividad)
        {
            var result = await VerificarActividadExiste(Id);
            if (result == false)
                return NotFound("La actividad no existe");
            var resultado = await _actividadFlujo.EditarActividad(Id, actividad);
            return Ok(resultado);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarActividad([FromRoute]Guid Id)
        {
            if (!await VerificarActividadExiste(Id))
            {
                return NotFound("La actividad no existe");
            }
            var result = await _actividadFlujo.EliminarActividad(Id);
            return Ok("Actividad eliminada correctamente");


        }

        [HttpGet]
        public async Task<IActionResult> ObtenerActividades()
        {
            var result = await _actividadFlujo.ObtenerActividades();
            if (result != null && result.Any())
            {
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("No se encontraron actividades");
                return NotFound("No se encontraron actividades");
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerActividadPorId([FromRoute]Guid Id)
        {
           var resultadoExiste = await VerificarActividadExiste(Id);
            if (!resultadoExiste)
            {
                return NotFound("La actividad no existe");
            }
            var resultadoActividad = await _actividadFlujo.ObtenerActividadPorId(Id);
            if (resultadoActividad != null)
            {
                return Ok(resultadoActividad);
            }
            else
            {
                _logger.LogError("Error al obtener la actividad por Id");
                return BadRequest("Error al obtener la actividad por Id");
            }
        }




        #region Helpers
        private async Task<bool> VerificarActividadExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoActividadExiste = await _actividadFlujo.ObtenerActividadPorId(Id);
            if (resultadoActividadExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
