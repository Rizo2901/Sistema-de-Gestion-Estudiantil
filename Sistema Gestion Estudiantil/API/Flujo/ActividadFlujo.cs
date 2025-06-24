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
    public class ActividadFlujo : IActividadFlujo
    {
        private IActividadDA _actividadDA;

        public ActividadFlujo(IActividadDA actividadDA)
        {
            _actividadDA = actividadDA;
        }

        public async Task<Guid> CrearActividad(ActividadRequest actividad)
        {
            return await _actividadDA.CrearActividad(actividad);
        }

        public async Task<Guid> EditarActividad(Guid Id, ActividadRequest actividad)
        {
            return await _actividadDA.EditarActividad(Id, actividad);
        }

        public async Task<Guid> EliminarActividad(Guid Id)
        {
           return await _actividadDA.EliminarActividad(Id);
        }

        public async Task<IEnumerable<ActividadResponse>> ObtenerActividades()
        {
            return await _actividadDA.ObtenerActividades();
        }

        public async Task<ActividadResponse> ObtenerActividadPorId(Guid Id)
        {
            return await _actividadDA.ObtenerActividadPorId(Id);
        }
    }
}
