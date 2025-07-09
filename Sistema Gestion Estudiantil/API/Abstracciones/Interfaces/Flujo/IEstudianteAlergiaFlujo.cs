using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IEstudianteAlergiaFlujo
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
