using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
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

        public async Task<IActionResult> OnPostCrearAsync(string Nombre)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearSeccion");
            var datos = new { Nombre };
            var contenido = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");

            using var cliente = new HttpClient();
            var respuesta = await cliente.PostAsync(endPoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditarAsync(Guid IdSeccion, string Nombre)
        {
            if (string.IsNullOrWhiteSpace(Nombre) || IdSeccion == Guid.Empty)
            {
                return Page();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarSeccion");

            var datos = new
            {
                IdSeccion = IdSeccion,
                Nombre = Nombre
            };

            var contenido = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");

            using var cliente = new HttpClient();
            var respuesta = await cliente.PutAsync(string.Format(endPoint, IdSeccion), contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostEliminarAsync(Guid? IdSeccion)
        {
            if (IdSeccion == null || IdSeccion == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarSeccion");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endPoint, IdSeccion));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

    }
}
