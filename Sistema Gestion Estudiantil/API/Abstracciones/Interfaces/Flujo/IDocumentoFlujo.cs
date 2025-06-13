using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IDocumentoFlujo
    {
        Task<Guid> CrearDocumento(DocumentosRequest documento);
        Task<Guid> EditarDocumento(Guid Id, DocumentosRequest documento);
        Task<Guid> EliminarDocumento(Guid Id);
        Task<DocumentosResponse> ObtenerDocumentoPorId(Guid Id);
        Task<IEnumerable<DocumentosResponse>> ObtenerDocumentosPorEstudiante(Guid IdEstudiante);
        Task<IEnumerable<DocumentosResponse>> ObtenerTodosLosDocumentos();

        Task<IEnumerable<EstudianteConDocumentosResponse>> ObtenerEstudiantesConDocumentos();
    }
}
