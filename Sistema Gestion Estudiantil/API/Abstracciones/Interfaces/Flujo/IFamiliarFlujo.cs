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
  
        Task<Guid> CrearFamiliar(FamiliarRequest familiar);
        Task<Guid> EditarFamiliar(Guid IdFamiliar, FamiliarRequest familiar);
        Task<Guid> EliminarFamiliar(Guid IdFamiliar);
        Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares();
        Task<FamiliarResponse> ObtenerFamiliarPorId(Guid IdFamiliar);
    }
}