using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IReporteDA
    {
        Task<IEnumerable<ReporteEstudiantilResponse>> ObtenerDatosReporte(ReporteEstudiantilRequest filtros);
    }


}

