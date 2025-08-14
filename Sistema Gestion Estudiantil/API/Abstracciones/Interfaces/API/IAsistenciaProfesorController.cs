using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAsistenciaProfesorController
    {
        Task<IActionResult>CrearAsitenciaProfesor(AsistenciaProfesorRequest asistenciaProfesor);
        Task<IActionResult>EditarAsitenciaProfesor(Guid IdAsistenciaProfesor, AsistenciaProfesorRequest asistenciaProfesor);
        Task<IActionResult>EliminarAsitenciaProfesor(Guid IdAsistenciaProfesor);
        Task<IActionResult>ObtenerAsitenciaProfesorPorId(Guid IdAsistenciaProfesor);
        Task<IActionResult>ObtenerAsitenciaProfesores();
        Task<IActionResult>ObtenerAsitenciaProfesoresPorIdProfesor(Guid IdProfesor);
    }
}
