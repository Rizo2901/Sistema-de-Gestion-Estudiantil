using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICondicionEspecialFlujo
    {
        Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionesEspeciales();
        Task<IEnumerable<EstudianteConCondicionesResponse>> ObtenerCondicionEspecialPorEstudiantes();
        Task<CondicionesEspecialesResponse> ObtenerCondicionEspecial(Guid Id);
        Task<Guid> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial);
        Task<Guid> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial);
        Task<Guid> EliminarCondicionEspecial(Guid Id);
    }
}
