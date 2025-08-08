using Abstracciones.Modelos;
using Abstracciones.Modelos.Seguridad;
using System.Security.Claims;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IAutenticacionFlujo
    {
        
        Task<string> AutenticarUsuarioAsync(LoginRequest request);


        Task<Perfil> ObtenerPerfilAsync(ClaimsPrincipal claims);

    }
}