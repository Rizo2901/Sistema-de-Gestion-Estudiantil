using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.AdaptacionesCurriculares
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;


        public IList<EstudianteConAdaptacionesResponse> AdaptacionCurriculares { get; set; }
        public List<SelectListItem> Estudiantes { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public AdaptacionCurricularRequest adaptacion { get; set; }

        [BindProperty]
        public string estudianteSeleccionado { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }
        public async Task<IActionResult> OnGet()
        {
            string metodo = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerAdaptacionesCurricularesEstudiantes");
            var client = new HttpClient();
            var response = await client.GetAsync(metodo);
            if (!response.IsSuccessStatusCode)
            {
                await ObtenerEstudiantes();
                return Page();
            }
            var result = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AdaptacionCurriculares = JsonSerializer.Deserialize<List<EstudianteConAdaptacionesResponse>>(result, options);

            // Asignar IdEstudiante a cada adaptación
            if (AdaptacionCurriculares != null)
            {
                foreach (var est in AdaptacionCurriculares)
                {
                    if (est.Adaptaciones != null)
                    {
                        foreach (var adaptacion in est.Adaptaciones)
                        {
                            adaptacion.IdEstudiante = est.IdEstudiante;
                        }
                    }
                }
            }

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

            adaptacion.IdEstudiante = idEstudiante;

            var contenido = new StringContent(JsonSerializer.Serialize(adaptacion), Encoding.UTF8, "application/json");
            var endpoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearAdaptacionCurricular");

            using var client = new HttpClient();
            var respuesta = await client.PostAsync(endpoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
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


        public async Task<IActionResult> OnPostEliminarAsync(Guid? IdAdaptacion)
        {
            if (IdAdaptacion == null || IdAdaptacion == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarAdaptacionCurricular");
            string urlFinal = string.Format(endPoint, IdAdaptacion);
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

            Console.WriteLine($"[DEBUG] Adaptación eliminada con ID: {IdAdaptacion}");
            return RedirectToPage(new { filtro = "General" });
        }



        public async Task<IActionResult> OnPostEditarAsync(Guid IdAdaptacion, AdaptacionCurricularRequest adaptacion)
        {
            try
            {
                adaptacion.IdAdaptacion = IdAdaptacion;

                

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarAdaptacionCurricular");

                var datos = new
                {
                    IdAdaptacion = IdAdaptacion,
                    IdEstudiante = adaptacion.IdEstudiante,
                    Tipo = adaptacion.Tipo,
                    AreaAfectada = adaptacion.AreaAfectada,
                    FechaInicio = adaptacion.FechaInicio,
                    FechaFin = adaptacion.FechaFin,
                    Observaciones = adaptacion.Observaciones,
                    Estado = adaptacion.Estado

                };

                var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

                using var cliente = new HttpClient();
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdAdaptacion), contenido);

                if (!respuesta.IsSuccessStatusCode)
                {
                    string contenidoError = await respuesta.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error al editar la condición especial.");
                    await ObtenerEstudiantes();
                    await OnGet();
                    return Page();
                }

                // Validación defensiva antes de redirigir
                if (adaptacion.IdEstudiante == Guid.Empty)
                {
                    return RedirectToPage("/Dashboard/AdaptacionesCurriculares/Index");
                }

                return RedirectToPage("/Dashboard/AdaptacionesCurriculares/Index", new { filtro = "PorEstudiante", IdEstudiante = adaptacion.IdEstudiante });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error inesperado al actualizar la condición especial.");
                await ObtenerEstudiantes();
                await OnGet();
                return Page();
            }
        }
    }
}
