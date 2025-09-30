using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriaAlergiaBase
    {
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class CategoriaAlergiaRequest : CategoriaAlergiaBase
    {
        public Guid IdCategoria { get; set; }
    }
    public class CategoriaAlergiaResponse : CategoriaAlergiaBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}