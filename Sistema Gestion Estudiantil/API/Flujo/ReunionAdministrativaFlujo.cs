using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class ReunionAdministrativaFlujo : IReunionAdministrativaFlujo
    {
        private IReunionAdministrativaDA _reunionAdministrativaDA;
        public ReunionAdministrativaFlujo(IReunionAdministrativaDA reunionAdministrativaDA)
        {
            _reunionAdministrativaDA = reunionAdministrativaDA;
        }
        public async Task<Guid> CrearReunionAdministrativa(ReunionAdministrativaRequest reunion)
        {
            return await _reunionAdministrativaDA.CrearReunionAdministrativa(reunion);
        }

        public async Task<Guid> EditarReunionAdministrativa(Guid IdReunion, ReunionAdministrativaRequest reunion)
        {
            return await _reunionAdministrativaDA.EditarReunionAdministrativa(IdReunion, reunion);
        }

        public async Task<Guid> EliminarReunionAdministrativa(Guid IdReunion)
        {
            return await _reunionAdministrativaDA.EliminarReunionAdministrativa(IdReunion);
        }

        public async Task<ReunionAdministrativaResponse> ObtenerReunionAdministrativa(Guid IdReunion)
        {
            return await _reunionAdministrativaDA.ObtenerReunionAdministrativa(IdReunion);
        }

        public async Task<IEnumerable<ReunionAdministrativaResponse>> ObtenerReunionesAdministrativas()
        {
            return await _reunionAdministrativaDA.ObtenerReunionesAdministrativas();
        }
    }
}
