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

        public async Task<Guid> EditarReunionAdministrativa(Guid IdReunionAdmin, ReunionAdministrativaRequest reunion)
        {
            return await _reunionAdministrativaDA.EditarReunionAdministrativa(IdReunionAdmin, reunion);
        }

        public async Task<Guid> EliminarReunionAdministrativa(Guid IdReunionAdmin)
        {
            return await _reunionAdministrativaDA.EliminarReunionAdministrativa(IdReunionAdmin);
        }

        public async Task<ReunionAdministrativaResponse> ObtenerReunionAdministrativa(Guid IdReunionAdmin)
        {
            return await _reunionAdministrativaDA.ObtenerReunionAdministrativa(IdReunionAdmin);
        }

        public async Task<IEnumerable<ReunionAdministrativaResponse>> ObtenerReunionesAdministrativas()
        {
            return await _reunionAdministrativaDA.ObtenerReunionesAdministrativas();
        }
    }
}
