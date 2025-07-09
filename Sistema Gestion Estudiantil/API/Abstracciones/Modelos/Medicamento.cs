using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class MedicamentoBase
    {
        public Guid IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class MedicamentoRequest : MedicamentoBase
    {
        public Guid IdMedicamento { get; set; }
    }
    public class MedicamentoResponse : MedicamentoBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
