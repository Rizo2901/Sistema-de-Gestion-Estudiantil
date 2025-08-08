using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IUsuarioFlujo _usuarioFlujo;

        public UsuarioController(IUsuarioFlujo usuarioFlujo)
        {
            _usuarioFlujo = usuarioFlujo;
        }

        [HttpPost("crear-desde-entidad")]
        public async Task<IActionResult> CrearUsuarioDesdeEntidad([FromBody] CrearUsuarioEntidadRequest request)
        {
            try
            {
                var id = await _usuarioFlujo.CrearUsuarioDesdeEntidad(request.TipoEntidad, request.IdEntidad);
                return Ok(new { UsuarioId = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioFlujo.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioFlujo.ObtenerUsuarioPorId(id);
            if (usuario == null)
                return NotFound(new { Error = "Usuario no encontrado." });

            return Ok(usuario);
        }
    }
}