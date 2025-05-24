using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class MateriasBase
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }
    }

    public class MateriasRequest : MateriasBase
    {
    }

    public class MateriasResponse : MateriasBase
    {
        public Guid IdMateria { get; set; }
    }
}

