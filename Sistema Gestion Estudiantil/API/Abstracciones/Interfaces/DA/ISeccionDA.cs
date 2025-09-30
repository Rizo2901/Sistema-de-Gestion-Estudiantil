using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ISeccionDA
    {
        Task<IEnumerable<SeccionesResponse>> ObtenerSecciones();
        Task<IEnumerable<EstudianteSeccionesRespone>> ObtenerEstudiantesSeccion(Guid IdSeccion);
        Task<SeccionesResponse> ObtenerSeccionPorId(Guid Id);
        Task<Guid> CrearSeccion(SeccionesRequest seccion);
        Task<Guid> EditarSeccion(Guid Id, SeccionesRequest seccion);
        Task<Guid> EliminarSeccion(Guid Id);

    }
}
