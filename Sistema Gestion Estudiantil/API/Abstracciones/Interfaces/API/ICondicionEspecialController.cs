using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICondicionEspecialController
    {
        Task<IActionResult> ObtenerCondicionesEspeciales();

        Task<IActionResult> ObtenerCondicionEspecial(Guid Id);

        Task<IActionResult> ObtenerCondicionesEspecialesPorEstudiante(Guid IdEstudiante);

        Task<IActionResult> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial);

        Task<IActionResult> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial);

        Task<IActionResult> EliminarCondicionEspecial(Guid Id);

        Task<IActionResult> ObtenerEstudiantesConCondiciones();


    }
}
