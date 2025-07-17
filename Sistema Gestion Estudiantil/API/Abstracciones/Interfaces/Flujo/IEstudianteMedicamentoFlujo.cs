using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IEstudianteMedicamentoFlujo
    {
        Task<Guid> CrearEstudianteMedicamento(EstudianteMedicamentoResponse estudianteMedicamento);
        Task<Guid> EditarEstudianteMedicamento(Guid Id, EstudianteMedicamentoResponse estudianteMedicamento);
        Task<Guid> EliminarEstudianteMedicamento(Guid Id);
        Task<EstudianteMedicamentoResponse> ObtenerEstudianteMedicamentoPorId(Guid Id);
        Task<IEnumerable<EstudianteMedicamentoResponse>> ObtenerEstudianteMedicamentosPorEstudiante(Guid IdEstudiante);

    }
}
