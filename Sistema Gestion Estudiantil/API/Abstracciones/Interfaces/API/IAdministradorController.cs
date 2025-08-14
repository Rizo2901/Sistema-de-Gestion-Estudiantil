using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAdministradorController
    {
        Task<IActionResult> CrearAdministrador(AdministradorRequest administradorRequest);
        Task<IActionResult> EditarAdministrador(Guid IdAdministrador, AdministradorRequest administradorRequest);
        Task<IActionResult> EliminarAdministrador(Guid IdAdministrador);
        Task<IActionResult> ObtenerAdministradorPorId(Guid IdAdministrador);
        Task<IActionResult> ObtenerAdministradores();


    }
}
