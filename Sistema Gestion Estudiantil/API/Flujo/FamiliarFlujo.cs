using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class FamiliarFlujo : IFamiliarFlujo
    {
        private readonly IFamiliarDA _familiarDA;

        public FamiliarFlujo(IFamiliarDA familiarDA)
        {
            _familiarDA = familiarDA;
        }

        public async Task<Guid> CrearFamiliar(FamiliarRequest familiar)
        {
            return await _familiarDA.CrearFamiliar(familiar);
        }

        public async Task<Guid> EditarFamiliar(Guid IdFamiliar, FamiliarRequest familiar)
        {
            return await _familiarDA.EditarFamiliar(IdFamiliar, familiar);
        }

        public async Task<Guid> EliminarFamiliar(Guid IdFamiliar)
        {
            return await _familiarDA.EliminarFamiliar(IdFamiliar);
        }

        public async Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares()
        {
            return await _familiarDA.ObtenerFamiliares();
        }

        public async Task<FamiliarResponse> ObtenerFamiliarPorId(Guid IdFamiliar)
        {
            return await _familiarDA.ObtenerFamiliarPorId(IdFamiliar);
        }
    }
}
