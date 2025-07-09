using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IDiscapacidadDA
    {
        Task<Guid> CrearDiscapacidad(DiscapacidadRequest discapacidad);
        Task<Guid> EditarDiscapacidad(Guid Id, DiscapacidadRequest discapacidad);
        Task<Guid> EliminarDiscapacidad(Guid Id);
        Task<DiscapacidadResponse> ObtenerDiscapacidadPorId(Guid Id);
        Task<IEnumerable<DiscapacidadResponse>> ObtenerDiscapacidades();


    }
}
