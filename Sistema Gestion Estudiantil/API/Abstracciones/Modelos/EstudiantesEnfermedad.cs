using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class EstudiantesEnfermedadBase
    {
        public Guid IdEstudiante { get; set; }
        public Guid IdEnfermedad { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
        public string Gravedad { get; set; }
        public string Observaciones { get; set; }
    }
    public class EstudiantesEnfermedadRequest : EstudiantesEnfermedadBase
    {
        public Guid IdEstudiante { get; set; }
        public Guid IdEnfermedad { get; set; }
    }
    public class EstudiantesEnfermedadResponse : EstudiantesEnfermedadBase
    {
        public string NombreEnfermedad { get; set; }
        public string DescripcionEnfermedad { get; set; }
        public string NombreEstudiante { get; set; }
        public string Apellido1Estudiante { get; set; }
        public string Apellido2Estudiante { get; set; }
    }
}
