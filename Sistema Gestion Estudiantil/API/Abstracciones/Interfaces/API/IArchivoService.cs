using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.API
{
    public interface IArchivoService
    {
        Task<IActionResult> SubirImagenFirebaseAsync(Stream fileStream, string fileName);
     
        Task<IActionResult> SubirDocumentoDriveAsync(Stream fileStream, string fileName);
        


    }
}
