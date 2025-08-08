using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Cuenta
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguracion _confg;

        [BindProperty]
        public LoginRequest LoginRequest { get; set; }

        public LoginModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            Console.WriteLine("Iniciando proceso de login...");

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "Autenticacion");
            Console.WriteLine($"Endpoint de login: {endPoint}");

            using var cliente = new HttpClient();
            var contenido = new StringContent(JsonSerializer.Serialize(LoginRequest), Encoding.UTF8, "application/json");
            Console.WriteLine($" Payload enviado: {JsonSerializer.Serialize(LoginRequest)}");

            var respuesta = await cliente.PostAsync(endPoint, contenido);
            Console.WriteLine($" Código de respuesta: {(int)respuesta.StatusCode}");

            if (!respuesta.IsSuccessStatusCode)
            {
                Console.WriteLine(" Login fallido. Credenciales inválidas.");
                ModelState.AddModelError("", "Credenciales inválidas.");
                return Page();
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();
            Console.WriteLine($" Respuesta recibida: {resultado}");

            var json = JsonDocument.Parse(resultado);

            if (!json.RootElement.TryGetProperty("token", out var tokenElement))
            {
                Console.WriteLine(" Token no encontrado en la respuesta.");
                ModelState.AddModelError("", "Error al procesar el token.");
                return Page();
            }

            var token = tokenElement.GetString();
            Console.WriteLine($"Token recibido: {token}");

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            Console.WriteLine("Redirigiendo a /Dashboard/Dashboard");
            return RedirectToPage("/Dashboard/Dashboard");
        }
    }
}