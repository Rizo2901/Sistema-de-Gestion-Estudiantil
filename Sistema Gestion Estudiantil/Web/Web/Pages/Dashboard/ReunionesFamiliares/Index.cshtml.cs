using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.ReunionesFamiliares
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        
        public IList<ReunionesResponse> Reuniones { get; set; }
        public List<SelectListItem> Estudiantes { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public ReunionesRequest reunion { get; set; }

        [BindProperty]
        public string estudianteSeleccionado { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task<IActionResult> OnGet()
        {
            string metodo = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerReunionesConPadre");
            var client = new HttpClient();
            var response = await client.GetAsync(metodo);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Reuniones = JsonSerializer.Deserialize<List<ReunionesResponse>>(result, options);
            await ObtenerEstudiantes();
            return Page();
        }

        public async Task<IActionResult> OnPostCrearAsync()
        {
            if (!Guid.TryParse(estudianteSeleccionado, out var idEstudiante))
            {
                ModelState.AddModelError("estudianteSeleccionado", "Debe seleccionar un estudiante válido.");
                await ObtenerEstudiantes();
                return Page();
            }

            reunion.IdEstudiante = idEstudiante;

            var contenido = new StringContent(JsonSerializer.Serialize(reunion), Encoding.UTF8, "application/json");
            var endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearReunionConPadre");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEliminarAsync(Guid idReunion)
        {
            if (idReunion == Guid.Empty)
            {
                return RedirectToPage();
            }

            string endpoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarReunionConPadre");
            using var client = new HttpClient();
            var respuesta = await client.DeleteAsync(string.Format(endpoint, idReunion));
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditarAsync(Guid idReunion)
        {
            var motivo = Request.Form["Motivo"];
            var fechaTexto = Request.Form["Fecha"];
            var estudianteIdTexto = Request.Form["reunion.IdEstudiante"];

            if (!Guid.TryParse(estudianteIdTexto, out var idEstudiante) || string.IsNullOrWhiteSpace(motivo) || string.IsNullOrEmpty(fechaTexto))
            {
                ModelState.AddModelError(string.Empty, "Todos los campos son obligatorios.");
                await ObtenerEstudiantes();
                return Page();
            }

            if (!DateTime.TryParse(fechaTexto, out var fecha))
            {
                ModelState.AddModelError(string.Empty, "Fecha inválida.");
                await ObtenerEstudiantes();
                return Page();
            }

            var reunionEditada = new ReunionesRequest
            {
                IdEstudiante = idEstudiante,
                Motivo = motivo,
                Fecha = fecha
            };

            var endpoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarReunionConPadre").Replace("{0}", idReunion.ToString());
            var contenido = new StringContent(JsonSerializer.Serialize(reunionEditada), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PutAsync(endpoint, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "No se pudo editar la reunión.");
                await ObtenerEstudiantes();
                return Page();
            }

            return RedirectToPage();
        }

        private async Task<Guid> ObtenerEstudianteActual(Guid idReunion)
        {
            string endpoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerReunionConPadre"), idReunion);

            using var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var reunion = JsonSerializer.Deserialize<EstudiantesResponse>(resultado, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return reunion?.IdEstudiante ?? Guid.Empty;
        }



        public async Task ObtenerEstudiantes()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var client = new HttpClient();
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
            var respuesta = await client.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var estudiantes = JsonSerializer.Deserialize<List<EstudiantesResponse>>(resultado, opciones);

            Estudiantes = estudiantes.Select(e => new SelectListItem
            {
                Value = e.IdEstudiante.ToString(),
                Text = $"{e.Nombre} {e.Apellido1} {e.Apellido2}" 
            }).ToList();
        }

    }
}
