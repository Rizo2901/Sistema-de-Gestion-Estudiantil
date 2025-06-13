using Abstracciones.Interfaces.Flujo;
using Google.Apis.Drive.v3;
using Google.Cloud.Storage.V1;


namespace Flujo
{
    public class ArchivoService : IArchivoService
    {
        private readonly StorageClient _firebaseStorageClient;
        private readonly DriveService _driveService;
        private readonly string _firebaseBucket = "tu-proyecto.appspot.com";
        private readonly string _driveFolderId = "ID_CARPETA_DRIVE";

        public Task<string> SubirDocumentoDriveAsync(Stream fileStream, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<string> SubirImagenFirebaseAsync(Stream fileStream, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
