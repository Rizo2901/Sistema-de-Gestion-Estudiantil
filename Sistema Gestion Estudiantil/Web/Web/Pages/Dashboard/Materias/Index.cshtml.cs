using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
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

        public async Task OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerMaterias");
            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Materias = JsonSerializer.Deserialize<List<MateriasResponse>>(resultado, opciones);
        }

        public async Task<IActionResult> OnPostCrear(MateriasRequest materia)
        {
            var datos = new
            {
                Nombre = materia.Nombre,
                Descripcion = materia.Descripcion
            };

            string jsonMateria = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearMateria");
            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonMateria, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            using var json = JsonDocument.Parse(resultado);
            var idMateria = json.RootElement.GetProperty("id").GetGuid();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditar(Guid IdMateria, MateriasRequest materia)
        {
            var datos = new
            {
                Nombre = materia.Nombre,
                Descripcion = materia.Descripcion,
                IdMateria = IdMateria
            };

            string jsonMateria = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarMateria");

            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonMateria, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PutAsync(string.Format(endPoint, IdMateria), contenido);
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync(); // ? Actualizar datos
            return Page();
        }

        public async Task<IActionResult> OnPostEliminar(Guid IdMateria)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarMateria");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdMateria));
            respuesta.EnsureSuccessStatusCode();

            await OnGetAsync(); // ? Actualizar datos
            return Page();
        }
    }
}
