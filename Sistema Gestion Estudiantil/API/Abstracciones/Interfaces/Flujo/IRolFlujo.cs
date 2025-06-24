using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IRolFlujo
    {
        Task<Guid> CrearRole(RolesRequest rol);
        Task<Guid> EditarRole(Guid Id, RolesRequest rol);
        Task<IEnumerable<RolesResponse>> ObtenerRoles();
        Task<RolesResponse> ObtenerRolePorId(Guid Id);
        Task<Guid> EliminarRole(Guid Id);
    }
}
