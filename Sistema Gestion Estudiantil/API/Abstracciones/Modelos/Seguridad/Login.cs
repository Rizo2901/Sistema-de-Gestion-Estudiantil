using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Seguridad
{
    public class LoginRequest
    {
        public string Identificador { get; set; } 
        public string Contrasena { get; set; }
    }
}
