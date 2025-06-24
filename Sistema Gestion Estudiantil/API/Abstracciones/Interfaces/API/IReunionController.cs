using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IReunionController
    {
        Task<IActionResult> ObtenerReuniones();
        Task<IActionResult> ObtenerReunion(Guid IdReunion);
        Task<IActionResult> CrearReunion(ReunionesRequest reunion);
        Task<IActionResult> EditarReunion(Guid IdReunion, ReunionesRequest reunion);
        Task<IActionResult> EliminarReunion(Guid IdReunion);
        Task<IActionResult> ObtenerReunionesPorEstudiante(Guid IdEstudiante);

    }
}
