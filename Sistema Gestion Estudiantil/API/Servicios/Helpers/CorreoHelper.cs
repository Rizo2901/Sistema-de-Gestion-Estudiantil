using System.Net;
using System.Net.Mail;
using Abstracciones.Modelos.Servicios;
using Abstracciones.Modelos.Servicios.Helpers;
using Microsoft.Extensions.Configuration;

namespace Servicios.Helpers
{
    public class CorreoHelper : ICorreoHelper
    {
        private readonly IConfiguration _config;

        public CorreoHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCorreoAsync(CorreoPayload payload)
        {
            var smtpHost = _config["Correo:SmtpHost"];
            var smtpPort = int.Parse(_config["Correo:SmtpPort"]);
            var smtpUser = _config["Correo:Usuario"];
            var smtpPass = _config["Correo:Clave"];

            using var cliente = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mensaje = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = payload.Asunto,
                Body = string.IsNullOrWhiteSpace(payload.CuerpoHtml) ? payload.CuerpoTextoPlano : payload.CuerpoHtml,
                IsBodyHtml = !string.IsNullOrWhiteSpace(payload.CuerpoHtml)
            };

            mensaje.To.Add(payload.Destinatario);

            foreach (var adjunto in payload.Adjuntos)
            {
                var stream = new MemoryStream(adjunto.Contenido);
                var attachment = new Attachment(stream, adjunto.NombreArchivo, adjunto.MimeType);
                mensaje.Attachments.Add(attachment);
            }

            await cliente.SendMailAsync(mensaje);
        }
    }
}