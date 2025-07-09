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
    public class ReunionAdministrativaDA : IReunionAdministrativaDA
    {
        private  IRepositorioDapper _repDapp;
        private SqlConnection _conn;
        public ReunionAdministrativaDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearReunionAdministrativa(ReunionAdministrativaRequest reunion)
        {
            string query = @"ReunionAdministrativa.Sp_CrearReunionAdministrativa";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Fecha = reunion.Fecha,
                Tema = reunion.Tema,
                Descripcion = reunion.Descripcion,
                Lugar = reunion.Lugar,
                Responsable = reunion.Responsable
            });

            return result;
        }

        public async Task<Guid> EditarReunionAdministrativa(Guid IdReunionAdmin, ReunionAdministrativaRequest reunion)
        {
            string query = @"ReunionAdministrativa.SP_EditarReunionAdministrativa";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdReunionAdmin = reunion.IdReunionAdmin,
                Fecha = reunion.Fecha,
                Tema = reunion.Tema,
                Descripcion = reunion.Descripcion,
                Lugar = reunion.Lugar,
                Responsable = reunion.Responsable
            });
            return result;
        }

        public async Task<Guid> EliminarReunionAdministrativa(Guid IdReunionAdmin)
        {
            string query = @"ReunionAdministrativa.SP_EliminarReunionAdministrativa";
            var result =await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdReunionAdmin = IdReunionAdmin
            });
            return result;


        }

        public async Task<ReunionAdministrativaResponse> ObtenerReunionAdministrativa(Guid IdReunionAdmin)
        {
            string query = @"ReunionAdministrativa.Sp_ObtenerReunionAdministrativa";
            var result = await _conn.QueryAsync<ReunionAdministrativaResponse>(query, new
            {
                IdReunionAdmin = IdReunionAdmin
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ReunionAdministrativaResponse>> ObtenerReunionesAdministrativas()
        {
            string query = @"ReunionAdministrativa.SP_ObtenerReunionesAdministrativas";
            var result = await _conn.QueryAsync<ReunionAdministrativaResponse>(query);
            return result;
        }
    }
}
