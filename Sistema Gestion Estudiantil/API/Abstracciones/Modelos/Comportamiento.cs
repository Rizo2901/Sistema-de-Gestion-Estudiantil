using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ComportamientoBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public string? Observacion { get; set; }

        public string? AccionTomada { get; set; }
    }

    public class ComportamientoRequest : ComportamientoBase
    {
        public Guid? IdComportamiento { get; set; }
    }

    public class ComportamientoResponse : ComportamientoBase
    {
        public Guid IdComportamiento { get; set; }
    }
}
