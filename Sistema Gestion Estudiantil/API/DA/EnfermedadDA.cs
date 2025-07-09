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
    public class EnfermedadDA : IEnfermedadDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public EnfermedadDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearEnfermedad(EnfermedadRequest enfermedad)
        {
            string query = @"Enfermedad.SP_Crear";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = enfermedad.Nombre,
                Descripcion = enfermedad.Descripcion
            });
            return result;

        }

        public async Task<Guid> EditarEnfermedad(Guid Id, EnfermedadRequest enfermedad)
        {
            string query = @"Enfermedad.Sp_Editar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEnfermedad = Id,
                Nombre = enfermedad.Nombre,
                Descripcion = enfermedad.Descripcion

            });
            return result;
        }

        public async Task<Guid> EliminarEnfermedad(Guid Id)
        {
            string query = @"Enfermedad.Sp_Eliminar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEnfermedad = Id
            });
            return result;
        }

        public async Task<IEnumerable<EnfermedadResponse>> ObtenerEnfermedades()
        {
            string query = @"Enfermedad.Sp_ObtenerTodas";
            var result = await _conn.QueryAsync<EnfermedadResponse>(query);
            return result;
        }

        public async Task<EnfermedadResponse> ObtenerEnfermedadPorId(Guid Id)
        {
            string query = @"Enfermedad.Sp_ObtenerPorId";
            var result = await _conn.QueryAsync<EnfermedadResponse>(query);
            return result.FirstOrDefault();
        }
    }
}
