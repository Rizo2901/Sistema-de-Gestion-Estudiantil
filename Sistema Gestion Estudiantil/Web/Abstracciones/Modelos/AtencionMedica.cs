using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class AtencionMedicaBase
    {
        public Guid IdAtencionMedica { get; set; }
        public Guid IdEstudiante { get; set; }
        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }

    }

    public class AtencionMedicaRequest : AtencionMedicaBase 
    { 
        public Guid IdAtencionMedica { get; set;}
    
    }

    public class AtencionMedicaResponse : AtencionMedicaBase
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }

        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
    }

    public class EstudianteConAtencionesMedicas
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public List<AtencionMedicaResponse> AtencionesMedicas { get; set; } = new();
    }
}
