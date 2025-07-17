

namespace Abstracciones.Modelos
{
    public class SituacionFamiliarBase
    {
        public Guid IdSituacion { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }

    public class SituacionFamiliarRequest : SituacionFamiliarBase
    {
        public Guid IdEstudiante { get; set; }
    }

    public class SituacionFamiliarResponse : SituacionFamiliarBase
    {
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
    }

    public class EstudianteConSituacionesRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public string Situaciones { get; set; } // JSON string
    }

    public class EstudianteConSituacionesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public List<SituacionItem> Situaciones { get; set; }
    }

    public class SituacionItem
    {
        public Guid IdSituacion { get; set; }
        public Guid IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }

        public string Descripcion { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}