using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text.Json;


namespace DA
{
    public class EstudianteDiscapacidadDA : IEstudianteDiscapacidadDA
    {
        private IRepositorioDapper _repDapp;
        private SqlConnection _conn;
        public EstudianteDiscapacidadDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }
        public async Task<Guid> CrearEstudianteDiscapacidad(EstudianteDiscapacidadRequest estudianteDiscapacidad)
        {
            string query = @"Estudiante_Discapacidad.Sp_CrearEstudianteDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = estudianteDiscapacidad.IdEstudiante,
                IdDiscapacidad = estudianteDiscapacidad.IdDiscapacidad,
                FechaDiagnostico = estudianteDiscapacidad.FechaDiagnostico,
                GradoAfectacion = estudianteDiscapacidad.GradoAfectacion,
                Observaciones = estudianteDiscapacidad.Observaciones
            });
            return result;
        }

        public async Task<Guid> EditarEstudianteDiscapacidad(Guid Id, EstudianteDiscapacidadRequest estudianteDiscapacidad)
        {
            string query = @"Estudiante_Discapacidad.Sp_EditarEstudianteDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudianteDiscapacidad = Id,
                FechaDiagnostico = estudianteDiscapacidad.FechaDiagnostico,
                GradoAfectacion = estudianteDiscapacidad.GradoAfectacion,
                Observaciones = estudianteDiscapacidad.Observaciones
            });
            return result;
        }

        public async Task<Guid> EliminarEstudianteDiscapacidad(Guid Id)
        {
            string query = @"Estudiante_Discapacidad.Sp_EliminarEstudianteDiscapacidad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudianteDiscapacidad = Id
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteDiscapacidadResponse>> ObtenerDiscapacidadesConEstudiantes()
        {
            string query = @"Estudiante_Discapacidad.Sp_ObtenerDiscapacidadesConEstudiantes";
            var result = await _conn.QueryAsync<EstudianteDiscapacidadResponse>(query);
            return result;
        }

        public async Task<IEnumerable<EstudianteDiscapacidadResponse>> ObtenerDiscapacidadesPorEstudiante(Guid idEstudiante)
        {
            string query = @"Estudiante_Discapacidad.SP_ObtenerDiscapacidadesPorEstudiante";
            var result = await _conn.QueryAsync<EstudianteDiscapacidadResponse>(query, new
            {
                IdEstudiante = idEstudiante
            });
            return result;
        }

        public async Task<EstudianteDiscapacidadResponse> ObtenerEstudianteDiscapacidadPorId(Guid Id)
        {
            string query = @"Estudiante_Discapacidad.Sp_ObtenerDiscapacidadPorId";
            var result = await _conn.QueryAsync<EstudianteDiscapacidadResponse>(query, new
            {
                IdEstudianteDiscapacidad = Id
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<EstudianteConDiscapacidadesResponse>> ObtenerResumenDiscapacidadesPorEstudiante()
        {
            string query = @"Estudiante_Discapacidad.SP_ObtenerResumenDiscapacidadesPorEstudiante";
            var rawResult = await _conn.QueryAsync<EstudianteConDiscapacidadesRaw>(query);

            var result = rawResult.Select(e => new EstudianteConDiscapacidadesResponse
            {
                IdEstudiante = e.IdEstudiante,
                Cedula = e.Cedula,
                NombreCompleto = e.NombreCompleto,
                GradoActual = e.GradoActual,
                Genero = e.Genero,
                Discapacidades = string.IsNullOrWhiteSpace(e.Discapacidades)
                    ? new List<DiscapacidadItem>()
                    : JsonSerializer.Deserialize<List<DiscapacidadItem>>(e.Discapacidades)
            });

            return result;


        }
    }
}
