

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

    public class SituacionFamiliarRequest
    {
        public Guid IdEstudiante { get; set; }
        public Guid IdCategoria { get; set; }
        public string Descripcion { get; set; }
    }

    public class SituacionFamiliarResponse : SituacionFamiliarBase
    {
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
    }
}