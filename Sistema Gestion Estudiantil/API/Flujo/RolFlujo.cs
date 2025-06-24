using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class RolFlujo : IRolFlujo
    {
        private IRolDA _rolDA;
        public RolFlujo(IRolDA rolDA)
        {
            _rolDA = rolDA;
        }

        public async Task<Guid> CrearRole(RolesRequest rol)
        {
            return await _rolDA.CrearRole(rol);
        }

        public async Task<Guid> EditarRole(Guid Id, RolesRequest rol)
        {
            return await _rolDA.EditarRole(Id, rol);
        }

        public async Task<Guid> EliminarRole(Guid Id)
        {
            return await _rolDA.EliminarRole(Id);
        }

        public async Task<RolesResponse> ObtenerRolePorId(Guid Id)
        {
            return await _rolDA.ObtenerRolePorId(Id);
        }

        public async Task<IEnumerable<RolesResponse>> ObtenerRoles()
        {
            return await _rolDA.ObtenerRoles();
        }
    }
}
