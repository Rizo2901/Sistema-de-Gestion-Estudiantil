using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.ReunionesAdministrativas
{
    public class IndexModel : PageModel
    {

        private readonly IConfiguracion _confg;

        public IList<ReunionAdministrativaResponse> ReunionesAdministrativas { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerReunionesAdministrativas");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            if (!respuesta.IsSuccessStatusCode)
            {
                return StatusCode((int)respuesta.StatusCode, "Error al obtener las reuniones.");
            }
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            ReunionesAdministrativas = JsonSerializer.Deserialize<List<ReunionAdministrativaResponse>>(resultado, opciones);
            return Page();
        }


        public async Task<IActionResult> OnPostCrearReunion(ReunionAdministrativaRequest reunion)
        {
            var datos = new
            {
                Tema = reunion.Tema,
                Descripcion = reunion.Descripcion,
                Lugar = reunion.Lugar,
                Fecha = reunion.Fecha
            };

            string jsonReunion = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            _logger.LogInformation("JSON que se enviará:\n{Json}", jsonReunion);

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearReunionAdministrativa");
            _logger.LogInformation("Endpoint utilizado: {Endpoint}", endPoint);

            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonReunion, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            _logger.LogInformation("Código de estado recibido: {StatusCode}", respuesta.StatusCode);

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            _logger.LogInformation("Respuesta del servidor:\n{Respuesta}", resultado);

            using var json = JsonDocument.Parse(resultado);
            var idReunion = json.RootElement.GetProperty("id").GetGuid();
            _logger.LogInformation("ID de la reunión creada: {IdReunion}", idReunion);

            return RedirectToPage();
        }



        public async Task<IActionResult> OnPostEliminarReunion(Guid IdReunionAdmin)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarReunionAdministrativa");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdReunionAdmin));

            await OnGetAsync(); // Recargar datos
            return Page();
        }

        public async Task<IActionResult> OnPostEditarReunion(Guid IdReunionAdmin, ReunionAdministrativaRequest reunion)
        {
            var datos = new
            {
                Tema = reunion.Tema,
                Descripcion = reunion.Descripcion,
                Lugar = reunion.Lugar,
                Fecha = reunion.Fecha,
                IdReunionAdmin = IdReunionAdmin
            };

            string jsonReunion = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarReunionAdministrativa");
            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonReunion, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PutAsync(string.Format(endPoint, IdReunionAdmin), contenido);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(resultado);
            var idActualizado = json.RootElement.GetGuid();

            await OnGetAsync(); // Refrescar lista
            return Page();
        }
    }
}
