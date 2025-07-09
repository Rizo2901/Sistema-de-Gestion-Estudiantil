using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IEstudianteMedicamentoController
    {
        Task<IActionResult> CrearEstudianteMedicamento(EstudianteMedicamentoRequest estudianteMedicamento);
        Task<IActionResult> EditarEstudianteMedicamento(Guid Id, EstudianteMedicamentoRequest estudianteMedicamento);
        Task<IActionResult> EliminarEstudianteMedicamento(Guid Id);
        Task<IActionResult> ObtenerEstudianteMedicamentoPorId(Guid Id);
        Task<IActionResult> ObtenerEstudianteMedicamentosPorEstudiante(Guid IdEstudiante);
    }
}
