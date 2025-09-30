using Abstracciones.Modelos;


namespace Abstracciones.Interfaces.Flujo
{
    public interface IReporteFlujo
    {
       
        Task<object> GenerarReporteUniversal(ReporteEstudiantilRequest filtros);
    }
}
