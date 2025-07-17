using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase, IReporteController
    {
        private readonly IReporteFlujo _reporteFlujo;

        public ReporteController(IReporteFlujo reporteFlujo)
        {
            _reporteFlujo = reporteFlujo;
        }

        [HttpPost("universal")]
        public async Task<IActionResult> ReporteUniversal([FromBody] ReporteEstudiantilRequest filtros)
        {
            var resultado = await _reporteFlujo.GenerarReporteUniversal(filtros);

            if (resultado is byte[] archivo && !string.IsNullOrWhiteSpace(filtros.ExportarComo))
            {
                var fecha = DateTime.Now.ToString("yyyyMMdd_HHmm");
                var extension = filtros.ExportarComo.ToLower();
                var contentType = extension switch
                {
                    "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "pdf" => "application/pdf",
                    "csv" => "text/csv; charset=utf-8",
                    _ => "application/octet-stream"
                };
                var nombreArchivo = $"reporte_universal_{fecha}.{extension}";
                return File(archivo, contentType, nombreArchivo);
            }

            return Ok(resultado);
        }
    }
}