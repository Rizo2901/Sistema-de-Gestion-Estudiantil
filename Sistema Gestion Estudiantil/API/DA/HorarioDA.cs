using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            string query = "Horario.SP_ObtenerHorarios";
            var result = await _conn.QueryAsync<HorarioResponse>(query);
            return result;
        }

        public async Task<HorarioResponse> ObtenerHorarioPorId(Guid IdHorario)
        {
            string query = "Horario.SP_ObtenerHorarioPorId";
            var result = await _conn.QueryAsync<HorarioResponse>(query, 
                new 
                { 
                    IdHorario = IdHorario
                });
            return result.FirstOrDefault();
        }

        public async Task<Guid> CrearHorario(HorarioRequest horario)
        {
            string query = @"Horario.SP_CrearHorario";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccion = horario.IdSeccion,
                IdMateria = horario.IdMateria,
                IdProfesor = horario.IdProfesor,
                Dia = horario.Dia,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Aula = horario.Aula
            });
            return result;
        }

        public async Task<Guid> EditarHorario(Guid IdHorario, HorarioRequest horario)
        {
            string query = "Horario.SP_EditarHorario";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdHorario = IdHorario,
                IdSeccion = horario.IdSeccion,
                IdMateria = horario.IdMateria,
                IdProfesor = horario.IdProfesor,
                Dia = horario.Dia,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Aula = horario.Aula
            });
            return result;
        }

        public async Task<Guid> EliminarHorario(Guid IdHorario)
        {
            string query = "Horario.SP_EliminarHorario";
            var result = await  _conn.ExecuteScalarAsync<Guid>(query, 
                new 
                { 
                    IdHorario = IdHorario
                });
            return result;
        }
    }
}
