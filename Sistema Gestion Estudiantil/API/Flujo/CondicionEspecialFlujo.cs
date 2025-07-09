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
    public class CondicionEspecialFlujo : ICondicionEspecialFlujo
    {
        private ICondicionEspecialDA _condEspecDA;

        public CondicionEspecialFlujo(ICondicionEspecialDA condEspecDA)
        {
            _condEspecDA = condEspecDA;
        }

        public async Task<Guid> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial)
        {
            return await _condEspecDA.CrearCondicionEspecial(condicionEspecial);
        }

        public async Task<Guid> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial)
        {
            return await _condEspecDA.EditarCondicionEspecial(Id, condicionEspecial);
        }

        public async Task<Guid> EliminarCondicionEspecial(Guid Id)
        {
            return await _condEspecDA.EliminarCondicionEspecial(Id);
        }

        public async Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionesEspeciales()
        {
            return await _condEspecDA.ObtenerCondicionesEspeciales();
        }

        public async Task<IEnumerable<EstudianteConCondicionesResponse>> ObtenerCondicionEspecialPorEstudiantes()
        {
            return await _condEspecDA.ObtenerCondicionEspecialPorEstudiantes();
        }

        public async Task<CondicionesEspecialesResponse> ObtenerCondicionEspecial(Guid Id)
        {
           return await _condEspecDA.ObtenerCondicionEspecial(Id);
        }

        
    }
}
