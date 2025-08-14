using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IHorarioController
    {
        Task<IActionResult> CrearHorario(HorarioRequest horarioRequest);
        Task<IActionResult> EditarHorario(Guid IdHorario, HorarioRequest horarioRequest);
        Task<IActionResult> EliminarHorario(Guid IdHorario);
        Task<IActionResult> ObtenerHorarioPorId(Guid IdHorario);
        Task<IActionResult> ObtenerHorarios();

        Task<IActionResult> ObtenerHorariosPorSeccion();

    }
}
