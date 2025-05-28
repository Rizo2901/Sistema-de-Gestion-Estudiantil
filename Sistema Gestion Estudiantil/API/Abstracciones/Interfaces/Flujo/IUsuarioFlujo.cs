using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<IEnumerable<UsuariosResponse>> ObtenerUsuarios();
        Task<UsuariosResponse> ObtenerUsuarioPorId(Guid Id);
        Task<Guid> CrearUsuario(UsuariosRequest usuario);
        Task<Guid> EditarUsuario(Guid Id, UsuariosRequest usuario);
        Task<Guid> EliminarUsuario(Guid Id);

        Task<Guid> ActivarUsuario(Guid Id);

        Task<Guid> DesactivarUsuario(Guid Id);

        Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena);

        Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual);

    }
}
