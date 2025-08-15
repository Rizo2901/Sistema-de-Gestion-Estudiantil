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
            var entidad = await _usuarioDA.ObtenerDatosEntidadAsync(tipoEntidad, idEntidad);
            if (entidad == null)
                throw new Exception("Entidad no encontrada.");

            var rol = await _rolFlujo.ObtenerRolePorNombre(tipoEntidad); 
            if (rol == null)
                throw new Exception($"No se encontró rol activo para entidad '{tipoEntidad}'.");

            var nombreUsuario = UsuarioHelper.GenerarNombreUsuario(entidad);
            var contrasena = UsuarioHelper.GenerarContrasenaSegura();
            var hash = BCrypt.Net.BCrypt.HashPassword(contrasena);

            var usuario = new UsuariosRequest
            {
                NombreUsuario = nombreUsuario,
                Contrasena = hash,
                CorreoElectronico = entidad.CorreoElectronico,
                IdRol = rol.IdRol,
                TipoEntidad = tipoEntidad
            };

            var usuarioId = await _usuarioDA.CrearUsuario(usuario);

            await _usuarioDA.ActualizarIdUsuarioEnEntidad(tipoEntidad, idEntidad, usuarioId);

            
            var correo = new CorreoPayload
            {

    Destinatario = usuario.CorreoElectronico,
    Asunto = "Credenciales Institucionales - S.G.E",
    CuerpoHtml = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }}
        /* Header estilo SG.E */
        .header {{
            background: linear-gradient(90deg, #dc3545, #b02a37);
            color: white;
            padding: 15px;
            text-align: center;
        }}
        .header img {{
            vertical-align: middle;
            margin-right: 10px;
        }}
        .header-title {{
            font-size: 18px;
            font-weight: bold;
        }}
        /* Contenido */
        .content {{
            padding: 20px;
            color: #212529;
        }}
        .content h2 {{
            color: #dc3545;
            margin-bottom: 10px;
        }}
        .credentials {{
            background-color: #f1f1f1;
            padding: 15px;
            border-radius: 6px;
            margin: 20px 0;
        }}
        .credentials p {{
            margin: 5px 0;
            font-size: 15px;
        }}
        /* Footer estilo SG.E */
        .footer {{
            background-color: #212529;
            color: white;
            text-align: center;
            padding: 15px;
            font-size: 12px;
        }}
        .footer a {{
            color: white;
            margin: 0 5px;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <!-- Header -->
        <div class='header'>
            <img src='https://tuservidor.com/static/images/icons8-colegio-100.svg' alt='Logo' width='40' height='40' />
            <span class='header-title'>Sistema de Gestión Estudiantil (S.G.E)</span>
        </div>

        <!-- Contenido -->
        <div class='content'>
            <h2>Bienvenido(a)</h2>
            <p>Se han creado tus credenciales institucionales para acceder al sistema de la <strong>Escuela Carolina Dent</strong>.</p>
            
            <div class='credentials'>
                <p><strong>Usuario:</strong> {usuario.NombreUsuario}</p>
                <p><strong>Contraseña:</strong> {contrasena}</p>
            </div>
            
            <p>Por seguridad, te recomendamos iniciar sesión y cambiar tu contraseña en tu primer acceso.</p>
            <p><a href='https://sgeesccarolinadent-web-aqbqaqefhfb3atee.eastus2-01.azurewebsites.net/Cuenta/Login' style='color: #dc3545; font-weight: bold;'>Iniciar Sesión en S.G.E</a></p>
        </div>

        <!-- Footer -->
        <div class='footer'>
            <p>Este es un mensaje automático, por favor no respondas a este correo.</p>
            <p>Contacto: <a href='mailto:sgeescuelacarolinaden@gmail.com'>sgeescuelacarolinaden@gmail.com</a></p>
            <p>© 2025 Sistema de Gestión Estudiantil (S.G.E), Inc</p>
        </div>
    </div>
</body>
</html>"
};


            await _correoHelper.EnviarCorreoAsync(correo);

            return usuarioId;
        }
    }
}