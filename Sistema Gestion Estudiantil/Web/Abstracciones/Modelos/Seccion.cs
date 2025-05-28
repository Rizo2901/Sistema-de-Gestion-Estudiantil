using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Abstracciones.Modelos
{
    public class SeccionesBase
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }

    public class SeccionesRequest : SeccionesBase
    {
    }

    public class SeccionesResponse : SeccionesBase
    {
        public Guid IdSeccion { get; set; }
    }
}


