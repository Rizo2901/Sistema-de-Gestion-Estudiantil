using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;


namespace Abstracciones.Interfaces.Flujo
{
    public interface IReporteFlujo
    {
       
        Task<object> GenerarReporteUniversal(ReporteEstudiantilRequest filtros);
        Task<List<ReporteEstudiantilResponse>> ObtenerVistaEstudiantilAsync(ReporteEstudiantilRequest filtros);
    }
}
