using Abstracciones.Interfaces.Reglas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Seguridad
{
    public class AuthConfiguracion
    {
        public static IConfiguracion? Instancia { get; set; }

    }
}
