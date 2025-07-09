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
    public class DiscapacidadFlujo : IDiscapacidadFlujo
    {
        private IDiscapacidadDA _discDa;
        public DiscapacidadFlujo(IDiscapacidadDA discDa)
        {
            _discDa = discDa;
        }
        public async Task<Guid> CrearDiscapacidad(DiscapacidadRequest discapacidad)
        {
            return await _discDa.CrearDiscapacidad(discapacidad);
        }

        public async Task<Guid> EditarDiscapacidad(Guid Id, DiscapacidadRequest discapacidad)
        {
            return await _discDa.EditarDiscapacidad(Id, discapacidad);
        }

        public async Task<Guid> EliminarDiscapacidad(Guid Id)
        {
           return await _discDa.EliminarDiscapacidad(Id);
        }

        public async Task<IEnumerable<DiscapacidadResponse>> ObtenerDiscapacidades()
        {
            return await _discDa.ObtenerDiscapacidades();
        }

       

        public async Task<DiscapacidadResponse> ObtenerDiscapacidadPorId(Guid Id)
        {
            return await _discDa.ObtenerDiscapacidadPorId(Id);
        }
    }
}
