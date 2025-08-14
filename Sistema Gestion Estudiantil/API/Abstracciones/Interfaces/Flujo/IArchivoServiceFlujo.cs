namespace Abstracciones.Interfaces.Flujo
{
    public interface IArchivoServiceFlujo
    {
        Task<string> SubirImagenFirebaseAsync(Stream fileStream, string fileName);
        Task<string> SubirDocumentoFirebaseAsync(Stream fileStream, string fileName);
    }
}
