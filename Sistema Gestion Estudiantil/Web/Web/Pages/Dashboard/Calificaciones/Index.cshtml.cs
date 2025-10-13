using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Calificaciones
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    namespace Web.Pages.Dashboard.Calificaciones
    {
        public class IndexModel : PageModel
        {

            public List<SelectListItem> Estudiantes { get; set; } = new List<SelectListItem>();

            [BindProperty]
            public string estudianteSeleccionado { get; set; }

            public List<SelectListItem> Materias { get; set; } = new List<SelectListItem>();

            [BindProperty]
            public string materiaSeleccionada { get; set; }

            private readonly IConfiguracion _confg;
            private readonly ILogger<IndexModel> _logger;

            [BindProperty]
            public IList<EstudianteConCalificaciones> EstudiantesConCalificaciones { get; set; } = new List<EstudianteConCalificaciones>();

            [BindProperty]
            public CalificacionRequest calificacion { get; set; }

            public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
            {
                _confg = confg;
                _logger = logger;
            }

            public async Task OnGet()
            {
                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerCalificacionesAgrupadas");

                using var cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(endPoint);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                EstudiantesConCalificaciones = JsonSerializer.Deserialize<List<EstudianteConCalificaciones>>(resultado, opciones);

                await ObtenerEstudiantes();
                await ObtenerMaterias();
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
                    Text = $"{s.Nombre}"
                }).ToList();
            }

            public async Task ObtenerMaterias()
            {
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                using var client = new HttpClient();
                string endpointMaterias = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerMaterias");
                var respuestaMaterias = await client.GetAsync(endpointMaterias);
                respuestaMaterias.EnsureSuccessStatusCode();
                var resultadoMaterias = await respuestaMaterias.Content.ReadAsStringAsync();
                var materiasResponse = JsonSerializer.Deserialize<List<MateriasResponse>>(resultadoMaterias, opciones);

                Materias = materiasResponse.Select(s => new SelectListItem
                {
                    Value = s.IdMateria.ToString(),
                    Text = $"{s.Nombre} ({s.Descripcion})"
                }).ToList();
            }
            public async Task<IActionResult> OnPostCrearAsync()
            {
                _logger.LogDebug("Inicio de OnPostCrearAsync");

                // Validación de estudiante
                if (!Guid.TryParse(estudianteSeleccionado, out var IdEstudiante))
                {
                    _logger.LogWarning("Estudiante inválido: {Valor}", estudianteSeleccionado);
                    ModelState.AddModelError("estudianteSeleccionado", "Debe seleccionar un estudiante válido.");
                    await RecargarCatalogosAsync();
                    return Page();
                }

                // Validación de materia
                if (!Guid.TryParse(materiaSeleccionada, out var IdMateria))
                {
                    _logger.LogWarning("Materia inválida: {Valor}", materiaSeleccionada);
                    ModelState.AddModelError("materiaSeleccionada", "Debe seleccionar una materia válida.");
                    await RecargarCatalogosAsync();
                    return Page();
                }

                // Preparar datos
                var datos = new
                {
                    IdEstudiante,
                    IdMateria,
                    Calificacion = calificacion.Calificacion,
                    Comentario = calificacion.Comentario?.Trim(),
                    Fecha = calificacion.Fecha,
                    UsuarioRegistro = User.Identity?.Name
                };

                _logger.LogDebug("Datos preparados para envío: {Datos}", JsonSerializer.Serialize(datos));

                // Serializar y enviar
                var json = JsonSerializer.Serialize(datos);
                var endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearCalificacion");

                _logger.LogDebug("Endpoint de creación: {EndPoint}", endPoint);

                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta;
                try
                {
                    using var cliente = new HttpClient(); 
                    respuesta = await cliente.PostAsync(endPoint, contenido);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Excepción al enviar la solicitud HTTP");
                    ModelState.AddModelError("", "Error de conexión con el servidor.");
                    await RecargarCatalogosAsync();
                    return Page();
                }

                // Verificar respuesta
                if (!respuesta.IsSuccessStatusCode)
                {
                    string contenidoError = await respuesta.Content.ReadAsStringAsync();
                    _logger.LogError("Respuesta HTTP fallida: {StatusCode} - {Contenido}", respuesta.StatusCode, contenidoError);
                    ModelState.AddModelError("", $"Error al crear la calificación: {contenidoError}");
                    await RecargarCatalogosAsync();
                    return Page();
                }

                _logger.LogInformation("Calificación creada exitosamente para estudiante {IdEstudiante}", IdEstudiante);
                TempData["Success"] = "Calificación registrada correctamente.";

                return RedirectToPage();
            }

            public async Task<IActionResult> OnPostEliminarAsync(Guid? IdCalificacion)
            {
                if (IdCalificacion == null || IdCalificacion == Guid.Empty)
                {
                    return NotFound();
                }

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarCalificacion");
                string urlFinal = string.Format(endPoint, IdCalificacion);

                using var cliente = new HttpClient();
                using var solicitud = new HttpRequestMessage(HttpMethod.Delete, urlFinal);

                var respuesta = await cliente.SendAsync(solicitud);
                if (!respuesta.IsSuccessStatusCode)
                {
                    string contenidoError = await respuesta.Content.ReadAsStringAsync();
                    _logger.LogError("[ERROR] Fallo al eliminar calificación: {Error}", contenidoError);
                    return Page();
                }

                _logger.LogInformation("Calificación eliminada con ID: {IdCalificacion}", IdCalificacion);
                return RedirectToPage(new { filtro = "General" });
            }

            public async Task<IActionResult> OnPostEditarAsync(
        Guid IdCalificacion,
        Guid IdEstudiante,
        Guid IdMateria,
        decimal Calificacion,
        DateTime Fecha,
        string Comentario)
            {
                _logger.LogDebug("Inicio de OnPostEditarAsync para IdCalificacion: {IdCalificacion}", IdCalificacion);

                try
                {
                    // Validación defensiva
                    if (IdMateria == Guid.Empty)
                    {
                        _logger.LogWarning("IdMateria vacío. Intentando recuperar desde materiaSeleccionada o BD.");

                        if (!string.IsNullOrEmpty(materiaSeleccionada) && Guid.TryParse(materiaSeleccionada, out var idMateriaParsed))
                        {
                            IdMateria = idMateriaParsed;
                            _logger.LogDebug("IdMateria obtenido desde materiaSeleccionada: {IdMateria}", IdMateria);
                        }
                        else
                        {
                            IdMateria = await ObtenerMateriaActual(IdCalificacion);
                            _logger.LogDebug("IdMateria obtenido desde BD: {IdMateria}", IdMateria);
                        }
                    }

                    if (string.IsNullOrWhiteSpace(Comentario))
                    {
                        _logger.LogWarning("Comentario vacío o nulo.");
                    }

                    var calificacion = new CalificacionRequest
                    {
                        IdCalificacion = IdCalificacion,
                        IdEstudiante = IdEstudiante,
                        IdMateria = IdMateria,
                        Calificacion = Calificacion,
                        Fecha = Fecha,
                        Comentario = Comentario?.Trim()
                    };

                    string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarCalificacion");
                    _logger.LogDebug("Endpoint de edición: {EndPoint}", endPoint);

                    var datos = new
                    {
                        IdCalificacion = calificacion.IdCalificacion,
                        IdMateria = calificacion.IdMateria,
                        IdEstudiante = calificacion.IdEstudiante,
                        Calificacion = calificacion.Calificacion,
                        Fecha = calificacion.Fecha,
                        Comentario = calificacion.Comentario
                    };

                    var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                    _logger.LogDebug("[DEBUG] JSON enviado para edición:\n{Json}", json);
                    Console.WriteLine("[PAYLOAD JSON] =>\n" + json);

                    using var cliente = new HttpClient();
                    var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                    _logger.LogDebug("Enviando solicitud PUT al endpoint...");
                    var respuesta = await cliente.PutAsync(string.Format(endPoint, IdCalificacion), contenido);

                    _logger.LogDebug("Respuesta HTTP recibida. StatusCode: {StatusCode}", respuesta.StatusCode);

                    if (!respuesta.IsSuccessStatusCode)
                    {
                        string contenidoError = await respuesta.Content.ReadAsStringAsync();
                        _logger.LogError("[ERROR] Fallo al editar calificación. StatusCode: {StatusCode}", respuesta.StatusCode);
                        _logger.LogError("[ERROR] Contenido de error recibido:\n{ContenidoError}", contenidoError);

                        ModelState.AddModelError("", $"Error al editar la calificación: {contenidoError}");
                        await ObtenerEstudiantes();
                        await ObtenerMaterias();
                        await OnGet();
                        return Page();
                    }

                    _logger.LogInformation("Calificación editada exitosamente. ID: {IdCalificacion}", IdCalificacion);

                    if (IdEstudiante == Guid.Empty)
                    {
                        _logger.LogWarning("IdEstudiante vacío tras edición. Redirigiendo sin filtro.");
                        return RedirectToPage("/Dashboard/Calificaciones/Index");
                    }

                    _logger.LogDebug("Redirigiendo con filtro por estudiante: {IdEstudiante}", IdEstudiante);
                    return RedirectToPage("/Dashboard/Calificaciones/Index", new { filtro = "PorEstudiante", IdEstudiante });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[EXCEPTION] Error al actualizar la calificación.");
                    ModelState.AddModelError("", "Error inesperado al actualizar la calificación.");

                    await ObtenerEstudiantes();
                    await ObtenerMaterias();
                    await OnGet();
                    return Page();
                }
            }

            private async Task<Guid> ObtenerMateriaActual(Guid idMateria)
            {
                string endpoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerCalificacion"), idMateria);

                using var client = new HttpClient();
                var respuesta = await client.GetAsync(endpoint);

                if (!respuesta.IsSuccessStatusCode)
                {
                    _logger.LogWarning("[WARN] No se pudo obtener la materia con ID {IdMateria}. Status: {StatusCode}", idMateria, respuesta.StatusCode);
                    return Guid.Empty;
                }

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var calificacion = JsonSerializer.Deserialize<CalificacionResponse>(resultado, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return calificacion?.IdMateria ?? Guid.Empty;
            }

            private async Task RecargarCatalogosAsync()
            {
                await ObtenerEstudiantes();
                await ObtenerMaterias();
                _logger.LogDebug("Catálogos recargados");
            }
        }

    }
}
