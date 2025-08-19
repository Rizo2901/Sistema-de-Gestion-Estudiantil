using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Abstracciones.Modelos
{
    public class EstudianteInfo
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cedula { get; set; }
        public string GradoActual { get; set; }
        public Guid IdSeccion { get; set; }

        public string NombreSeccion { get; set; }
    }

    public class ReunionInfo
    {
        public Guid IdReunion { get; set; }
        public Guid IdEstudiante { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
    }

    public class FamiliarBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

        [Required, MaxLength(255)]
        public string Nombre { get; set; }

        [Required, MaxLength(255)]
        public string Apellido1 { get; set; }

        [Required, MaxLength(255)]
        public string Apellido2 { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required, MaxLength(10)]
        public string Parentesco { get; set; }

        [MaxLength(20)]
        public string? TelefonoCelular { get; set; }

        [MaxLength(100), EmailAddress]
        public string? CorreoElectronico { get; set; }

        [MaxLength(100)]
        public string? Ocupacion { get; set; }

        public string Cedula { get; set; }

        [Required, MaxLength(10)]
        public string EstadoCivil { get; set; }

        public int? CantidadHijos { get; set; }

        public Guid? IdUsuario { get; set; }

        public bool ViveConEstudiante { get; set; }

        public bool EsResponsableLegal { get; set; }
    }

    public class FamiliarRequest : FamiliarBase
    {
        public Guid? IdFamiliar { get; set; }
    }


    public class FamiliarResponse : FamiliarBase
    {
        public Guid IdFamiliar { get; set; }

        // Ocultamos los campos planos del estudiante
        [JsonIgnore] public string NombreEstudiante { get; set; }
        [JsonIgnore] public string Apellido1Estudiante { get; set; }
        [JsonIgnore] public string Apellido2Estudiante { get; set; }
        [JsonIgnore] public string CedulaEstudiante { get; set; }
        [JsonIgnore] public string GradoEstudiante { get; set; }
        [JsonIgnore] public Guid SeccionEstudiante { get; set; }
        [JsonIgnore] public string NombreSeccion { get; set; }
        // Consolidado
        public EstudianteInfo Estudiante { get; set; }

        public List<ReunionInfo> Reuniones { get; set; } = new List<ReunionInfo>();
    }




}
