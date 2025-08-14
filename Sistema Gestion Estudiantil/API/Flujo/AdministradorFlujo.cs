using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos.Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class AdministradorFlujo : IAdministradorFlujo
    {
        private  IAdministradorDA _administradorDA;
        public AdministradorFlujo(IAdministradorDA administradorDA)
        {
            _administradorDA = administradorDA;
        }
        public async Task<Guid> CrearAdministrador(AdministradorRequest administrador)
        {
            return await _administradorDA.CrearAdministrador(administrador);
        }

        public async Task<Guid> EditarAdministrador(Guid IdAdministrador, AdministradorRequest administrador)
        {
            return await _administradorDA.EditarAdministrador(IdAdministrador, administrador);
        }

        public async Task<Guid> EliminarAdministrador(Guid IdAdministrador)
        {
            return await _administradorDA.EliminarAdministrador(IdAdministrador);
        }

        public async Task<IEnumerable<AdministradorResponse>> ObtenerAdministradores()
        {
            return await _administradorDA.ObtenerAdministradores();
        }

        public async Task<AdministradorResponse> ObtenerAdministradorPorId(Guid IdAdministrador)
        {
           return await _administradorDA.ObtenerAdministradorPorId(IdAdministrador);
        }
    }
}
