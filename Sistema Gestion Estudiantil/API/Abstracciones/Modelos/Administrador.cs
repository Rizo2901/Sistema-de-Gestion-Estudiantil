using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    using System;

    namespace Abstracciones.Modelos
    {
        public class AdministradorBase
        {
            public string Nombre { get; set; }
            public string Apellido1 { get; set; }
            public string Apellido2 { get; set; }
            public string Direccion { get; set; }
            public string TelefonoCelular { get; set; }
            public string CorreoElectronico { get; set; }
            public Guid? IdUsuario { get; set; }
            public string Cedula { get; set; }
        }

        public class AdministradorRequest : AdministradorBase
        {
            public Guid? IdAdministrador { get; set; }
        }

        public class AdministradorResponse : AdministradorBase
        {
            public Guid IdAdministrador { get; set; }
        }
    }

}
