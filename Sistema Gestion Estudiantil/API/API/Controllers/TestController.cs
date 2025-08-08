using Abstracciones.Modelos.Servicios;
using Abstracciones.Modelos.Servicios.Helpers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ICorreoHelper _correoHelper;

    public TestController(ICorreoHelper correoHelper)
    {
        _correoHelper = correoHelper;
    }

    [HttpPost("correo")]
    public async Task<IActionResult> ProbarEnvioCorreo([FromBody] string destinatario)
    {
        var correo = new CorreoPayload
        {
            Destinatario = destinatario,
            Asunto = "Prueba de correo institucional",
            CuerpoHtml = @"
                <h2>Prueba de envío</h2>
                <p>Este es un correo de prueba desde el backend institucional.</p>
                <p>Si lo recibís correctamente, el helper está funcionando.</p>"
        };

        try
        {
            await _correoHelper.EnviarCorreoAsync(correo);
            return Ok("Correo enviado correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al enviar correo: {ex.Message}");
        }
    }
}