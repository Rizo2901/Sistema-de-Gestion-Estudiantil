using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProfesorBase
    {
        public Guid IdProfesor { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoCelular { get; set; }

        public string Direccion { get; set; }
    }

    public class ProfesorRequest : ProfesorBase
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoCelular { get; set; }

        public string Direccion { get; set; }

    }

    public class ProfesorResponse : ProfesorBase
    {
        public Guid IdProfesor { get; set; }

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }


    }
}
