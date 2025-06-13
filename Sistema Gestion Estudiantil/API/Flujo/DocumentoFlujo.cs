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

        public async Task<IEnumerable<EstudianteConDocumentosResponse>> ObtenerEstudiantesConDocumentos()
        {
            return await _documentoDA.ObtenerEstudiantesConDocumentos();
        }

        public async Task<IEnumerable<DocumentosResponse>> ObtenerTodosLosDocumentos()
        {
            return await _documentoDA.ObtenerTodosLosDocumentos();
        }
    }
}
