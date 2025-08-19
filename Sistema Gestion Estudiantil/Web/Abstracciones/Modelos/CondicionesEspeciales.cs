using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abstracciones.Modelos
{
    public class CondicionesEspecialesBase
    {
        public Guid IdCondicion { get; set; }
        public Guid IdCategoria { get; set; }

        [Required]
        public Guid IdEstudiante { get; set; }



        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "9999-12-31", ErrorMessage = "Fecha inválida.")]
        public DateTime? FechaDiagnostico { get; set; }

        public string? Observaciones { get; set; }
    }

    public class CondicionesEspecialesRequest : CondicionesEspecialesBase
    {
        public Guid? IdCondicion { get; set; }
    }

    public class CondicionesEspecialesResponse
    {
        public Guid IdCondicion { get; set; }
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public Guid IdCategoria { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
        public string? Observaciones { get; set; }

        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
    }
    public class EstudianteConCondicionesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string? NombreCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }

        public List<CondicionesEspecialesResponse> Condiciones { get; set; } = new();
    }


}