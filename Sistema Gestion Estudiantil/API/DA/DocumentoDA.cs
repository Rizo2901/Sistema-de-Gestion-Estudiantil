using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA.Repositorios;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace DA
{
    public class DocumentoDA : IDocumentoDA
    {
        private readonly SqlConnection _conn;
        private readonly IRepositorioDapper _repDapp;
        private IArchivoService _archivoService;

        public DocumentoDA(IRepositorioDapper repDapp, IArchivoService archivoService)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
            _archivoService = archivoService;
        }

        public async Task<Guid> CrearDocumento(DocumentosRequest documento)
        {
            string query = "Documento.SP_CrearDocumento";
            string archivoUrl = string.Empty;

            if (!string.IsNullOrEmpty(documento.Archivo))
            {
                string base64 = documento.Archivo;

                //  Elimina cualquier encabezado MIME (dinámicamente)
                int index = base64.IndexOf("base64,");
                if (index >= 0)
                    base64 = base64.Substring(index + 7);

                byte[] archivoBytes = Convert.FromBase64String(base64);
                using var stream = new MemoryStream(archivoBytes);

                //  Usa extensión según tipo MIME, opcional pero elegante
                string extension = documento.TipoArchivo switch
                {
                    "application/pdf" => "pdf",
                    "image/jpeg" => "jpg",
                    "image/png" => "png",
                    "application/msword" => "doc",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => "docx",
                    "application/vnd.ms-excel" => "xls",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "xlsx",
                    _ => "bin"
                };

                string nombreArchivo = $"documento_{Guid.NewGuid()}.{extension}";

                archivoUrl = await _archivoService.SubirImagenFirebaseAsync(stream, nombreArchivo);
            }

            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = documento.IdEstudiante,
                TipoDocumento = documento.TipoDocumento,
                Descripcion = documento.Descripcion,
                Archivo = archivoUrl,
                TipoArchivo = documento.TipoArchivo
            });

            return result;
        }


        public async Task<Guid> EditarDocumento(Guid id, DocumentosRequest documento)
        {
            string query = @"Documento.SP_EditarDocumento";
            string archivoUrl = documento.Archivo;

            // 🔹 Si contiene Base64 con encabezado, subir nuevo archivo
            if (!string.IsNullOrEmpty(documento.Archivo) &&
                documento.Archivo.StartsWith("data:application/pdf;base64,"))
            {
                string base64 = documento.Archivo.Substring(documento.Archivo.IndexOf(",") + 1);
                byte[] archivoBytes = Convert.FromBase64String(base64);
                using var stream = new MemoryStream(archivoBytes);

                archivoUrl = await _archivoService.SubirDocumentoFirebaseAsync(stream, $"documento_{Guid.NewGuid()}.pdf");
            }

            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdDocumento = id,
                TipoDocumento = documento.TipoDocumento,
                Descripcion = documento.Descripcion,
                Archivo = archivoUrl,
                TipoArchivo = documento.TipoArchivo
            });

            return result;
        }
        public async Task<Guid> EliminarDocumento(Guid id)
        {
            string query = @"Documento.SP_EliminarDocumento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdDocumento = id
            });
            return result;
        }



        public async Task<DocumentosResponse> ObtenerDocumentoPorId(Guid id)
        {
            string query = @"Documento.SP_ObtenerDocumentoPorId";

            var result = await _conn.QueryAsync<DocumentosResponse>(query, new
            {
                IdDocumento = id
            });

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<DocumentosResponse>> ObtenerDocumentosPorEstudiante(Guid idEstudiante)
        {
            string query = @"Documento.SP_Obtener_Documentos_Por_Estudiante";

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
                string query = @"Documento.SP_ObtenerEstudiantesConDocumentos";
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
            string query = @"Documento.SP_ObtenerDocumentos";

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
