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

        [Required]
        public string? Fotografia { get; set; }


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
        public Guid IdSeccion { get; set; }
        public string? Seccion { get; set; }

        public string? NombreUsuario { get; set; }
    }
    public class EstudiantesResumenResponse : EstudiantesResponse
    {
        public int Documentos { get; set; }
        public int Alergias { get; set; }
        public int Discapacidades { get; set; }
        public int Enfermedades { get; set; }
        public int Medicamentos { get; set; }
        public int CondicionesEspeciales { get; set; }
        public int SituacionesFamiliares { get; set; }
        public int AdaptacionesCurriculares { get; set; }
        public int AtencionesMedicas { get; set; }
    }

}
