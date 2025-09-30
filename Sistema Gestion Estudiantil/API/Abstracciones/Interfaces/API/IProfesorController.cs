using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IProfesorController
    {
        Task<IActionResult> CrearProfesor(ProfesorRequest profesor);
        Task<IActionResult> EditarProfesor(Guid Id, ProfesorRequest profesor);

        Task<IActionResult> EliminarProfesor(Guid Id);
        Task<IActionResult> ObtenerProfesor(Guid Id);
        Task<IActionResult> ObtenerProfesores();

        Task<IActionResult> ObtenerProfesoresTotal();




    }
}
