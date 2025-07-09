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
    public class CategoriaCondicionEspecialDA : ICategoriaCondicionEspecialDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public CategoriaCondicionEspecialDA( IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }
        public async Task<Guid> CrearCategoriaCondicionEspecial(CategoriasCondicionEspecialRequest categoriaCondicionEspecial)
        {
            string query = @"CondicionEspecial.SP_CrearCategoriaCondicionEspecial";
            var result =await  _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = categoriaCondicionEspecial.Nombre,
                Descripcion = categoriaCondicionEspecial.Descripcion
            });
            return result;
        }

        public async Task<Guid> EditarCategoriaCondicionEspecial(Guid Id, CategoriasCondicionEspecialRequest categoriaCondicionEspecial)
        {
            string query = @"CondicionEspecial.SP_EditarCategoriaCondicionEspecial";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoria = Id,
                Nombre = categoriaCondicionEspecial.Nombre,
                Descripcion = categoriaCondicionEspecial.Descripcion
            });
            return result;
        }

        public async Task<Guid> EliminarCategoriaCondicionEspecial(Guid Id)
        {
            string query = @"CondicionEspecial.SP_EliminarCategoriaCondicionEspecial";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoria = Id
            });
            return result;
        }

        public async Task<CategoriasCondicionEspecialResponse> ObtenerCategoriaCondicionEspecialPorId(Guid Id)
        {
            string query = @"CondicionEspecial.SP_ObtenerCategoriaCondicionEspecialPorId";
            var result = await _conn.QueryAsync<CategoriasCondicionEspecialResponse>(query, new
            {
                IdCategoria = Id
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CategoriasCondicionEspecialResponse>> ObtenerCategoriasCondicionEspeciales()
        {
            string query = @"CondicionEspecial.SP_ObtenerTodasCategoriasCondicionEspecial";
            var result = await _conn.QueryAsync<CategoriasCondicionEspecialResponse>(query);
            
            return result;
        }
    }
}
