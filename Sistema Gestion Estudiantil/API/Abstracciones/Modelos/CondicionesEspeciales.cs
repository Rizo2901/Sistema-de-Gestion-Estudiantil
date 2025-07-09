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

    public class CondicionesEspecialesResponse : CondicionesEspecialesBase
    {
        public Guid IdCondicion { get; set; }

        public DateTime? FechaDiagnostico { get; set; }

        public string? Observaciones { get; set; }

        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }

    }
    public class EstudianteConCondicionesRaw
    {
        public Guid IdEstudiante { get; set; }
        public string NombreCompleto { get; set; }

        public string Condiciones { get; set; }
    }
    public class EstudianteConCondicionesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string NombreCompleto { get; set; }
        public List<CondicionEspecialItem> Condiciones { get; set; }
    }

    public class CondicionEspecialItem
    {
        public Guid IdCondicion { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
        public string? Observaciones { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaDescripcion { get; set; }
    }





}