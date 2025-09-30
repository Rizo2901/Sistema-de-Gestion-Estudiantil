using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Categorias.CategoriasDiscapacidad
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;
        public IList<CategoriasDiscapacidadResponse> Discapacidades { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerCategoriasDiscapacidades");
            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Discapacidades = JsonSerializer.Deserialize<List<CategoriasDiscapacidadResponse>>(resultado, opciones);
        }

        public async Task<IActionResult> OnPostCrear(MateriasRequest discapacidad)
        {
            var datos = new
            {
                Nombre = discapacidad.Nombre,
                Descripcion = discapacidad.Descripcion
            };

            string json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearCategoriaDiscapacidad");

            using var cliente = new HttpClient();
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditar(Guid IdDiscapacidad, MateriasRequest discapacidad)
        {
            var datos = new
            {
                Nombre = discapacidad.Nombre,
                Descripcion = discapacidad.Descripcion,
                IdDiscapacidad = IdDiscapacidad
            };

            string json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarCategoriaDiscapacidad");

            using var cliente = new HttpClient();
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PutAsync(string.Format(endPoint, IdDiscapacidad), contenido);
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(Guid IdDiscapacidad)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarCategoriaDiscapacidad");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdDiscapacidad));
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync();
            return Page();
        }
    }
}
