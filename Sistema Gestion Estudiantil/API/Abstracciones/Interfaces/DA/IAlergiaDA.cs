using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IAlergiaDA
    {
        Task<Guid> CrearAlergia(AlergiaRequest alergia);
        Task<Guid> EditarAlergia(Guid IdAlergia, AlergiaRequest alergia);
        Task<Guid> EliminarAlergia(Guid IdAlergia);
        Task<AlergiaResponse> ObtenerAlergiaPorId(Guid IdAlergia);
        Task<IEnumerable<AlergiaResponse>> ObtenerAlergias();

    }
}
