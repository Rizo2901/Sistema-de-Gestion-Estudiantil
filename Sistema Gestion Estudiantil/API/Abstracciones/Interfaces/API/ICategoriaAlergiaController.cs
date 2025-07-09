using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICategoriaAlergiaController
    {
        Task<IActionResult>CrearCategoriaAlergia(CategoriaAlergiaRequest categoriaAlergia);
        Task<IActionResult> EditarCategoriaAlergia(Guid Id, CategoriaAlergiaRequest categoriaAlergia);
        Task<IActionResult> EliminarCategoriaAlergia(Guid Id);
        Task<IActionResult> ObtenerCategoriaAlergiaPorId(Guid IdCategoria);
        Task<IActionResult> ObtenerCategoriasAlergia();

    }
}
