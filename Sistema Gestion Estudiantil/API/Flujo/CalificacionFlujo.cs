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

        public async Task<List<EstudianteConCalificaciones>> ObtenerCalificacionesPorEstudiantes()
        {
            var calificacionesPlanas = await _calificacionDA.ObtenerCalificaciones();

            var agrupado = calificacionesPlanas
                .GroupBy(c => new
                {
                    c.IdEstudiante,
                    c.Cedula,
                    c.Nombre,
                    c.Apellido1,
                    c.Apellido2
                })
                .Select(g => new EstudianteConCalificaciones
                {
                    IdEstudiante = g.Key.IdEstudiante,
                    Cedula = g.Key.Cedula?.Trim(),
                    Nombre = g.Key.Nombre?.Trim(),
                    Apellido1 = g.Key.Apellido1?.Trim(),
                    Apellido2 = g.Key.Apellido2?.Trim(),
                    Calificaciones = g
                        .Where(c => c.IdCalificacion != Guid.Empty)
                        .Select(c => new CalificacionResponse
                        {
                            IdCalificacion = c.IdCalificacion,
                            IdEstudiante = c.IdEstudiante,
                            IdMateria = c.IdMateria,
                            NombreMateria = c.NombreMateria?.Trim(),
                            NombreCompleto = $"{c.Nombre?.Trim()} {c.Apellido1?.Trim()} {c.Apellido2?.Trim()}".Trim(),
                            Calificacion = c.Calificacion,
                            Comentario = c.Comentario?.Trim(),
                            Fecha = c.Fecha
                        })
                        .OrderBy(c => c.Fecha)
                        .ToList()
                })
                .OrderBy(e => e.Apellido1)
                .ThenBy(e => e.Apellido2)
                .ToList();

            return agrupado;
        }

    }
}
