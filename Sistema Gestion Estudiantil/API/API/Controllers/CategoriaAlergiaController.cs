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
    public class CategoriaAlergiaController : ControllerBase, ICategoriaAlergiaController
    {
        private readonly ICategoriaAlergiaFlujo _categoriaAlergiaFlujo;
        private readonly ILogger<CategoriaAlergiaController> _logger;
        public CategoriaAlergiaController(ICategoriaAlergiaFlujo categoriaAlergiaFlujo, ILogger<CategoriaAlergiaController> logger)
        {
            _categoriaAlergiaFlujo = categoriaAlergiaFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriaAlergia([FromBody]CategoriaAlergiaRequest categoriaAlergia)
        {
            var result = await _categoriaAlergiaFlujo.CrearCategoriaAlergia(categoriaAlergia);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear la categoria");
                return BadRequest("No se pudo crear la categoria");
            }
            return Ok(result);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarCategoriaAlergia([FromRoute]Guid Id, [FromBody]CategoriaAlergiaRequest categoriaAlergia)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaAlergiaFlujo.EditarCategoriaAlergia(Id, categoriaAlergia);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]    
        public async Task<IActionResult> EliminarCategoriaAlergia([FromRoute]Guid Id)
        {
            var result = await VerificarExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaAlergiaFlujo.EliminarCategoriaAlergia(Id);
            return Ok(resultado);
        }

        [HttpGet("{IdCategoria}")]
        public async Task<IActionResult> ObtenerCategoriaAlergiaPorId([FromRoute]Guid IdCategoria)
        {
            var result = await VerificarExiste(IdCategoria);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaAlergiaFlujo.ObtenerCategoriaAlergiaPorId(IdCategoria);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriasAlergia()
        {
           var result = await _categoriaAlergiaFlujo.ObtenerCategoriasAlergia();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron categorias de alergia");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid IdCategoria)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _categoriaAlergiaFlujo.ObtenerCategoriaAlergiaPorId(IdCategoria);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
