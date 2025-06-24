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
    public class ReunionDA : IReunionDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public ReunionDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearReunion(ReunionesRequest reunion)
        {
            string query = @"Sp_CrearReunion";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = reunion.IdEstudiante,
                Fecha = reunion.Fecha,
                Motivo = reunion.Motivo
            });
            return result;
        }

        public async Task<Guid> EditarReunion(Guid IdReunion, ReunionesRequest reunion)
        {
            string query = @"SP_EditarReunion";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdReunion = IdReunion,
                IdEstudiante = reunion.IdEstudiante,
                Fecha = reunion.Fecha,
                Motivo = reunion.Motivo
            });
            return result;
        }

        public async Task<Guid> EliminarReunion(Guid IdReunion)
        {
           string query = @"SP_EliminarReunion";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdReunion = IdReunion
            });
            return result;
        }

        public async Task<ReunionesResponse> ObtenerReunion(Guid IdReunion)
        {
            string query = @"SP_ObtenerReunion";
            var result = await _conn.QueryAsync<ReunionesResponse>(query, new
            {
                IdReunion = IdReunion
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ReunionesResponse>> ObtenerReuniones()
        {
            string query = @"SP_ObtenerReuniones";
            var result = await _conn.QueryAsync<ReunionesResponse>(query);
            return result;
        }

        public async Task<IEnumerable<ReunionesResponse>> ObtenerReunionesPorEstudiante(Guid IdEstudiante)
        {
           string query = @"SP_ObtenerReunionesPorEstudiante";
            var result = await _conn.QueryAsync<ReunionesResponse>(query, new
            {
                IdEstudiante = IdEstudiante
            });
            return result;
        }
    }
}
