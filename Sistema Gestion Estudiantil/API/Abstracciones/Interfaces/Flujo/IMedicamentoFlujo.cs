using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMedicamentoFlujo
    {
        Task<Guid> CrearMedicamento(MedicamentoResponse medicamento);
        Task<Guid> EditarMedicamento(Guid Id, MedicamentoResponse medicamento);
        Task<Guid> EliminarMedicamento(Guid Id);
        Task<MedicamentoResponse> ObtenerMedicamentoPorId(Guid Id);
        Task<IEnumerable<MedicamentoResponse>> ObtenerMedicamentos();

    }
}
