using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ISituacionFamiliarController
    {
        Task<IActionResult> CrearSituacionFamiliar(SituacionFamiliarRequest request);
        Task<IActionResult> EditarSituacionFamiliar(Guid Id, SituacionFamiliarRequest request);
        Task<IActionResult> EliminarSituacionFamiliar(Guid Id);
        Task<IActionResult> ObtenerSituacionFamiliarPorId(Guid Id);
        Task<IActionResult> ObtenerSituacionesFamiliaresPorEstudiante(Guid IdEstudiante);
        Task<IActionResult> ObtenerEstudiantesConSituacionesFamiliar();

        Task<IActionResult> ObtenerSituacionesFamiliares();

    }
}
