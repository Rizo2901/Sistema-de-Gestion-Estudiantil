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
    public class AtencionMedicaFlujo : IAtencionMedicaFlujo
    {
        private IAtencionMedicaDA _antencionMedica;

        public AtencionMedicaFlujo(IAtencionMedicaDA antencionMedica)
        {
            _antencionMedica = antencionMedica;
        }

        public async Task<Guid> CrearAtencionMedica(AtencionMedicaRequest atencion)
        {
            return await _antencionMedica.CrearAtencionMedica(atencion);
        }

        public async Task<Guid> EditarAtencionMedica(Guid IdAtencionMedica, AtencionMedicaRequest atencion)
        {
            return await _antencionMedica.EditarAtencionMedica(IdAtencionMedica, atencion);
        }

        public async Task<Guid> EliminarAtencionMedica(Guid IdAtencionMedica)
        {
            return await _antencionMedica.EliminarAtencionMedica(IdAtencionMedica);
        }

        public async Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicas()
        {
            return await _antencionMedica.ObtenerAtencionesMedicas();
        }

        public async Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicasPorEstudiante(Guid IdEstudiante)
        {
           return await _antencionMedica.ObtenerAtencionesMedicasPorEstudiante(IdEstudiante);
        }

        public async Task<AtencionMedicaResponse> ObtenerAtencionMedicaPorId(Guid IdAtencionMedica)
        {
            return await _antencionMedica.ObtenerAtencionMedicaPorId(IdAtencionMedica);
        }
    }
}
