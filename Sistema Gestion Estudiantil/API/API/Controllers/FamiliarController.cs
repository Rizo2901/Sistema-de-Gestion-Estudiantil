using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Flujo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliarController : ControllerBase, IFamiliarController
    {
        private readonly IFamiliarFlujo _familiarFlujo;

        public FamiliarController(IFamiliarFlujo familiarFlujo)
        {
            _familiarFlujo = familiarFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFamiliares()
        {
            var result = await _familiarFlujo.ObtenerFamiliares();
            return Ok(result);
        }

        [HttpGet("{IdFamiliar}")]
        public async Task<IActionResult> ObtenerFamiliarPorId(Guid IdFamiliar)
        {
            var result = await VerificarExiste(IdFamiliar);
            if (result == false)
            {
                return NotFound("El Familiar no existe");
            }
            var resultado = await _familiarFlujo.ObtenerFamiliarPorId(IdFamiliar);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearFamiliar([FromBody] FamiliarRequest familiar)
        {
            var result = await _familiarFlujo.CrearFamiliar(familiar);
            if (result != Guid.Empty)
                return Ok(new { mensaje = "Familiar creado correctamente", id = result });
            return BadRequest("No se pudo crear el familiar");
        }

        [HttpPut("{IdFamiliar}")]
        public async Task<IActionResult> EditarFamiliar([FromRoute]Guid IdFamiliar, [FromBody] FamiliarRequest familiar)
        {
            var result = await VerificarExiste(IdFamiliar);
            if (result == false)
            {
                return NotFound("El Familiar no existe");
            }
            var resultado = await _familiarFlujo.EditarFamiliar(IdFamiliar, familiar);
            return Ok(resultado);
          
        }

        [HttpDelete("{IdFamiliar}")]
        public async Task<IActionResult> EliminarFamiliar(Guid IdFamiliar)
        {
            var result = await VerificarExiste(IdFamiliar);
            if (result == false)
            {
                return NotFound("El Familiar no existe");
            }
            var resultado = await _familiarFlujo.EliminarFamiliar(IdFamiliar);
            return Ok(resultado);
        }

        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _familiarFlujo.ObtenerFamiliarPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
    }
}
