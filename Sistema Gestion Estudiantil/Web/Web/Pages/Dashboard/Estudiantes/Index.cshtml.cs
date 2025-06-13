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

        public string? FiltroNombre { get; set; }
        public string? FiltroCedula { get; set; }  
        public string? FiltroGrado { get; set; }
        public string? FiltroSeccion { get; set; }


        public IList<EstudiantesResponse> Estudiantes { get; set; }
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

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }



        public async Task OnGetAsync(string? filtroNombre, string? filtroCedula, string? filtroGrado, string? filtroSeccion)
        {
            FiltroNombre = filtroNombre;
            FiltroCedula = filtroCedula;
            FiltroGrado = filtroGrado;
            FiltroSeccion = filtroSeccion;

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "BuscarEstudiantes");

            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(FiltroNombre))
                queryParams.Add($"nombre={Uri.EscapeDataString(FiltroNombre)}");
            if (!string.IsNullOrEmpty(FiltroCedula))
                queryParams.Add($"cedula={Uri.EscapeDataString(FiltroCedula)}");
            if (!string.IsNullOrEmpty(FiltroGrado))
                queryParams.Add($"grado={Uri.EscapeDataString(FiltroGrado)}");
            if (!string.IsNullOrEmpty(FiltroSeccion))
                queryParams.Add($"seccionNombre={Uri.EscapeDataString(FiltroSeccion)}");

            if (queryParams.Any())
                endpoint += "?" + string.Join("&", queryParams);

            var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Estudiantes = JsonSerializer.Deserialize<List<EstudiantesResponse>>(resultado, opciones);

            await ObtenerSecciones();
        }



        public async Task<IActionResult> OnPostCrearAsync()
        {
            // Elimina errores de ModelState que no sean de Estudiante, FotografiaArchivo o seccionseleccionada
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

            // Validar que se haya seleccionado una sección válida
            if (!Guid.TryParse(seccionseleccionada, out var idSeccion))
            {
                ModelState.AddModelError("seccionseleccionada", "Debe seleccionar una sección válida.");
                await ObtenerSecciones();
                return Page();
            }

            Estudiante.IdSeccion = idSeccion;

            // Procesar la fotografía si se subió
            if (FotografiaArchivo != null && FotografiaArchivo.Length > 0)
            {
                using var ms = new MemoryStream();
                await FotografiaArchivo.CopyToAsync(ms);
                Estudiante.Fotografia = Convert.ToBase64String(ms.ToArray());
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearEstudiante");
            var contenido = new StringContent(JsonSerializer.Serialize(Estudiante), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }




        public async Task<IActionResult> OnPostCrearDocumentoAsync()
        {
            Debug.WriteLine("Entró al handler OnPostCrearDocumentoAsync");

            // Elimina errores de ModelState que no sean de documento o ArchivoDocumento
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("documento.") && k != nameof(ArchivoDocumento))
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

            if (ArchivoDocumento == null || ArchivoDocumento.Length == 0)
            {
                Debug.WriteLine("ArchivoDocumento es null o vacío");
                ModelState.AddModelError("ArchivoDocumento", "Debe subir un archivo.");
                await ObtenerSecciones();
                return Page();
            }

            using var ms = new MemoryStream();
            await ArchivoDocumento.CopyToAsync(ms);
            var archivoBytes = ms.ToArray();

            documento.Archivo = Convert.ToBase64String(archivoBytes);
            documento.TipoArchivo = ArchivoDocumento.ContentType;

            Debug.WriteLine("Documento JSON: " + JsonSerializer.Serialize(documento));

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearDocumento");
            var contenido = new StringContent(JsonSerializer.Serialize(documento), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            var responseContent = await respuesta.Content.ReadAsStringAsync();
            Debug.WriteLine("Respuesta API: " + responseContent);

            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }





        public async Task<IActionResult> OnPostEditarAsync(Guid idEstudiante, EstudiantesRequest estudiante)
        {
            Debug.WriteLine("Entró al handler OnPostEditarAsync");

            // Limpieza de ModelState omitida por brevedad...

            if (estudiante.IdSeccion == Guid.Empty && !string.IsNullOrEmpty(seccionseleccionada) && Guid.TryParse(seccionseleccionada, out var idSeccion))
                estudiante.IdSeccion = idSeccion;

            if (FotografiaArchivo != null && FotografiaArchivo.Length > 0)
            {
                using var ms = new MemoryStream();
                await FotografiaArchivo.CopyToAsync(ms);
                estudiante.Fotografia = Convert.ToBase64String(ms.ToArray());
            }

            estudiante.IdEstudiante = idEstudiante;

            // Corrige el endpoint para que tenga el id real
            string endpoint = string.Format(_configuracion.ObtenerMetodo("ApiEndPoints", "EditarEstudiante"), idEstudiante);
            Debug.WriteLine($"Endpoint de edición: {endpoint}");

            var contenido = new StringContent(JsonSerializer.Serialize(estudiante), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PutAsync(endpoint, contenido);
            var responseContent = await respuesta.Content.ReadAsStringAsync();
            Debug.WriteLine($"Respuesta de la API: {responseContent}");
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage();
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

        public async Task OnPostBuscarAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");

            var queryParameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(FiltroNombre)) queryParameters.Add("nombre", FiltroNombre); 
            if (!string.IsNullOrEmpty(FiltroCedula)) queryParameters.Add("cedula", FiltroCedula);
            if (!string.IsNullOrEmpty(FiltroGrado)) queryParameters.Add("grado", FiltroGrado);
            if (!string.IsNullOrEmpty(FiltroSeccion)) queryParameters.Add("seccion", FiltroSeccion);

            string queryString = string.Join("&", queryParameters.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
            string url = string.IsNullOrWhiteSpace(queryString) ? endpoint : $"{endpoint}?{queryString}";

            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Estudiantes = JsonSerializer.Deserialize<List<EstudiantesResponse>>(json, options);

            await ObtenerSecciones();
        }


        public async Task<IActionResult> OnPostCrearCondicionAsync()
        {
            Debug.WriteLine("Entró al handler OnPostCrearCondicionAsync");

            // Elimina errores de ModelState que no sean de condicion
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("condicion.") && k != nameof(ArchivoDocumento))
                .ToList();

            foreach (var key in keysToRemove)
                ModelState.Remove(key);

            // Elimina el error de ArchivoDocumento si existe
            if (ModelState.ContainsKey(nameof(ArchivoDocumento)))
                ModelState.Remove(nameof(ArchivoDocumento));

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
                return Page();
            }

            try
            {
                if (string.IsNullOrWhiteSpace(condicion.TipoCondicion) || string.IsNullOrWhiteSpace(condicion.NombreCondicion))
                {
                    ModelState.AddModelError("", "Los campos requeridos no pueden estar vacíos.");
                    return Page();
                }

                // Generar GUID si es nulo
                condicion.IdCondicion ??= Guid.NewGuid();

                // Convertir GUID a string antes de serializar
                var datos = new
                {
                    IdCondicion = condicion.IdCondicion.ToString(),
                    IdEstudiante = condicion.IdEstudiante.ToString(),
                    TipoCondicion = condicion.TipoCondicion,
                    NombreCondicion = condicion.NombreCondicion,
                    FechaDiagnostico = condicion.FechaDiagnostico,
                    Observaciones = condicion.Observaciones
                };

                string jsonCondicionEspecial = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                Debug.WriteLine($"[DEBUG] JSON enviado:\n{jsonCondicionEspecial}");

                string endPoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearCondicion");
                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonCondicionEspecial, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(endPoint, contenido);
                var responseContent = await respuesta.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Respuesta API: {responseContent}");

                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Fallo al crear la condición: {ex.Message}");
                ModelState.AddModelError("", "Error al crear la condición especial.");
                return Page();
            }
        }




    }
}
