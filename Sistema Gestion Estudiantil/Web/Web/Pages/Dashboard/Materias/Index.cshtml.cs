using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Dashboard.Materias
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;
        public IList<MateriasResponse> Materias { get; set; }



        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }
        public async void OnGet()
        {
            string ednPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerMaterias");
            var cliente = new HttpClient();
            var respuesta = cliente.GetAsync(ednPoint).Result;
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Materias = JsonSerializer.Deserialize<List<MateriasResponse>>(resultado, opciones);
        }
    }
}
