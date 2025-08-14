using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class HorarioBase
    {
        public Guid IdHorario { get; set; }

        public Guid IdSeccion { get; set; }

        public string? NombreSeccion { get; set; }  // mapeo de s.Nombre AS NombreSeccion

        public string Dia { get; set; }

        public TimeSpan HoraInicio { get; set; }    // string para 'HH:mm' desde SQL

        public TimeSpan HoraFin { get; set; }       // string para 'HH:mm' desde SQL

        public Guid IdMateria { get; set; }

        public string? NombreMateria { get; set; } // mapeo de m.Nombre AS NombreMateria

        public Guid IdProfesor { get; set; }

        public string? NombreProfesor { get; set; } // CONCAT(p.Nombre, ' ', p.Apellido1)

        public string Aula { get; set; }
    }

    public class HorarioRequest : HorarioBase
    {
        public Guid? IdHorario { get; set; }
    }

    public class HorarioResponse : HorarioBase
    {
        // IdHorario ya está en HorarioBase
    }

    public class HorarioPorSeccion : HorarioBase
    {
        public Guid IdSeccion { get; set; }
        public string NombreSeccion { get; set; }
        public List<HorarioPorDia> Dias { get; set; } = new();
    }

    public class HorarioPorDia : HorarioBase
    {
        public string Dia { get; set; }
        public List<HorarioDetalle> Horarios { get; set; } = new();
    }

    public class HorarioDetalle : HorarioBase
    {
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string NombreMateria { get; set; }
        public string NombreProfesor { get; set; }
        public string Aula { get; set; }
    }
}
