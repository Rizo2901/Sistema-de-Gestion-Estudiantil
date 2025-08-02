using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;


namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuariosRequest usuario)
        {
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Guid> EditarUsuario(Guid Id, UsuariosRequest usuario)
        {
            // Si se envía una nueva contraseña, la hasheamos
            if (!string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            }

            return await _usuarioDA.EditarUsuario(Id, usuario);
        }

        public async Task<IEnumerable<UsuariosResponse>> ObtenerUsuarios()
        {
            return await _usuarioDA.ObtenerUsuarios();
        }

        public async Task<UsuariosResponse> ObtenerUsuarioPorId(Guid Id)
        {
            return await _usuarioDA.ObtenerUsuarioPorId(Id);
        }

        public async Task<Guid> EliminarUsuario(Guid Id)
        {
            return await _usuarioDA.EliminarUsuario(Id);
        }

        public async Task<Guid> ActivarUsuario(Guid Id)
        {
            return await _usuarioDA.CambiarEstadoUsuario(Id, true);
        }

        public async Task<Guid> DesactivarUsuario(Guid Id)
        {
            return await _usuarioDA.CambiarEstadoUsuario(Id, false);
        }

        public async Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
            return await _usuarioDA.CambiarContrasena(Id, hash);
        }

        public async Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual)
        {
            var usuario = await _usuarioDA.ObtenerUsuarioPorId(Id);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            bool esValida = BCrypt.Net.BCrypt.Verify(contrasenaActual, usuario.Contrasena);
            if (!esValida)
                throw new Exception("La contraseña actual es incorrecta.");

            string hash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
            return await _usuarioDA.CambiarContrasena(Id, hash);
        }
    }
}