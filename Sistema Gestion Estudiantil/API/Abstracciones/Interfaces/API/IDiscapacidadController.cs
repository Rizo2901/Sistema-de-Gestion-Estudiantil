using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IDiscapacidadController
    {
        Task<IActionResult> CrearDiscapacidad(DiscapacidadRequest discapacidad);
        Task<IActionResult> EditarDiscapacidad(Guid Id, DiscapacidadRequest discapacidad);
        Task<IActionResult> EliminarDiscapacidad(Guid Id);
        Task<IActionResult> ObtenerDiscapacidadPorId(Guid Id);
        Task<IActionResult> ObtenerDiscapacidades();
    }
}
