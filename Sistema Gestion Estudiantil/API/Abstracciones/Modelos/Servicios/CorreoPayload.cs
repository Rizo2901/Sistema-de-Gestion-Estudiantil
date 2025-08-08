using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios
{
    public class CorreoPayload
    {
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string CuerpoTextoPlano { get; set; }
        public string CuerpoHtml { get; set; }
        public List<AdjuntoCorreo> Adjuntos { get; set; } = new();
    }

    public class AdjuntoCorreo
    {
        public string NombreArchivo { get; set; }
        public byte[] Contenido { get; set; }
        public string MimeType { get; set; }
    }


}
