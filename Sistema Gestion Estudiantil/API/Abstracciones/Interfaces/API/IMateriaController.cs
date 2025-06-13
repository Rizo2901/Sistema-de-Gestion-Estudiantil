using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IMateriaController
    {
        Task<IActionResult> ObtenerMaterias();

        Task<IActionResult> ObtenerMateria(Guid Id);

        Task<IActionResult> CrearMateria(MateriasRequest materia);
        Task<IActionResult> EditarMateria(Guid Id, MateriasRequest materia);
        Task<IActionResult> EliminarMateria(Guid Id);

    }
}
