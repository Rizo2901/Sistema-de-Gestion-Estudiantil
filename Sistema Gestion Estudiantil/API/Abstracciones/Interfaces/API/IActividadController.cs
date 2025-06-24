using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IActividadController
    {
        Task<IActionResult> CrearActividad(ActividadRequest actividad);
        Task<IActionResult> EditarActividad(Guid Id, ActividadRequest actividad);
        Task<IActionResult> ObtenerActividades();
        Task<IActionResult> ObtenerActividadPorId(Guid Id);
        Task<IActionResult> EliminarActividad(Guid Id);


    }
}
