using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class ComportamientoFlujo : IComportamientoFlujo
    {
        private readonly IComportamientoDA _comportamientoDA;

        public ComportamientoFlujo(IComportamientoDA comportamientoDA)
        {
            _comportamientoDA = comportamientoDA;
        }

        public Task<IEnumerable<ComportamientoResponse>> ObtenerComportamientos() => _comportamientoDA.ObtenerComportamientos();

        public Task<ComportamientoResponse> ObtenerComportamientoPorId(Guid id) => _comportamientoDA.ObtenerComportamientoPorId(id);

        public Task<Guid> CrearComportamiento(ComportamientoRequest comportamiento) => _comportamientoDA.CrearComportamiento(comportamiento);

        public Task<Guid> EditarComportamiento(Guid id, ComportamientoRequest comportamiento) => _comportamientoDA.EditarComportamiento(id, comportamiento);

        public Task<Guid> EliminarComportamiento(Guid id) => _comportamientoDA.EliminarComportamiento(id);
    }
}
