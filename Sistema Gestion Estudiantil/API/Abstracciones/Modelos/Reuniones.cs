

namespace Abstracciones.Modelos
{
    public class ReunionesBase
    {
        public Guid IdReunion { get; set; }
        public Guid IdEstudiante { get; set; }

        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
    }

    public class ReunionesRequest : ReunionesBase
    {
        public Guid IdEstudiante { get; set; }
    }

    public class ReunionesResponse : ReunionesBase
    {
        public Guid IdReunion { get; set; }
        public Guid IdEstudiante { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }
    }

}
