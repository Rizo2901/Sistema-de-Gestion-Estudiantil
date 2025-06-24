using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IMateriaProfesorController
    {
        Task<IActionResult> CrearMateriaProfesor(MateriaProfesorRequest materiaProfesor);
        Task<IActionResult> EditarMateriaProfesor(Guid Id, MateriaProfesorRequest materiaProfesor);
        Task<IActionResult> ObtenerMateriasProfesores();

        Task<IActionResult> MateriasPorIdProfesor(Guid IdProfesor);
        Task<IActionResult> ProfesoresPorMateria(Guid IdMateria);
        Task<IActionResult> EliminarMateriaProfesor(Guid Id);



    }
}
