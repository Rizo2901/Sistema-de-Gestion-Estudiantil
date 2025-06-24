using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ActividadBase
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public string? Tipo { get; set; }
    }

    public class ActividadRequest : ActividadBase
    {
        public Guid IdActividad { get; set; }
    }

    public class ActividadResponse : ActividadBase
    {
        public Guid IdActividad { get; set; }
    }
}
