using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class EnfermedadBase
    {
        public Guid IdEnfermedad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
    public class EnfermedadRequest : EnfermedadBase
    {
        public Guid IdEnfermedad { get; set; }
    }
    public class EnfermedadResponse : EnfermedadBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
