using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IEstudianteDiscapacidadFlujo
    {
        Task<Guid> CrearEstudianteDiscapacidad(EstudianteDiscapacidadRequest estudianteDiscapacidad);
        Task<Guid> EditarEstudianteDiscapacidad(Guid Id, EstudianteDiscapacidadRequest estudianteDiscapacidad);
        Task<Guid> EliminarEstudianteDiscapacidad(Guid Id);
        Task<EstudianteDiscapacidadResponse> ObtenerEstudianteDiscapacidadPorId(Guid Id);
        Task<IEnumerable<EstudianteDiscapacidadResponse>> ObtenerDiscapacidadesPorEstudiante(Guid idEstudiante);
        Task<IEnumerable<EstudianteConDiscapacidadesResponse>> ObtenerResumenDiscapacidadesPorEstudiante();
        Task<IEnumerable<EstudianteDiscapacidadResponse>> ObtenerDiscapacidadesConEstudiantes();
    }
}
