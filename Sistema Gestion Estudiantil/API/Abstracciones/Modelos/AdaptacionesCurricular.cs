using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class AdaptacionesCurricular
    {
        public Guid IdAdaptacion { get; set; }
        public Guid IdEstudiante { get; set; }
        public string Tipo { get; set; }
        public string AreaAfectada { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public string Observaciones { get; set; }

        public bool Estado { get; set; } // Activo, Inactivo, En Proceso, etc.
    }

    public class AdaptacionCurricularRequest : AdaptacionesCurricular
    {
        public Guid IdAdaptacion { get; set; }

    }
    public class AdaptacionCurricularResponse : AdaptacionesCurricular
    {
       
        public string Tipo { get; set; }
        public string AreaAfectada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; } 
    }
}
