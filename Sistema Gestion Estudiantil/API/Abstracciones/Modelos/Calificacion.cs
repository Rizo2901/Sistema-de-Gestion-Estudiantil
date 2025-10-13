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
        public Guid IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2  { get; set; }
        public string NombreCompleto { get; set; }
        public decimal Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class EstudianteConCalificaciones 
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public List<CalificacionResponse> Calificaciones { get; set; } = new();
    }
}
