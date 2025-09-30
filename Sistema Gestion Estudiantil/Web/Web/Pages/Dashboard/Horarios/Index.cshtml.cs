using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Horarios
{
    public class IndexModel : PageModel
    {
        // ? Propiedades con BindProperty para el formulario
        [BindProperty]
        public HorarioRequest horario { get; set; }

        [BindProperty]
        public string seccionSeleccionada { get; set; }

        [BindProperty]
        public string materiaSeleccionada { get; set; }

        [BindProperty]
        public string profesorSeleccionado { get; set; }

        // ? Listas para mostrar en la vista
        public List<HorarioPorSeccion> Horarios { get; set; } = new();

        public List<SelectListItem> Secciones { get; set; } = new();
        public List<SelectListItem> Materias { get; set; } = new();
        public List<SelectListItem> Profesores { get; set; } = new();

        private readonly IConfiguracion _confg;
        private ILogger<IndexModel> _logger;
        public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public async Task OnGet()
        {
            try
            {
                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerHorariosPorSeccion");
                using var cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(endPoint);
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Horarios = JsonSerializer.Deserialize<List<HorarioPorSeccion>>(resultado, opciones) ?? new List<HorarioPorSeccion>();
                }
                else
                {
                    Horarios = new List<HorarioPorSeccion>();
                }

                await RecargarListasAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los horarios por sección");
                Horarios = new List<HorarioPorSeccion>(); // fallback defensivo
            }
            await RecargarListasAsync();
        }

        // ? Acción POST para crear horario
        public async Task<IActionResult> OnPostCrearHorarioAsync()
        {
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

                await RecargarListasAsync();
                return Page();
            }

            // Validaciones defensivas
            if (!Guid.TryParse(seccionSeleccionada, out var idSeccion))
            {
                ModelState.AddModelError("seccionSeleccionada", "Debe seleccionar una sección válida.");
                await RecargarListasAsync();
                return Page();
            }

            if (!Guid.TryParse(materiaSeleccionada, out var idMateria))
            {
                ModelState.AddModelError("materiaSeleccionada", "Debe seleccionar una materia válida.");
                await RecargarListasAsync();
                return Page();
            }

            if (!Guid.TryParse(profesorSeleccionado, out var idProfesor))
            {
                ModelState.AddModelError("profesorSeleccionado", "Debe seleccionar un profesor válido.");
                await RecargarListasAsync();
                return Page();
            }

            using var client = new HttpClient();

            // Crear horario
            var datosHorario = new
            {
                IdSeccion = idSeccion,
                IdMateria = idMateria,
                IdProfesor = idProfesor,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Dia = horario.Dia,
                Aula = horario.Aula
            };

            var jsonHorario = JsonSerializer.Serialize(datosHorario, new JsonSerializerOptions { WriteIndented = true });
            string endpointHorario = _confg.ObtenerMetodo("ApiEndPoints", "CrearHorario");

            var responseHorario = await client.PostAsync(endpointHorario, new StringContent(jsonHorario, Encoding.UTF8, "application/json"));
            responseHorario.EnsureSuccessStatusCode();

            // Crear vínculo en MateriaProfesor
            var datosMateria = new { IdProfesor = idProfesor, IdMateria = idMateria };
            var jsonMateria = JsonSerializer.Serialize(datosMateria);
            string endpointMateria = _confg.ObtenerMetodo("ApiEndPoints", "CrearMateriaProfesor");

            var responseMateria = await client.PostAsync(endpointMateria, new StringContent(jsonMateria, Encoding.UTF8, "application/json"));
            responseMateria.EnsureSuccessStatusCode();

            // Crear vínculo en SeccionProfesor
            var datosSeccion = new { IdProfesor = idProfesor, IdSeccion = idSeccion };
            var jsonSeccion = JsonSerializer.Serialize(datosSeccion);
            string endpointSeccion = _confg.ObtenerMetodo("ApiEndPoints", "CrearSeccionProfesor");

            var responseSeccion = await client.PostAsync(endpointSeccion, new StringContent(jsonSeccion, Encoding.UTF8, "application/json"));
            responseSeccion.EnsureSuccessStatusCode();

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostEliminarHorarioAsync(Guid? IdHorario)
        {
            _logger.LogDebug("🗑️ [EliminarHorario] Inicio de eliminación de horario.");

            if (IdHorario == null || IdHorario == Guid.Empty)
            {
                _logger.LogWarning("❌ [EliminarHorario] IdHorario nulo o vacío.");
                return NotFound();
            }

            _logger.LogDebug("📥 [EliminarHorario] Id recibido: {Id}", IdHorario);

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarHorario");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdHorario));

            _logger.LogDebug("📡 [EliminarHorario] Código de respuesta: {StatusCode}", respuesta.StatusCode);

            if (!respuesta.IsSuccessStatusCode)
            {
                var contenidoError = await respuesta.Content.ReadAsStringAsync();

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    _logger.LogWarning("⚠️ [EliminarHorario] Conflicto: horario asociado a otras entidades.");
                    ModelState.AddModelError(string.Empty, "No se puede eliminar el horario porque está asociado a otras entidades.");
                }
                else
                {
                    _logger.LogError("❌ [EliminarHorario] Error al eliminar: {Error}", contenidoError);
                    ModelState.AddModelError(string.Empty, $"Error al eliminar el horario: {contenidoError}");
                }

                await OnGet();
                return Page();
            }

            _logger.LogInformation("✅ [EliminarHorario] Horario eliminado correctamente.");
            TempData["Mensaje"] = "✅ Horario eliminado correctamente.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditarHorarioAsync(Guid IdHorario)
        {
            _logger.LogInformation("[INICIO] Editar horario con ID: {IdHorario}", IdHorario);

            try
            {
                // 🔁 Eliminar validaciones automáticas de propiedades que no aplican en edición
                ModelState.Remove("materiaSeleccionada");
                ModelState.Remove("seccionSeleccionada");
                ModelState.Remove("profesorSeleccionado");

                // Validaciones defensivas de GUIDs
                if (horario.IdSeccion == Guid.Empty)
                {
                    ModelState.AddModelError("horario.IdSeccion", "Debe seleccionar una sección válida.");
                    _logger.LogWarning("[VALIDACIÓN] IdSeccion vacío para horario ID: {IdHorario}", IdHorario);
                }

                if (horario.IdMateria == Guid.Empty)
                {
                    ModelState.AddModelError("horario.IdMateria", "Debe seleccionar una materia válida.");
                    _logger.LogWarning("[VALIDACIÓN] IdMateria vacío para horario ID: {IdHorario}", IdHorario);
                }

                if (horario.IdProfesor == Guid.Empty)
                {
                    ModelState.AddModelError("horario.IdProfesor", "Debe seleccionar un profesor válido.");
                    _logger.LogWarning("[VALIDACIÓN] IdProfesor vacío para horario ID: {IdHorario}", IdHorario);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("[VALIDACIÓN] Modelo inválido. Se recargan listas.");
                    foreach (var error in ModelState.Where(ms => ms.Value.Errors.Count > 0))
                    {
                        foreach (var detalle in error.Value.Errors)
                        {
                            _logger.LogWarning("[VALIDACIÓN] Campo: {Campo} - Error: {Mensaje}", error.Key, detalle.ErrorMessage);
                        }
                    }
                    await RecargarListasAsync();
                    return Page();
                }

                var datos = new
                {
                    IdSeccion = horario.IdSeccion,
                    IdMateria = horario.IdMateria,
                    IdProfesor = horario.IdProfesor,
                    HoraInicio = horario.HoraInicio,
                    HoraFin = horario.HoraFin,
                    Dia = horario.Dia,
                    Aula = horario.Aula
                };

                string jsonHorario = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                _logger.LogDebug("[SERIALIZACIÓN] JSON enviado:\n{JsonHorario}", jsonHorario);

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarHorario");
                _logger.LogInformation("[HTTP] Endpoint de edición: {EndPoint}", endPoint);

                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonHorario, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdHorario), contenido);
                _logger.LogInformation("[HTTP] PUT enviado. StatusCode: {StatusCode}", respuesta.StatusCode);

                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(resultado);
                var idActualizado = json.RootElement.GetGuid();

                _logger.LogInformation("[ÉXITO] Horario editado correctamente. ID actualizado: {IdActualizado}", idActualizado);

                await OnGet(); // Refrescar lista
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ERROR] Fallo al editar el horario con ID: {IdHorario}", IdHorario);
                ModelState.AddModelError("", "Error al editar el horario.");
                await OnGet();
                return Page();
            }
        }
        // ? Métodos auxiliares para cargar listas
        public async Task ObtenerSecciones()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endpointSecciones = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerSecciones");
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
                Text = s.Nombre
            }).ToList();
        }

        public async Task ObtenerProfesores()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endpointProfesores = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerProfesores");
            var respuestaProfesores = await client.GetAsync(endpointProfesores);
            respuestaProfesores.EnsureSuccessStatusCode();
            var resultadoProfesores = await respuestaProfesores.Content.ReadAsStringAsync();
            var profesoresResponse = JsonSerializer.Deserialize<List<ProfesorResponse>>(resultadoProfesores, opciones);

            Profesores = profesoresResponse.Select(s => new SelectListItem
            {
                Value = s.IdProfesor.ToString(),
                Text = $"{s.Nombre} {s.Apellido1} {s.Apellido2}"
            }).ToList();
        }

        async Task RecargarListasAsync()
        {
            await ObtenerSecciones();
            await ObtenerMaterias();
            await ObtenerProfesores();
        }
    }
}