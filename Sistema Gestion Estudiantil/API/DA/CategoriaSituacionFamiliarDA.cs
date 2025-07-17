using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DA
{
    public class CategoriaSituacionFamiliarDA : ICategoriaSituacionFamiliarDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;

        public CategoriaSituacionFamiliarDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearCategoriaSituacionFamiliar(CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar)
        {
            string query = @"SituacionFamiliar.Sp_CrearCategoriaSituacionFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = categoriaSituacionFamiliar.Nombre,
                Descripcion = categoriaSituacionFamiliar.Descripcion,

            });
            return result;
        }

        public async Task<Guid> EditarCategoriaSituacionFamiliar(Guid Id, CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar)
        {

            string query = @"SituacionFamiliar.Sp_EditarCategoriaSituacionFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoria = Id,
                Nombre = categoriaSituacionFamiliar.Nombre,
                Descripcion = categoriaSituacionFamiliar.Descripcion,

            });
            return result;
        }

        public async Task<Guid> EliminarCategoriaSituacionFamiliar(Guid Id)
        {
            string query = @"SituacionFamiliar.Sp_EliminarCategoriaSituacionFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCategoria = Id

            });
            return result;
        }

        public async Task<CategoriasSituacionFamiliarResponse> ObtenerCategoriaSituacionFamiliarPorId(Guid Id)
        {
            string query = @"SituacionFamiliar.Sp_ObtenerCategoriaSituacionFamiliarPorId";
            var result = await _conn.QueryAsync<CategoriasSituacionFamiliarResponse>(query, new
            {
                IdCategoria = Id

            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CategoriasSituacionFamiliarResponse>> ObtenerCategoriasSituacionFamiliar()
        {
            string query = @"SituacionFamiliar.SP_ObtenerTodasCategoriasSituacionFamiliar";
            var result = await _conn.QueryAsync<CategoriasSituacionFamiliarResponse>(query);
            return result;

        }
    }
}
