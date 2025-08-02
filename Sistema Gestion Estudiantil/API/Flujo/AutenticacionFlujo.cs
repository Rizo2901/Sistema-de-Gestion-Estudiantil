using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
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

        /// <summary>
        /// Autentica al usuario por identificador único (correo o nombre de usuario).
        /// Retorna token JWT con claims estructurados.
        /// </summary>
        public async Task<string> AutenticarUsuarioAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Identificador) || string.IsNullOrWhiteSpace(request.Contrasena))
                throw new Exception("Credenciales incompletas.");

            var usuario = await _usuarioDA.ObtenerPorNombreOEmail(request.Identificador);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.ContrasenaHash))
                throw new Exception("Credenciales inválidas.");

            var claims = new[]
            {
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim("Rol", usuario.IdRol.ToString()),
                new Claim("Entidad", usuario.TipoEntidad ?? "")
            };

            var key = Encoding.ASCII.GetBytes(_config["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

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