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
    public class EstudianteMedicamentoFlujo : IEstudianteMedicamentoFlujo
    {
        private IEstudianteMedicamentoDA _estudianteMedicamentoDA;
        public EstudianteMedicamentoFlujo(IEstudianteMedicamentoDA estudianteMedicamentoDA)
        {
            _estudianteMedicamentoDA = estudianteMedicamentoDA;
        }

        public async Task<Guid> CrearEstudianteMedicamento(EstudianteMedicamentoResponse estudianteMedicamento)
        {
            return await _estudianteMedicamentoDA.CrearEstudianteMedicamento(estudianteMedicamento);
        }

        public async Task<Guid> EditarEstudianteMedicamento(Guid Id, EstudianteMedicamentoResponse estudianteMedicamento)
        {
            return await _estudianteMedicamentoDA.EditarEstudianteMedicamento(Id, estudianteMedicamento);
        }

        public async Task<Guid> EliminarEstudianteMedicamento(Guid Id)
        {
            return await _estudianteMedicamentoDA.EliminarEstudianteMedicamento(Id);
        }

        public async Task<EstudianteMedicamentoResponse> ObtenerEstudianteMedicamentoPorId(Guid Id)
        {
            return await _estudianteMedicamentoDA.ObtenerEstudianteMedicamentoPorId(Id);
        }

        public async Task<IEnumerable<EstudianteMedicamentoResponse>> ObtenerEstudianteMedicamentos()
        {
            return await _estudianteMedicamentoDA.ObtenerEstudianteMedicamentos();
        }

        public async Task<IEnumerable<EstudianteMedicamentoResponse>> ObtenerEstudianteMedicamentosPorEstudiante(Guid IdEstudiante)
        {
            return await _estudianteMedicamentoDA.ObtenerEstudianteMedicamentosPorEstudiante(IdEstudiante);
        }

        public async Task<IEnumerable<EstudianteConMedicamentosResponse>> ObtenerEstudianteMedicamentosPorEstudiantes()
        {
            return await _estudianteMedicamentoDA.ObtenerEstudianteMedicamentosPorEstudiantes();
        }
    }
}
