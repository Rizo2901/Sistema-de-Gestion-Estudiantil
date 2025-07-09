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
    public class AlergiaFlujo : IAlergiaFlujo
    {
        private  IAlergiaDA _alergiaDA;
        public AlergiaFlujo(IAlergiaDA alergiaDA)
        {
            _alergiaDA = alergiaDA;
        }
        public async Task<Guid> CrearAlergia(AlergiaRequest alergia)
        {
            return await _alergiaDA.CrearAlergia(alergia);
        }

        public async Task<Guid> EditarAlergia(Guid IdAlergia, AlergiaRequest alergia)
        {
            return await _alergiaDA.EditarAlergia(IdAlergia, alergia);
        }

        public async Task<Guid> EliminarAlergia(Guid IdAlergia)
        {
            return await _alergiaDA.EliminarAlergia(IdAlergia);
        }

        public async Task<AlergiaResponse> ObtenerAlergiaPorId(Guid IdAlergia)
        {
            return await _alergiaDA.ObtenerAlergiaPorId(IdAlergia);
        }

        public async Task<IEnumerable<AlergiaResponse>> ObtenerAlergias()
        {
            return await _alergiaDA.ObtenerAlergias();
        }
    }
}
