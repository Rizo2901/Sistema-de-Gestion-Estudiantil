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
    public class CalificacionFlujo : ICalificacionFlujo
    {
        private ICalificacionDA _calificacionDA;
        public CalificacionFlujo(ICalificacionDA calificacionDA)
        {
            _calificacionDA = calificacionDA;
        }

        public async Task<Guid> CrearCalificacion(CalificacionRequest calificacion)
        {
            return await _calificacionDA.CrearCalificacion(calificacion);
        }

        public async Task<Guid> EditarCalificacion(Guid IdCalificacion, CalificacionRequest calificacion)
        {
            return await _calificacionDA.EditarCalificacion(IdCalificacion, calificacion);
        }

        public async Task<Guid> EliminarCalificacion(Guid IdCalificacion)
        {
            return await _calificacionDA.EliminarCalificacion(IdCalificacion);
        }

        public async Task<CalificacionResponse> ObtenerCalificacion(Guid IdCalificacion)
        {
            return await _calificacionDA.ObtenerCalificacion(IdCalificacion);
        }

        public async Task<IEnumerable<CalificacionResponse>> ObtenerCalificaciones()
        {
            return await _calificacionDA.ObtenerCalificaciones();
        }

        public async Task<IEnumerable<CalificacionResponse>> ObtenerCalificacionesPorEstudiante(Guid IdEstudiante)
        {
            return await _calificacionDA.ObtenerCalificacionesPorEstudiante(IdEstudiante);
        }
    }
}
