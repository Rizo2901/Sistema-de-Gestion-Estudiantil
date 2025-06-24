using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CalificacionDA:  ICalificacionDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;

        public CalificacionDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearCalificacion(CalificacionRequest calificacion)
        {
            string query = @"SP_CrearCalificacion";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = calificacion.IdEstudiante,
                IdMateria = calificacion.IdMateria,
                Calificacion = calificacion.Calificacion,
                Fecha = calificacion.Fecha,
                Comentario = calificacion.Comentario
            });
            return result;

        }

        public async Task<Guid> EditarCalificacion(Guid IdCalificacion, CalificacionRequest calificacion)
        {
            string query = @"SP_EditarCalificacion";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCalificacion = IdCalificacion,
                IdEstudiante = calificacion.IdEstudiante,
                IdMateria = calificacion.IdMateria,
                Calificacion = calificacion.Calificacion,
                Fecha = calificacion.Fecha,
                Comentario = calificacion.Comentario
            });
            return result;
        }

        public async Task<Guid> EliminarCalificacion(Guid IdCalificacion)
        {
            string query = @"SP_EliminarCalificacion";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCalificacion = IdCalificacion
            });
            return result;
        }

        public async Task<CalificacionResponse> ObtenerCalificacion(Guid IdCalificacion)
        {
           string query = @"SP_ObtenerCalificacion";
            var result = await _conn.QueryAsync<CalificacionResponse>(query, new
            {
                IdCalificacion = IdCalificacion
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CalificacionResponse>> ObtenerCalificaciones()
        {
            string query = @"SP_ObtenerCalificaciones";
            var result = await _conn.QueryAsync<CalificacionResponse>(query);
            return result;
        }

        public async Task<IEnumerable<CalificacionResponse>> ObtenerCalificacionesPorEstudiante(Guid IdEstudiante)
        {
            string query = @"SP_ObtenerCalificacionesPorEstudiante";
            var result = await _conn.QueryAsync<CalificacionResponse>(query, new
            {
                IdEstudiante = IdEstudiante
            });
            return result;
        }
    }
}
