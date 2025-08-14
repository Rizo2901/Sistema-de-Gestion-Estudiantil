using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Cuenta
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPostLogout()
        {
            Console.WriteLine("Iniciando proceso de logout...");

            // Eliminar la cookie JWT
            Response.Cookies.Delete("jwt");

            Console.WriteLine("Cookie jwt eliminada. Redirigiendo a /Cuenta/Login");
            return RedirectToPage("/Cuenta/Login");
        }
    }
}