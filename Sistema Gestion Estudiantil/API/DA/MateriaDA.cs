using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MateriaDA : IMateriaDA
    {
        private SqlConnection _conn;

        private IRepositorioDapper _repDapp;

        public MateriaDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearMateria(MateriasRequest materia)
        {
            string query = @"Materia.SP_CrearMateria";

            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = materia.Nombre,
                Descripcion = materia.Descripcion
            }, commandType: CommandType.StoredProcedure);

            return result;
        }

        public Task<Guid> EditarMateria(Guid Id, MateriasRequest materia)
        {
            string query = @"Materia.SP_EditarMateria";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMateria = Id,
                Nombre = materia.Nombre,
                Descripcion = materia.Descripcion
            });

            return result;
        }

        public Task<Guid> EliminarMateria(Guid Id)
        {
            string query = @"Materia.SP_EliminarMateria";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMateria = Id
            });
            return result;
        }

        public async Task<MateriasResponse> ObtenerMateria(Guid Id)
        {
            string query = @"Materia.SP_ObtenerMateriaPorId";
            var result = await _conn.QueryAsync<MateriasResponse>(query, new
            {
                IdMateria = Id
            });
            return result.FirstOrDefault();
        }

        public Task<IEnumerable<MateriasResponse>> ObtenerMaterias()
        {
            string query = "Materia.SP_ObtenerMaterias";
            var result = _conn.QueryAsync<MateriasResponse>(query, commandType: CommandType.StoredProcedure);
            return result;
        }

    }
}
