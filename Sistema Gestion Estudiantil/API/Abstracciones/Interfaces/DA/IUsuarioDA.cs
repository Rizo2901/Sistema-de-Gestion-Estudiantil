using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
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
        Task<Guid> CambiarEstadoUsuario(Guid Id, bool estado);

        // Coincide con Usuario.ActualizarContrasena
        Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena);

        // Lógica extendida en backend: verificación previa
        Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual);

        Task<UsuariosResponse> ObtenerPorNombreOEmail(string identificador);
    }
}
