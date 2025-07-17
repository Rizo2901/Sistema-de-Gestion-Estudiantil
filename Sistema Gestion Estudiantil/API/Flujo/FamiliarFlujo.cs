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

        public Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares() => _familiarDA.ObtenerFamiliares();

        public Task<FamiliarResponse> ObtenerFamiliarPorId(Guid id) => _familiarDA.ObtenerFamiliarPorId(id);

        public Task<Guid> CrearFamiliar(FamiliarRequest familiar) => _familiarDA.CrearFamiliar(familiar);

        public Task<Guid> EditarFamiliar(Guid id, FamiliarRequest familiar) => _familiarDA.EditarFamiliar(id, familiar);

        public Task<Guid> EliminarFamiliar(Guid id) => _familiarDA.EliminarFamiliar(id);
    }
}
