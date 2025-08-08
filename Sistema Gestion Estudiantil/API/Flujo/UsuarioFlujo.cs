using Abstracciones.Helpers;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios;
using Abstracciones.Modelos.Servicios.Helpers;


namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;
        private readonly ICorreoHelper _correoHelper;
        private  IRolFlujo _rolFlujo;

        public UsuarioFlujo(IUsuarioDA usuarioDA, ICorreoHelper correoHelper, IRolFlujo rolFlujo)
        {
            _usuarioDA = usuarioDA;
            _correoHelper = correoHelper;
            _rolFlujo = rolFlujo;
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


        public async Task<Guid> CrearUsuarioDesdeEntidad(string tipoEntidad, Guid idEntidad)
        {
            // 🔍 1. Obtener datos de la entidad (Profesor, Estudiante, etc.)
            var entidad = await _usuarioDA.ObtenerDatosEntidadAsync(tipoEntidad, idEntidad);
            if (entidad == null)
                throw new Exception("Entidad no encontrada.");

            // 🧠 2. Resolver el rol automáticamente por nombre
            var rol = await _rolFlujo.ObtenerRolePorNombre(tipoEntidad); // ← ¿es el flujo?
            if (rol == null)
                throw new Exception($"No se encontró rol activo para entidad '{tipoEntidad}'.");

            // 🔐 3. Generar credenciales
            var nombreUsuario = UsuarioHelper.GenerarNombreUsuario(entidad);
            var contrasena = UsuarioHelper.GenerarContrasenaSegura();
            var hash = BCrypt.Net.BCrypt.HashPassword(contrasena);

            // 🧩 4. Crear el usuario
            var usuario = new UsuariosRequest
            {
                NombreUsuario = nombreUsuario,
                Contrasena = hash,
                CorreoElectronico = entidad.CorreoElectronico,
                IdRol = rol.IdRol,
                TipoEntidad = tipoEntidad
            };

            var usuarioId = await _usuarioDA.CrearUsuario(usuario);

            // 🔗 5. Vincular usuario con la entidad
            await _usuarioDA.ActualizarIdUsuarioEnEntidad(tipoEntidad, idEntidad, usuarioId);

            // 📧 6. Enviar correo institucional
            var correo = new CorreoPayload
            {
                Destinatario = usuario.CorreoElectronico,
                Asunto = "Credenciales institucionales",
                CuerpoHtml = $@"
            <h2>Bienvenido</h2>
            <p>Tu usuario es <strong>{usuario.NombreUsuario}</strong></p>
            <p>Tu contraseña es <strong>{contrasena}</strong></p>
            <p>Accedé al sistema y actualizá tu contraseña.</p>"
            };

            await _correoHelper.EnviarCorreoAsync(correo);

            return usuarioId;
        }
    }
}