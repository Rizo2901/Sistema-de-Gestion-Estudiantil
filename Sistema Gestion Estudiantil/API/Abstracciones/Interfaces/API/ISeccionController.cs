using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ISeccionController
    {
        Task<IActionResult> ObtenerSecciones();
        Task<IActionResult> ObtenerSeccion(Guid Id);

        Task<IActionResult> CrearSeccion(SeccionesRequest seccion);

        Task<IActionResult> EditarSeccion(Guid Id, SeccionesRequest seccion);

        Task<IActionResult> EliminarSeccion(Guid Id);


    }
}
