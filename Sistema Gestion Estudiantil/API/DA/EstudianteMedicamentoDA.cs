using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text.Json;


namespace DA
{
    public class EstudianteMedicamentoDA : IEstudianteMedicamentoDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public EstudianteMedicamentoDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearEstudianteMedicamento(EstudianteMedicamentoResponse estudianteMedicamento)
        {
            string query = @"EstudianteMedicamento.Sp_CrearEstudianteMedicamento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                
                IdEstudiante = estudianteMedicamento.IdEstudiante,
                IdMedicamento = estudianteMedicamento.IdMedicamento,
                Dosis = estudianteMedicamento.Dosis,
                Frecuencia = estudianteMedicamento.Frecuencia,
                FechaInicio = estudianteMedicamento.FechaInicio,
                FechaFin = estudianteMedicamento.FechaFin,
                Observaciones = estudianteMedicamento.Observaciones
            });
            return result;
        }

        public async Task<Guid> EditarEstudianteMedicamento(Guid Id, EstudianteMedicamentoResponse estudianteMedicamento)
        {
            string query = @"EstudianteMedicamento.Sp_EditarEstudianteMedicamento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudianteMedicamento = Id,
                IdEstudiante = estudianteMedicamento.IdEstudiante,
                IdMedicamento = estudianteMedicamento.IdMedicamento,
                Dosis = estudianteMedicamento.Dosis,
                Frecuencia = estudianteMedicamento.Frecuencia,
                FechaInicio = estudianteMedicamento.FechaInicio,
                FechaFin = estudianteMedicamento.FechaFin,
                Observaciones = estudianteMedicamento.Observaciones
            });
            return result;
        }

        public async Task<Guid> EliminarEstudianteMedicamento(Guid Id)
        {
            string query = @"EstudianteMedicamento.Sp_EliminarEstudianteMedicamento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudianteMedicamento = Id
            });
            return result;
        }

        public async Task<EstudianteMedicamentoResponse> ObtenerEstudianteMedicamentoPorId(Guid Id)
        {
            string query = @"EstudianteMedicamento.Sp_ObtenerEstudianteMedicamentoPorId";
            var result = await _conn.QueryAsync<EstudianteMedicamentoResponse>(query, new
            {
                IdEstudianteMedicamento = Id
            });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<EstudianteMedicamentoResponse>> ObtenerEstudianteMedicamentos()
        {
            
            string query = @"EstudianteMedicamento.Sp_ObtenerEstudiantesMedicamentos";
            var result = await _conn.QueryAsync<EstudianteMedicamentoResponse>(query);
            
            return result;
        }

        public async Task<IEnumerable<EstudianteMedicamentoResponse>> ObtenerEstudianteMedicamentosPorEstudiante(Guid IdEstudiante)
        {
            string query = @"EstudianteMedicamento.Sp_ObtenerEstudianteMedicamentoPorEstudiante";
            var result = await _conn.QueryAsync<EstudianteMedicamentoResponse>(query, new
            {
                IdEsttudiante = IdEstudiante
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteConMedicamentosResponse>> ObtenerEstudianteMedicamentosPorEstudiantes()
        {
            string query = @"Estudiante_Discapacidad.SP_ObtenerMedicamentosPorEstudiantes";
            var rawResult = await _conn.QueryAsync<EstudianteConMedicamentosRaw>(query);

            var result = rawResult.Select(e => new EstudianteConMedicamentosResponse
            {
                IdEstudiante = e.IdEstudiante,
                Cedula = e.Cedula,
                NombreCompleto = e.NombreCompleto,
                GradoActual = e.GradoActual,
                Genero = e.Genero,
                Medicamentos = string.IsNullOrWhiteSpace(e.Medicamentos)
                    ? new List<MedicamentoItem>()
                    : JsonSerializer.Deserialize<List<MedicamentoItem>>(e.Medicamentos)
            });

            return result;
        }
    }
}
