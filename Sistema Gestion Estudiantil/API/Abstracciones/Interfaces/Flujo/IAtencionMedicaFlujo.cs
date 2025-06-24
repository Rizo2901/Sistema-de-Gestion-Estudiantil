using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IAtencionMedicaFlujo
    {
        Task<Guid> CrearAtencionMedica(AtencionMedicaRequest atencion);
        Task<Guid> EditarAtencionMedica(Guid IdAtencionMedica, AtencionMedicaRequest atencion);

        Task<Guid> EliminarAtencionMedica(Guid IdAtencionMedica);

        Task<AtencionMedicaResponse> ObtenerAtencionMedicaPorId(Guid IdAtencionMedica);
        Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicasPorEstudiante(Guid IdEstudiante);
        Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicas();
    }
}
