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
    public class MedicamentoFlujo : IMedicamentoFlujo
    {
        private  IMedicamentoDA _medicamentoDA;
        public MedicamentoFlujo(IMedicamentoDA medicamentoDA)
        {
            _medicamentoDA = medicamentoDA;
        }
        public async Task<Guid> CrearMedicamento(MedicamentoResponse medicamento)
        {
            return await _medicamentoDA.CrearMedicamento(medicamento);
        }

        public async Task<Guid> EditarMedicamento(Guid Id, MedicamentoResponse medicamento)
        {
            return await _medicamentoDA.EditarMedicamento(Id, medicamento);
        }

        public async Task<Guid> EliminarMedicamento(Guid Id)
        {
            return await _medicamentoDA.EliminarMedicamento(Id);
        }

        public async Task<MedicamentoResponse> ObtenerMedicamentoPorId(Guid Id)
        {
            return await _medicamentoDA.ObtenerMedicamentoPorId(Id);
        }

        public async Task<IEnumerable<MedicamentoResponse>> ObtenerMedicamentos()
        {
            return await _medicamentoDA.ObtenerMedicamentos();
        }
    }
}
