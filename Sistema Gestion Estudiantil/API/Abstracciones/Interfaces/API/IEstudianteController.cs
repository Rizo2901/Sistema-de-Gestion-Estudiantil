using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IEstudianteController
    {
        Task<IActionResult> ObtenerEstudiantes();
        Task<IActionResult> ObtenerEstudiantePorId(Guid Id);
        Task<IActionResult> CrearEstudiante(EstudiantesResponse estudiante);
        Task<IActionResult> EditarEstudiante(Guid Id, EstudiantesResponse estudiante);
        Task<IActionResult> EliminarEstudiante(Guid Id);

    }
}
