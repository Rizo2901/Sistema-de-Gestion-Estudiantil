using Abstracciones.Modelos.Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IAdministradorDA
    {
        Task<Guid> CrearAdministrador(AdministradorRequest administrador);
        Task<Guid> EditarAdministrador(Guid IdAdministrador, AdministradorRequest administrador);
        Task<Guid> EliminarAdministrador(Guid IdAdministrador);
        Task<IEnumerable<AdministradorResponse>> ObtenerAdministradores();
        Task<AdministradorResponse> ObtenerAdministradorPorId(Guid IdAdministrador);

    }
}
