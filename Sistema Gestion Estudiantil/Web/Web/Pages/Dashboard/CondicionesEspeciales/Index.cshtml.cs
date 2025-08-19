using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.CondicionesEspeciales
{
    public class IndexModel : PageModel
    {
        public List<SelectListItem> Estudiantes { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string estudianteSeleccionado { get; set; }


        public List<SelectListItem> Categorias { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string categoriaSeleccionada { get; set; }

        private readonly IConfiguracion _confg;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public IList<EstudianteConCondicionesResponse> EstudiantesConCondiciones { get; set; } = new List<EstudianteConCondicionesResponse>();

        [BindProperty]
        public CondicionesEspecialesRequest condicion { get; set; }
        public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public async Task OnGet()
        {

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerCondicionesEstudiantes");
            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            EstudiantesConCondiciones = JsonSerializer.Deserialize<List<EstudianteConCondicionesResponse>>(resultado, opciones);
            await ObtenerEstudiantes();
            await ObtenerCategorias();



        }

        public async Task ObtenerEstudiantes()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endpointEstudiantes = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
            var respuestaEstudiantes = await client.GetAsync(endpointEstudiantes);
            respuestaEstudiantes.EnsureSuccessStatusCode();
            var resultadoEstudiantes = await respuestaEstudiantes.Content.ReadAsStringAsync();
            var estudiantesResponse = JsonSerializer.Deserialize<List<EstudiantesResponse>>(resultadoEstudiantes, opciones);

            Estudiantes = estudiantesResponse.Select(s => new SelectListItem
            {
                Value = s.IdEstudiante.ToString(),
                Text = $"{s.Nombre} {s.Apellido1} {s.Apellido2}"
            }).ToList();

        }


        public async Task ObtenerCategorias()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endpointCategorias = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerCategoriasCondiciones");
            var respuestaCategorias = await client.GetAsync(endpointCategorias);
            respuestaCategorias.EnsureSuccessStatusCode();
            var resultadoCategorias = await respuestaCategorias.Content.ReadAsStringAsync();
            var categoriasResponse = JsonSerializer.Deserialize<List<CategoriasCondicionEspecialResponse>>(resultadoCategorias, opciones);

            Categorias = categoriasResponse.Select(s => new SelectListItem
            {
                Value = s.IdCategoria.ToString(),
                Text = $"{s.Nombre} {s.Descripcion} "
            }).ToList();

        }


        public async Task<IActionResult> OnPostCrearAsync()
        {
            // Validaciones defensivas
            if (!Guid.TryParse(estudianteSeleccionado, out var IdEstudiante))
            {
                ModelState.AddModelError("estudianteSeleccionado", "Debe seleccionar un estudiante válido.");
                await ObtenerEstudiantes();
                await ObtenerCategorias();
                return Page();
            }

            if (!Guid.TryParse(categoriaSeleccionada, out var IdCategoria))
            {
                ModelState.AddModelError("categoriaSeleccionada", "Debe seleccionar una categoría válida.");
                await ObtenerEstudiantes();
                await ObtenerCategorias();
                return Page();
            }

            var datos = new
            {
                IdEstudiante = IdEstudiante,
                IdCategoria = IdCategoria,
                FechaDiagnostico = condicion.FechaDiagnostico,
                Observaciones = condicion.Observaciones?.Trim()
            };

            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            using var cliente = new HttpClient();
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearCondicion");
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                string contenidoError = await respuesta.Content.ReadAsStringAsync();
                _logger.LogError("[ERROR] Fallo al crear la condición especial: {Error}", contenidoError);
                ModelState.AddModelError("", "Error al crear la condición especial.");
                await ObtenerEstudiantes();
                await ObtenerCategorias();
                return Page();
            }

            _logger.LogInformation("Condición especial creada exitosamente para estudiante {IdEstudiante}", IdEstudiante);

            await OnGet();
            return Page();
        }


        public async Task<IActionResult> OnPostEliminarAsync(Guid? IdCondicion)
        {
            if (IdCondicion == null || IdCondicion == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarCondicion");
            string urlFinal = string.Format(endPoint, IdCondicion);
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

            Console.WriteLine($"[DEBUG] Condición eliminada con ID: {IdCondicion}");
            return RedirectToPage(new { filtro = "General" });
        }



        public async Task<IActionResult> OnPostEditarAsync(Guid IdCondicion, CondicionesEspecialesRequest condicion)
        {
            try
            {
                condicion.IdCondicion = IdCondicion;

                // Validación defensiva de categoría
                if (condicion.IdCategoria == Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(categoriaSeleccionada) && Guid.TryParse(categoriaSeleccionada, out var idCategoria))
                    {
                        condicion.IdCategoria = idCategoria;
                    }
                    else
                    {
                        condicion.IdCategoria = await ObtenerCategoriaActual(IdCondicion);
                    }
                }

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarCondicion");

                var datos = new
                {
                    IdCondicion = IdCondicion,
                    IdCategoria = condicion.IdCategoria,
                    FechaDiagnostico = condicion.FechaDiagnostico,
                    Observaciones = condicion.Observaciones?.Trim()
                };

                var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                _logger.LogDebug("[DEBUG] JSON enviado para edición:\n{Json}", json);

                using var cliente = new HttpClient();
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdCondicion), contenido);

                if (!respuesta.IsSuccessStatusCode)
                {
                    string contenidoError = await respuesta.Content.ReadAsStringAsync();
                    _logger.LogError("[ERROR] Fallo al editar condición especial: {Error}", contenidoError);
                    ModelState.AddModelError("", "Error al editar la condición especial.");
                    await ObtenerEstudiantes();
                    await ObtenerCategorias();
                    await OnGet();
                    return Page();
                }

                _logger.LogInformation("Condición especial editada exitosamente. ID: {IdCondicion}", IdCondicion);

                // Validación defensiva antes de redirigir
                if (condicion.IdEstudiante == Guid.Empty)
                {
                    _logger.LogWarning("IdEstudiante vacío tras edición. Redirigiendo sin filtro.");
                    return RedirectToPage("/Dashboard/CondicionesEspeciales/Index");
                }

                return RedirectToPage("/Dashboard/CondicionesEspeciales/Index", new { filtro = "PorEstudiante", IdEstudiante = condicion.IdEstudiante });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EXCEPTION] Error al actualizar la condición especial.");
                ModelState.AddModelError("", "Error inesperado al actualizar la condición especial.");
                await ObtenerEstudiantes();
                await ObtenerCategorias();
                await OnGet();
                return Page();
            }
        }


        private async Task<Guid> ObtenerCategoriaActual(Guid idCondicion)
        {
            string endpoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerCondicion"), idCondicion);

            using var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);

            if (!respuesta.IsSuccessStatusCode)
            {
                _logger.LogWarning("[WARN] No se pudo obtener la condición con ID {IdCondicion}. Status: {StatusCode}", idCondicion, respuesta.StatusCode);
                return Guid.Empty;
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var condicion = JsonSerializer.Deserialize<CondicionesEspecialesResponse>(resultado, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return condicion?.IdCategoria ?? Guid.Empty;
        }

    }
}
