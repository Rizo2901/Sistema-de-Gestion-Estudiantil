using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Dashboard.Actividades
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IList<ActividadResponse> Actividades { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerActividades");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            if (!respuesta.IsSuccessStatusCode)
            {
                return StatusCode((int)respuesta.StatusCode, "Error al obtener las actividades.");
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Actividades = JsonSerializer.Deserialize<List<ActividadResponse>>(resultado, opciones);
            return Page();
        }
    }
}
