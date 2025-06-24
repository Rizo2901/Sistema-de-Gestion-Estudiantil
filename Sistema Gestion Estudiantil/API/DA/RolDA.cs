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
    public class RolDA : IRolDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public RolDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearRole(RolesRequest rol)
        {
           string query = "SP_CrearRol";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EditarRole(Guid Id, RolesRequest rol)
        {
            string query = @"Sp_EditarRol";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdRol = Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EliminarRole(Guid Id)
        {
           string query = "SP_EliminarRol";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new 
            { IdRol = Id 

            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<RolesResponse> ObtenerRolePorId(Guid Id)
        {
            string query = @"SP_ObtenerRolPorId";
            var result = await _conn.QueryAsync<RolesResponse>(query, new
            {
                IdRol = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<RolesResponse>> ObtenerRoles()
        {
            string query = @"SP_ObtenerRoles";
            var result = await _conn.QueryAsync<RolesResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
