using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Abstracciones.Modelos
{
    public class SeccionesBase
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }

    public class SeccionesRequest : SeccionesBase
    {
    }

    public class SeccionesResponse : SeccionesBase
    {
        public Guid IdSeccion { get; set; }
    }

    public class EstudianteSeccionesRespone
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string GradoActual { get; set; }
        public bool Repitiendo { get; set; }
        public string? HistorialAcademico { get; set; }
        public string? Fotografia { get; set; }
        public string? Direccion { get; set; }
        public string? TelefonoCelular { get; set; }
        public string? CorreoElectronico { get; set; }
        public bool Estado { get; set; }
        public Guid IdSeccion { get; set; }
        public Guid? IdUsuario { get; set; }
    }
}


