using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using Abstracciones.Modelos;

namespace Web.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public Perfil PerfilUsuario { get; set; }
        public string RolUsuario { get; set; }
        public Guid IdUsuario { get; set; }

        // Métricas para Administrador
        public int TotalEstudiantes { get; set; }
        public int TotalProfesores { get; set; }
        public List<SeccionesResponse> Secciones { get; set; }
        public List<ActividadResponse> Actividades { get; set; }

        public DashboardModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            PerfilUsuario = await AuthHelper.ObtenerPerfilAsync(Request);
            if (PerfilUsuario == null)
                return RedirectToPage("/Login");

            RolUsuario = PerfilUsuario.TipoEntidad;
            IdUsuario = PerfilUsuario.IdUsuario;

            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHelper.ObtenerToken(Request));

            if (RolUsuario == "Administrador")
            {
                TotalEstudiantes = await ObtenerTotalEstudiantes(cliente);
                TotalProfesores = await ObtenerTotalProfesores(cliente);
                Secciones = await ObtenerSecciones(cliente);
                Actividades = await ObtenerActividades(cliente);
            }

            return Page();
        }

        private async Task<int> ObtenerTotalEstudiantes(HttpClient cliente)
        {
            string url = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerTotalEstudiantes");
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(json);
        }

        private async Task<int> ObtenerTotalProfesores(HttpClient cliente)
        {
            string url = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerTotalProfesores");
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(json);
        }

        private async Task<List<SeccionesResponse>> ObtenerSecciones(HttpClient cliente)
        {
            string url = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerSecciones");
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SeccionesResponse>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<SeccionesResponse>();
        }

        private async Task<List<ActividadResponse>> ObtenerActividades(HttpClient cliente)
        {
            string url = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerActividades");
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ActividadResponse>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ActividadResponse>();
        }
    }
}