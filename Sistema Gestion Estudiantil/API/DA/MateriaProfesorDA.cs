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
    public class MateriaProfesorDA : IMateriaProfesorDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public MateriaProfesorDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearMateriaProfesor(MateriaProfesorRequest materiaProfesor)
        {
            string query = "SP_CrearMateriaProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMateria = materiaProfesor.IdMateria,
                IdProfesor = materiaProfesor.IdProfesor
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EditarMateriaProfesor(Guid Id, MateriaProfesorRequest materiaProfesor)
        {
            string query = "SP_EditarMateriaProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMateriaProfesor = Id,
                IdMateria = materiaProfesor.IdMateria,
                IdProfesor = materiaProfesor.IdProfesor
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EliminarMateriaProfesor(Guid Id)
        {
            string query = "SP_EliminarMateriaProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMateriaProfesor = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<MateriaPorProfesorResponse>> MateriasPorIdProfesor(Guid IdProfesor)
        {
            string query = "SP_MateriasPorProfesor";
            var result = await _conn.QueryAsync<MateriaPorProfesorResponse>(query, new
            {
                IdProfesor = IdProfesor
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Task<MateriaProfesorResponse> ObtenerMateriaProfesorPorId(Guid Id)
        {
            string query = "SP_ObtenerMateriaProfesorPorId";
            var result = _conn.QuerySingleOrDefaultAsync<MateriaProfesorResponse>(query, new
            {
                IdMateriaProfesor = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Task<IEnumerable<MateriaProfesorResponse>> ObtenerMateriasProfesores()
        {
           string query = "SP_ObtenerMateriasProfesores";
            var result = _conn.QueryAsync<MateriaProfesorResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Task<IEnumerable<MateriaProfesorResponse>> ProfesoresPorMateria(Guid IdMateria)
        {
            string query = "SP_ProfesoresPorMateria";
            var result = _conn.QueryAsync<MateriaProfesorResponse>(query, new
            {
                IdMateria = IdMateria
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
    }
}
