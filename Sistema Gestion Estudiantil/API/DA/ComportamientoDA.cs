using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DA
{
    public class ComportamientoDA : IComportamientoDA
    {
        private readonly IRepositorioDapper _repDapp;
        private readonly SqlConnection _conn;

        public ComportamientoDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<ComportamientoResponse>> ObtenerComportamientos()
        {
            string query = "SP_ObtenerComportamientos";
            return await _conn.QueryAsync<ComportamientoResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<ComportamientoResponse> ObtenerComportamientoPorId(Guid id)
        {
            string query = "SP_ObtenerComportamientoPorId";
            var result = await _conn.QueryAsync<ComportamientoResponse>(query, new { IdComportamiento = id }, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<Guid> CrearComportamiento(ComportamientoRequest comportamiento)
        {
            string query = "SP_CrearComportamiento";
            return await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = comportamiento.IdEstudiante,
                Fecha = comportamiento.Fecha,
                Observacion = comportamiento.Observacion,
                AccionTomada = comportamiento.AccionTomada
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Guid> EditarComportamiento(Guid id, ComportamientoRequest comportamiento)
        {
            string query = "SP_EditarComportamiento";
            return await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdComportamiento = id,
                IdEstudiante = comportamiento.IdEstudiante,
                Fecha = comportamiento.Fecha,
                Observacion = comportamiento.Observacion,
                AccionTomada = comportamiento.AccionTomada
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Guid> EliminarComportamiento(Guid id)
        {
            string query = "SP_EliminarComportamiento";
            return await _conn.ExecuteScalarAsync<Guid>(query, new { IdComportamiento = id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
