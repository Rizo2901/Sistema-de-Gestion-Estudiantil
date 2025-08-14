using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
 
        public class AsistenciaProfesorBase
        {
            public Guid IdProfesor { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan HoraEntrada { get; set; }
            public TimeSpan HoraSalida { get; set; }
        }

        public class AsistenciaProfesorRequest : AsistenciaProfesorBase
        {
            public Guid? IdAsistenciaProfesor { get; set; }
        }

        public class AsistenciaProfesorResponse : AsistenciaProfesorBase
        {
            public Guid IdAsistenciaProfesor { get; set; }
        }
}


