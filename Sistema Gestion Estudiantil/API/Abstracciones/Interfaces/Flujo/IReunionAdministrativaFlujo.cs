using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IReunionAdministrativaFlujo
    {
        Task<Guid> CrearReunionAdministrativa(ReunionAdministrativaRequest reunion);
        Task<Guid> EditarReunionAdministrativa(Guid IdReunion, ReunionAdministrativaRequest reunion);
        Task<Guid> EliminarReunionAdministrativa(Guid IdReunion);
        Task<ReunionAdministrativaResponse> ObtenerReunionAdministrativa(Guid IdReunion);
        Task<IEnumerable<ReunionAdministrativaResponse>> ObtenerReunionesAdministrativas();
    }
}
