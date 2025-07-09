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
    public class CategoriaCondicionEspecialController : ControllerBase, ICategoriaCondicionEspecialController
    {
        private readonly ICategoriaCondicionEspecialFlujo _categoriaCondicionEspecialFlujo;
        private ILogger<CategoriaCondicionEspecialController> _logger;

        public CategoriaCondicionEspecialController(ICategoriaCondicionEspecialFlujo categoriaCondicionEspecialFlujo, ILogger<CategoriaCondicionEspecialController> logger)
        {
            _categoriaCondicionEspecialFlujo = categoriaCondicionEspecialFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriaCondicionEspecial([FromBody]CategoriasCondicionEspecialRequest categoriaCondicionEspecial)
        {
            var result = await _categoriaCondicionEspecialFlujo.CrearCategoriaCondicionEspecial(categoriaCondicionEspecial);
            if (result == Guid.Empty)
            {
                return BadRequest("No se pudo crear la categoria de condicion especial");
            }
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarCategoriaCondicionEspecial([FromRoute]Guid Id, [FromBody]CategoriasCondicionEspecialRequest categoriaCondicionEspecial)
        {
            var result = await VerificarCategoriaExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaCondicionEspecialFlujo.EditarCategoriaCondicionEspecial(Id, categoriaCondicionEspecial);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarCategoriaCondicionEspecial([FromRoute]Guid Id)
        {
            var result = await VerificarCategoriaExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaCondicionEspecialFlujo.EliminarCategoriaCondicionEspecial(Id);
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerCategoriaCondicionEspecialPorId([FromRoute]Guid Id)
        {
            var result = await VerificarCategoriaExiste(Id);
            if (result == false)
            {
                return NotFound("La categoria no existe");
            }
            var resultado = await _categoriaCondicionEspecialFlujo.ObtenerCategoriaCondicionEspecialPorId(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriasCondicionEspeciales()
        {
            var result = await _categoriaCondicionEspecialFlujo.ObtenerCategoriasCondicionEspeciales();
            if(result == null || !result.Any())
            {
                return NotFound("No se encontraron categorias de condicion especial");
            }
            return Ok(result);
        }


        #region Helpers
        private async Task<bool> VerificarCategoriaExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _categoriaCondicionEspecialFlujo.ObtenerCategoriaCondicionEspecialPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }

         #endregion
    }
}
