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
    public class CategoriaSituacionFamiliarController : ControllerBase, ICategoriaSituacionFamiliarController
    {
        private ICategoriaSituacionFamiliarFlujo _categoriaSituacionFamiliarFlujo;
        private readonly ILogger<CategoriaSituacionFamiliarController> _logger;
        public CategoriaSituacionFamiliarController(ICategoriaSituacionFamiliarFlujo categoriaSituacionFamiliarFlujo, ILogger<CategoriaSituacionFamiliarController> logger)
        {
            _categoriaSituacionFamiliarFlujo = categoriaSituacionFamiliarFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriaSituacionFamiliar([FromBody] CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar)
        {
            var result = await _categoriaSituacionFamiliarFlujo.CrearCategoriaSituacionFamiliar(categoriaSituacionFamiliar);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la categoria de situacion familiar");
                throw new Exception("No se pudo crear la categoria de situacion familiar");
            }
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarCategoriaSituacionFamiliar([FromRoute] Guid Id, [FromBody] CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaSituacionFamiliarFlujo.EditarCategoriaSituacionFamiliar(Id, categoriaSituacionFamiliar);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarCategoriaSituacionFamiliar([FromRoute] Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaSituacionFamiliarFlujo.EliminarCategoriaSituacionFamiliar(Id);
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerCategoriaSituacionFamiliarPorId([FromRoute] Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaSituacionFamiliarFlujo.ObtenerCategoriaSituacionFamiliarPorId(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriasSituacionFamiliar()
        {
            var result = await _categoriaSituacionFamiliarFlujo.ObtenerCategoriasSituacionFamiliar();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron categorias de situacion familiar");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _categoriaSituacionFamiliarFlujo.ObtenerCategoriaSituacionFamiliarPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion

    }
}
