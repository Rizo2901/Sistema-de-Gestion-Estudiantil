using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IActividadFlujo
    {
        Task<IEnumerable<ActividadResponse>> ObtenerActividades();
        Task<ActividadResponse> ObtenerActividadPorId(Guid Id);
        Task<Guid> CrearActividad(ActividadRequest actividad);
        Task<Guid> EditarActividad(Guid Id, ActividadRequest actividad);
        Task<Guid> EliminarActividad(Guid Id);
    }
}
