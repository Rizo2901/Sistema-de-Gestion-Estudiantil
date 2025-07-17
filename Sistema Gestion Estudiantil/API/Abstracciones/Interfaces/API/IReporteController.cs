using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IReporteController
    {

        Task<IActionResult> ReporteUniversal(ReporteEstudiantilRequest filtros);
    }
}
