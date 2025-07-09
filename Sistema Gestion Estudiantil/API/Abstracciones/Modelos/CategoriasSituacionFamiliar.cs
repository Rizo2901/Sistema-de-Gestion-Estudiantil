using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriasSituacionFamiliarBase
    {
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class CategoriasSituacionFamiliarRequest : CategoriasSituacionFamiliarBase
    {
        public Guid IdCategoria { get; set; }
    }
    public class CategoriasSituacionFamiliarResponse : CategoriasSituacionFamiliarBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
