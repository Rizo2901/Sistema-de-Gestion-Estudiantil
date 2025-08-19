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

        Task<IActionResult> ObtenerEstudiantesResumen();

        Task<IActionResult> BuscarEstudiantes(string? nombre = null, string? cedula = null, string? grado = null, string? seccionNombre = null);


        Task<IActionResult> ObtenerEstudiante(Guid Id);
        Task<IActionResult> CrearEstudiante(EstudiantesRequest estudiante);
        Task<IActionResult> EditarEstudiante(Guid Id, EstudiantesRequest estudiante);
        Task<IActionResult> EliminarEstudiante(Guid Id);

    }
}
