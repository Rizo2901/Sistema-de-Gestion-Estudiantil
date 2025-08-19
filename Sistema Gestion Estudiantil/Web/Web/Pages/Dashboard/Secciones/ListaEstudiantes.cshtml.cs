using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Web.Pages.Dashboard.Secciones
{
    public class ListaEstudiantesModel : PageModel
    {
        private readonly IConfiguracion _confg;
        private readonly ILogger<ListaEstudiantesModel> _logger;

        public IList<EstudianteSeccionesRespone> EstudianteSecciones { get; set; } = new List<EstudianteSeccionesRespone>();
        public string NombreSeccion { get; set; } = string.Empty;

        public ListaEstudiantesModel(IConfiguracion confg, ILogger<ListaEstudiantesModel> logger)
        {
            _confg = confg;
            _logger = logger;
        }

        public async Task OnGetAsync(Guid IdSeccion, string nombre)
        {
            try
            {
                // Guardar el nombre de la sección para mostrarlo en la vista
                NombreSeccion = nombre;

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantesSeccion");

                using var cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(string.Format(endPoint, IdSeccion));
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                EstudianteSecciones = JsonSerializer.Deserialize<List<EstudianteSeccionesRespone>>(resultado, opciones)
                                      ?? new List<EstudianteSeccionesRespone>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo estudiantes de la sección {Seccion}", IdSeccion);
                EstudianteSecciones = new List<EstudianteSeccionesRespone>();
            }
        }
    }
}
