using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IEstudianteFlujo
    {
        Task<IEnumerable<EstudiantesResponse>> ObtenerEstudiantes();
        Task<EstudiantesResponse> ObtenerEstudiantePorId(Guid Id);
        Task<Guid> CrearEstudiante(EstudiantesRequest estudiante);
        Task<Guid> EditarEstudiante(Guid Id, EstudiantesRequest estudiante);
        Task<Guid> EliminarEstudiante(Guid Id);

    }
}
