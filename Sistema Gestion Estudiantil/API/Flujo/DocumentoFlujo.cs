using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class DocumentoFlujo : IDocumentoFlujo
    {
        private IDocumentoDA _documentoDA;

        public DocumentoFlujo(IDocumentoDA documentoDA)
        {
            _documentoDA = documentoDA;
        }

        public async Task<Guid> CrearDocumento(DocumentosRequest documento)
        {
           return await _documentoDA.CrearDocumento(documento);
        }

        public async Task<Guid> EditarDocumento(Guid Id, DocumentosRequest documento)
        {
            return await _documentoDA.EditarDocumento(Id, documento);
        }

        public async Task<Guid> EliminarDocumento(Guid Id)
        {
            return await _documentoDA.EliminarDocumento(Id);
        }

        public async Task<DocumentosResponse> ObtenerDocumentoPorId(Guid Id)
        {
            return await _documentoDA.ObtenerDocumentoPorId(Id);
        }

        public async Task<IEnumerable<DocumentosResponse>> ObtenerDocumentosPorEstudiante(Guid IdEstudiante)
        {
            return await _documentoDA.ObtenerDocumentosPorEstudiante(IdEstudiante);
        }


        public async Task<IEnumerable<DocumentosResponse>> ObtenerTodosLosDocumentos()
        {
            return await _documentoDA.ObtenerTodosLosDocumentos();
        }

        public async Task<List<EstudianteConDocumentos>> ObtenerDocumentosAgrupados()
        {
            var documentosPlanos = await _documentoDA.ObtenerTodosLosDocumentos();

            var resultado = documentosPlanos
                .GroupBy(d => new
                {
                    d.IdEstudiante,
                    d.Cedula,
                    d.Nombre,
                    d.Apellido1,
                    d.Apellido2
                })
                .Select(gEst => new EstudianteConDocumentos
                {
                    IdEstudiante = gEst.Key.IdEstudiante,
                    Cedula = gEst.Key.Cedula?.Trim(),
                    Nombre = gEst.Key.Nombre?.Trim(),
                    Apellido1 = gEst.Key.Apellido1?.Trim(),
                    Apellido2 = gEst.Key.Apellido2?.Trim(),

                    Documentos = gEst
                        .Where(doc => doc.IdDocumento != Guid.Empty && !string.IsNullOrWhiteSpace(doc.TipoDocumento))
                        .Select(doc => new DocumentoDetalle
                        {
                            IdDocumento = doc.IdDocumento,
                            TipoDocumento = doc.TipoDocumento,
                            Descripcion = doc.Descripcion,
                            Archivo = doc.Archivo,
                            TipoArchivo = doc.TipoArchivo
                        })
                        .OrderBy(d => d.TipoDocumento)
                        .ToList()
                })
                .OrderBy(e => e.Apellido1)
                .ThenBy(e => e.Apellido2)
                .ToList();

            return resultado;
        }
    }
}
