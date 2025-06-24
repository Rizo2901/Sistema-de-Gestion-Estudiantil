using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IReunionAdministrativaController
    {
        Task<IActionResult> CrearReunionAdministrativa(ReunionAdministrativaRequest reunion);
        Task<IActionResult> EditarReunionAdministrativa(Guid IdReunion, ReunionAdministrativaRequest reunion);
        Task<IActionResult> EliminarReunionAdministrativa(Guid IdReunion);
        Task<IActionResult> ObtenerReunionAdministrativa(Guid IdReunion);
        Task<IActionResult>ObtenerReunionesAdministrativas();
    }
}
