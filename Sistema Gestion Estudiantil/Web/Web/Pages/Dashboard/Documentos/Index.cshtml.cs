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
            Console.WriteLine("===> Entró a OnPostCrearDocumentoAsync (Documentos)");

            Console.WriteLine($"ArchivoDocumento: {(ArchivoDocumento != null ? ArchivoDocumento.FileName : "null")}");
            Console.WriteLine($"documento.IdEstudiante: {documento?.IdEstudiante}");
            Console.WriteLine($"documento.TipoDocumento: {documento?.TipoDocumento}");
            Console.WriteLine($"documento.Descripcion: {documento?.Descripcion}");

            // Convertir archivo a base64 con prefijo 'data:'
            if (ArchivoDocumento != null && ArchivoDocumento.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await ArchivoDocumento.CopyToAsync(memoryStream);
                var base64 = Convert.ToBase64String(memoryStream.ToArray());
                documento.Archivo = $"data:{ArchivoDocumento.ContentType};base64,{base64}";
                documento.TipoArchivo = ArchivoDocumento.ContentType;

                Console.WriteLine($"Base64 inicio: {base64.Substring(0, Math.Min(30, base64.Length))}...");
                Console.WriteLine($"Base64 fin: ...{base64.Substring(Math.Max(0, base64.Length - 30))}");
                Console.WriteLine($"Tamaño base64: {base64.Length}");
            }
            else
            {
                Console.WriteLine("ArchivoDocumento es null o vacío");
                ModelState.AddModelError("ArchivoDocumento", "Debe subir un archivo.");
                await OnGetAsync();
                return Page();
            }

            // Limpiar ModelState
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("documento.") && k != nameof(ArchivoDocumento))
                .ToList();
            foreach (var key in keysToRemove)
                ModelState.Remove(key);

            if (ModelState.ContainsKey("documento.Archivo"))
                ModelState["documento.Archivo"].Errors.Clear();

            // Construir JSON
            var datos = new
            {
                tipoDocumento = documento.TipoDocumento,
                descripcion = documento.Descripcion,
                archivo = documento.Archivo,
                tipoArchivo = documento.TipoArchivo,
                idEstudiante = documento.IdEstudiante
            };
            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            Console.WriteLine("JSON enviado a la API:");
            Console.WriteLine(json);

            // Enviar POST a la API
            var endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearDocumento");
            Console.WriteLine($"Endpoint: {endpoint}");

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                Console.WriteLine("Enviando POST a la API...");
                var response = await client.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine("POST enviado, esperando respuesta...");
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Código de respuesta: {response.StatusCode}");
                Console.WriteLine($"Respuesta: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", $"Error al crear documento: {response.StatusCode}");
                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.GetType().Name} - {ex.Message}");
                ModelState.AddModelError("", $"Excepción al conectar con la API: {ex.Message}");
                await OnGetAsync();
                return Page();
            }

            Console.WriteLine("Documento creado correctamente.");
            return RedirectToPage("Index", new { filtro = "General" });
        }


        public async Task<IActionResult> OnPostEditarDocumentoAsync(Guid idDocumento)
        {
            Console.WriteLine("===> Entró a OnPostEditarDocumentoAsync (Documentos)");

            if (idDocumento == Guid.Empty)
            {
                Console.WriteLine("IdDocumento es inválido.");
                ModelState.AddModelError("", "El Id del documento es inválido.");
                return Page();
            }

            Console.WriteLine($"ArchivoDocumento: {(ArchivoDocumento != null ? ArchivoDocumento.FileName : "null")}");
            Console.WriteLine($"documento.IdEstudiante: {documento?.IdEstudiante}");
            Console.WriteLine($"documento.TipoDocumento: {documento?.TipoDocumento}");
            Console.WriteLine($"documento.Descripcion: {documento?.Descripcion}");

            // Si se subió un nuevo archivo
            if (ArchivoDocumento != null && ArchivoDocumento.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await ArchivoDocumento.CopyToAsync(memoryStream);
                var base64 = Convert.ToBase64String(memoryStream.ToArray());
                documento.Archivo = $"data:{ArchivoDocumento.ContentType};base64,{base64}";
                documento.TipoArchivo = ArchivoDocumento.ContentType;

                Console.WriteLine($"Base64 inicio: {base64.Substring(0, Math.Min(30, base64.Length))}...");
                Console.WriteLine($"Base64 fin: ...{base64.Substring(Math.Max(0, base64.Length - 30))}");
                Console.WriteLine($"Tamaño base64: {base64.Length}");
            }

            // Limpiar ModelState de campos no relevantes
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("documento.") && k != nameof(ArchivoDocumento))
                .ToList();
            foreach (var key in keysToRemove)
                ModelState.Remove(key);

            if (ModelState.ContainsKey("documento.Archivo"))
                ModelState["documento.Archivo"].Errors.Clear();

            if (string.IsNullOrEmpty(documento.Archivo))
            {
                Console.WriteLine("No hay archivo para enviar.");
                ModelState.AddModelError("ArchivoDocumento", "Debe subir o conservar un archivo.");
                await OnGetAsync();
                return Page();
            }

            // Construir JSON
            var datos = new
            {
                tipoDocumento = documento.TipoDocumento,
                descripcion = documento.Descripcion,
                archivo = documento.Archivo,
                tipoArchivo = documento.TipoArchivo,
                idEstudiante = documento.IdEstudiante
            };

            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Console.WriteLine("JSON enviado a la API:");
            Console.WriteLine(json);

            string endpoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "EditarDocumento"), idDocumento);
            Console.WriteLine($"Endpoint: {endpoint}");

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                Console.WriteLine("Enviando PUT a la API...");

                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = await client.PutAsync(endpoint, contenido);
                var responseContent = await respuesta.Content.ReadAsStringAsync();

                Console.WriteLine($"Código de respuesta: {respuesta.StatusCode}");
                Console.WriteLine($"Respuesta: {responseContent}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", $"Error al editar documento: {respuesta.StatusCode}");
                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.GetType().Name} - {ex.Message}");
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
                await OnGetAsync();
                return Page();
            }

            Console.WriteLine("Documento editado correctamente.");
            return RedirectToPage("Index", new { filtro = "General" });
        }

    }
}