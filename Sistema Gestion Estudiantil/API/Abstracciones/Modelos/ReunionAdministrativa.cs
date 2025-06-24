using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ReunionAdministrativaBase
    {
        public Guid IdReunionAdministrativa { get; set; }
        public Guid Responsable { get; set; }
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
    }

    public class ReunionAdministrativaRequest : ReunionAdministrativaBase
    {
        public Guid IdReunionAdministrativa { get; set; }
      
    }
    public class ReunionAdministrativaResponse : ReunionAdministrativaBase
    {
        public Guid IdReunionAdministrativa { get; set; }
        public Guid Responsable { get; set; }
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
    }
}
