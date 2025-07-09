using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class EstudianteMedicamentoBase
    {
        public Guid IdEstudianteMedicamento { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdMedicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }

    public class EstudianteMedicamentoRequest : EstudianteMedicamentoBase 
    {
        public Guid IdEstudianteMedicamento { get; set; }

    }

    public class EstudianteMedicamentoResponse : EstudianteMedicamentoBase
    {
        public string NombreMedicamento { get; set; }
        public string DescripcionMedicamento { get; set; }
        public string NombreEstudiante { get; set; }
        public string Apellido1Estudiante { get; set; }
        public string Apellido2Estudiante { get; set; }

    }
}
