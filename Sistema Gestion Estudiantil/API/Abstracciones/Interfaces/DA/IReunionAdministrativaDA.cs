using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IReunionAdministrativaDA
    {
        Task<Guid> CrearReunionAdministrativa(ReunionAdministrativaRequest reunion);
        Task<Guid> EditarReunionAdministrativa(Guid IdReunionAdmin, ReunionAdministrativaRequest reunion);
        Task<Guid> EliminarReunionAdministrativa(Guid IdReunionAdmin);
        Task<ReunionAdministrativaResponse> ObtenerReunionAdministrativa(Guid IdReunionAdmin);
        Task<IEnumerable<ReunionAdministrativaResponse>> ObtenerReunionesAdministrativas();
    }
}
