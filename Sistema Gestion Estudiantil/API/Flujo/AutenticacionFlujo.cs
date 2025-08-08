using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Seguridad;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Flujo
{
    public class AutenticacionFlujo : IAutenticacionFlujo
    {
        private readonly IUsuarioDA _usuarioDA;
        private readonly IConfiguration _config;

        public AutenticacionFlujo(IUsuarioDA usuarioDA, IConfiguration config)
        {
            _usuarioDA = usuarioDA;
            _config = config;
        }


        public async Task<string> AutenticarUsuarioAsync(LoginRequest request)
        {
            Console.WriteLine("Iniciando autenticación...");

            if (string.IsNullOrWhiteSpace(request.Identificador) || string.IsNullOrWhiteSpace(request.Contrasena))
                throw new Exception("Credenciales incompletas.");

            var usuario = await _usuarioDA.ObtenerPorNombreOEmail(request.Identificador);
            Console.WriteLine(usuario == null ? "Usuario no encontrado" : $"Usuario encontrado: {usuario.NombreUsuario}");

            if (usuario == null)
                throw new Exception("Credenciales inválidas (usuario no existe).");

            if (usuario.ContrasenaHash == null)
            {
                Console.WriteLine("El hash de la contraseña es nulo.");
                throw new Exception("El hash de la contraseña es nulo.");
            }

            bool passwordOk = BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.ContrasenaHash);
            Console.WriteLine($"Verificación de contraseña: {passwordOk}");

            if (!passwordOk)
                throw new Exception("Credenciales inválidas (contraseña incorrecta).");

            Console.WriteLine($"IdUsuario: {usuario.IdUsuario}");
            Console.WriteLine($"NombreUsuario: {usuario.NombreUsuario}");
            Console.WriteLine($"IdRol: {usuario.IdRol}");
            Console.WriteLine($"TipoEntidad: {usuario.TipoEntidad}");

            if (usuario.IdUsuario == Guid.Empty)
                throw new Exception("Usuario sin identificador válido.");

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new Exception("Usuario sin nombre asignado.");

            if (usuario.IdRol == null)
                throw new Exception("Usuario sin rol asignado.");

            var claims = new[]
            {
        new Claim("IdUsuario", usuario.IdUsuario.ToString()),
        new Claim(ClaimTypes.Name, usuario.NombreUsuario ?? ""),
        new Claim("Rol", usuario.IdRol.ToString() ?? "0"),
        new Claim("Entidad", usuario.TipoEntidad ?? "")
    };

            var secret = _config["JwtSettings:SecretKey"];
            Console.WriteLine($"SecretKey: {(string.IsNullOrWhiteSpace(secret) ? "VACÍA O NULA" : "OK")}");

            if (string.IsNullOrWhiteSpace(secret))
                throw new Exception("Configuración de clave JWT ausente.");

            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Console.WriteLine("Token generado correctamente.");
            return tokenHandler.WriteToken(token);
        }

        public async Task<Perfil> ObtenerPerfilAsync(ClaimsPrincipal claims)
        {
            var id = claims.FindFirst("IdUsuario")?.Value;

            if (string.IsNullOrEmpty(id))
                throw new UnauthorizedAccessException("Token inválido.");

            var usuario = await _usuarioDA.ObtenerUsuarioPorId(Guid.Parse(id));

            return new Perfil
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                CorreoElectronico = usuario.CorreoElectronico,
                FotoPerfil = usuario.FotoPerfil,
                RolNombre = usuario.RolNombre,
                RolDescripcion = usuario.RolDescripcion,
                TipoEntidad = usuario.TipoEntidad,
                NombreEntidad = usuario.NombreEntidad
            };
        }
    }
}