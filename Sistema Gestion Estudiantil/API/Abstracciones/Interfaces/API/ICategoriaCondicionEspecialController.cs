using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICategoriaCondicionEspecialController
    {
        Task<IActionResult> CrearCategoriaCondicionEspecial(CategoriasCondicionEspecialRequest categoriaCondicionEspecial);

        Task<IActionResult> EditarCategoriaCondicionEspecial(Guid Id, CategoriasCondicionEspecialRequest categoriaCondicionEspecial);
        Task<IActionResult> EliminarCategoriaCondicionEspecial(Guid Id);
        Task<IActionResult> ObtenerCategoriaCondicionEspecialPorId(Guid Id);
        Task<IActionResult> ObtenerCategoriasCondicionEspeciales();

    }
}
