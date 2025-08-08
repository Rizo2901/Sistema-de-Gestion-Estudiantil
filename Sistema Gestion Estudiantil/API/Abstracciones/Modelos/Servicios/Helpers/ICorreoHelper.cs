using Abstracciones.Modelos.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Helpers
{
    public interface ICorreoHelper
    {
        Task EnviarCorreoAsync(CorreoPayload payload);
    }
}
