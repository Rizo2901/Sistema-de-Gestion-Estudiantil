using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICalificacionDA
    {
        Task<Guid>CrearCalificacion(CalificacionRequest calificacion);
        Task<Guid> EditarCalificacion(Guid IdCalificacion, CalificacionRequest calificacion);
        Task<Guid> EliminarCalificacion(Guid IdCalificacion);
        Task<CalificacionResponse> ObtenerCalificacion(Guid IdCalificacion);
        Task<IEnumerable<CalificacionResponse>> ObtenerCalificaciones();

        Task<IEnumerable<CalificacionResponse>> ObtenerCalificacionesPorEstudiante(Guid IdEstudiante);

    }
}
