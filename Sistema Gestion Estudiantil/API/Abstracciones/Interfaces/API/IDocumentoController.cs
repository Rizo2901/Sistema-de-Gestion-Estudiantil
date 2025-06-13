using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IDocumentoController
    {
        Task<IActionResult> CrearDocumento(DocumentosRequest documento);
        Task<IActionResult> EditarDocumento(Guid Id, DocumentosRequest documento);
        Task<IActionResult> EliminarDocumento(Guid Id);
        Task<IActionResult> ObtenerDocumentoPorId(Guid Id);
        Task<IActionResult> ObtenerDocumentosPorEstudiante(Guid IdEstudiante);
        Task<IActionResult> ObtenerTodosLosDocumentos();

        Task<IActionResult> ObtenerEstudiantesConDocumentos();


    }
}
