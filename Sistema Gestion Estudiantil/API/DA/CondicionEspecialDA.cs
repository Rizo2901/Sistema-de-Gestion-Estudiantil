using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text.Json;
using System.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DA
{
    public class CondicionEspecialDA : ICondicionEspecialDA
    {
        private IRepositorioDapper _repDapp;
        private SqlConnection _conn;

        public CondicionEspecialDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial)
        {
            string query = @"CondicionEspecial.Sp_CrearCondicionEspecial";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = condicionEspecial.IdEstudiante,
                IdCategoria = condicionEspecial.IdCategoria,
                FechaDiagnostico = condicionEspecial.FechaDiagnostico,
                Observaciones = condicionEspecial.Observaciones
            });
            return result;

        }

        public Task<Guid> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial)
        {
            string query = @"CondicionEspecial.SP_ActualizarCondicionEspecial";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCondicion = Id,
                IdEstudiante = condicionEspecial.IdEstudiante,
                IdCategoria = condicionEspecial.IdCategoria,
                FechaDiagnostico = condicionEspecial.FechaDiagnostico,
                Observaciones = condicionEspecial.Observaciones
            });
            return result;
        }

        public Task<Guid> EliminarCondicionEspecial(Guid Id)
        {
            string query = @"CondicionEspecial.SP_EliminarCondicionEspecial";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCondicion = Id
            });
            return result;
        }

        public Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionesEspeciales()
        {
            string query = @"CondicionEspecial.SP_ObtenerCondicionesEspeciales";
            var result = _conn.QueryAsync<CondicionesEspecialesResponse>(query);
            return result;
        }

        public Task<CondicionesEspecialesResponse> ObtenerCondicionEspecial(Guid Id)
        {
            string query = @"CondicionEspecial.SP_ObtenerCondicionEspecialPorId";
            var result = _conn.QuerySingleAsync<CondicionesEspecialesResponse>(query, new
            {
                IdCondicion = Id
            });
            return result;

        }

        public async Task<IEnumerable<EstudianteConCondicionesResponse>> ObtenerCondicionEspecialPorEstudiantes()
        {
            string query = @"CondicionEspecial.SP_ObtenerCondicionesPorEstudiante";

            var rawResult = await _conn.QueryAsync<EstudianteConCondicionesRaw>(query);

            var resultadoFinal = rawResult.Select(e => new EstudianteConCondicionesResponse
            {
                IdEstudiante = e.IdEstudiante,
                NombreCompleto = e.NombreCompleto,
                Condiciones = string.IsNullOrWhiteSpace(e.Condiciones)
                    ? new List<CondicionEspecialItem>()
                    : JsonSerializer.Deserialize<List<CondicionEspecialItem>>(e.Condiciones)
            });

            return resultadoFinal;
        }


    }
}
