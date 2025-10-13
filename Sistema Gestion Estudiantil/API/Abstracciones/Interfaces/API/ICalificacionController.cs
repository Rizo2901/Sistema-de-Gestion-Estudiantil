using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICalificacionController
    {
        Task<IActionResult> ObtenerCalificaciones();
        Task<IActionResult> ObtenerCalificacion(Guid IdCalificacion);
        Task<IActionResult> CrearCalificacion(CalificacionRequest calificacion);
        Task<IActionResult> EditarCalificacion(Guid IdCalificacion, CalificacionRequest calificacion);
        Task<IActionResult> EliminarCalificacion(Guid IdCalificacion);
        Task<IActionResult> ObtenerCalificacionesPorEstudiante(Guid IdEstudiante);

        Task<IActionResult> ObtenerCalificacionesPorEstudiantes();

    }
}
