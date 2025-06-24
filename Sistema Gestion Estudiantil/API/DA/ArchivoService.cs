using Abstracciones.Interfaces.DA;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;


namespace DA
{
    public class ArchivoService : IArchivoService
    {
        private readonly StorageClient _firebaseStorageClient;
        private readonly DriveService _driveService;
        private readonly string _firebaseBucket;
        private readonly string _driveFolderId;

        public ArchivoService(IConfiguration configuration)
        {
            _firebaseBucket = configuration["Firebase:StorageBucket"]
                ?? throw new ArgumentNullException("Firebase:StorageBucket no está definido.");
            _driveFolderId = configuration["GoogleDrive:FolderId"]
                ?? throw new ArgumentNullException("GoogleDrive:FolderId no está definido.");

            // Reconstruir el JSON de la cuenta de servicio de Google Drive con los nombres correctos
            var driveSection = configuration.GetSection("GoogleDrive");
            var serviceAccountJson = new
            {
                type = driveSection["Type"],
                project_id = driveSection["ProjectId"],
                private_key_id = driveSection["PrivateKeyId"],
                private_key = driveSection["PrivateKey"]?.Replace("\\n", "\n"),
                client_email = driveSection["ClientEmail"],
                client_id = driveSection["ClientId"],
                auth_uri = driveSection["AuthUri"],
                token_uri = driveSection["TokenUri"],
                auth_provider_x509_cert_url = driveSection["AuthProviderCertUrl"],
                client_x509_cert_url = driveSection["ClientCertUrl"]
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(serviceAccountJson);

            var credential = GoogleCredential.FromJson(json)
                .CreateScoped(DriveService.ScopeConstants.DriveFile);

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = credential
                });
            }

            _firebaseStorageClient = StorageClient.Create(credential);

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "TuAplicacion"
            });
        }


        public async Task<string> SubirImagenFirebaseAsync(Stream fileStream, string fileName)
        {
            var rutaConCarpeta = $"estudiantes_imagenes/{fileName}";
            var token = Guid.NewGuid().ToString();

            var objeto = new Google.Apis.Storage.v1.Data.Object
            {
                Bucket = _firebaseBucket,
                Name = rutaConCarpeta,
                ContentType = "image/jpeg", // Asegura que se trate como imagen
                Metadata = new Dictionary<string, string>
                {
                    { "firebaseStorageDownloadTokens", Guid.NewGuid().ToString() }
                }
            };

            await _firebaseStorageClient.UploadObjectAsync(
                objeto,
                fileStream,
                new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                }
            );

            var rutaPublica = $"https://firebasestorage.googleapis.com/v0/b/{_firebaseBucket}/o/{Uri.EscapeDataString(rutaConCarpeta)}?alt=media&token={token}";
            return rutaPublica;
        }

        public async Task<string> SubirDocumentoDriveAsync(Stream fileStream, string fileName)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                Parents = new List<string> { _driveFolderId }
            };

            var request = _driveService.Files.Create(fileMetadata, fileStream, "application/pdf");
            request.Fields = "id";

            var result = await request.UploadAsync();

            // 🔹 Verificar si la subida se completó correctamente
            if (result.Status != UploadStatus.Completed)
            {
                throw new Exception($"Error en la subida: {result.Exception?.Message}");
            }

            // 🔹 Verificar si la respuesta de Google Drive es válida
            if (request.ResponseBody == null)
            {
                throw new Exception("La respuesta de Google Drive es nula. Verifica la configuración de la API.");
            }

            return $"https://drive.google.com/file/d/{request.ResponseBody.Id}/view";
        }
    }
}