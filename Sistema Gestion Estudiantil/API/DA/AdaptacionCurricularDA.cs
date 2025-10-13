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
    public class AdaptacionCurricularDA : IAdaptacionCurricularDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;

        public AdaptacionCurricularDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }


        public async Task<Guid> CrearAdaptacionCurricular(AdaptacionCurricularRequest adaptacionCurricular)
        {
            string query = @"AdaptacionCurricular.Sp_CrearAdaptacionCurricular";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = adaptacionCurricular.IdEstudiante,
                Tipo = adaptacionCurricular.Tipo,
                AreaAfectada = adaptacionCurricular.AreaAfectada,
                FechaInicio = adaptacionCurricular.FechaInicio,
                FechaFin = adaptacionCurricular.FechaFin,
                Observaciones = adaptacionCurricular.Observaciones,
                Estado = adaptacionCurricular.Estado

            });
            return result;
        }

        public async Task<Guid> EditarAdaptacionCurricular(Guid Id, AdaptacionCurricularRequest adaptacionCurricular)
        {
            string query = @"AdaptacionCurricular.Sp_EditarAdaptacionCurricular";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAdaptacion = Id,
                IdEstudiante = adaptacionCurricular.IdEstudiante,
                Tipo = adaptacionCurricular.Tipo,
                AreaAfectada = adaptacionCurricular.AreaAfectada,
                FechaInicio = adaptacionCurricular.FechaInicio,
                FechaFin = adaptacionCurricular.FechaFin,
                Observaciones = adaptacionCurricular.Observaciones,
                Estado = adaptacionCurricular.Estado

            });
            return result;
        }

        public async Task<Guid> EliminarAdaptacionCurricular(Guid Id)
        {
            string query = @"AdaptacionCurricular.Sp_EliminarAdaptacionCurricular";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAdaptacion = Id,
            });
            return result;
        }

        public async Task<AdaptacionCurricularResponse> ObtenerAdaptacionCurricularPorId(Guid Id)
        {
            string query = @"AdaptacionCurricular.Sp_ObtenerAdaptacionCurricularPorId";
            var result = await _conn.QueryAsync<AdaptacionCurricularResponse>(query, new
            {
                IdAdaptacion = Id,
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<AdaptacionCurricularResponse>> ObtenerAdaptacionesCurriculares()
        {
            string query = @"AdaptacionCurricular.SP_ObtenerAdaptacionesCurriculares";
            var result = await _conn.QueryAsync<AdaptacionCurricularResponse>(query);
            return result;
        }

        public async Task<IEnumerable<AdaptacionCurricularResponse>> ObtenerAdaptacionesPorEstudiante(Guid IdEstudiante)
        {
            string query = @"AdaptacionCurricular.Sp_ObtenerAdaptacionCurricularPorEstudiante";
            var result = await _conn.QueryAsync<AdaptacionCurricularResponse>(query, new
            {
                IdEstudiante = IdEstudiante
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteConAdaptacionesResponse>> ObtenerAdaptacionesPorEstudiantes()
        {
            string query = @"AdaptacionCurricular.SP_ObtenerAdaptacionesPorEstudiantes";

            var rawResult = await _conn.QueryAsync<EstudianteConAdaptacionesRaw>(query);

            var result = rawResult.Select(e => new EstudianteConAdaptacionesResponse
            {
                IdEstudiante = e.IdEstudiante,
                Cedula = e.Cedula,
                NombreCompleto = e.NombreCompleto,
                GradoActual = e.GradoActual,
                Genero = e.Genero,
                Adaptaciones = string.IsNullOrWhiteSpace(e.Adaptaciones)
                    ? new List<AdaptacionItem>()
                    : System.Text.Json.JsonSerializer.Deserialize<List<AdaptacionItem>>(e.Adaptaciones,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            });

            return result;
        }
    }
}
