using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAdaptacionCurricularController
    {
        Task<IActionResult> CrearAdaptacionCurricular(AdaptacionCurricularRequest adaptacionCurricular);
        Task<IActionResult> EditarAdaptacionCurricular(Guid Id, AdaptacionCurricularRequest adaptacionCurricular);
        Task<IActionResult> EliminarAdaptacionCurricular(Guid Id);
        Task<IActionResult> ObtenerAdaptacionCurricularPorId(Guid Id);
        Task<IActionResult> ObtenerAdaptacionesPorEstudiante(Guid IdEstudiante);

    }
}
