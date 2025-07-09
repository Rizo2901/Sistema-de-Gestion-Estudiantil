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
    public class DiscapacidadDA : IDiscapacidadDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public DiscapacidadDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearDiscapacidad(DiscapacidadRequest discapacidad)
        {
            string query = @"Discapacidad.SP_CrearDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoria = discapacidad.IdCategoria,
                Nombre = discapacidad.Nombre,
                Descripcion = discapacidad.Descripcion,
            });
            return result;
        }

        public async Task<Guid> EditarDiscapacidad(Guid Id, DiscapacidadRequest discapacidad)
        {
            string query = @"Discapacidad.SP_EditarDiscapacidad";
            var result =  await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdDiscapacidad = Id,
                IdCategoria = discapacidad.IdCategoria,
                Nombre = discapacidad.Nombre,
                Descripcion = discapacidad.Descripcion,
            });
            return result;
        }

        public async Task<Guid> EliminarDiscapacidad(Guid Id)
        {
            string query = @"Discapacidad.SP_EliminarDiscapacidad";
            var result =  await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdDiscapacidad = Id
            });
            return result;

        }

        public  async Task<IEnumerable<DiscapacidadResponse>> ObtenerDiscapacidades()
        {
            string query = @"Discapacidad.SP_ObtenerTodasDiscapacidades";
            var result = await _conn.QueryAsync<DiscapacidadResponse>(query);
            return result;

        }

        

        public async Task<DiscapacidadResponse> ObtenerDiscapacidadPorId(Guid Id)
        {
            string query = @"Discapacidad.SP_ObtenerDiscapacidadPorId";
            var result = await _conn.QueryAsync<DiscapacidadResponse>(query, new
            {
                IdDiscapacidad = Id
            });
            return result.FirstOrDefault();
        }
    }
}
