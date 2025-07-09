using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IEnfermedadController
    {
        Task<IActionResult> CrearEnfermedad(EnfermedadRequest enfermedad);
        Task<IActionResult> EditarEnfermedad(Guid Id, EnfermedadRequest enfermedad);
        Task<IActionResult> EliminarEnfermedad(Guid Id);
        Task<IActionResult> ObtenerEnfermedadPorId(Guid Id);
        Task<IActionResult> ObtenerEnfermedades();
    }
}
