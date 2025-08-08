using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Seguridad
{
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
