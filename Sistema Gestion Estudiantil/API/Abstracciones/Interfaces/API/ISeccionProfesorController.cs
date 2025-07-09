using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ISeccionProfesorController
    {
        Task<IActionResult> ObtenerSeccionesProfesores();                       
        Task<IActionResult> ObtenerSeccionProfesorPorId(Guid Id);                

        Task<IActionResult> ObtenerProfesoresPorSeccion(Guid IdSeccion);         
        Task<IActionResult> ObtenerSeccionesPorProfesor(Guid IdProfesor);       

        Task<IActionResult> CrearSeccionProfesor(SeccionProfesorRequest request);
        Task<IActionResult> EditarSeccionProfesor(Guid Id, SeccionProfesorRequest request);
        Task<IActionResult> EliminarSeccionProfesor(Guid Id);
    }
}