using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IReunionFlujo
    {
        Task<Guid> CrearReunion(ReunionesRequest reunion);
        Task<Guid> EditarReunion(Guid IdReunion, ReunionesRequest reunion);
        Task<Guid> EliminarReunion(Guid IdReunion);
        Task<ReunionesResponse> ObtenerReunion(Guid IdReunion);
        Task<IEnumerable<ReunionesResponse>> ObtenerReuniones();
        Task<IEnumerable<ReunionesResponse>> ObtenerReunionesPorEstudiante(Guid IdEstudiante);
    }
}
