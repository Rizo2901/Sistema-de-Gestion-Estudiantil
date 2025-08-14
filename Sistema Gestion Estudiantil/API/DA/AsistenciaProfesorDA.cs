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
    public class AsistenciaProfesorDA : IAsistenciaProfesorDA
    {
        private  IRepositorioDapper _repDapp;
        private  SqlConnection _conn;
        public AsistenciaProfesorDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearAsitenciaProfesor(AsistenciaProfesorRequest asistenciaProfesor)
        {
            string query = @"AsistenciaProfesor.Sp_CrearAsistenciaProfesor";
            var result =  await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdProfesor = asistenciaProfesor.IdProfesor,
                Fecha = asistenciaProfesor.Fecha,
                HoraEntrada = asistenciaProfesor.HoraEntrada,
                HoraSalida = asistenciaProfesor.HoraSalida
            });
            return result;
        }

        public async Task<Guid> EditarAsitenciaProfesor(Guid IdAsistenciaProfesor, AsistenciaProfesorRequest asistenciaProfesor)
        {
            string query = @"AsistenciaProfesor.Sp_ActualizarAsistenciaProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAsistenciaProfesor = IdAsistenciaProfesor,
                IdProfesor = asistenciaProfesor.IdProfesor,
                Fecha = asistenciaProfesor.Fecha,
                HoraEntrada = asistenciaProfesor.HoraEntrada,
                HoraSalida = asistenciaProfesor.HoraSalida
            });
            return result;
        }

        public async Task<Guid> EliminarAsitenciaProfesor(Guid IdAsistenciaProfesor)
        {
            string query = @"AsistenciaProfesor.Sp_EliminarAsistenciaProfesor";
            var result =  await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAsistenciaProfesor = IdAsistenciaProfesor
            });
            return result;
        }

        public async Task<IEnumerable<AsistenciaProfesorResponse>> ObtenerAsitenciaProfesores()
        {
            string query = @"AsistenciaProfesor.Sp_ObtenerAsistenciasProfesor";
            var result = await _conn.QueryAsync<AsistenciaProfesorResponse>(query);
            return result;

        }

        public async Task<IEnumerable<AsistenciaProfesorResponse>> ObtenerAsitenciaProfesoresPorIdProfesor(Guid IdProfesor)
        {
            string query = @"AsistenciaProfesor.Sp_ObtenerAsistenciasPorProfesor";
            var result = await _conn.QueryAsync<AsistenciaProfesorResponse>(query, new
            {
                IdProfesor = IdProfesor
            });
            return result;
        }

        public async Task<AsistenciaProfesorResponse> ObtenerAsitenciaProfesorPorId(Guid IdAsistenciaProfesor)
        {
            string query = @"AsistenciaProfesor.Sp_ObtenerAsistenciaProfesorPorId";
            var result = await _conn.QueryAsync<AsistenciaProfesorResponse>(query, new
            {
                IdAsistenciaProfesor = IdAsistenciaProfesor
            });
            return result.FirstOrDefault();
        }
    }
}
