using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Documentos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;
        [BindProperty]
        public IFormFile ArchivoDocumento { get; set; }

        [BindProperty]
        public DocumentosRequest documento { get; set; }


        public IList<DocumentosResponse> Documentos { get; set; } = new List<DocumentosResponse>();

        [BindProperty]
        public IList<EstudiantesResponse> Estudiantes { get; set; } = new List<EstudiantesResponse>();

        [BindProperty]
        public IList<EstudianteConDocumentosResponse> EstudiantesConDocumentos { get; set; } = new List<EstudianteConDocumentosResponse>();

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task OnGetAsync(string filtro = "General", string IdDocumento = "", string IdEstudiante = "")
        {
            try
            {
                ViewData["Filtro"] = filtro;
                ViewData["IdDocumento"] = IdDocumento;
                ViewData["IdEstudiante"] = IdEstudiante;

                using var cliente = new HttpClient();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Obtener lista de estudiantes
                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
                var respuesta = await cliente.GetAsync(endPoint);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                Estudiantes = JsonSerializer.Deserialize<List<EstudiantesResponse>>(resultado, opciones) ?? new List<EstudiantesResponse>();

                Console.WriteLine($"[DEBUG] Estudiantes cargados: {Estudiantes.Count}");

                if (filtro == "ConLista")
                {
                    // Obtener estudiantes con documentos
                    endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantesConDocumentos");
                    respuesta = await cliente.GetAsync(endPoint);
                    respuesta.EnsureSuccessStatusCode();
                    resultado = await respuesta.Content.ReadAsStringAsync();
                    EstudiantesConDocumentos = JsonSerializer.Deserialize<List<EstudianteConDocumentosResponse>>(resultado, opciones) ?? new List<EstudianteConDocumentosResponse>();

                    Console.WriteLine($"[DEBUG] Estudiantes con documentos cargados: {EstudiantesConDocumentos.Count}");
                }
                else if (filtro == "PorDocumento" && !string.IsNullOrEmpty(IdDocumento))
                {
                    // Obtener un documento específico por ID
                    endPoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerDocumentosPorId"), IdDocumento);
                    Console.WriteLine($"[DEBUG] URL generada: {endPoint}");

                    respuesta = await cliente.GetAsync(endPoint);
                    if (!respuesta.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[ERROR] Fallo al obtener documento {IdDocumento}, Status Code: {respuesta.StatusCode}");
                        return;
                    }

                    resultado = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DEBUG] JSON recibido para documento {IdDocumento}: {resultado}");

                    Documentos = JsonSerializer.Deserialize<List<DocumentosResponse>>(resultado, opciones) ?? new List<DocumentosResponse>();

                    Console.WriteLine($"[DEBUG] Documentos cargados para ID {IdDocumento}: {Documentos.Count}");
                }
                else if (filtro == "PorEstudiante" && !string.IsNullOrEmpty(IdEstudiante))
                {
                    // Obtener documentos por estudiante
                    endPoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerDocumentosPorEstudiante"), IdEstudiante);
                    Console.WriteLine($"[DEBUG] URL generada: {endPoint}");

                    respuesta = await cliente.GetAsync(endPoint);
                    if (!respuesta.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[ERROR] Fallo al obtener documentos por estudiante {IdEstudiante}, Status Code: {respuesta.StatusCode}");
                        return;
                    }

                    resultado = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DEBUG] JSON recibido para estudiante {IdEstudiante}: {resultado}");

                    Documentos = JsonSerializer.Deserialize<List<DocumentosResponse>>(resultado, opciones) ?? new List<DocumentosResponse>();

                    Console.WriteLine($"[DEBUG] Documentos cargados para estudiante {IdEstudiante}: {Documentos.Count}");

                    var estudiante = Estudiantes.FirstOrDefault(e => e.IdEstudiante.ToString() == IdEstudiante);

                    if (estudiante != null)
                    {
                        ViewData["Nombre"] = estudiante.Nombre;
                        ViewData["Apellido1"] = estudiante.Apellido1;
                        ViewData["Apellido2"] = estudiante.Apellido2;

                        Console.WriteLine($"[DEBUG] Nombre Completo del Estudiante: {estudiante.Nombre} {estudiante.Apellido1} {estudiante.Apellido2}");
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR] No se encontró el estudiante con ID {IdEstudiante}");
                    }
                }
                else
                {
                    // Obtener lista completa de documentos
                    endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerDocumentos");
                    respuesta = await cliente.GetAsync(endPoint);
                    respuesta.EnsureSuccessStatusCode();
                    resultado = await respuesta.Content.ReadAsStringAsync();
                    Documentos = JsonSerializer.Deserialize<List<DocumentosResponse>>(resultado, opciones) ?? new List<DocumentosResponse>();

                    Console.WriteLine($"[DEBUG] Total documentos cargados: {Documentos.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo en laASDA carga de documentos: {ex.Message}");
                Documentos = new List<DocumentosResponse>();
                Estudiantes = new List<EstudiantesResponse>();
                EstudiantesConDocumentos = new List<EstudianteConDocumentosResponse>();
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(Guid? IdDocumento)
        {
            if (IdDocumento == null || IdDocumento == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarDocumento");
            string urlFinal = string.Format(endPoint, IdDocumento);
            Console.WriteLine($"[DEBUG] URL generada para eliminación: {urlFinal}");

            using var cliente = new HttpClient();
            using var solicitud = new HttpRequestMessage(HttpMethod.Delete, urlFinal);

            var respuesta = await cliente.SendAsync(solicitud);
            if (!respuesta.IsSuccessStatusCode)
            {
                string contenidoError = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine($"[ERROR] Respuesta de eliminación: {contenidoError}");
                return Page();
            }

            Console.WriteLine($"[DEBUG] Documento eliminado con ID: {IdDocumento}");
            return RedirectToPage(new { filtro = "General" });
        }


        public async Task<IActionResult> OnPostCrearDocumentoAsync()
        {
            Debug.WriteLine("Entró al handler OnPostCrearDocumentoAsync");

            // Procesar el archivo antes de validar ModelState
            if (ArchivoDocumento != null && ArchivoDocumento.Length > 0)
            {
                using var ms = new MemoryStream();
                await ArchivoDocumento.CopyToAsync(ms);
                var archivoBytes = ms.ToArray();

                //  Asignar el archivo en formato Base64 antes de enviar a la API
                documento.Archivo = Convert.ToBase64String(archivoBytes);
                documento.TipoArchivo = ArchivoDocumento.ContentType;
            }
            else
            {
                Debug.WriteLine("ArchivoDocumento es null o vacío");
                ModelState.AddModelError("ArchivoDocumento", "Debe subir un archivo.");
                await OnGetAsync();
                return Page();
            }

            Debug.WriteLine("Documento JSON antes de envío: " + JsonSerializer.Serialize(documento));

            string endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearDocumento");
            var contenido = new StringContent(JsonSerializer.Serialize(documento), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            var responseContent = await respuesta.Content.ReadAsStringAsync();
            Debug.WriteLine("Respuesta API: " + responseContent);

            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("Index", new { filtro = "General" });
        }

        public async Task<IActionResult> OnGetEditarDocumentoAsync(Guid idDocumento)
        {
            Debug.WriteLine("Entró al handler OnGetEditarDocumentoAsync");

            // Consumir el endpoint para obtener el documento por ID
            string endpoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerDocumentosPorId"), idDocumento);
            using var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();

            // Deserializar el documento (asume que es una lista con un solo elemento)
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var documentos = JsonSerializer.Deserialize<List<DocumentosResponse>>(resultado, opciones);
            var doc = documentos?.FirstOrDefault();

            if (doc == null)
            {
                ModelState.AddModelError("", "No se encontró el documento.");
                return Page();
            }

            // Asignar el archivo directamente (ya es string base64)
            documento = new DocumentosRequest
            {
                IdEstudiante = doc.IdEstudiante,
                TipoDocumento = doc.TipoDocumento,
                Descripcion = doc.Descripcion,
                Archivo = doc.Archivo ?? string.Empty,
                TipoArchivo = doc.TipoArchivo
            };

            // Aquí puedes cargar otros datos necesarios para la vista

            Debug.WriteLine("Documento preparado para edición.");
            return Page();
        }

        public async Task<IActionResult> OnPostEditarDocumentoAsync(Guid idDocumento)
        {
            try
            {
                Debug.WriteLine("Entró al handler OnPostEditarDocumentoAsync");

                if (idDocumento == Guid.Empty)
                {
                    Debug.WriteLine("? [ERROR] IdDocumento recibido es inválido.");
                    ModelState.AddModelError("", "El Id del documento es inválido.");
                    return Page();
                }

                var idEstudiante = documento.IdEstudiante;
                if (idEstudiante == Guid.Empty)
                {
                    Debug.WriteLine("? [ERROR] IdEstudiante recibido es inválido.");
                    ModelState.AddModelError("", "El Id del estudiante es obligatorio.");
                    return Page();
                }
                Debug.WriteLine($"? [DEBUG] IdEstudiante recibido correctamente: {idEstudiante}");

                // Procesar el archivo solo si se sube uno nuevo
                if (ArchivoDocumento != null && ArchivoDocumento.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await ArchivoDocumento.CopyToAsync(ms);
                    var archivoBytes = ms.ToArray();
                    documento.Archivo = Convert.ToBase64String(archivoBytes);
                    documento.TipoArchivo = ArchivoDocumento.ContentType;
                    Debug.WriteLine($"? [DEBUG] Archivo procesado correctamente. Tipo: {documento.TipoArchivo}");
                }
                // Si no se sube archivo nuevo, se mantiene el archivo actual (debe venir ya en documento.Archivo)

                // Limpiar ModelState de campos no relevantes y de los campos de archivo
                ModelState.Remove(nameof(ArchivoDocumento));
                ModelState.Remove("documento.Archivo");
                var keysToRemove = ModelState.Keys
                    .Where(k => !k.StartsWith("documento.", StringComparison.OrdinalIgnoreCase) && k != nameof(ArchivoDocumento))
                    .ToList();
                foreach (var key in keysToRemove)
                    ModelState.Remove(key);

                // Validar ModelState después de procesar el archivo
                if (!ModelState.IsValid)
                {
                    Debug.WriteLine("? [ERROR] ModelState tiene errores:");
                    foreach (var kvp in ModelState)
                    {
                        foreach (var error in kvp.Value.Errors)
                            Debug.WriteLine($"- ? {kvp.Key}: {error.ErrorMessage}");
                    }
                    await OnGetAsync();
                    return Page();
                }

                // Validar que el archivo exista antes de enviar a la API
                if (string.IsNullOrEmpty(documento.Archivo))
                {
                    Debug.WriteLine("? [ERROR] No hay archivo para enviar a la API.");
                    ModelState.AddModelError("ArchivoDocumento", "Debe existir un archivo para el documento.");
                    await OnGetAsync();
                    return Page();
                }

                // Construir JSON a enviar en camelCase
                var datos = new
                {
                    tipoDocumento = documento.TipoDocumento,
                    descripcion = documento.Descripcion,
                    archivo = documento.Archivo,
                    tipoArchivo = documento.TipoArchivo,
                    idEstudiante = idEstudiante
                };

                var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                Debug.WriteLine($"?? [DEBUG] JSON generado:\n{json}");

                string endpoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "EditarDocumento"), idDocumento);
                using var client = new HttpClient();
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                Debug.WriteLine("?? [DEBUG] Enviando solicitud PUT a la API...");
                var respuesta = await client.PutAsync(endpoint, contenido);
                var responseBody = await respuesta.Content.ReadAsStringAsync();

                Debug.WriteLine($"?? [DEBUG] Respuesta de la API: {respuesta.StatusCode}");
                Debug.WriteLine($"?? [DEBUG] Cuerpo de respuesta de la API:\n{responseBody}");

                respuesta.EnsureSuccessStatusCode();

                Debug.WriteLine("? [DEBUG] Documento editado con éxito.");

                return RedirectToPage("Index", new { filtro = "General" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"? [ERROR] Fallo al editar documento: {ex.Message}");
                ModelState.AddModelError("", "Ocurrió un error al editar el documento.");
                return Page();
            }
        }








    }
}