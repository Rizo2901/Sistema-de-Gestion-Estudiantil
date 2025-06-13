namespace Abstracciones.Interfaces.Flujo
{
    public interface IArchivoService
    {
        Task<string> SubirImagenFirebaseAsync(Stream fileStream, string fileName);
        Task<string> SubirDocumentoDriveAsync(Stream fileStream, string fileName);

    }
}
