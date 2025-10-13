using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class DiscapacidadBase
    {
        public Guid IdDiscapacidad { get; set; }
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; } 
        public string? Descripcion { get; set; }
    }

    public class DiscapacidadRequest : DiscapacidadBase
    {
        public Guid IdDiscapacidad { get; set; }
    }
    public class DiscapacidadResponse : DiscapacidadBase
    {
        public string NombreCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }
    }
}
