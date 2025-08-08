using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> CrearUsuarioDesdeEntidad(CrearUsuarioEntidadRequest request);
        Task<IActionResult> ObtenerUsuarios();
        Task<IActionResult> ObtenerUsuarioPorId(Guid Id);

    }
}