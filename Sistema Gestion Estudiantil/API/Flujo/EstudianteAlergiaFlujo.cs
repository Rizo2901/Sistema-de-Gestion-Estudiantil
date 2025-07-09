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
    public class EstudianteAlergiaFlujo : IEstudianteAlergiaFlujo
    {
        private IEstudianteAlergiaDA _estudianteAlergiaDA;
        public EstudianteAlergiaFlujo(IEstudianteAlergiaDA estudianteAlergiaDA)
        {
            _estudianteAlergiaDA = estudianteAlergiaDA;
        }
        public async Task<Guid> CrearEstudianteAlergia(EstudianteAlergiaRequest estudianteAlergia)
        {
            return await _estudianteAlergiaDA.CrearEstudianteAlergia(estudianteAlergia);
        }

        public async Task<Guid> EditarEstudianteAlergia(Guid Id, EstudianteAlergiaRequest estudianteAlergia)
        {
            return await _estudianteAlergiaDA.EditarEstudianteAlergia(Id, estudianteAlergia);
        }

        public async Task<Guid> EliminarEstudianteAlergia(Guid Id)
        {
            return await _estudianteAlergiaDA.EliminarEstudianteAlergia(Id);
        }

        public async Task<IEnumerable<EstudianteConAlergiasResponse>> ObtenerAlergiasAgrupadasPorEstudiantes()
        {
            return await _estudianteAlergiaDA.ObtenerAlergiasAgrupadasPorEstudiantes();
        }

        public async Task<IEnumerable<EstudianteAlergiaResponse>> ObtenerAlergiasPorEstudiante(Guid IdEstudiante)
        {
            return await _estudianteAlergiaDA.ObtenerAlergiasPorEstudiante(IdEstudiante);
        }

        public async Task<EstudianteAlergiaResponse> ObtenerEstudianteAlergiaPorId(Guid Id)
        {
            return await _estudianteAlergiaDA.ObtenerEstudianteAlergiaPorId(Id);
        }

        public async Task<IEnumerable<EstudianteAlergiaResponse>> ObtenerTodasLasAlergiasEstudiantes()
        {
            return await _estudianteAlergiaDA.ObtenerTodasLasAlergiasEstudiantes();
        }
    }
}
