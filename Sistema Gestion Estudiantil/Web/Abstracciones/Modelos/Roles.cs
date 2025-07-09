using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class RolesBase
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }

    public class RolesRequest : RolesBase
    {
        public Guid IdRol { get; set; }
    }


    public class RolesResponse : RolesBase
    {
        public Guid IdRol { get; set; }
    }


}

