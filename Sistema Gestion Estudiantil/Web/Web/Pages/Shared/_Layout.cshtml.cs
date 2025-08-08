using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Web.Pages.Shared
{
    public class LayoutModel : PageModel
    {
        public Perfil? PerfilUsuario { get; private set; }

        public async Task OnGetAsync()
        {
            PerfilUsuario = await AuthHelper.ObtenerPerfilAsync(Request);
        }
    }
}