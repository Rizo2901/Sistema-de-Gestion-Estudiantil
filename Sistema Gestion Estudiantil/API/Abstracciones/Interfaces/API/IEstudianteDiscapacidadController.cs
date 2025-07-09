using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IEstudianteDiscapacidadController
    {
        Task<IActionResult> CrearEstudianteDiscapacidad(EstudianteDiscapacidadRequest estudianteDiscapacidad);
        Task<IActionResult>EditarEstudianteDiscapacidad(Guid Id , EstudianteDiscapacidadRequest estudianteDiscapacidad);
        Task<IActionResult>EliminarEstudianteDiscapacidad (Guid Id);
        Task<IActionResult>ObtenerEstudianteDiscapacidadPorId (Guid Id);
        Task<IActionResult>ObtenerDiscapacidadesPorEstudiante (Guid IdEstudiante);
        Task<IActionResult> ObtenerResumenDiscapacidadesPorEstudiante();
        Task<IActionResult> ObtenerDiscapacidadesConEstudiantes();
    }
}
