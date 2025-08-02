using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DA
{
    public class FamiliarDA : IFamiliarDA
    {
        private readonly IRepositorioDapper _repDapp;
        private readonly SqlConnection _conn;

        public FamiliarDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares()
        {
            string query = "SP_ObtenerFamiliares";
            return await _conn.QueryAsync<FamiliarResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<FamiliarResponse> ObtenerFamiliarPorId(Guid id)
        {
            string query = "SP_ObtenerFamiliarPorId";
            var result = await _conn.QueryAsync<FamiliarResponse>(query, new { IdFamiliar = id }, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<Guid> CrearFamiliar(FamiliarRequest familiar)
        {
            string query = "SP_CrearFamiliar";
            return await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = familiar.IdEstudiante,
                Nombre = familiar.Nombre,
                Cedula = familiar.Cedula,
                Apellido1 = familiar.Apellido1,
                Apellido2 = familiar.Apellido2,
                Direccion = familiar.Direccion,
                Parentesco = familiar.Parentesco,
                TelefonoCelular = familiar.TelefonoCelular,
                CorreoElectronico = familiar.CorreoElectronico,
                Ocupacion = familiar.Ocupacion,
                EstadoCivil = familiar.EstadoCivil,
                CantidadHijos = familiar.CantidadHijos,
                IdUsuario = familiar.IdUsuario,
                ViveConEstudiante = familiar.ViveConEstudiante,
                EsResponsableLegal = familiar.EsResponsableLegal
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Guid> EditarFamiliar(Guid id, FamiliarRequest familiar)
        {
            string query = "SP_EditarFamiliar";
            return await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdFamiliar = id,
                IdEstudiante = familiar.IdEstudiante,
                Nombre = familiar.Nombre,
                Cedula = familiar.Cedula,
                Apellido1 = familiar.Apellido1,
                Apellido2 = familiar.Apellido2,
                Direccion = familiar.Direccion,
                Parentesco = familiar.Parentesco,
                TelefonoCelular = familiar.TelefonoCelular,
                CorreoElectronico = familiar.CorreoElectronico,
                Ocupacion = familiar.Ocupacion,
                EstadoCivil = familiar.EstadoCivil,
                CantidadHijos = familiar.CantidadHijos,
                IdUsuario = familiar.IdUsuario,
                ViveConEstudiante = familiar.ViveConEstudiante,
                EsResponsableLegal = familiar.EsResponsableLegal
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Guid> EliminarFamiliar(Guid id)
        {
            string query = "SP_EliminarFamiliar";
            return await _conn.ExecuteScalarAsync<Guid>(query, new { IdFamiliar = id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
