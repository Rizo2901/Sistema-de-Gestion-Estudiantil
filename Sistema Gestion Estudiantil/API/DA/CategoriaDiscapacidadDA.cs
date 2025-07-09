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
    public class CategoriaDiscapacidadDA : ICategoriaDiscapacidadDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public CategoriaDiscapacidadDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearCategoriaDiscapacidad(CategoriasDiscapacidadRequest categoriaDiscapacidad)
        {
            string query = @"CategoriaDiscapacidad.SP_CrearCategoriaDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = categoriaDiscapacidad.Nombre,
                Descripcion = categoriaDiscapacidad.Descripcion
            });
            return result;
        }

        public async Task<Guid> EditarCategoriaDiscapacidad(Guid Id, CategoriasDiscapacidadRequest categoriaDiscapacidad)
        {
            string query = @"CategoriaDiscapacidad.SP_EditarCategoriaDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoriaDiscapacidad = Id,
                Nombre = categoriaDiscapacidad.Nombre,
                Descripcion = categoriaDiscapacidad.Descripcion
            });
            return result;
        }

        public async Task<Guid> EliminarCategoriaDiscapacidad(Guid Id)
        {
            string query = @"CategoriaDiscapacidad.SP_EliminarCategoriaDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoriaDiscapacidad = Id
            });
            return result;
        }

        public async Task<CategoriasDiscapacidadResponse> ObtenerCategoriaDiscapacidadPorId(Guid Id)
        {
            string query = @"CategoriaDiscapacidad.SP_ObtenerCategoriaDiscapacidadPorId";
            var result = await _conn.QueryAsync<CategoriasDiscapacidadResponse>(query, new
            {
                IdCategoria = Id
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CategoriasDiscapacidadResponse>> ObtenerCategoriasDiscapacidad()
        {
            string query = @"CategoriaDiscapacidad.SP_ObtenerTodasCategoriasDiscapacidad";
            var result = await _conn.QueryAsync<CategoriasDiscapacidadResponse>(query);
            return result;
        }
    }
}
