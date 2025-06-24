using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Diagnostics;


namespace DA
{
    public class EstudianteDA : IEstudianteDA
    {
        private IRepositorioDapper _repDapp;
        private SqlConnection _conn;
        private IArchivoServiceFlujo _archivoServiceFlujo;
        public EstudianteDA(IRepositorioDapper repDapp, IArchivoServiceFlujo archivoServiceFlujo)
        {
            _archivoServiceFlujo = archivoServiceFlujo;
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

            // Detectar tipo de imagen y extensión
            string Fotografia = null;

            if (!string.IsNullOrEmpty(estudiante.Fotografia))
            {
                string base64 = estudiante.Fotografia;
                string extension = "jpg"; // valor por defecto

                // Si la cadena contiene encabezado, se procesa
                if (base64.StartsWith("data:image"))
                {
                    if (base64.StartsWith("data:image/png")) extension = "png";
                    else if (base64.StartsWith("data:image/webp")) extension = "webp";
                    else if (base64.StartsWith("data:image/gif")) extension = "gif";
                    else if (base64.StartsWith("data:image/jpeg")) extension = "jpg";

                    // Cortar encabezado ("data:image/...;base64,")
                    base64 = base64.Substring(base64.IndexOf(",") + 1);
                }

                var imagenBytes = Convert.FromBase64String(base64);
                var nombreArchivo = $"{Guid.NewGuid()}.{extension}";

                Fotografia = await _archivoServiceFlujo.SubirImagenFirebaseAsync(
                    new MemoryStream(imagenBytes),
                    nombreArchivo
                );
            }

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
                Fotografia = Fotografia, // Guarda la URL generada
                Direccion = estudiante.Direccion,
                TelefonoCelular = estudiante.TelefonoCelular,
                CorreoElectronico = estudiante.CorreoElectronico,
                Estado = estudiante.Estado,
                IdSeccion = estudiante.IdSeccion == Guid.Empty ? (Guid?)null : estudiante.IdSeccion,
                IdUsuario = estudiante.IdUsuario == Guid.Empty ? (Guid?)null : estudiante.IdUsuario
            });

            return result;
        }

        public async Task<Guid> EditarEstudiante(Guid Id, EstudiantesRequest estudiante)
        {
            string query = @"SP_ActualizarEstudiante";
            string Fotografia = estudiante.Fotografia; // Mantener la imagen existente

            // 🔹 Detectar si la imagen enviada es Base64 o ya es una URL pública
            if (!string.IsNullOrEmpty(estudiante.Fotografia) && estudiante.Fotografia.StartsWith("data:image"))
            {
                string base64 = estudiante.Fotografia;
                string extension = "jpg"; // Valor por defecto

                // 🔹 Determinar la extensión según el tipo MIME
                if (base64.StartsWith("data:image/png")) extension = "png";
                else if (base64.StartsWith("data:image/webp")) extension = "webp";
                else if (base64.StartsWith("data:image/gif")) extension = "gif";
                else if (base64.StartsWith("data:image/jpeg")) extension = "jpg";

                // 🔹 Extraer solo la parte Base64 y convertirla
                base64 = base64.Substring(base64.IndexOf(",") + 1);
                var imagenBytes = Convert.FromBase64String(base64);

                // 🔹 Crear el nombre del archivo sin duplicar la carpeta
                var nombreArchivo = $"{Guid.NewGuid()}.{extension}";

                // 🔹 Subir la imagen correctamente en `estudiantes_imagenes`
                Fotografia = await _archivoServiceFlujo.SubirImagenFirebaseAsync(new MemoryStream(imagenBytes), nombreArchivo);

                // 🔹 Imprimir la URL generada para verificar que se guarda correctamente
                Debug.WriteLine($"URL de imagen guardada en BD: {Fotografia}");
            }

            // 🔹 Ejecutar la actualización en la base de datos con la URL generada correctamente
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
                    Fotografia = Fotografia, // ✅ Guarda la URL correcta generada por Firebase
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
