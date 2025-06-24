namespace Abstracciones.Interfaces.Flujo
{
    public interface IArchivoServiceFlujo
    {
        Task<string> SubirImagenFirebaseAsync(Stream fileStream, string fileName);
        Task<string> SubirDocumentoDriveAsync(Stream fileStream, string fileName);

    }
}
