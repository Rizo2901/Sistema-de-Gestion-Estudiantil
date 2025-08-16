using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Actividades
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        [BindProperty]
        public ActividadRequest Actividad { get; set; }

        public IList<ActividadResponse> Actividades { get; set; } = new List<ActividadResponse>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerActividades");
            var cliente = new HttpClient();

            try
            {
                var respuesta = await cliente.GetAsync(endPoint);

                if (!respuesta.IsSuccessStatusCode)
                {
                    // No redirige ni lanza error: simplemente muestra la página sin actividades
                    return Page();
                }

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Actividades = JsonSerializer.Deserialize<List<ActividadResponse>>(resultado, opciones) ?? new List<ActividadResponse>();
            }
            catch
            {
                // En caso de excepción (timeout, red, etc.), muestra la página sin actividades
                Actividades = new List<ActividadResponse>();
            }

            return Page();
        }





        public async Task<IActionResult> OnPostCrearActividadAsync()
        {
            _logger.LogInformation("Inicio de creación de actividad: {@Actividad}", Actividad);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inválido. Errores: {@Errores}", ModelState.Values.SelectMany(v => v.Errors));
                await OnGetAsync();
                return Page();
            }

            var fechaBase = Actividad.Fecha.Date;

            var fechaInicio = fechaBase.Add(Actividad.FechaInicio.TimeOfDay);
            var fechaFin = fechaBase.Add(Actividad.FechaFin.TimeOfDay);

            if (fechaInicio >= fechaFin)
            {
                _logger.LogWarning("FechaInicio >= FechaFin. FechaInicio: {FechaInicio}, FechaFin: {FechaFin}", fechaInicio, fechaFin);
                ModelState.AddModelError(string.Empty, "La hora de inicio debe ser anterior a la hora de fin.");
                await OnGetAsync();
                return Page();
            }

            _logger.LogInformation("Fecha: {Fecha}, Inicio: {Inicio}, Fin: {Fin}", fechaBase, fechaInicio, fechaFin);

            var datos = new
            {
                Titulo = Actividad.Titulo,
                Descripcion = Actividad.Descripcion,
                Tipo = Actividad.Tipo,
                Fecha = fechaBase,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            _logger.LogDebug("Datos enviados al API: {@Datos}", datos);

            var opcionesJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(datos, opcionesJson);
            _logger.LogDebug("Payload JSON serializado: {Json}", json);

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearActividad");
            using var cliente = new HttpClient();
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            var resultado = await respuesta.Content.ReadAsStringAsync();

            if (!respuesta.IsSuccessStatusCode)
            {
                _logger.LogError("Error al crear actividad. Status: {StatusCode}, Respuesta: {Error}", respuesta.StatusCode, resultado);
                ModelState.AddModelError(string.Empty, $"Error al crear actividad: {resultado}");
                await OnGetAsync();
                return Page();
            }

            _logger.LogInformation("Respuesta exitosa del API: {Resultado}", resultado);

            using var jsonDoc = JsonDocument.Parse(resultado);
            if (!jsonDoc.RootElement.TryGetProperty("id", out var idProp))
            {
                _logger.LogError("La respuesta del API no contiene 'id'. Respuesta: {Json}", resultado);
                ModelState.AddModelError(string.Empty, "La actividad no fue creada correctamente.");
                await OnGetAsync();
                return Page();
            }

            var idActividad = idProp.GetGuid();
            _logger.LogInformation("Actividad creada con ID: {IdActividad}", idActividad);

            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEditarActividadAsync(Guid IdActividad)
        {
            _logger.LogInformation("Inicio de edición de actividad: {@Actividad}", Actividad);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inválido. Errores: {@Errores}", ModelState.Values.SelectMany(v => v.Errors));
                await OnGetAsync();
                return Page();
            }

            var fechaBase = Actividad.Fecha.Date;
            var fechaInicio = fechaBase.Add(Actividad.FechaInicio.TimeOfDay);
            var fechaFin = fechaBase.Add(Actividad.FechaFin.TimeOfDay);

            if (fechaInicio >= fechaFin)
            {
                _logger.LogWarning("FechaInicio >= FechaFin. FechaInicio: {FechaInicio}, FechaFin: {FechaFin}", fechaInicio, fechaFin);
                ModelState.AddModelError(string.Empty, "La hora de inicio debe ser anterior a la hora de fin.");
                await OnGetAsync();
                return Page();
            }

            _logger.LogInformation("Fecha base: {Fecha}, Inicio: {Inicio}, Fin: {Fin}", fechaBase, fechaInicio, fechaFin);

            var datos = new
            {
                Titulo = Actividad.Titulo,
                Descripcion = Actividad.Descripcion,
                Tipo = Actividad.Tipo,
                Fecha = fechaBase,           // ✅ Este campo es obligatorio para el API
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            _logger.LogDebug("Datos enviados al API para edición: {@Datos}", datos);

            var opcionesJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(datos, opcionesJson);
            _logger.LogDebug("Payload JSON serializado para edición: {Json}", json);

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarActividad");
            using var cliente = new HttpClient();
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PutAsync(string.Format(endPoint, IdActividad), contenido);
            var resultado = await respuesta.Content.ReadAsStringAsync();

            if (!respuesta.IsSuccessStatusCode)
            {
                _logger.LogError("Error al editar actividad. Status: {StatusCode}, Respuesta: {Error}", respuesta.StatusCode, resultado);
                ModelState.AddModelError(string.Empty, $"Error al editar actividad: {resultado}");
                await OnGetAsync();
                return Page();
            }

            _logger.LogInformation("Actividad editada correctamente. Respuesta: {Resultado}", resultado);

            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarActividadAsync(Guid IdActividad)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarActividad");

            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdActividad));
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync();
            return Page();
        }
    }
}