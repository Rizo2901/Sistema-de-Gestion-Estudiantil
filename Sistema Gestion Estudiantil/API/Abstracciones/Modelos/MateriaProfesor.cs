using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class MateriaProfesorBase
    {
        [Required]
        public Guid IdMateria { get; set; }

        [Required]
        public Guid IdProfesor { get; set; }
    }

    public class MateriaProfesorRequest : MateriaProfesorBase
    {
        public Guid IdMateriaProfesor { get; set; }

    }

    public class MateriaProfesorResponse : MateriaProfesorBase
    {
        public Guid IdMateriaProfesor { get; set; }

        public string? NombreMateria { get; set; }
        public string? NombreProfesor { get; set; }
    }

    public class MateriaPorProfesorResponse : MateriaProfesorBase
    {
        public Guid IdMateria { get; set; }

        public string? NombreMateria { get; set; }

    }
}