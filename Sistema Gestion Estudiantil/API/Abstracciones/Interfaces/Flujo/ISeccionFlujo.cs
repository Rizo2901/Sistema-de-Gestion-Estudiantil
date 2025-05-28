using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ISeccionFlujo
    {
        Task<IEnumerable<SeccionesResponse>> ObtenerSecciones();

        Task<SeccionesResponse> ObtenerSeccionPorId(Guid Id);

        Task <Guid> CrearSeccion(SeccionesRequest seccion);

        Task<Guid> EditarSeccion(Guid Id, SeccionesRequest seccion);

        Task<Guid> EliminarSeccion(Guid Id);



    }
}
