using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class FamiliarBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string Apellido1 { get; set; }

        [Required]
        [MaxLength(255)]
        public string Apellido2 { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [MaxLength(10)]
        public string Parentesco { get; set; }

        [MaxLength(20)]
        public string? TelefonoCelular { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        [MaxLength(100)]
        public string? Ocupacion { get; set; }

        [Required]
        [MaxLength(10)]
        public string EstadoCivil { get; set; }

        public int? CantidadHijos { get; set; }

        public Guid? IdUsuario { get; set; }

        public bool? ViveConEstudiante { get; set; }

        public bool? EsResponsableLegal { get; set; }
    }

    public class FamiliarRequest : FamiliarBase
    {
        public Guid? IdFamiliar { get; set; }
    }

    public class FamiliarResponse : FamiliarBase
    {
        public Guid IdFamiliar { get; set; }
    }
}
