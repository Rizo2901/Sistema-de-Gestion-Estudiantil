using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IEstudianteAlergiaController
    {
        Task<IActionResult> CrearEstudianteAlergia(EstudianteAlergiaRequest estudianteAlergia);
        Task<IActionResult> EditarEstudianteAlergia(Guid Id, EstudianteAlergiaRequest estudianteAlergia);
        Task<IActionResult> EliminarEstudianteAlergia(Guid Id);
        Task<IActionResult> ObtenerEstudianteAlergiaPorId(Guid Id);
        Task<IActionResult> ObtenerAlergiasPorEstudiante(Guid IdEstudiante);
       

        Task<IActionResult> ObtenerTodasLasAlergiasEstudiantes();
        Task<IActionResult> ObtenerAlergiasAgrupadasPorEstudiantes();

    }
}
