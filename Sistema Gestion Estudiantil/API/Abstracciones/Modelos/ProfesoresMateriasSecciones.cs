
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
   
        public class ProfesoresMateriasSeccionesBase
        {
            [Required]
            public Guid IdProfesor { get; set; }

            [Required]
            public Guid IdMateria { get; set; }

            [Required]
            public Guid IdSeccion { get; set; }
        }

        public class ProfesoresMateriasSeccionesRequest : ProfesoresMateriasSeccionesBase
        {

        }

        public class ProfesoresMateriasSeccionesResponse : ProfesoresMateriasSeccionesBase
        {
            public string? NombreProfesor { get; set; }

            public string? NombreMateria { get; set; }

            public string? NombreSeccion { get; set; }
        }
    }


