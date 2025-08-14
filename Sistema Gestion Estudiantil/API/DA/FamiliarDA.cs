using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
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
            var result = await _conn.QueryAsync<FamiliarResponse>(query);
            return result;
        }

        public async Task<FamiliarResponse> ObtenerFamiliarPorId(Guid IdFamiliar)
        {
            string query = "SP_ObtenerFamiliarPorId";
            var result = await _conn.QueryAsync<FamiliarResponse>(query, 
                new 
                { 
                    IdFamiliar = IdFamiliar
                });
            return result.FirstOrDefault();
        }

        public async Task<Guid> CrearFamiliar(FamiliarRequest familiar)
        {
            string query = "SP_CrearFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
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
            },commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EditarFamiliar(Guid IdFamiliar, FamiliarRequest familiar)
        {
            string query = "SP_EditarFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdFamiliar = IdFamiliar,
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
            });
            return result;
        }

        public async Task<Guid> EliminarFamiliar(Guid IdFamiliar)
        {
            string query = "SP_EliminarFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, 
                new 
                { 
                    IdFamiliar = IdFamiliar
                });
            return result;
        }
    }
}
