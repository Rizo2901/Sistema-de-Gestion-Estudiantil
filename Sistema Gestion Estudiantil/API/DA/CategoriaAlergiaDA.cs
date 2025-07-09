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
    public class CategoriaAlergiaDA : ICategoriaAlergiaDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public CategoriaAlergiaDA( IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }
        public async Task<Guid> CrearCategoriaAlergia(CategoriaAlergiaRequest categoriaAlergia)
        {
            string quey = @"EstudianteAlergia.Sp_CrearCategoriaAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(quey, new
            {
               Nombre = categoriaAlergia.Nombre,
                Descripcion = categoriaAlergia.Descripcion
            });
            return result;
        }

        public async Task<Guid> EditarCategoriaAlergia(Guid Id, CategoriaAlergiaRequest categoriaAlergia)
        {
            string quey = @"EstudianteAlergia.Sp_EditarCategoriaAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(quey, new
            {
                IdCategoria = Id,
                Nombre = categoriaAlergia.Nombre,
                Descripcion = categoriaAlergia.Descripcion
            });
            return result;
        }

        public async Task<Guid> EliminarCategoriaAlergia(Guid Id)
        {
            string quey = @"EstudianteAlergia.Sp_EliminarCategoriaAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(quey, new
            {
                IdCategoria = Id
            });
            return result;
        }

        public async Task<CategoriaAlergiaResponse> ObtenerCategoriaAlergiaPorId(Guid IdCategoria)
        {
            string quey = @"EstudianteAlergia.Sp_ObtenerCategoriaAlergiaPorId";
            var result = await _conn.QueryAsync<CategoriaAlergiaResponse>(quey, new
            {
                IdCategoria = IdCategoria
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CategoriaAlergiaResponse>> ObtenerCategoriasAlergia()
        {
            string quey = @"EstudianteAlergia.SP_ObtenerTodasCategoriasAlergia";
            var result = await _conn.QueryAsync<CategoriaAlergiaResponse>(quey);
           
            return result;
        }
    }
}
