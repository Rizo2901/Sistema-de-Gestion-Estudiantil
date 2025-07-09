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
    public class EnfermedadFlujo : IEnfermedadFlujo
    {
        private IEnfermedadDA _enfermedadDA;
        public EnfermedadFlujo(IEnfermedadDA enfermedadDA)
        {
            _enfermedadDA = enfermedadDA;
        }
        public async Task<Guid> CrearEnfermedad(EnfermedadRequest enfermedad)
        {
            return await _enfermedadDA.CrearEnfermedad(enfermedad);
        }

        public async Task<Guid> EditarEnfermedad(Guid Id, EnfermedadRequest enfermedad)
        {
            return await _enfermedadDA.EditarEnfermedad(Id, enfermedad);
        }

        public async Task<Guid> EliminarEnfermedad(Guid Id)
        {
            return await _enfermedadDA.EliminarEnfermedad(Id);
        }

        public async Task<IEnumerable<EnfermedadResponse>> ObtenerEnfermedades()
        {
            return await _enfermedadDA.ObtenerEnfermedades();
        }

        public async Task<EnfermedadResponse> ObtenerEnfermedadPorId(Guid Id)
        {
            return await _enfermedadDA.ObtenerEnfermedadPorId(Id);
        }
    }
}
