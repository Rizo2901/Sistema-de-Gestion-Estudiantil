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
    public class ReunionFlujo : IReunionFlujo
    {
        private IReunionDA _reunionDA;
        public ReunionFlujo(IReunionDA reunionDA)
        {
            _reunionDA = reunionDA;
        }

        public async Task<Guid> CrearReunion(ReunionesRequest reunion)
        {
            return await _reunionDA.CrearReunion(reunion);
        }

        public async Task<Guid> EditarReunion(Guid IdReunion, ReunionesRequest reunion)
        {
           return await _reunionDA.EditarReunion(IdReunion, reunion);
        }

        public async Task<Guid> EliminarReunion(Guid IdReunion)
        {
            return await _reunionDA.EliminarReunion(IdReunion);
        }

        public async Task<ReunionesResponse> ObtenerReunion(Guid IdReunion)
        {
            return await _reunionDA.ObtenerReunion(IdReunion);
        }

        public async Task<IEnumerable<ReunionesResponse>> ObtenerReuniones()
        {
            return await _reunionDA.ObtenerReuniones();
        }

        public async Task<IEnumerable<ReunionesResponse>> ObtenerReunionesPorEstudiante(Guid IdEstudiante)
        {
            return await _reunionDA.ObtenerReunionesPorEstudiante(IdEstudiante);
        }
    }
}
