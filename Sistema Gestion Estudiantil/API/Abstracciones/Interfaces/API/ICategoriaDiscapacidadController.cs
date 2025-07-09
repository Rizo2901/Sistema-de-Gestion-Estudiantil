using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICategoriaDiscapacidadController
    {
        Task<IActionResult> CrearCategoriaDiscapacidad(CategoriasDiscapacidadRequest categoriaDiscapacidad);
        Task<IActionResult> EditarCategoriaDiscapacidad(Guid Id, CategoriasDiscapacidadRequest categoriaDiscapacidad);
        Task<IActionResult> EliminarCategoriaDiscapacidad(Guid Id);
        Task<IActionResult> ObtenerCategoriaDiscapacidadPorId(Guid Id);
        Task<IActionResult> ObtenerCategoriasDiscapacidad();
    }
}
