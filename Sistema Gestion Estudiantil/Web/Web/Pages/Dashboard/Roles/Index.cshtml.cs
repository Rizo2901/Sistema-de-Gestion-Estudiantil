using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Roles
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IList<RolesResponse> Roles { get; set; }

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerRoles");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            if (!respuesta.IsSuccessStatusCode)
            {
                return StatusCode((int)respuesta.StatusCode, "Error al obtener los roles.");
            }
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Roles = JsonSerializer.Deserialize<List<RolesResponse>>(resultado, opciones);
            return Page();
        }

        public async Task<IActionResult> OnPostCrear (RolesRequest rol)
        {
            var datos = new
            {
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            };
            string jsonRol = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
           

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearRol");
            using var cliente = new HttpClient();
            var contenido = new StringContent(jsonRol, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            using var json = JsonDocument.Parse(resultado);
            var idRol = json.RootElement.GetProperty("id").GetGuid();
            return RedirectToPage();
        }

        public async Task <IActionResult> OnPostEliminarRol(Guid IdRol)
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarRol");
            using var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endPoint, IdRol));


            await OnGetAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostEditarRol(Guid IdRol, RolesRequest rol)
        {
            var datos = new
                {  
                    Nombre = rol.Nombre,
                    Descripcion = rol.Descripcion,
                    IdRol = IdRol
                    
                };

                string jsonRol = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
               

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarRol");
                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonRol, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdRol), contenido);
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(resultado);
                var idActualizado = json.RootElement.GetGuid();

                await OnGetAsync(); // Refrescar lista
                return Page();
            
        }

    }
}
