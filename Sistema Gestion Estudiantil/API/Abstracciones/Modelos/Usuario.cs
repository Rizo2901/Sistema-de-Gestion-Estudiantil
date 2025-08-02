using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class UsuariosBase
    {
        public Guid IdUsuario { get; set; }
        public Guid IdRol { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Contrasena { get; set; }

        [MaxLength(100)]
        public string? CorreoElectronico { get; set; }

        public bool Estado { get; set; }

        public string TipoEntidad { get; set; }

        public DateTime? FechaActivacion { get; set; }
    }

    public class UsuariosResponse : UsuariosBase
    {
        // Información del rol
        public string? RolNombre { get; set; }
        public string? RolDescripcion { get; set; }

        // Descripción institucional del tipo de entidad (profesor, admin, familiar...)
        public string? NombreEntidad { get; set; }

        // Foto de perfil del usuario
        public string? FotoPerfil { get; set; }

        // Hash usado en backend (opcional: puede omitirse si no se usa en el retorno)
        public string? ContrasenaHash { get; set; }
    }

    public class UsuariosRequest : UsuariosBase
    {
        [Required]
        public Guid IdRol { get; set; } // Aunque ya está en la base, lo mantenés obligatorio en request
    }

    public class LoginRequest
    {
        public string Identificador { get; set; } // Puede ser usuario o correo
        public string Contrasena { get; set; }
    }


    public class Perfil
    {
        public Guid IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string CorreoElectronico { get; set; }

        public string FotoPerfil { get; set; }

        // Rol institucional
        public string RolNombre { get; set; }
        public string RolDescripcion { get; set; }

        // Entidad (Profesor, Familiar, etc.)
        public string TipoEntidad { get; set; }
        public string NombreEntidad { get; set; }
    }
}