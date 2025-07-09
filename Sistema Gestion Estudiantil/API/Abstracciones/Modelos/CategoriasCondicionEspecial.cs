using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriasCondicionEspecialBase
    {
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class CategoriasCondicionEspecialRequest : CategoriasCondicionEspecialBase
    {
        public Guid IdCategoria { get; set; }
    }
    public class CategoriasCondicionEspecialResponse : CategoriasCondicionEspecialBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
