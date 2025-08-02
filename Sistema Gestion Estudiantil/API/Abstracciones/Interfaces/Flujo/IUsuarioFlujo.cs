using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        // Coincide con Usuario.ConsultarUsuarios
        Task<IEnumerable<UsuariosResponse>> ObtenerUsuarios();

        // Coincide con Usuario.ObtenerUsuarioPorId
        Task<UsuariosResponse> ObtenerUsuarioPorId(Guid Id);

        // Coincide con Usuario.CrearUsuario
        Task<Guid> CrearUsuario(UsuariosRequest usuario);

        // Coincide con Usuario.ActualizarUsuario
        Task<Guid> EditarUsuario(Guid Id, UsuariosRequest usuario);

        // Coincide con Usuario.EliminarUsuario
        Task<Guid> EliminarUsuario(Guid Id);

        // Coincide con Usuario.CambiarEstadoUsuario
        Task<Guid> ActivarUsuario(Guid Id);
        Task<Guid> DesactivarUsuario(Guid Id);

        // Coincide con Usuario.ActualizarContrasena
        Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena);

        // Lógica extendida en backend: verificación previa
        Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual);
    }
}