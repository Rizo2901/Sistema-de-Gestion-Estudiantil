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
    public class SituacionFamiliarFlujo : ISituacionFamiliarFlujo
    {
        private  ISituacionFamiliarDA _situacionFamiliarDA;
        public SituacionFamiliarFlujo(ISituacionFamiliarDA situacionFamiliarDA)
        {
            _situacionFamiliarDA = situacionFamiliarDA;
        }
        public async Task<Guid> CrearSituacionFamiliar(SituacionFamiliarRequest request)
        {
            return await _situacionFamiliarDA.CrearSituacionFamiliar(request);
        }

        public async Task<Guid> EditarSituacionFamiliar(Guid Id, SituacionFamiliarRequest request)
        {
            return await _situacionFamiliarDA.EditarSituacionFamiliar(Id, request);
        }

        public async Task<Guid> EliminarSituacionFamiliar(Guid Id)
        {
            return await _situacionFamiliarDA.EliminarSituacionFamiliar(Id);
        }

        public async Task<IEnumerable<EstudianteConSituacionesResponse>> ObtenerEstudiantesConSituacionesFamiliar()
        {
            return await _situacionFamiliarDA.ObtenerEstudiantesConSituacionesFamiliar();
        }

        public async Task<IEnumerable<SituacionFamiliarResponse>> ObtenerSituacionesFamiliares()
        {
            return await _situacionFamiliarDA.ObtenerSituacionesFamiliares();
        }

        public async Task<IEnumerable<SituacionFamiliarResponse>> ObtenerSituacionesFamiliaresPorEstudiante(Guid IdEstudiante)
        {
            return await _situacionFamiliarDA.ObtenerSituacionesFamiliaresPorEstudiante(IdEstudiante);
        }

        public async Task<SituacionFamiliarResponse> ObtenerSituacionFamiliarPorId(Guid Id)
        {
            return await _situacionFamiliarDA.ObtenerSituacionFamiliarPorId(Id);
        }
    }
}
