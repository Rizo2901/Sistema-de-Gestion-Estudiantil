using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAutenticacionController
    {
        /// <summary>
        /// Endpoint para autenticar usuario y retornar token.
        /// </summary>
        Task<IActionResult> Autenticar(LoginRequest request);

        Task<IActionResult> ObtenerPerfil();
    }
}