using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CalificacionBase
    {
        public Guid IdCalificacion { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdMateria { get; set; }
        public decimal Calificacion { get; set; }
        public string Comentario { get; set; }

        public DateTime Fecha { get; set; }

    }

    public class CalificacionRequest : CalificacionBase
    {
        public Guid IdCalificacion { get; set; }
    }

    public class CalificacionResponse : CalificacionBase
    {
        public Guid IdCalificacion { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public string NombreEstudiante { get; set; }
        public decimal Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
