using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;


namespace DA
{
    public class EstudianteDA : IEstudianteDA
    {
        private IRepositorioDapper _repDapp;
        private SqlConnection _conn;

        public EstudianteDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<EstudiantesResponse>> BuscarEstudiantes(string? nombre = null, string? cedula = null, string? grado = null, string? seccionNombre = null)
        {
            string query = "SP_BuscarEstudiantes";

            var parametros = new
            {
                Nombre = nombre,
                Cedula = cedula,
                Grado = grado,
                SeccionNombre = seccionNombre
            };

            var resultados = await _conn.QueryAsync<EstudiantesResponse>(
                query,
                parametros,
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultados;
        }


        public async Task<Guid> CrearEstudiante(EstudiantesRequest estudiante)
        {
            string query = @"SP_InsertarEstudiante";
            byte[] fotografiaBytes = string.IsNullOrEmpty(estudiante.Fotografia)
                ? Array.Empty<byte>()
                : Convert.FromBase64String(estudiante.Fotografia);

            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = Guid.NewGuid(),
                Cedula = estudiante.Cedula,
                Nombre = estudiante.Nombre,
                Apellido1 = estudiante.Apellido1,
                Apellido2 = estudiante.Apellido2,
                FechaNacimiento = estudiante.FechaNacimiento,
                Genero = estudiante.Genero,
                GradoActual = estudiante.GradoActual,
                Repitiendo = estudiante.Repitiendo,
                HistorialAcademico = estudiante.HistorialAcademico,
                Fotografia = fotografiaBytes,
                Direccion = estudiante.Direccion,
                TelefonoCelular = estudiante.TelefonoCelular,
                CorreoElectronico = estudiante.CorreoElectronico,
                Estado = estudiante.Estado,
                IdSeccion = estudiante.IdSeccion,
                IdUsuario = estudiante.IdUsuario
            });
            return result;
        }

        public async Task<Guid> EditarEstudiante(Guid Id, EstudiantesRequest estudiante)
        {
            string query = @"SP_ActualizarEstudiante";
            byte[] fotografiaBytes = string.IsNullOrEmpty(estudiante.Fotografia)
                ? Array.Empty<byte>()
                : Convert.FromBase64String(estudiante.Fotografia);

            var result = await _conn.ExecuteScalarAsync<Guid>(
                query,
                new
                {
                    IdEstudiante = Id,
                    Cedula = estudiante.Cedula,
                    Nombre = estudiante.Nombre,
                    Apellido1 = estudiante.Apellido1,
                    Apellido2 = estudiante.Apellido2,
                    FechaNacimiento = estudiante.FechaNacimiento,
                    Genero = estudiante.Genero,
                    GradoActual = estudiante.GradoActual,
                    Repitiendo = estudiante.Repitiendo,
                    HistorialAcademico = estudiante.HistorialAcademico,
                    Fotografia = fotografiaBytes,
                    Direccion = estudiante.Direccion,
                    TelefonoCelular = estudiante.TelefonoCelular,
                    CorreoElectronico = estudiante.CorreoElectronico,
                    Estado = estudiante.Estado,
                    IdSeccion = estudiante.IdSeccion == Guid.Empty ? (Guid?)null : estudiante.IdSeccion,
                    IdUsuario = estudiante.IdUsuario == Guid.Empty ? (Guid?)null : estudiante.IdUsuario
                },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return result;
        }


        public async Task<Guid> EliminarEstudiante(Guid Id)
        {
            await verificarEstudianteExiste(Id);
            string query = @"SP_EliminarEstudiante";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = Id
            });
            return result;
        }

        public async Task<EstudiantesResponse> ObtenerEstudiantePorId(Guid Id)
        {
            string query = @"SP_ObtenerEstudiantePorId";
            var result = await _conn.QueryAsync<EstudiantesResponse>(query, new
            {
                IdEstudiante = Id
            }); 
            return result.FirstOrDefault();
        }

        public Task<IEnumerable<EstudiantesResponse>> ObtenerEstudiantes()
        {
           string query = @"SP_ObtenerTodosLosEstudiantes";
            var result = _conn.QueryAsync<EstudiantesResponse>(query);
            return result;
        }
    


    private async Task verificarEstudianteExiste(Guid Id)
        {
            EstudiantesResponse? resultadoConsulta = await ObtenerEstudiantePorId(Id);
            if (resultadoConsulta == null)
            {
                throw new Exception("El estudiante no existe");
            }
        }
    }
    }
