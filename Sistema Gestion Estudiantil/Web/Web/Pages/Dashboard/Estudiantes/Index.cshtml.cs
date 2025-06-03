using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Estudiantes
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IList<EstudiantesResponse> Estudiantes { get; set; }
        public List<SelectListItem> Secciones { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public EstudiantesRequest Estudiante { get; set; }

        [BindProperty]
        public string seccionseleccionada { get; set; }

        // 1. Propiedad para el archivo de fotografía
        [BindProperty]
        public IFormFile FotografiaArchivo { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
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
            if (!ModelState.IsValid)
            {
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

            // 2. Procesar la fotografía si se subió
            if (FotografiaArchivo != null && FotografiaArchivo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await FotografiaArchivo.CopyToAsync(ms);
                    // Si tu modelo espera Base64:
                    Estudiante.Fotografia = Convert.ToBase64String(ms.ToArray());
                    // Si tu modelo espera byte[]:
                    // Estudiante.Fotografia = ms.ToArray();
                }
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearEstudiante");
            var contenido = new StringContent(JsonSerializer.Serialize(Estudiante), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditarAsync(Guid idEstudiante, EstudiantesRequest estudiante)
        {
            if (!ModelState.IsValid || idEstudiante == Guid.Empty)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarEstudiante");
            var datos = new
            {
                IdEstudiante = idEstudiante,
                estudiante
            };
            var contenido = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var respuesta = await client.PutAsync(endpoint, contenido);
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
    }
}
