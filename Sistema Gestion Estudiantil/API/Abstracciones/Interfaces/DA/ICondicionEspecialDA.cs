using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICondicionEspecialDA
    {
    
        Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionesEspeciales();
        Task<CondicionesEspecialesResponse> ObtenerCondicionEspecial(Guid Id);
        Task<Guid> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial);
        Task<Guid> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial);
        Task<Guid> EliminarCondicionEspecial(Guid Id);
    }
}
