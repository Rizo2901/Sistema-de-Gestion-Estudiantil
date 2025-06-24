using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Profesores
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IList<ProfesorResponse> Profesores { get; set; } = new List<ProfesorResponse>();

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async void OnGet()
        {
            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerProfesores");
            var client = new HttpClient();
            var response = client.GetAsync(endPoint).Result;
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Profesores = JsonSerializer.Deserialize<List<ProfesorResponse>>(result, options);
            Console.WriteLine($"[DEBUG] Profesores cargados: {Profesores.Count}");
        }

        public async Task<IActionResult> OnPostCrear(ProfesorRequest profesor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profesor.Nombre) || string.IsNullOrWhiteSpace(profesor.Apellido1))
                {
                    ModelState.AddModelError("", "Los campos obligatorios no pueden estar vacíos.");
                    return Page();
                }


                var datos = new
                {
                    Nombre = profesor.Nombre,
                    Apellido1 = profesor.Apellido1,
                    Apellido2 = profesor.Apellido2,
                    Direccion = profesor.Direccion,
                    TelefonoCelular = profesor.TelefonoCelular,
                    CorreoElectronico = profesor.CorreoElectronico,
                    IdUsuario = profesor.IdUsuario?.ToString()
                };

                string jsonProfesor = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"[DEBUG] JSON enviado:\n{jsonProfesor}");

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearProfesor");
                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonProfesor, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(endPoint, contenido);
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var idProfesor = JsonSerializer.Deserialize<Guid>(resultado);

                Console.WriteLine($"[DEBUG] Profesor creado con ID: {idProfesor}");

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo al crear el profesor: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el profesor.");
                return Page();
            }
        }



    }
}
