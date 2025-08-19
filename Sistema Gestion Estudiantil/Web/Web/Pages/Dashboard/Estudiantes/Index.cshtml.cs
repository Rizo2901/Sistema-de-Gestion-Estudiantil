using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text;
using System.Text.Json;


namespace Web.Pages.Dashboard.Estudiantes
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;



        public IList<EstudiantesResumenResponse> Estudiantes { get; set; } = new List<EstudiantesResumenResponse>();
        public List<SelectListItem> Secciones { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public EstudiantesRequest Estudiante { get; set; }

        [BindProperty]
        public DocumentosRequest documento { get; set; }

        [BindProperty]
        public CondicionesEspecialesRequest condicion { get; set; }

        [BindProperty]
        public IFormFile ArchivoDocumento { get; set; }



        [BindProperty]
        public string seccionseleccionada { get; set; }

        [BindProperty]
        public IFormFile FotografiaArchivo { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IConfiguracion configuracion, ILogger<IndexModel> logger)
        {
            _configuracion = configuracion;
            _logger = logger;
        }



        public async Task OnGetAsync()
        {
            // Obtener endpoint desde configuración
            string endPoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantesResumen");

            if (string.IsNullOrWhiteSpace(endPoint))
            {
                _logger.LogError("[Estudiantes] Endpoint no configurado correctamente.");
                Estudiantes = new List<EstudiantesResumenResponse>();
                return;
            }

            using var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(endPoint);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("[Estudiantes] Error HTTP {StatusCode} al acceder a {Url}", response.StatusCode, endPoint);
                    Estudiantes = new List<EstudiantesResumenResponse>();
                    return;
                }

                var result = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("[Estudiantes] JSON recibido: {Json}", result);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Estudiantes = JsonSerializer.Deserialize<List<EstudiantesResumenResponse>>(result, options) ?? new List<EstudiantesResumenResponse>();

                _logger.LogDebug("[Estudiantes] Estudiantes cargados: {Count}", Estudiantes.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Estudiantes] Excepción al obtener estudiantes desde {Url}", endPoint);
                Estudiantes = new List<EstudiantesResumenResponse>();
            }

            // Inicialización defensiva de modelos vinculados
            Estudiante ??= new EstudiantesRequest();
            documento ??= new DocumentosRequest();
            condicion ??= new CondicionesEspecialesRequest();

            await ObtenerSecciones();
        }

        public async Task<IActionResult> OnPostCrearAsync()
        {
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("Estudiante.") && k != nameof(FotografiaArchivo) && k != nameof(seccionseleccionada))
                .ToList();
            foreach (var key in keysToRemove)
                ModelState.Remove(key);

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Debug.WriteLine($"ModelState error for '{key}': {error.ErrorMessage}");
                    }
                }

                await ObtenerSecciones();
                return Page();
            }

            // Validar sección
            if (!Guid.TryParse(seccionseleccionada, out var idSeccion))
            {
                ModelState.AddModelError("seccionseleccionada", "Debe seleccionar una sección válida.");
                await ObtenerSecciones();
                return Page();
            }

            Estudiante.IdSeccion = idSeccion;

            // Convertir imagen a Base64 dentro del método si el usuario subió una
            if (FotografiaArchivo != null && FotografiaArchivo.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await FotografiaArchivo.CopyToAsync(memoryStream);
                Estudiante.Fotografia = Convert.ToBase64String(memoryStream.ToArray());
            }
            else
            {
                Estudiante.Fotografia = null; // Si no hay imagen, enviamos null
            }

            // Enviar al API
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearEstudiante");
            var contenido = new StringContent(JsonSerializer.Serialize(Estudiante), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostCrearDocumentoAsync()
        {
            Console.WriteLine("===> Entró a OnPostCrearDocumentoAsync");

            // Mostrar valores recibidos para depuración
            Console.WriteLine($"ArchivoDocumento: {(ArchivoDocumento != null ? ArchivoDocumento.FileName : "null")}");
            Console.WriteLine($"documento.IdEstudiante: {documento?.IdEstudiante}");
            Console.WriteLine($"documento.TipoDocumento: {documento?.TipoDocumento}");
            Console.WriteLine($"documento.Descripcion: {documento?.Descripcion}");

            // Conversión base64 igual que en OnPostCrearAsync (fotografía)
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
            }

            // LIMPIA ModelState de TODO lo que no sea documento o ArchivoDocumento
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("documento.") && k != nameof(ArchivoDocumento))
                .ToList();
            foreach (var key in keysToRemove)
                ModelState.Remove(key);

            // LIMPIA el error de ModelState para documento.Archivo si existe
            if (ModelState.ContainsKey("documento.Archivo"))
                ModelState["documento.Archivo"].Errors.Clear();

          

            // Construir el JSON
            var datos = new
            {
                tipoDocumento = documento.TipoDocumento,
                descripcion = documento.Descripcion,
                archivo = documento.Archivo,
                tipoArchivo = documento.TipoArchivo,
                idEstudiante = documento.IdEstudiante
            };
            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            // Log para depuración
            Console.WriteLine("JSON enviado a la API:");
            Console.WriteLine(json);

            // Endpoint
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearDocumento");
            Console.WriteLine($"Endpoint: {endpoint}");

            // Enviar a la API con manejo de timeout y excepciones
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10); // Timeout corto para depuración
                Console.WriteLine("Enviando POST a la API...");
                var response = await client.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine("POST enviado, esperando respuesta...");
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Código de respuesta: {response.StatusCode}");
                Console.WriteLine($"Respuesta: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", $"Error al crear documento: {response.StatusCode}");
                    await ObtenerSecciones();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.GetType().Name} - {ex.Message}");
                ModelState.AddModelError("", $"Excepción al conectar con la API: {ex.Message}");
                await ObtenerSecciones();
                return Page();
            }

            Console.WriteLine("Documento creado correctamente.");
            return RedirectToPage();
        }




        public async Task<IActionResult> OnPostEditarAsync(Guid idEstudiante, EstudiantesRequest estudiante)
        {
            Debug.WriteLine("Entró al handler OnPostEditarAsync");

            try
            {
                //  Limpiar ModelState para evitar validaciones innecesarias
                ModelState.Remove(nameof(FotografiaArchivo));
                ModelState.Remove(nameof(seccionseleccionada));

                var keysToRemove = ModelState.Keys
                    .Where(k => !k.StartsWith("Estudiante.") && k != nameof(FotografiaArchivo) && k != nameof(seccionseleccionada))
                    .ToList();

                foreach (var key in keysToRemove)
                    ModelState.Remove(key);

                //  Validar ModelState antes de continuar
                if (!ModelState.IsValid)
                {
                    foreach (var key in ModelState.Keys)
                    {
                        foreach (var error in ModelState[key].Errors)
                        {
                            Debug.WriteLine($"ModelState error for '{key}': {error.ErrorMessage}");
                        }
                    }
                    await ObtenerSecciones();
                    return Page();
                }

                estudiante.IdEstudiante = idEstudiante;

                // Mantener la sección actual si no se selecciona una nueva
                if (estudiante.IdSeccion == Guid.Empty && !string.IsNullOrEmpty(seccionseleccionada) && Guid.TryParse(seccionseleccionada, out var idSeccion))
                {
                    estudiante.IdSeccion = idSeccion;
                }
                else if (estudiante.IdSeccion == Guid.Empty)
                {
                    estudiante.IdSeccion = await ObtenerSeccionActual(idEstudiante);
                }

                // Si el usuario sube una nueva imagen, convertirla a Base64
                if (FotografiaArchivo != null && FotografiaArchivo.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await FotografiaArchivo.CopyToAsync(ms);
                    estudiante.Fotografia = $"data:image/jpeg;base64,{Convert.ToBase64String(ms.ToArray())}"; // Asegurar formato correcto
                }
                else
                {
                    //  Si no se sube una nueva imagen, mantener la URL actual
                    estudiante.Fotografia = await ObtenerImagenActual(idEstudiante);
                }

                // Imprimir la imagen enviada a la API para depuración
                Debug.WriteLine($"Imagen enviada a la API: {estudiante.Fotografia}");

                string endpoint = string.Format(_configuracion.ObtenerMetodo("ApiEndPoints", "EditarEstudiante"), idEstudiante);
                Debug.WriteLine($"Endpoint de edición: {endpoint}");
                Debug.WriteLine($"Datos enviados a la API: {JsonSerializer.Serialize(estudiante)}");

                var contenido = new StringContent(JsonSerializer.Serialize(estudiante), Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                var respuesta = await client.PutAsync(endpoint, contenido);
                var responseContent = await respuesta.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la API: {responseContent}");

                respuesta.EnsureSuccessStatusCode();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en OnPostEditarAsync: {ex.Message}");
                return Page();
            }
        }


        public async Task<IActionResult> OnPostEliminarAsync(Guid idEstudiante)
        {
            if (idEstudiante == Guid.Empty)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarEstudiante");
            using var client = new HttpClient();
            var respuesta = await client.DeleteAsync(string.Format(endpoint, idEstudiante));
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage();
        }

        public async Task ObtenerSecciones()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endpointSecciones = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSecciones");
            var respuestaSecciones = await client.GetAsync(endpointSecciones);
            respuestaSecciones.EnsureSuccessStatusCode();
            var resultadoSecciones = await respuestaSecciones.Content.ReadAsStringAsync();
            var seccionesResponse = JsonSerializer.Deserialize<List<SeccionesResponse>>(resultadoSecciones, opciones);

            Secciones = seccionesResponse.Select(s => new SelectListItem
            {
                Value = s.IdSeccion.ToString(),
                Text = s.Nombre
            }).ToList();
        }

       
        //public async Task<IActionResult> OnPostCrearCondicionAsync()
        //{
        //    Debug.WriteLine("Entró al handler OnPostCrearCondicionAsync");

        //    // Elimina errores de ModelState que no sean de condicion
        //    var keysToRemove = ModelState.Keys
        //        .Where(k => !k.StartsWith("condicion.") && k != nameof(ArchivoDocumento))
        //        .ToList();

        //    foreach (var key in keysToRemove)
        //        ModelState.Remove(key);

        //    // Elimina el error de ArchivoDocumento si existe
        //    if (ModelState.ContainsKey(nameof(ArchivoDocumento)))
        //        ModelState.Remove(nameof(ArchivoDocumento));

        //    if (!ModelState.IsValid)
        //    {
        //        foreach (var key in ModelState.Keys)
        //        {
        //            var errors = ModelState[key].Errors;
        //            foreach (var error in errors)
        //            {
        //                Debug.WriteLine($"ModelState error for '{key}': {error.ErrorMessage}");
        //            }
        //        }
        //        return Page();
        //    }

        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(condicion.TipoCondicion) || string.IsNullOrWhiteSpace(condicion.NombreCondicion))
        //        {
        //            ModelState.AddModelError("", "Los campos requeridos no pueden estar vacíos.");
        //            return Page();
        //        }

        //        // Generar GUID si es nulo
        //        condicion.IdCondicion ??= Guid.NewGuid();

        //        // Convertir GUID a string antes de serializar
        //        var datos = new
        //        {
        //            IdCondicion = condicion.IdCondicion.ToString(),
        //            IdEstudiante = condicion.IdEstudiante.ToString(),
        //            TipoCondicion = condicion.TipoCondicion,
        //            NombreCondicion = condicion.NombreCondicion,
        //            FechaDiagnostico = condicion.FechaDiagnostico,
        //            Observaciones = condicion.Observaciones
        //        };

        //        string jsonCondicionEspecial = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
        //        Debug.WriteLine($"[DEBUG] JSON enviado:\n{jsonCondicionEspecial}");

        //        string endPoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearCondicion");
        //        using var cliente = new HttpClient();
        //        var contenido = new StringContent(jsonCondicionEspecial, Encoding.UTF8, "application/json");

        //        var respuesta = await cliente.PostAsync(endPoint, contenido);
        //        var responseContent = await respuesta.Content.ReadAsStringAsync();
        //        Debug.WriteLine($"[DEBUG] Respuesta API: {responseContent}");

        //        respuesta.EnsureSuccessStatusCode();

        //        return RedirectToPage();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"[ERROR] Fallo al crear la condición: {ex.Message}");
        //        ModelState.AddModelError("", "Error al crear la condición especial.");
        //        return Page();
        //    }
        //}

        #region Helpers
        private async Task<string> ObtenerImagenActual(Guid idEstudiante)
        {
            string endpoint = string.Format(_configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiante"), idEstudiante);

            using var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var estudiante = JsonSerializer.Deserialize<EstudiantesResponse>(resultado, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return estudiante?.Fotografia ?? "/img/user-default.png"; // Si no tiene imagen, usar una por defecto
        }

        private async Task<Guid> ObtenerSeccionActual(Guid idEstudiante)
        {
            string endpoint = string.Format(_configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiante"), idEstudiante);

            using var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var estudiante = JsonSerializer.Deserialize<EstudiantesResponse>(resultado, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return estudiante?.IdSeccion ?? Guid.Empty;
        }
        #endregion

    }
}
