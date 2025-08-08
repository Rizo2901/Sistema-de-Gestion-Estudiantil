using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        public Perfil PerfilUsuario { get; set; }
        private readonly IConfiguracion _confg;
        public DashboardModel(IConfiguracion confg)
        {
            _confg = confg;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            PerfilUsuario = await AuthHelper.ObtenerPerfilAsync(Request);

            if (PerfilUsuario == null)
                return RedirectToPage("/Cuenta/Login");

            return Page();
        }
    }
}
