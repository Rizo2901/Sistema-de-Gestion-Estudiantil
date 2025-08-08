using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Seguridad
{
    public static class AuthHelper
    {
        public static string? ObtenerToken(HttpRequest request)
            => request.Cookies["jwt"];

        public static async Task<Perfil?> ObtenerPerfilAsync(HttpRequest request, string? perfilEndpoint = null)
        {
            var token = ObtenerToken(request);
            if (string.IsNullOrEmpty(token)) return null;

            perfilEndpoint ??= AuthConfiguracion.Instancia?.ObtenerMetodo("ApiEndPoints", "ObtenerPerfil");
            if (string.IsNullOrEmpty(perfilEndpoint)) return null;

            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var respuesta = await cliente.GetAsync(perfilEndpoint);
            if (!respuesta.IsSuccessStatusCode) return null;

            var resultado = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Perfil>(resultado, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static bool EstaLogeado(HttpRequest request)
            => !string.IsNullOrEmpty(request.Cookies["jwt"]);
    }

}
