using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.DA
{
    public interface ISituacionFamiliarDA
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
