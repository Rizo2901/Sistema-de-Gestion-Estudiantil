using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Profesores
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public ProfesorResponse profesor { get; set; }

        [BindProperty]
        public MateriaProfesorResponse Materiaprofesor { get; set; }
        public List<SelectListItem> Secciones { get; set; } = new List<SelectListItem>();
        public string seccionseleccionada { get; set; }

        public List<SelectListItem> Materias { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string materiaSeleccionada { get; set; }

        public IList<ProfesorResponse> Profesores { get; set; } = new List<ProfesorResponse>();

        

        public async Task OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerProfesores");
            using var client = new HttpClient();
            var response = await client.GetAsync(endPoint);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Profesores = JsonSerializer.Deserialize<List<ProfesorResponse>>(result, options);

            Console.WriteLine($"[DEBUG] Profesores cargados: {Profesores.Count}");
            await ObtenerSecciones();
            await ObtenerMaterias();
        }

        public async Task<IActionResult> OnPostCrearAsync(ProfesorRequest profesor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profesor.Nombre) || string.IsNullOrWhiteSpace(profesor.Apellido1))
                {
                    ModelState.AddModelError("", "Los campos obligatorios no pueden estar vacíos.");
                    return Page();
                }

                var datos = new
                {
                    Nombre = profesor.Nombre,
                    Apellido1 = profesor.Apellido1,
                    Apellido2 = profesor.Apellido2,
                    Direccion = profesor.Direccion,
                    TelefonoCelular = profesor.TelefonoCelular,
                    CorreoElectronico = profesor.CorreoElectronico,
                    IdUsuario = profesor.IdUsuario
                };

                string jsonProfesor = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"[DEBUG] JSON enviado:\n{jsonProfesor}");

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearProfesor");
                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonProfesor, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(endPoint, contenido);
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                using var json = JsonDocument.Parse(resultado);
                var idProfesor = json.RootElement.GetProperty("id").GetGuid();

                Console.WriteLine($"[DEBUG] Profesor creado con ID: {idProfesor}");

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo al crear el profesor: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el profesor.");
                return Page();
            }
        }

        public async Task<IActionResult> OnGetObtenerProfesorAsync(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerProfesor");
            using var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, Id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            profesor = JsonSerializer.Deserialize<ProfesorResponse>(resultado, opciones);

            return Page();
        }

        public async Task<IActionResult> OnPostEliminarProfesorAsync(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarProfesor");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, Id));

            if (!respuesta.IsSuccessStatusCode)
            {
                var contenidoError = await respuesta.Content.ReadAsStringAsync();

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError(string.Empty, "No se puede eliminar el profesor porque está asociado a otras entidades.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Error al eliminar el profesor: {contenidoError}");
                }

                // Carga los profesores otra vez para que no quede vacío el listado
                await OnGetAsync();
                return Page();
            }

            // Recargar la lista después de eliminar
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEditarProfesorAsync(Guid IdProfesor, ProfesorRequest profesor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profesor.Nombre) || string.IsNullOrWhiteSpace(profesor.Apellido1))
                {
                    ModelState.AddModelError("", "Los campos obligatorios no pueden estar vacíos.");
                    await OnGetAsync();
                    return Page();
                }

                var datos = new
                {
                    Nombre = profesor.Nombre,
                    Apellido1 = profesor.Apellido1,
                    Apellido2 = profesor.Apellido2,
                    Direccion = profesor.Direccion,
                    TelefonoCelular = profesor.TelefonoCelular,
                    CorreoElectronico = profesor.CorreoElectronico
                };

                string jsonProfesor = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"[DEBUG] JSON enviado para edición:\n{jsonProfesor}");

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarProfesor");
                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonProfesor, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdProfesor), contenido);
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(resultado);
                var idActualizado = json.RootElement.GetGuid();

                Console.WriteLine($"[DEBUG] Profesor editado con ID: {idActualizado}");

                await OnGetAsync(); // Refrescar lista
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo al editar el profesor: {ex.Message}");
                ModelState.AddModelError("", "Error al editar el profesor.");
                await OnGetAsync();
                return Page();
            }
        }

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

        public async Task<IActionResult> OnPostAsignarMateriaAsync()
        {
            _logger.LogDebug("? Iniciando asignación de materia…");

            await ObtenerMaterias();

            // Limpieza de ModelState
            var keysToRemove = ModelState.Keys
                .Where(k => !k.StartsWith("Materiaprofesor.") && k != nameof(materiaSeleccionada))
                .ToList();
            foreach (var key in keysToRemove)
                ModelState.Remove(key);
            _logger.LogDebug("ModelState depurado, llaves restantes: {Keys}", string.Join(", ", ModelState.Keys));

            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                    foreach (var err in entry.Value.Errors)
                        _logger.LogWarning("ModelState[{Key}] error: {Error}", entry.Key, err.ErrorMessage);

                await ObtenerMaterias();
                await OnGetAsync();
                return Page();
            }

            if (!Guid.TryParse(materiaSeleccionada, out var IdMateria))
            {
                _logger.LogWarning("Materia seleccionada inválida: '{Value}'", materiaSeleccionada);
                ModelState.AddModelError(nameof(materiaSeleccionada), "Debe seleccionar una materia válida.");
                await ObtenerMaterias();
                return Page();
            }

            if (Materiaprofesor == null || Materiaprofesor.IdProfesor == Guid.Empty)
            {
                _logger.LogError("Materiaprofesor es null o IdProfesor vacío.");
                ModelState.AddModelError(string.Empty, "No se encontró el profesor a asignar.");
                return Page();
            }

            Materiaprofesor.IdMateria = IdMateria;
            _logger.LogDebug("Asignando IdMateria={IdMateria} a profesor {IdProfesor}", IdMateria, Materiaprofesor.IdProfesor);

            try
            {
                var endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearMateriaProfesor");
                _logger.LogDebug("Endpoint: {Endpoint}", endpoint);

                var json = JsonSerializer.Serialize(Materiaprofesor, new JsonSerializerOptions { WriteIndented = true });
                _logger.LogDebug("Payload JSON:\n{Json}", json);

                using var client = new HttpClient();
                var resp = await client.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));

                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    _logger.LogError("API devolvió {Status}: {Error}", resp.StatusCode, err);
                    ModelState.AddModelError(string.Empty, "Error al asignar la materia.");
                    return Page();
                }

                _logger.LogInformation("Materia asignada con éxito.");
                ViewData["MensajeAsignacion"] = "Materia asignada correctamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al asignar materia");
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado.");
            }

            await OnGetAsync();
            _logger.LogDebug("Refrescando página tras asignación.");
            return Page();
        }


        //public async Task <IActionResult> AsignarMateria()
        //{

        //await ObtenerSecciones();



        // Validar sección
        //if (!Guid.TryParse(seccionseleccionada, out var idSeccion))
        //{
        //      ModelState.AddModelError("seccionseleccionada", "Debe seleccionar una sección válida.");
        //        await ObtenerSecciones();
        //          return Page();
        //}

        //          Materiaprofesor.



        // Enviar al API
        //string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearEstudiante");
        //var contenido = new StringContent(JsonSerializer.Serialize(Estudiante), Encoding.UTF8, "application/json");

        //using var client = new HttpClient();
        //var respuesta = await client.PostAsync(endpoint, contenido);
        //respuesta.EnsureSuccessStatusCode();

        //return RedirectToPage();

        //        }

    }
}
