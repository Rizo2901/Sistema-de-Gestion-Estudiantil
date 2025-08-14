using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.API
{
    public interface IFamiliarController
    {
        Task<IActionResult> CrearFamiliar(FamiliarRequest familiarRequest);
        Task<IActionResult> EditarFamiliar(Guid IdFamiliar, FamiliarRequest familiarRequest);
        Task<IActionResult> EliminarFamiliar(Guid IdFamiliar);
        Task<IActionResult> ObtenerFamiliarPorId(Guid IdFamiliar);
        Task<IActionResult> ObtenerFamiliares();

    }
}
