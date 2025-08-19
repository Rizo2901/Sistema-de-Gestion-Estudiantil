using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Familiares
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

        public FamiliarResponse Familiar { get; set; }

        public List<SelectListItem> Estudiantes { get; set; } = new List<SelectListItem>();
        
        [BindProperty]
        public string estudianteSeleccionado { get; set; }  

        [BindProperty]
        public FamiliarRequest FamiliarForm { get; set; }

        public IList<FamiliarResponse> Familiares { get; set; } = new List<FamiliarResponse>();

        public async Task OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerFamiliares");
            using var client = new HttpClient();
            var response = await client.GetAsync(endPoint);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Familiares = JsonSerializer.Deserialize<List<FamiliarResponse>>(result, options);

            _logger.LogDebug(" Familiares cargados: {Count}", Familiares.Count);
            await ObtenerEstudiantes();
        }

        public async Task<IActionResult> OnPostCrearAsync(FamiliarRequest familiar)
        {
            try
            {
                // Validaciones defensivas
                if (!Guid.TryParse(estudianteSeleccionado, out var idEstudiante))
                {
                    ModelState.AddModelError("estudianteSeleccionado", "Debe seleccionar un estudiante válido.");
                    await ObtenerEstudiantes();
                    return Page();
                }
                // Construcción del objeto para enviar
                var datos = new
                {
                    IdEstudiante = idEstudiante,
                    Nombre = familiar.Nombre,
                    Apellido1 = familiar.Apellido1,
                    Apellido2 = familiar.Apellido2,
                    Cedula = familiar.Cedula,
                    Parentesco = familiar.Parentesco,
                    EstadoCivil = familiar.EstadoCivil,
                    Direccion = familiar.Direccion,
                    TelefonoCelular = familiar.TelefonoCelular,
                    CorreoElectronico = familiar.CorreoElectronico,
                    Ocupacion = familiar.Ocupacion,
                    CantidadHijos = familiar.CantidadHijos,
                    ViveConEstudiante = familiar.ViveConEstudiante,
                    EsResponsableLegal = familiar.EsResponsableLegal
                };

                var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

                string endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearFamiliar");

                using var client = new HttpClient();
                var response = await client.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fallo al crear el familiar");
                ModelState.AddModelError("", "Error al crear el familiar.");
                return Page();
            }
        }

        public async Task<IActionResult> OnGetObtenerFamiliarAsync(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return NotFound();

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerFamiliar");
            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(string.Format(endPoint, Id));
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Familiar = JsonSerializer.Deserialize<FamiliarResponse>(resultado, opciones);

            return Page();
        }

        public async Task<IActionResult> OnPostEditarFamiliarAsync(Guid IdFamiliar, FamiliarRequest familiar)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(familiar.Nombre) || string.IsNullOrWhiteSpace(familiar.Apellido1))
                {
                    ModelState.AddModelError("", "Los campos obligatorios no pueden estar vacíos.");
                    await OnGetAsync();
                    return Page();
                }

                string json = JsonSerializer.Serialize(familiar, new JsonSerializerOptions { WriteIndented = true });
                _logger.LogDebug(" JSON enviado para edición:\n{Json}", json);

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarFamiliar");
                using var cliente = new HttpClient();
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdFamiliar), contenido);
                respuesta.EnsureSuccessStatusCode();

                _logger.LogInformation(" Familiar editado correctamente.");
                await OnGetAsync();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Fallo al editar el familiar");
                ModelState.AddModelError("", "Error al editar el familiar.");
                await OnGetAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEliminarFamiliarAsync(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return NotFound();

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarFamiliar");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, Id));

            if (!respuesta.IsSuccessStatusCode)
            {
                var contenidoError = await respuesta.Content.ReadAsStringAsync();

                if (respuesta.StatusCode == System.Net.HttpStatusCode.Conflict)
                    ModelState.AddModelError("", "No se puede eliminar el familiar porque está asociado a otras entidades.");
                else
                    ModelState.AddModelError("", $"Error al eliminar el familiar: {contenidoError}");

                await OnGetAsync();
                return Page();
            }

            _logger.LogInformation(" Familiar eliminado correctamente.");
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCrearUsuarioAutoAsync()
        {
            _logger.LogDebug(" Iniciando creación de usuario institucional desde Familiar…");

            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
            if (data == null || !data.ContainsKey("IdEntidad") || !data.ContainsKey("TipoEntidad"))
                return new JsonResult(new { success = false, error = "Datos incompletos." }) { StatusCode = 400 };

            if (!Guid.TryParse(data["IdEntidad"], out var idEntidad))
                return new JsonResult(new { success = false, error = "Entidad inválida." }) { StatusCode = 400 };

            var tipoEntidad = data["TipoEntidad"];
            var payload = new { tipoEntidad, idEntidad };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });

            try
            {
                var endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearUsuarioDesdeEntidad");
                using var client = new HttpClient();
                var response = await client.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("API devolvió {Status}: {Error}", response.StatusCode, error);
                    return new JsonResult(new { success = false, error = "Error al crear el usuario." }) { StatusCode = 400 };
                }

                _logger.LogInformation(" Usuario institucional de familiar creado con éxito.");
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al crear usuario desde familiar.");
                return new JsonResult(new { success = false, error = "Error inesperado." }) { StatusCode = 500 };
            }
            }

           public async Task ObtenerEstudiantes(){
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endpointEstudiantes= _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
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
    }
  }

