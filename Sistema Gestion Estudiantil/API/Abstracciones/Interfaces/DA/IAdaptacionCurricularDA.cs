using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IAdaptacionCurricularDA
    {
        Task<Guid> CrearAdaptacionCurricular(AdaptacionCurricularRequest adaptacionCurricular);
        Task<Guid> EditarAdaptacionCurricular(Guid Id, AdaptacionCurricularRequest adaptacionCurricular);
        Task<Guid> EliminarAdaptacionCurricular(Guid Id);
        Task<AdaptacionCurricularResponse> ObtenerAdaptacionCurricularPorId(Guid Id);
        Task<IEnumerable<AdaptacionCurricularResponse>> ObtenerAdaptacionesPorEstudiante(Guid IdEstudiante);

    }
}
