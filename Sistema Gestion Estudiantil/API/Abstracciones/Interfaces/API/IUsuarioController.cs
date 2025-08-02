using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        // Coincide con Usuario.CrearUsuario
        Task<IActionResult> CrearUsuario(UsuariosResponse usuario);

        // Coincide con Usuario.ActualizarUsuario
        Task<IActionResult> EditarUsuario(Guid Id, UsuariosResponse usuario);

        // Coincide con Usuario.ObtenerUsuarioPorId
        Task<IActionResult> ObtenerUsuarioPorId(Guid Id);

        // Coincide con Usuario.ConsultarUsuarios
        Task<IActionResult> ObtenerUsuarios();

        // Coincide con Usuario.EliminarUsuario
        Task<IActionResult> EliminarUsuario(Guid Id);

        // Coincide con Usuario.CambiarEstadoUsuario (activar)
        Task<IActionResult> ActivarUsuario(Guid Id);

        // Coincide con Usuario.CambiarEstadoUsuario (desactivar)
        Task<IActionResult> DesactivarUsuario(Guid Id);

        // Coincide con Usuario.ActualizarContrasena (sin verificación)
        Task<IActionResult> CambiarContrasena(Guid Id, string nuevaContrasena);

        // Coincide con lógica extendida en backend (verificación previa)
        Task<IActionResult> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual);
    }
}