using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class SituacionFamiliarDA : ISituacionFamiliarDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public SituacionFamiliarDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearSituacionFamiliar(SituacionFamiliarRequest request)
        {
            string query = @"SituacionFamiliar.SP_CrearSituacionFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = request.IdEstudiante,
                IdCategoria = request.IdCategoria,
                Descripcion = request.Descripcion,
                FechaRegistro = request.FechaRegistro
            });
            return result;
        }

        public async Task<Guid> EditarSituacionFamiliar(Guid Id, SituacionFamiliarRequest request)
        {
            string query = @"SituacionFamiliar.SP_CrearSituacionFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSituacion = Id,
                IdEstudiante = request.IdEstudiante,
                IdCategoria = request.IdCategoria,
                Descripcion = request.Descripcion,
                FechaRegistro = request.FechaRegistro
            });
            return result;
        }

        public async Task<Guid> EliminarSituacionFamiliar(Guid Id)
        {
            string query = @"SituacionFamiliar.SP_EliminarSituacionFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSituacion = Id
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteConSituacionesResponse>> ObtenerEstudiantesConSituacionesFamiliar()
        {
            string query = @"SituacionFamiliar.SP_ObtenerSituacionesFamiliaresPorEstudiantes";

            var rawResult = await _conn.QueryAsync<EstudianteConSituacionesRaw>(query);

            var result = rawResult.Select(e => new EstudianteConSituacionesResponse
            {
                IdEstudiante = e.IdEstudiante,
                Cedula = e.Cedula,
                NombreCompleto = e.NombreCompleto,
                GradoActual = e.GradoActual,
                Genero = e.Genero,
                Situaciones = string.IsNullOrWhiteSpace(e.Situaciones)
                    ? new List<SituacionItem>()
                    : System.Text.Json.JsonSerializer.Deserialize<List<SituacionItem>>(e.Situaciones,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            });

            return result;

        }

        public async Task<IEnumerable<SituacionFamiliarResponse>> ObtenerSituacionesFamiliares()
        {
            string query = @"SituacionFamiliar.SP_ObtenerSituacionesFamiliares";
            var result = await _conn.QueryAsync<SituacionFamiliarResponse>(query);
           
            return result;
        }

        public async Task<IEnumerable<SituacionFamiliarResponse>> ObtenerSituacionesFamiliaresPorEstudiante(Guid IdEstudiante)
        {
            string query = @"SituacionFamiliar.SP_CrearSituacionFamiliar";
            var result = await _conn.QueryAsync<SituacionFamiliarResponse>(query, new
            {
                IdEstudiante = IdEstudiante
            });
            return result;
        }

        public async Task<SituacionFamiliarResponse> ObtenerSituacionFamiliarPorId(Guid Id)
        {
            string query = @"SituacionFamiliar.SP_CrearSituacionFamiliar";
            var result = await _conn.QueryAsync<SituacionFamiliarResponse>(query, new
            {
                Id = Id
            });
            return result.FirstOrDefault();

        }
    }
}
