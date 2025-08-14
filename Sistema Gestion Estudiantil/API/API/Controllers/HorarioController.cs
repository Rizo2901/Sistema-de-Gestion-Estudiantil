using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Flujo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase, IHorarioController
    {
        private readonly IHorarioFlujo _horarioFlujo;
        private readonly ILogger<HorarioController> _logger;

        public HorarioController(IHorarioFlujo horarioFlujo, ILogger<HorarioController> logger)
        {
            _horarioFlujo = horarioFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearHorario([FromBody]HorarioRequest horarioRequest)
        {
            var result = await _horarioFlujo.CrearHorario(horarioRequest);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear el horario");
                return BadRequest("No se pudo crear el horario");
            }
            return Ok(new { mensaje = "Horario creado correctamente", id = result });
        }

        [HttpPut("{IdHorario}")]
        public async Task<IActionResult> EditarHorario([FromRoute]Guid IdHorario, [FromBody]HorarioRequest horarioRequest)
        {
            var result = await VerificarExiste(IdHorario);
            if (result == false)
            {
                return NotFound("El Horario no existe");
            }
            var resultado = await _horarioFlujo.EditarHorario(IdHorario, horarioRequest);
            return Ok(resultado);
        }

        [HttpDelete("{IdHorario}")]
        public async Task<IActionResult> EliminarHorario([FromRoute]Guid IdHorario)
        {
            var result = await VerificarExiste(IdHorario);
            if (result == false)
            {
                return NotFound("El Horario no existe");
            }
            var resultado = await _horarioFlujo.EliminarHorario(IdHorario);
            return Ok(resultado);
        }

        [HttpGet("{IdHorario}")]
        public async Task<IActionResult> ObtenerHorarioPorId([FromRoute]Guid IdHorario)
        {
            var result = await VerificarExiste(IdHorario);
            if (result == false)
            {
                return NotFound("El Horario no existe");
            }
            var resultado = await _horarioFlujo.ObtenerHorarioPorId(IdHorario);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHorarios()
        {
            var result = await _horarioFlujo.ObtenerHorarios();
            if (result == null)
            {
                return NotFound("No se encontraron horarios");
            }
            return Ok(result);
        }

        [HttpGet("obtenerPorSeccion")]
        public async Task<IActionResult> ObtenerHorariosPorSeccion()
        {
            var result = await _horarioFlujo.ObtenerHorariosPorSeccion();
            if (result == null || result.Count == 0)
            {
                return NotFound("No se encontraron horarios por sección");
            }
            return Ok(result);
        }



        #region Helpers
        private async Task<bool> VerificarExiste(Guid IdHorario)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _horarioFlujo.ObtenerHorarioPorId(IdHorario);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
