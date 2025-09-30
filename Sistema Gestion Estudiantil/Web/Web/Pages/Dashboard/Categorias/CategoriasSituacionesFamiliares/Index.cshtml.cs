using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Categorias.CategoriasSituacionFamiliar
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IList<CategoriasSituacionFamiliarResponse> Categorias { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerCategoriasSituacionFamiliar");
            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Categorias = JsonSerializer.Deserialize<List<CategoriasSituacionFamiliarResponse>>(resultado, opciones);
        }

        public async Task<IActionResult> OnPostCrear(CategoriasSituacionFamiliarRequest categoria)
        {
            var datos = new
            {
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };

            string jsonCategoria = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearCategoriaSituacionFamiliar");
            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonCategoria, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditar(Guid IdCategoria, CategoriasSituacionFamiliarRequest categoria)
        {
            var datos = new
            {
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                IdCategoria = IdCategoria
            };

            string jsonCategoria = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarCategoriaSituacionFamiliar");

            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonCategoria, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PutAsync(string.Format(endPoint, IdCategoria), contenido);
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync(); // refrescar datos
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(Guid IdCategoria)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarCategoriaSituacionFamiliar");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdCategoria));
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync(); // refrescar datos
            return Page();
        }
    }
}
