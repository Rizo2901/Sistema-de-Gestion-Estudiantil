using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAtencionMedicaController
    {
        Task<IActionResult> CrearAtencionMedica(AtencionMedicaRequest atencion);
        Task<IActionResult> EditarAtencionMedica(Guid IdAtencionMedica, AtencionMedicaRequest atencion);

        Task<IActionResult> EliminarAtencionMedica(Guid IdAtencionMedica);

        Task<IActionResult> ObtenerAtencionMedicaPorId(Guid IdAtencionMedica);
        Task<IActionResult> ObtenerAtencionesMedicasPorEstudiante(Guid IdEstudiante);
        Task<IActionResult> ObtenerAtencionesMedicas();


    }
}
