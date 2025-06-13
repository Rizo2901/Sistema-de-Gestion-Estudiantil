using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abstracciones.Modelos
{
    public class CondicionesEspecialesBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoCondicion { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCondicion { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "9999-12-31", ErrorMessage = "Fecha inválida.")]
        public DateTime? FechaDiagnostico { get; set; }

        public string? Observaciones { get; set; }
    }

    public class CondicionesEspecialesRequest : CondicionesEspecialesBase
    {
        public Guid? IdCondicion { get; set; }
    }

    public class CondicionesEspecialesResponse : CondicionesEspecialesBase
    {
        public Guid IdCondicion { get; set; }

        [MaxLength(50)]
        public string TipoCondicion { get; set; }

        [MaxLength(100)]
        public string NombreCondicion { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "9999-12-31", ErrorMessage = "Fecha inválida.")]
        public DateTime? FechaDiagnostico { get; set; }

        public string? Observaciones { get; set; }

    }

    public class EstudianteConCondicionesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public List<CondicionesEspecialesResponse> Condiciones { get; set; } = new List<CondicionesEspecialesResponse>();
    }

    public class EstudiantesCondicionesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public List<CondicionesEspecialesResponse> Condiciones { get; set; } = new List<CondicionesEspecialesResponse>();
    }

    public class EstudianteConCondicionesRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        [Column("Condiciones")] // Dapper necesita este nombre exacto para mapear correctamente
        public string? Condiciones { get; set; }
    }
}