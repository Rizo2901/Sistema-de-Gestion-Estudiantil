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

                // Elimina encabezado MIME si existe
                int index = base64.IndexOf("base64,");
                if (index >= 0)
                    base64 = base64.Substring(index + 7);

                byte[] archivoBytes = Convert.FromBase64String(base64);
                using var stream = new MemoryStream(archivoBytes);

                // Determinar extensión según tipo MIME
                string extension = documento.TipoArchivo switch
                {
                    "application/pdf" => "pdf",
                    "image/jpeg" => "jpg",
                    "image/png" => "png",
                    "application/msword" => "doc",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => "docx",
                    "application/vnd.ms-excel" => "xls",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "xlsx",
                    "text/plain" => "txt",
                    _ => "bin"
                };

                string nombreArchivo = $"documento_{Guid.NewGuid()}.{extension}";

                // ✅ Llamada corregida
                archivoUrl = await _archivoService.SubirDocumentoFirebaseAsync(stream, nombreArchivo);
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

       



        public async Task<IEnumerable<DocumentosResponse>> ObtenerTodosLosDocumentos()
        {
            string query = @"Documento.SP_ObtenerDocumentos";

            var result = await _conn.QueryAsync<DocumentosResponse>(query);

            return result;
        }

        

    }
}
