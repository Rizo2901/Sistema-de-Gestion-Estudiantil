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
        Task<IActionResult> EditarReunionAdministrativa(Guid IdReunionAdmin, ReunionAdministrativaRequest reunion);
        Task<IActionResult> EliminarReunionAdministrativa(Guid IdReunionAdmin);
        Task<IActionResult> ObtenerReunionAdministrativa(Guid IdReunionAdmin);
        Task<IActionResult>ObtenerReunionesAdministrativas();
    }
}
