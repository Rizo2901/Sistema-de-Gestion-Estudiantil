using Abstracciones.Modelos;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAutenticacionController
    {
        Task<IActionResult> Autenticar(LoginRequest request);

        Task<IActionResult> ObtenerPerfil();
    }
}