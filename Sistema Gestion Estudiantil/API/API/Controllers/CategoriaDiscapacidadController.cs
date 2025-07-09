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
    public class CategoriaDiscapacidadController : ControllerBase, ICategoriaDiscapacidadController
    {
        private  ILogger<CategoriaDiscapacidadController> _logger;
        private  ICategoriaDiscapacidadFlujo _categoriaDiscapacidadFlujo;
        public CategoriaDiscapacidadController(ILogger<CategoriaDiscapacidadController> logger, ICategoriaDiscapacidadFlujo categoriaDiscapacidadFlujo)
        {
            _logger = logger;
            _categoriaDiscapacidadFlujo = categoriaDiscapacidadFlujo;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriaDiscapacidad([FromBody]CategoriasDiscapacidadRequest categoriaDiscapacidad)
        {
            var result= await _categoriaDiscapacidadFlujo.CrearCategoriaDiscapacidad(categoriaDiscapacidad);
            if (result != Guid.Empty)
            {
                return Ok(new { mensaje = "Categoría de discapacidad creada correctamente", id = result });
            }
            else
            {
                _logger.LogError("Error al crear la categoría de discapacidad");
                return BadRequest(new { mensaje = "No se pudo crear la categoría de discapacidad" });
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarCategoriaDiscapacidad([FromRoute]Guid Id, [FromBody] CategoriasDiscapacidadRequest categoriaDiscapacidad)
        {
            if(!await VerificarExiste(Id))
            {
                _logger.LogWarning($"La categoría de discapacidad con ID {Id} no existe.");
                return NotFound(new { mensaje = "Categoría de discapacidad no encontrada" });
            }
            var result = await _categoriaDiscapacidadFlujo.EditarCategoriaDiscapacidad(Id, categoriaDiscapacidad);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarCategoriaDiscapacidad([FromRoute]Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria de discapacidad no existe");
            }
            var resultado = await _categoriaDiscapacidadFlujo.EliminarCategoriaDiscapacidad(Id);
            return Ok(resultado);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerCategoriaDiscapacidadPorId([FromRoute]Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria de discapacidad no existe");
            }
            var resultado = await _categoriaDiscapacidadFlujo.ObtenerCategoriaDiscapacidadPorId(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriasDiscapacidad()
        {
            var result = await _categoriaDiscapacidadFlujo.ObtenerCategoriasDiscapacidad();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron categorías de discapacidad");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _categoriaDiscapacidadFlujo.ObtenerCategoriaDiscapacidadPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
