using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DA
{
    public class HorarioDA : IHorarioDA
    {
        private readonly IRepositorioDapper _repDapp;
        private readonly SqlConnection _conn;

        public HorarioDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<HorarioResponse>> ObtenerHorarios()
        {
            string query = "SP_ObtenerHorarios";
            return await _conn.QueryAsync<HorarioResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<HorarioResponse> ObtenerHorarioPorId(Guid id)
        {
            string query = "SP_ObtenerHorarioPorId";
            var result = await _conn.QueryAsync<HorarioResponse>(query, new { IdHorario = id }, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<Guid> CrearHorario(HorarioRequest horario)
        {
            string query = "SP_CrearHorario";
            return await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Dia = horario.Dia,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                IdSeccion = horario.IdSeccion
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Guid> EditarHorario(Guid id, HorarioRequest horario)
        {
            string query = "SP_EditarHorario";
            return await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdHorario = id,
                Dia = horario.Dia,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                IdSeccion = horario.IdSeccion
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Guid> EliminarHorario(Guid id)
        {
            string query = "SP_EliminarHorario";
            return await _conn.ExecuteScalarAsync<Guid>(query, new { IdHorario = id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
