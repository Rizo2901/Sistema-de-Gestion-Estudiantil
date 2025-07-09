using System;

namespace Abstracciones.Modelos
{
    public class SeccionProfesorBase
    {
        public Guid IdProfesorSeccion { get; set; }
        public Guid IdSeccion { get; set; }
        public Guid IdProfesor { get; set; }
    }

    public class SeccionProfesorRequest : SeccionProfesorBase
    {
        
        public Guid IdProfesorSeccion { get; set; }
    }

    public class SeccionProfesorResponse : SeccionProfesorBase
    {
        public string NombreSeccion { get; set; }

        public string NombreProfesor { get; set; }
        public string Apellido1Profesor { get; set; }
        public string Apellido2Profesor { get; set; }

        public string CorreoProfesor { get; set; }
    }
}