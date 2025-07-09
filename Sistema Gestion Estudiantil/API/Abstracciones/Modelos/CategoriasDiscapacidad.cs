using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriasDiscapacidadBase
    {
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class CategoriasDiscapacidadRequest : CategoriasDiscapacidadBase
    {
        public Guid IdCategoria { get; set; }
    }
    public class CategoriasDiscapacidadResponse : CategoriasDiscapacidadBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

}
