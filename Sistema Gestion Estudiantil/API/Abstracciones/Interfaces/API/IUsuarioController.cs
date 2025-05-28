using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> CrearUsuario(UsuariosResponse usuario);

        Task<IActionResult> EditarUsuario(Guid Id, UsuariosResponse usuario);

        Task<IActionResult> ObtenerUsuarioPorId(Guid Id);

        Task<IActionResult> ObtenerUsuarios();
        Task<IActionResult> EliminarUsuario(Guid Id);
        Task<IActionResult> ActivarUsuario(Guid Id);
        Task<IActionResult> DesactivarUsuario(Guid Id);
        Task<IActionResult> CambiarContrasena(Guid Id, string nuevaContrasena);
        Task<IActionResult> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual);


    }
}
