using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Dashboard.Estudiantes
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IList<EstudiantesResponse> Estudiantes { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
            System.Diagnostics.Debug.WriteLine($"Endpoint: {endpoint}");

            var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Estudiantes = JsonSerializer.Deserialize<List<EstudiantesResponse>>(resultado, opciones);
        }
    }
}
