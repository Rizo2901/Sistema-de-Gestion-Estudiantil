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
    public class MateriaController : ControllerBase , IMateriaController
    {
        private IMateriaFlujo _materiaFlujo;
        private ILogger<MateriaController> _logg;

        public MateriaController(IMateriaFlujo materiaFlujo, ILogger<MateriaController> logg)
        {
            _materiaFlujo = materiaFlujo;
            _logg = logg;
        }


        [HttpPost]
        public async Task<IActionResult> CrearMateria(MateriasRequest materia)
        {
            var result = await _materiaFlujo.CrearMateria(materia);
            if (result != Guid.Empty)
            {
                return Ok(new { Id = result });
            }
            else
            {
                _logg.LogError("Error al crear la materia");
                return BadRequest("Error al crear la materia");
            }
            return Ok(new { mensaje = "Materia creada correctamente", id = result });

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarMateria([FromRoute]Guid Id,[FromBody] MateriasRequest materia)
        {
            var result = await VerificarMateriaExiste(Id);
            if (result == false)
                return NotFound("La materia no existe");
            var resultado = await _materiaFlujo.EditarMateria(Id, materia);
            return Ok(resultado);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarMateria([FromRoute]Guid Id)
        {
            if(!await VerificarMateriaExiste(Id))
            {
               return NotFound("La materia no existe");
            }
            var resultadoEliminacion = await _materiaFlujo.EliminarMateria(Id);
            return Ok(new { mensaje = "Materia eliminada correctamente", id = resultadoEliminacion });
         
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerMateria([FromRoute] Guid Id)
        {
            var result = await _materiaFlujo.ObtenerMateria(Id);
            if (result == null)
            {
                _logg.LogError("La materia no existe");
                return NotFound("La materia no existe");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMaterias()
        {
            var result = await _materiaFlujo.ObtenerMaterias();
            if (result == null)
            {
                return NotFound("No se encontraron materias");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarMateriaExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _materiaFlujo.ObtenerMateria(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
