using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IMedicamentoController
    {
        Task<IActionResult> CrearMedicamento( MedicamentoResponse medicamento);

        Task<IActionResult> EditarMedicamento(Guid Id, MedicamentoResponse medicamento);

        Task<IActionResult> EliminarMedicamento(Guid Id);
        Task<IActionResult> ObtenerMedicamentoPorId(Guid Id);
        Task<IActionResult> ObtenerMedicamentos();

    }
}
