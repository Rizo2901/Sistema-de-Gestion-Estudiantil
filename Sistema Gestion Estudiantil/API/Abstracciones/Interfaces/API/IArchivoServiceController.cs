using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IArchivoServiceController
    {
        Task<IActionResult> SubirImagenFirebaseAsync(IFormFile file);
        Task<IActionResult> SubirDocumentoFirebaseAsync(IFormFile file);
    }
}