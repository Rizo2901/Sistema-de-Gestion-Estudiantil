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
    public class AdaptacionCurricularFlujo : IAdaptacionCurricularFlujo
    {
        private IAdaptacionCurricularDA _adaptacionCurricularDA;
        public AdaptacionCurricularFlujo(IAdaptacionCurricularDA adaptacionCurricularDA)
        {
            _adaptacionCurricularDA = adaptacionCurricularDA;
        }

        public async Task<Guid> CrearAdaptacionCurricular(AdaptacionCurricularRequest adaptacionCurricular)
        {
            return await _adaptacionCurricularDA.CrearAdaptacionCurricular(adaptacionCurricular);
        }

        public async Task<Guid> EditarAdaptacionCurricular(Guid Id, AdaptacionCurricularRequest adaptacionCurricular)
        {
            return await _adaptacionCurricularDA.EditarAdaptacionCurricular(Id, adaptacionCurricular);
        }

        public async Task<Guid> EliminarAdaptacionCurricular(Guid Id)
        {
            return await _adaptacionCurricularDA.EliminarAdaptacionCurricular(Id);
        }

        public async Task<AdaptacionCurricularResponse> ObtenerAdaptacionCurricularPorId(Guid Id)
        {
            return await _adaptacionCurricularDA.ObtenerAdaptacionCurricularPorId(Id);
        }

        public async Task<IEnumerable<AdaptacionCurricularResponse>> ObtenerAdaptacionesCurriculares()
        {
            return await _adaptacionCurricularDA.ObtenerAdaptacionesCurriculares();
        }

        public async Task<IEnumerable<AdaptacionCurricularResponse>> ObtenerAdaptacionesPorEstudiante(Guid IdEstudiante)
        {
            return await _adaptacionCurricularDA.ObtenerAdaptacionesPorEstudiante(IdEstudiante);
        }

        public async Task<IEnumerable<EstudianteConAdaptacionesResponse>> ObtenerAdaptacionesPorEstudiantes()
        {
            return await _adaptacionCurricularDA.ObtenerAdaptacionesPorEstudiantes();
        }
    }
}
