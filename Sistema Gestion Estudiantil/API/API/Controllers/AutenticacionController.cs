using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase, IAutenticacionController
    {
        private readonly IAutenticacionFlujo _autenticacionFlujo;

        public AutenticacionController(IAutenticacionFlujo autenticacionFlujo)
        {
            _autenticacionFlujo = autenticacionFlujo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Autenticar([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _autenticacionFlujo.AutenticarUsuarioAsync(request);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized("Credenciales inválidas.");

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("perfil")]
        [Authorize]
        public async Task<IActionResult> ObtenerPerfil()
        {
            try
            {
                var perfil = await _autenticacionFlujo.ObtenerPerfilAsync(User);

                if (perfil == null)
                    return NotFound("Perfil no encontrado.");

                return Ok(perfil);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno", detalle = ex.Message });
            }
        }
    }
}