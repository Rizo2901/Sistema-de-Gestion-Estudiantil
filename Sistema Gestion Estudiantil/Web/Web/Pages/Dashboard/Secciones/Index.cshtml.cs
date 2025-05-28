using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Dashboard.Secciones
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IList<SeccionesResponse> Secciones { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task OnGet()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerSecciones");

            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Secciones = JsonSerializer.Deserialize<List<SeccionesResponse>>(resultado, opciones);
        }
    }
}
