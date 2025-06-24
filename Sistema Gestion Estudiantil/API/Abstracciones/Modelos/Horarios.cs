using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class HorarioBase
    {
        [Required]
        public string Dia { get; set; } // e.g., "Lunes", "Martes", etc.

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required]
        public Guid IdSeccion { get; set; } // Relates to a section
    }

    public class HorarioRequest : HorarioBase
    {
        public Guid? IdHorario { get; set; }
    }

    public class HorarioResponse : HorarioBase
    {
        public Guid IdHorario { get; set; }
    }
}

