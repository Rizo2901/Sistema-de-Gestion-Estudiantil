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
    public class ProfesorDA : IProfesorDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;

        public ProfesorDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearProfesor(ProfesorRequest profesor)
        {
            string query = @"Sp_CrearProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = profesor.Nombre,
                Cedula = profesor.Cedula,
                Apellido1 = profesor.Apellido1,
                Apellido2 = profesor.Apellido2,
                Direccion = profesor.Direccion,
                CorreoElectronico = profesor.CorreoElectronico,
                TelefonoCelular = profesor.TelefonoCelular,

            }, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public Task<Guid> EditarProfesor(Guid Id, ProfesorRequest profesor)
        {
            string query = @"SP_EditarProfesor";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdProfesor = Id,
                Nombre = profesor.Nombre,
                Cedula = profesor.Cedula,
                Apellido1 = profesor.Apellido1,
                Apellido2 = profesor.Apellido2,
                CorreoElectronico = profesor.CorreoElectronico,
                TelefonoCelular = profesor.TelefonoCelular,
                Direccion = profesor.Direccion
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Task<Guid> EliminarProfesor(Guid Id)
        {
            string query = @"SP_EliminarProfesor";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdProfesor = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<ProfesorResponse> ObtenerProfesor(Guid Id)
        {
            string query = @"SP_ObtenerProfesorPorId";
            var result = await _conn.QueryAsync<ProfesorResponse>(query, new
            {
                IdProfesor = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();

        }

        public Task<IEnumerable<ProfesorResponse>> ObtenerProfesores()
        {
            string query = @"SP_ObtenerProfesores";
            var result = _conn.QueryAsync<ProfesorResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ObtenerProfesoresTotal()
        {
            string query = @"Sp_ObtenerProfesoresTotal";
            var result = await _conn.QuerySingleAsync<int>(query);
            return result;
        }
    }
}
