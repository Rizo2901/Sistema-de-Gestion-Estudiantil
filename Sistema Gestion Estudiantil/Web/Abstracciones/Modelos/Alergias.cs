using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class AlergiaBase
    {
        public Guid IdAlergia { get; set; }
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class AlergiaRequest : AlergiaBase
    {
        public Guid IdAlergia { get; set; }
    }
    public class AlergiaResponse : AlergiaBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
    }
}
