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
    public class MateriaProfesorController : ControllerBase , IMateriaProfesorController
    {
        private ILogger<MateriaProfesorController> _logger;
        private IMateriaProfesorFlujo _mateProfFlujo;

        public MateriaProfesorController(ILogger<MateriaProfesorController> logger, IMateriaProfesorFlujo mateProfFlujo)
        {
            _logger = logger;
            _mateProfFlujo = mateProfFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearMateriaProfesor(MateriaProfesorRequest materiaProfesor)
        { 
            var result = await _mateProfFlujo.CrearMateriaProfesor(materiaProfesor);
            if (result == Guid.Empty)
                return BadRequest("No se pudo crear la materia profesor");
            return Ok(result);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarMateriaProfesor([FromRoute]Guid Id, [FromBody]MateriaProfesorRequest materiaProfesor)
        {
            var result = await VerificarMateriaProfesorExiste(Id);
            if (result == false)
                return NotFound("La materia profesor no existe");
            var resultado = await _mateProfFlujo.EditarMateriaProfesor(Id, materiaProfesor);
            return Ok(resultado );
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarMateriaProfesor([FromRoute]Guid Id)
        {
            if (!await VerificarMateriaProfesorExiste(Id))
            {
                return NotFound("La materia no existe");
            }
            var resultadoEliminacion = await _mateProfFlujo.EliminarMateriaProfesor(Id);
            return Ok(new { mensaje = "Materia Profesor eliminado correctamente", id = resultadoEliminacion });
        }

        [HttpGet ("materiasPorIdProfesor")]
        public async Task<IActionResult> MateriasPorIdProfesor(Guid IdProfesor)
        {
            var result = await _mateProfFlujo.MateriasPorIdProfesor(IdProfesor);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMateriasProfesores()
        {
            var result = await _mateProfFlujo.ObtenerMateriasProfesores();
            return Ok(result);
        }

        [HttpGet ("profesorPorMateria")]
        public async Task<IActionResult> ProfesoresPorMateria(Guid IdMateria)
        {
            var result = await _mateProfFlujo.ProfesoresPorMateria(IdMateria);
            return Ok(result);
        }


        #region Helpers
        private async Task<bool> VerificarMateriaProfesorExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _mateProfFlujo.ObtenerMateriaProfesorPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion


    }
}
