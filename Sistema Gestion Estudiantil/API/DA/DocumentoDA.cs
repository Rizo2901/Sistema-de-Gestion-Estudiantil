using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using DA.Repositorios;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace DA
{
    public class DocumentoDA : IDocumentoDA
    {
        private readonly SqlConnection _conn;
        private readonly IRepositorioDapper _repDapp;

        public DocumentoDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearDocumento(DocumentosRequest documento)
        {
            // Convertir base64 a byte[]
            byte[] archivoBytes = string.IsNullOrEmpty(documento.Archivo)
                ? Array.Empty<byte>()
                : Convert.FromBase64String(documento.Archivo);

            string query = "SP_InsertarDocumento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = documento.IdEstudiante,
                TipoDocumento = documento.TipoDocumento,
                Descripcion = documento.Descripcion,
                Archivo = archivoBytes, // <-- Aquí va el byte[]
                TipoArchivo = documento.TipoArchivo
            });

            return result;
        }


        public async Task<Guid> EditarDocumento(Guid id, DocumentosRequest documento)
        {
            string query = "SP_EditarDocumento";

            // ✅ Convertir Base64 a byte[] si el archivo no es null
            byte[] archivoBytes = string.IsNullOrEmpty(documento.Archivo)
                ? Array.Empty<byte>()
                : Convert.FromBase64String(documento.Archivo);

            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdDocumento = id,  
                TipoDocumento = documento.TipoDocumento,
                Descripcion = documento.Descripcion,
                Archivo = archivoBytes,
                TipoArchivo = documento.TipoArchivo
            });

            return result;
        }

        public async Task<Guid> EliminarDocumento(Guid id)
        {
            string query = "SP_EliminarDocumento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdDocumento = id
            });
            return result;
        }



        public async Task<DocumentosResponse> ObtenerDocumentoPorId(Guid id)
        {
            string query = "SP_ObtenerDocumentoPorId";

            var result = await _conn.QueryAsync<DocumentosResponse>(query, new
            {
                IdDocumento = id
            });

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<DocumentosResponse>> ObtenerDocumentosPorEstudiante(Guid idEstudiante)
        {
            string query = "SP_Obtener_Documentos_Por_Estudiante";

            var result = await _conn.QueryAsync<DocumentosResponse>(query, new
            {
                IdEstudiante = idEstudiante
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteConDocumentosResponse>> ObtenerEstudiantesConDocumentos()
        {
            try
            {
                string query = "SP_ObtenerEstudiantesConDocumentos";
                var result = await _conn.QueryAsync<EstudianteConDocumentosRaw>(query);
                foreach (var estudiante in result)
                {
                    Console.WriteLine($"[DEBUG] Datos JSON Documentos para {estudiante.Nombre}: {estudiante.Documentos ?? "NULL"}");
                }

                var estudiantesAgrupados = result.Select(e => new EstudianteConDocumentosResponse
                {
                    IdEstudiante = e.IdEstudiante,
                    Nombre = e.Nombre,
                    Apellido1 = e.Apellido1,
                    Apellido2 = e.Apellido2,
                    Documentos = DebugDeserializarDocumentos(e.Documentos)
                }).ToList();

                foreach (var estudiante in estudiantesAgrupados)
                {
                    Console.WriteLine($"[DEBUG] Estudiante: {estudiante.Nombre}, Documentos Cargados: {estudiante.Documentos.Count}");
                }
                return estudiantesAgrupados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] No se pudo ejecutar el procedimiento almacenado: {ex.Message}");
                throw new Exception($"Error crítico al obtener estudiantes con condiciones: {ex.Message}", ex);
            }
        }



        public async Task<IEnumerable<DocumentosResponse>> ObtenerTodosLosDocumentos()
        {
            string query = "SP_ObtenerDocumentos";

            var result = await _conn.QueryAsync<DocumentosResponse>(query
            );

            return result;
        }

        private List<DocumentosResponse> DebugDeserializarDocumentos(string? json)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    Console.WriteLine($"[DEBUG] Intentando deserializar: {json}");
                    var documentos = JsonConvert.DeserializeObject<List<DocumentosResponse>>(json);
                    Console.WriteLine($"[DEBUG] Documentos deserializados correctamente: {documentos?.Count}");
                    return documentos ?? new List<DocumentosResponse>();
                }
                else
                {
                    Console.WriteLine($"[DEBUG] JSON vacío o nulo.");
                    return new List<DocumentosResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo en la deserialización: {ex.Message}");
                return new List<DocumentosResponse>();
            }
        }

    }
}
