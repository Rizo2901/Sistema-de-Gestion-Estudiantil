using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;


namespace Flujo
{
    public class ArchivoServiceFlujo : IArchivoServiceFlujo
    {
        private readonly IArchivoService _archivoService;

        public ArchivoServiceFlujo(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        public async Task<string> SubirDocumentoDriveAsync(Stream fileStream, string fileName)
        {
            return await _archivoService.SubirDocumentoDriveAsync(fileStream, fileName);
        }

        public async Task<string> SubirImagenFirebaseAsync(Stream fileStream, string fileName)
        {
            return await _archivoService.SubirImagenFirebaseAsync(fileStream, fileName);
        }
    }
}