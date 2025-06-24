using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class SeccionProfesorBase
    {
        public Guid IdProfesorSeccion { get; set; }
        public Guid IdSeccion { get; set; }
        public Guid IdProfesor { get; set; }

    }
    public class SeccionProfesorRequuest : SeccionProfesorBase
    {
        public Guid IdProfesorSeccion { get; set; }
    }
    public class SeccionProfesorResponse : SeccionProfesorBase
    {
        public string NombreSeccion { get; set; }
        public string NombreProfesor { get; set; }
        public string ApellidoProfesor { get; set; }
        public string CorreoProfesor { get; set; }
    }
}
