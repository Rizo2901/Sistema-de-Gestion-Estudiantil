using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IRolController
    {
        Task<IActionResult> CrearRole(RolesRequest rol);
        Task<IActionResult> EditarRole(Guid Id, RolesRequest rol);
        Task<IActionResult> EliminarRole(Guid Id);
        Task<IActionResult> ObtenerRoles();
        Task<IActionResult> ObtenerRole(Guid Id);

    }
}
