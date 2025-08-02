using Abstracciones.Modelos;
using System.Security.Claims;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IAutenticacionFlujo
    {
        /// <summary>
        /// Autentica al usuario usando nombre de usuario o correo electrónico y retorna un JWT.
        /// </summary>
        Task<string> AutenticarUsuarioAsync(LoginRequest request);


        Task<Perfil> ObtenerPerfilAsync(ClaimsPrincipal claims);

    }
}