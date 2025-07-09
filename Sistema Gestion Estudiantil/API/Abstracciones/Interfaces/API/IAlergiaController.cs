using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IAlergiaController
    {
        Task<IActionResult> CrearAlergia(AlergiaRequest alergia);
        Task<IActionResult> EditarAlergia(Guid IdAlergia, AlergiaRequest alergia);
        Task<IActionResult> EliminarAlergia(Guid IdAlergia);
        Task<IActionResult> ObtenerAlergiaPorId(Guid IdAlergia);
        Task<IActionResult> ObtenerAlergias();

    }
}
