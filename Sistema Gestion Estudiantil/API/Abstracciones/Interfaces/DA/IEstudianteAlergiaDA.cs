using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IEstudianteAlergiaDA
    {
        Task<Guid> CrearEstudianteAlergia(EstudianteAlergiaRequest estudianteAlergia);
        Task<Guid> EditarEstudianteAlergia(Guid Id, EstudianteAlergiaRequest estudianteAlergia);
        Task<Guid> EliminarEstudianteAlergia(Guid Id);

        Task<EstudianteAlergiaResponse> ObtenerEstudianteAlergiaPorId(Guid Id);
        Task<IEnumerable<EstudianteAlergiaResponse>> ObtenerAlergiasPorEstudiante(Guid IdEstudiante);
        Task<IEnumerable<EstudianteAlergiaResponse>> ObtenerTodasLasAlergiasEstudiantes();
        Task<IEnumerable<EstudianteConAlergiasResponse>> ObtenerAlergiasAgrupadasPorEstudiantes();
    }
}