using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ISituacionFamiliarFlujo
    {
        Task<Guid> CrearSituacionFamiliar(SituacionFamiliarRequest request);
        Task<Guid> EditarSituacionFamiliar(Guid Id, SituacionFamiliarRequest request);
        Task<Guid> EliminarSituacionFamiliar(Guid Id);
        Task<SituacionFamiliarResponse> ObtenerSituacionFamiliarPorId(Guid Id);
        Task<IEnumerable<SituacionFamiliarResponse>> ObtenerSituacionesFamiliaresPorEstudiante(Guid IdEstudiante);
        Task<IEnumerable<EstudianteConSituacionesResponse>> ObtenerEstudiantesConSituacionesFamiliar();

        Task<IEnumerable<SituacionFamiliarResponse>> ObtenerSituacionesFamiliares();
    }
}
