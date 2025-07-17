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
    public class MedicamentoController : ControllerBase, IMedicamentoController
    {
        private  ILogger<MedicamentoController> _logger;
        private  IMedicamentoFlujo _medicamentoFlujo;
        public MedicamentoController(ILogger<MedicamentoController> logger, IMedicamentoFlujo medicamentoFlujo)
        {
            _logger = logger;
            _medicamentoFlujo = medicamentoFlujo;
        }
        [HttpPost]
        public async Task<IActionResult> CrearMedicamento([FromBody]MedicamentoResponse medicamento)
        {
            var result = await _medicamentoFlujo.CrearMedicamento(medicamento);
            if (result == Guid.Empty)
            {
                _logger.LogError("Error al crear el medicamento");
                return BadRequest("No se pudo crear el medicamento");
            }
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditarMedicamento([FromRoute] Guid Id, [FromBody] MedicamentoResponse medicamento)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("El medicamento no existe");
            }
            var resultado = _medicamentoFlujo.EditarMedicamento(Id, medicamento);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarMedicamento([FromRoute]Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("El medicamento no existe");
            }
            var resultado = _medicamentoFlujo.EliminarMedicamento(Id);
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerMedicamentoPorId([FromRoute]Guid Id)
        {
            if (!await VerificarExiste(Id))
            {
                return NotFound("El medicamento no existe");
            }
            var resultado = _medicamentoFlujo.ObtenerMedicamentoPorId(Id);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMedicamentos()
        {
            var result = await _medicamentoFlujo.ObtenerMedicamentos();
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No se encontraron medicamentos");
                return NotFound("No se encontraron medicamentos");
            }
            return Ok(result);
        }

        #region Helpers
        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoExiste = await _medicamentoFlujo.ObtenerMedicamentoPorId(Id);
            if (resultadoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }


        #endregion
    }
}
