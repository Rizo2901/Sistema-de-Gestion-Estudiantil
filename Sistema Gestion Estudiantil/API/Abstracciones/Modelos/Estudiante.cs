using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class EstudiantesBase
    {
        [Required]
        [MaxLength(20)]
        public string Cedula { get; set; }

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
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [MaxLength(10)]
        public string Genero { get; set; }

        [Required]
        [MaxLength(20)]
        public string GradoActual { get; set; }

        public bool Repitiendo { get; set; }

        public string? HistorialAcademico { get; set; }

        public string? Discapacidades { get; set; }

        public bool UsoLentes { get; set; }

        public string? Enfermedades { get; set; }

        public string? Alergias { get; set; }

        public byte[]? Fotografia { get; set; }


        public string? Direccion { get; set; }

        [MaxLength(20)]
        public string? TelefonoCelular { get; set; }

        [MaxLength(100)]
        public string? CorreoElectronico { get; set; }

        public bool Estado { get; set; }
    }

    public class EstudiantesRequest : EstudiantesBase
    {
    
        public Guid IdSeccion { get; set; }

        public Guid? IdUsuario { get; set; }
    }

    public class EstudiantesResponse : EstudiantesBase
    {
        public Guid IdEstudiante { get; set; }

        public string? Seccion { get; set; }

        public string? NombreUsuario { get; set; }
    }
}
