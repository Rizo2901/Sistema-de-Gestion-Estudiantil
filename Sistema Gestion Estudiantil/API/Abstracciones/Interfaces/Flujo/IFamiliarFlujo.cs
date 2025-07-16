using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IFamiliarFlujo
    {
        Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares();
        Task<FamiliarResponse> ObtenerFamiliarPorId(Guid id);
        Task<Guid> CrearFamiliar(FamiliarRequest familiar);
        Task<Guid> EditarFamiliar(Guid id, FamiliarRequest familiar);
        Task<Guid> EliminarFamiliar(Guid id);
    }
}