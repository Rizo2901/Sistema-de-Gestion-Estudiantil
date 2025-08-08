using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IRolDA
    {
        Task<Guid> CrearRole(RolesRequest rol);
        Task<Guid> EditarRole( Guid Id, RolesRequest rol );
        Task<IEnumerable<RolesResponse>> ObtenerRoles();
        Task<RolesResponse> ObtenerRolePorId(Guid Id);
        Task<RolesResponse> ObtenerRolePorNombre(string nombre);
        Task<Guid> EliminarRole(Guid Id);



    }
}
