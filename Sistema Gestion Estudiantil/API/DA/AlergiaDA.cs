using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DA
{
    public class AlergiaDA : IAlergiaDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public AlergiaDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }
        public async Task<Guid> CrearAlergia(AlergiaRequest alergia)
        {
            string quey = @"EstudianteAlergia.Sp_CrearAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(quey, new
            {
                IdCategoria = alergia.IdCategoria,
                Nombre = alergia.Nombre,
                Descripcion = alergia.Descripcion
            });
            return result;
        }

        public async Task<Guid> EditarAlergia(Guid IdAlergia, AlergiaRequest alergia)
        {
            string quey = @"EstudianteAlergia.Sp_EditarAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(quey, new
            {
                IdAlergia = IdAlergia,
                IdCategoria = alergia.IdCategoria,
                Nombre = alergia.Nombre,
                Descripcion = alergia.Descripcion
            });
            return result;
        }

        public async Task<Guid> EliminarAlergia(Guid IdAlergia)
        {
            string quey = @"EstudianteAlergia.Sp_EliminarAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(quey, new
            {
                IdAlergia = IdAlergia
            });
            return result;
        }

        public async Task<AlergiaResponse> ObtenerAlergiaPorId(Guid IdAlergia)
        {
            string quey = @"EstudianteAlergia.SP_ObtenerAlergiaPorId";
            var result = await _conn.QueryAsync<AlergiaResponse>(quey, new
            {
                IdAlergia = IdAlergia
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<AlergiaResponse>> ObtenerAlergias()
        {
            string quey = @"EstudianteAlergia.SP_ObtenerTodasAlergias";
            var result = await _conn.QueryAsync<AlergiaResponse>(quey);
            return result;
        }
    }
}
