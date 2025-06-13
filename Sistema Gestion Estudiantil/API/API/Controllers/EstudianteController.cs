using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase, IEstudianteController
    {
        private IEstudianteFlujo _estudianteFlujo;
        private ILogger<EstudianteController> _logg;

        public EstudianteController(IEstudianteFlujo estudianteFlujo, ILogger<EstudianteController> logg)
        {
            _estudianteFlujo = estudianteFlujo;
            _logg = logg;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerEstudiante([FromRoute] Guid Id)
        {
            var result = await _estudianteFlujo.ObtenerEstudiantePorId(Id);
            if (result == null)
            {
                return NotFound("El estudiante no existe");
            }
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerEstudiantes()
        {
            var result = await _estudianteFlujo.ObtenerEstudiantes();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CrearEstudiante([FromBody] EstudiantesRequest estudiante)
        {
            var result = await _estudianteFlujo.CrearEstudiante(estudiante);

            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Estudiante creado correctamente", id = result });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo crear el estudiante" });
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarEstudiante([FromRoute] Guid Id)
        {
            if (!await VerificarEstudianteExiste(Id))
            {
                return NotFound("El estudiante no existe");
            }
            var result = await _estudianteFlujo.EliminarEstudiante(Id);
            return Ok("Estudiante eliminado correctamente");
        }



        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarEstudiante([FromRoute] Guid Id, [FromBody] EstudiantesRequest estudiante)
        {
            var result = await VerificarEstudianteExiste(Id);
            if (result == false)
                return NotFound("El estudiante no existe");
            var resultado = await _estudianteFlujo.EditarEstudiante(Id, estudiante);
            return Ok(resultado);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarEstudiantes(
    [FromQuery] string? nombre = null,
    [FromQuery] string? cedula = null,
    [FromQuery] string? grado = null,
    [FromQuery] string? seccionNombre = null)
        {
            var resultados = await _estudianteFlujo.BuscarEstudiantes(nombre, cedula, grado, seccionNombre);
            return Ok(resultados);
        }


        #region Helpers
        private async Task<bool> VerificarEstudianteExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoEstudianteExiste = await _estudianteFlujo.ObtenerEstudiantePorId(Id);
            if (resultadoEstudianteExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }

        
        #endregion
    }
}