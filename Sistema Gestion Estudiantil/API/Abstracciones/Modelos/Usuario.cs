using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class UsuariosBase
    {
        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Contrasena { get; set; }

        [MaxLength(100)]
        public string? CorreoElectronico { get; set; }

        public bool Estado { get; set; }

        public DateTime? FechaActivacion { get; set; }
    }

    public class UsuariosRequest : UsuariosBase
    {
        [Required]
        public Guid IdRol { get; set; }
    }

    public class UsuariosResponse : UsuariosBase
    {
        public Guid IdUsuario { get; set; }

        public string? RolNombre { get; set; }
    }
}
