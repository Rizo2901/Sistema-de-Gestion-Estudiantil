using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMateriaFlujo
    {
        Task<IEnumerable<MateriasResponse>> ObtenerMaterias();
        Task<MateriasResponse> ObtenerMateria(Guid Id);
        Task<Guid> CrearMateria(MateriasRequest materia);
        Task<Guid> EditarMateria(Guid Id, MateriasRequest materia);
        Task<Guid> EliminarMateria(Guid Id);

    }
}
