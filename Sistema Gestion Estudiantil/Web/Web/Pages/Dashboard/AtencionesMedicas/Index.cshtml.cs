using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.AtencionesMedicas
{
    public class IndexModel : PageModel
    {
        public List<SelectListItem> Estudiantes { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string estudianteSeleccionado { get; set; }




        private readonly IConfiguracion _confg;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public IList<EstudianteConAtencionesMedicas> EstudianteConAtenciones { get; set; } = new List<EstudianteConAtencionesMedicas>();

        [BindProperty]
        public AtencionMedicaRequest atencion { get; set; }
        public IndexModel(IConfiguracion confg, ILogger<IndexModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public async Task OnGet()
        {

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerAtencionesAgrupadas");
            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            EstudianteConAtenciones = JsonSerializer.Deserialize<List<EstudianteConAtencionesMedicas>>(resultado, opciones);
            await ObtenerEstudiantes();



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


       


        public async Task<IActionResult> OnPostCrearAsync()
        {
            // Validaciones defensivas
            if (!Guid.TryParse(estudianteSeleccionado, out var IdEstudiante))
            {
                ModelState.AddModelError("estudianteSeleccionado", "Debe seleccionar un estudiante válido.");
                await ObtenerEstudiantes();
                return Page();
            }

           

            var datos = new
            {
                IdEstudiante = IdEstudiante,
                Fecha = atencion.Fecha,
                Diagnostico = atencion.Diagnostico,
                Tratamiento = atencion.Tratamiento
            };

            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            using var cliente = new HttpClient();
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearAtencionMedica");
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                string contenidoError = await respuesta.Content.ReadAsStringAsync();
                _logger.LogError("[ERROR] Fallo al crear la atencion medica: {Error}", contenidoError);
                ModelState.AddModelError("", "Error al crear la condición especial.");
                await ObtenerEstudiantes();
                return Page();
            }

            _logger.LogInformation("Atencion creada exitosamente para estudiante {IdEstudiante}", IdEstudiante);

            await OnGet();
            return Page();
        }


        public async Task<IActionResult> OnPostEliminarAsync(Guid? IdAtencionMedica)
        {
            if (IdAtencionMedica == null || IdAtencionMedica == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarAtencionMedica");
            string urlFinal = string.Format(endPoint, IdAtencionMedica);
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

            Console.WriteLine($"[DEBUG] Atencion eliminada con ID: {IdAtencionMedica}");
            await OnGet();
            return Page();
        }



        public async Task<IActionResult> OnPostEditarAsync(Guid IdAtencionMedica, AtencionMedicaRequest atencion)
        {
            try
            {
                atencion.IdAtencionMedica = IdAtencionMedica;

               
                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarAtencionMedica");

                var datos = new
                {
                    IdAtencionMedica = IdAtencionMedica,
                    IdEstudiante = atencion.IdEstudiante,
                    Fecha = atencion.Fecha,
                    Diagnostico = atencion.Diagnostico,
                    Tratamiento = atencion.Tratamiento
                };

                var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                _logger.LogDebug("[DEBUG] JSON enviado para edición:\n{Json}", json);

                using var cliente = new HttpClient();
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdAtencionMedica), contenido);

                if (!respuesta.IsSuccessStatusCode)
                {
                    string contenidoError = await respuesta.Content.ReadAsStringAsync();
                    _logger.LogError("[ERROR] Fallo al editar atencion medica: {Error}", contenidoError);
                    ModelState.AddModelError("", "Error al editar la atencion medica.");
                    await ObtenerEstudiantes();
                    await OnGet();
                    return Page();
                }

                _logger.LogInformation("Atencion medica editada exitosamente. ID: {IdCondicion}", IdAtencionMedica);

                // Validación defensiva antes de redirigir
                if (atencion.IdEstudiante == Guid.Empty)
                {
                    _logger.LogWarning("IdEstudiante vacío tras edición. Redirigiendo sin filtro.");
                    return RedirectToPage("/Dashboard/AtencionesMedicas/Index");
                }

                return RedirectToPage("/Dashboard/AtencionesMedicas/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EXCEPTION] Error al actualizar la atencion medica.");
                ModelState.AddModelError("", "Error inesperado al actualizar la atencion medica.");
                await ObtenerEstudiantes();
                await OnGet();
                return Page();
            }
        }


       

    }
}
