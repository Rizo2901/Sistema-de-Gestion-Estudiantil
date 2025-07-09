using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICategoriaSituacionFamiliarController
    {
        Task<IActionResult> CrearCategoriaSituacionFamiliar(CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar);
        Task<IActionResult> EditarCategoriaSituacionFamiliar(Guid Id, CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar);
        Task<IActionResult> EliminarCategoriaSituacionFamiliar(Guid Id);
        Task<IActionResult> ObtenerCategoriaSituacionFamiliarPorId(Guid Id);
        Task<IActionResult> ObtenerCategoriasSituacionFamiliar();

    }
}
