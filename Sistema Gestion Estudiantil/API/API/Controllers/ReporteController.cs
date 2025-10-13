using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReporteController : ControllerBase, IReporteController
{
    private readonly IReporteFlujo _reporteFlujo;

    public ReporteController(IReporteFlujo reporteFlujo)
    {
        _reporteFlujo = reporteFlujo;
    }

    [HttpPost("universal")]
    public async Task<IActionResult> ReporteUniversal([FromBody] ReporteEstudiantilRequest request)
    {
        var archivo = await _reporteFlujo.GenerarReporteUniversal(request);

        string contentType;
        string extension;

        switch (request.ExportarComo?.ToLower())
        {
            case "excel":
                contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                extension = "xlsx";
                break;
            case "csv":
                contentType = "text/csv";
                extension = "csv";
                break;
            default:
                contentType = "application/pdf";
                extension = "pdf";
                break;
        }

        var nombreArchivo = $"reporte_universal_{DateTime.Now:yyyyMMdd_HHmm}.{extension}";

        return File((byte[])archivo, contentType, nombreArchivo);
    }

    [HttpPost("Vista")]
    public async Task<IActionResult> ObtenerVistaEstudiantilAsync([FromBody] ReporteEstudiantilRequest filtros)
    {
        try
        {
            var resultado = await _reporteFlujo.ObtenerVistaEstudiantilAsync(filtros);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener datos: {ex.Message}");
        }
    }


}
