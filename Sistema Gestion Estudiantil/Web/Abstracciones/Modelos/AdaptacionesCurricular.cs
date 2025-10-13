

namespace Abstracciones.Modelos
{
    public class AdaptacionCurricularBase
    {
        public Guid IdAdaptacion { get; set; }
        public Guid IdEstudiante { get; set; }
        public string Tipo { get; set; }
        public string AreaAfectada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }

    public class AdaptacionCurricularRequest : AdaptacionCurricularBase
    {
        public Guid IdEstudiante { get; set; }
        public string Tipo { get; set; }
        public string AreaAfectada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }


    public class AdaptacionCurricularResponse : AdaptacionCurricularBase
    {
        public string Cedula { get; set; }
        public string NombreEstudiante { get; set; }
    }


    public class EstudianteConAdaptacionesRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }
        public string Adaptaciones { get; set; }
    }

    public class EstudianteConAdaptacionesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public List<AdaptacionItem> Adaptaciones { get; set; }
    }


    public class AdaptacionItem
    {
        public Guid IdEstudiante { get; set; }
        public Guid IdAdaptacion { get; set; }
        public string Tipo { get; set; }
        public string AreaAfectada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }
}