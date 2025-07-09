using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class EstudianteAlergiaDA : IEstudianteAlergiaDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public EstudianteAlergiaDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }
        public async Task<Guid> CrearEstudianteAlergia(EstudianteAlergiaRequest estudianteAlergia)
        {
            string query = @"EstudianteAlergia.SP_CrearEstudianteAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = estudianteAlergia.IdEstudiante,
                IdAlergia = estudianteAlergia.IdAlergia,
                FechaDeteccion = estudianteAlergia.FechaDeteccion,
                Gravedad = estudianteAlergia.Gravedad,
                Observaciones = estudianteAlergia.Observaciones
            });
            return result;
        }

        public async Task<Guid> EditarEstudianteAlergia(Guid Id, EstudianteAlergiaRequest estudianteAlergia)
        {
             string query = @"EstudianteAlergia.SP_EditarEstudianteAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudianteAlergia = Id,
                FechaDeteccion = estudianteAlergia.FechaDeteccion,
                Gravedad = estudianteAlergia.Gravedad,
                Observaciones = estudianteAlergia.Observaciones
            });
            return result;
        }

        public async Task<Guid> EliminarEstudianteAlergia(Guid Id)
        {
            string query = @"EstudianteAlergia.SP_EliminarEstudianteAlergia";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudianteAlergia = Id,
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteConAlergiasResponse>> ObtenerAlergiasAgrupadasPorEstudiantes()
        {
            string query = @"EstudianteAlergia.SP_ObtenerAlergiasPorEstudiantes";

            var rawResult = await _conn.QueryAsync<EstudianteConAlergiasRaw>(query);

            var result = rawResult.Select(e => new EstudianteConAlergiasResponse
            {
                IdEstudiante = e.IdEstudiante,
                Cedula = e.Cedula,
                NombreCompleto = e.NombreCompleto,
                GradoActual = e.GradoActual,
                Genero = e.Genero,
                Alergias = string.IsNullOrWhiteSpace(e.Alergias)
                    ? new List<AlergiaItem>()
                    : System.Text.Json.JsonSerializer.Deserialize<List<AlergiaItem>>(e.Alergias,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            });

            return result;
        }

        public async Task<IEnumerable<EstudianteAlergiaResponse>> ObtenerAlergiasPorEstudiante(Guid IdEstudiante)
        {
            string query = @"EstudianteAlergia.SP_ObtenerAlergiasPorEstudiante";
            var result = await _conn.QueryAsync<EstudianteAlergiaResponse>(query, new
            {
                IdEstudiante= IdEstudiante,
            });
            return result;
        }

        public async Task<EstudianteAlergiaResponse> ObtenerEstudianteAlergiaPorId(Guid Id)
        {
            string query = @"EstudianteAlergia.SP_ObtenerEstudianteAlergiaPorId";
            var result = await _conn.QueryAsync<EstudianteAlergiaResponse>(query, new
            {
                IdEstudianteAlergia = Id,
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<EstudianteAlergiaResponse>> ObtenerTodasLasAlergiasEstudiantes()
        {
            string query = @"EstudianteAlergia.SP_ObtenerTodasLasAlergiasEstudiantes";
            var result = await _conn.QueryAsync<EstudianteAlergiaResponse>(query);
            return result;
        }
    }
}
