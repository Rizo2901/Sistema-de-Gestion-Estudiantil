using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;


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
            string query = @"SP_InsertarCondicionEspecial";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCondicion = Guid.NewGuid(),
                IdEstudiante = condicionEspecial.IdEstudiante,
                TipoCondicion = condicionEspecial.TipoCondicion,
                NombreCondicion = condicionEspecial.NombreCondicion,
                FechaDiagnostico = condicionEspecial.FechaDiagnostico,
                Observaciones = condicionEspecial.Observaciones,
            });
            return result;

        }

        public Task<Guid> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial)
        {
            string query = @"SP_ActualizarCondicionEspecial";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCondicion = Id,
                IdEstudiante = condicionEspecial.IdEstudiante,
                TipoCondicion = condicionEspecial.TipoCondicion,
                NombreCondicion = condicionEspecial.NombreCondicion,
                FechaDiagnostico = condicionEspecial.FechaDiagnostico,
                Observaciones = condicionEspecial.Observaciones,
            });
            return result;
        }

        public Task<Guid> EliminarCondicionEspecial(Guid Id)
        {
            string query = @"SP_EliminarCondicionEspecial";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdCondicion = Id
            });
            return result;
        }

        public Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionesEspeciales()
        {
            string query = @"SP_ObtenerCondicionesEspeciales";
            var result = _conn.QueryAsync<CondicionesEspecialesResponse>(query);
            return result;
        }

        public Task<CondicionesEspecialesResponse> ObtenerCondicionEspecial(Guid Id)
        {
            string query = @"SP_ObtenerCondicionEspecialPorId";
            var result = _conn.QuerySingleAsync<CondicionesEspecialesResponse>(query, new
            {
                IdCondicion = Id
            });
            return result;

        }

        public Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionEspecialPorEstudiante(Guid IdEstudiante)
        {
            string query = @"SP_ObtenerCondicionesEspecialesPorEstudiante";
            var result = _conn.QueryAsync<CondicionesEspecialesResponse>(query, new
            {
                IdEstudiante = IdEstudiante
            });
            return result;

        }

        public async Task<IEnumerable<EstudianteConCondicionesResponse>> ObtenerEstudiantesConCondiciones()
        {
            try
            {
                string query = "EXEC ObtenerEstudiantesConCondiciones";
                var result = await _conn.QueryAsync<EstudianteConCondicionesRaw>(query);

                foreach (var estudiante in result)
                {
                    Console.WriteLine($"[DEBUG] Datos JSON Condiciones para {estudiante.Nombre}: {estudiante.Condiciones ?? "NULL"}");
                }

                var estudiantesAgrupados = result.Select(e => new EstudianteConCondicionesResponse
                {
                    IdEstudiante = e.IdEstudiante,
                    Nombre = e.Nombre,
                    Apellido1 = e.Apellido1,
                    Apellido2 = e.Apellido2,
                    Condiciones = DebugDeserializarCondiciones(e.Condiciones)
                }).ToList();

                foreach (var estudiante in estudiantesAgrupados)
                {
                    Console.WriteLine($"[DEBUG] Estudiante: {estudiante.Nombre}, Condiciones Cargadas: {estudiante.Condiciones.Count}");
                }

                return estudiantesAgrupados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] No se pudo ejecutar el procedimiento almacenado: {ex.Message}");
                throw new Exception($"Error crítico al obtener estudiantes con condiciones: {ex.Message}", ex);
            }
        }
        // Función de depuración para deserializar JSON
        private List<CondicionesEspecialesResponse> DebugDeserializarCondiciones(string? json)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    Console.WriteLine($"[DEBUG] Intentando deserializar: {json}");
                    var condiciones = JsonConvert.DeserializeObject<List<CondicionesEspecialesResponse>>(json);
                    Console.WriteLine($"[DEBUG] Condiciones deserializadas correctamente: {condiciones?.Count}");
                    return condiciones ?? new List<CondicionesEspecialesResponse>();
                }
                else
                {
                    Console.WriteLine($"[DEBUG] JSON vacío o nulo.");
                    return new List<CondicionesEspecialesResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo en la deserialización: {ex.Message}");
                return new List<CondicionesEspecialesResponse>();
            }
        }
    }
}
