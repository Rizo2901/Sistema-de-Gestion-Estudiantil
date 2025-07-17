using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IComportamientoFlujo
    {
        Task<IEnumerable<ComportamientoResponse>> ObtenerComportamientos();
        Task<ComportamientoResponse> ObtenerComportamientoPorId(Guid id);
        Task<Guid> CrearComportamiento(ComportamientoRequest comportamiento);
        Task<Guid> EditarComportamiento(Guid id, ComportamientoRequest comportamiento);
        Task<Guid> EliminarComportamiento(Guid id);
    }
}