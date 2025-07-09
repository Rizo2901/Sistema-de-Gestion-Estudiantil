using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IEnfermedadDA
    {
        Task<Guid>CrearEnfermedad(EnfermedadRequest enfermedad);
        Task<Guid> EditarEnfermedad(Guid Id, EnfermedadRequest enfermedad);
        Task<Guid> EliminarEnfermedad(Guid Id);
        Task<EnfermedadResponse> ObtenerEnfermedadPorId(Guid Id);
        Task<IEnumerable<EnfermedadResponse>> ObtenerEnfermedades();

    }
}
