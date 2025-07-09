using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaSituacionFamiliarDA
    {
        Task<Guid> CrearCategoriaSituacionFamiliar(CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar);
        Task<Guid> EditarCategoriaSituacionFamiliar(Guid Id, CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar);
        Task<Guid> EliminarCategoriaSituacionFamiliar(Guid Id);
        Task<CategoriasSituacionFamiliarResponse> ObtenerCategoriaSituacionFamiliarPorId(Guid Id);
        Task<IEnumerable<CategoriasSituacionFamiliarResponse>> ObtenerCategoriasSituacionFamiliar();

    }
}
