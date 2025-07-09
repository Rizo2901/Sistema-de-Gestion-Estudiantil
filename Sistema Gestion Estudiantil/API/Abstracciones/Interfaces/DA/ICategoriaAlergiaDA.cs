using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaAlergiaDA
    {
        Task<Guid> CrearCategoriaAlergia(CategoriaAlergiaRequest categoriaAlergia);
        Task<Guid> EditarCategoriaAlergia(Guid Id, CategoriaAlergiaRequest categoriaAlergia);
        Task<Guid> EliminarCategoriaAlergia(Guid Id);
        Task<CategoriaAlergiaResponse> ObtenerCategoriaAlergiaPorId(Guid IdCategoria);
        Task<IEnumerable<CategoriaAlergiaResponse>> ObtenerCategoriasAlergia();

    }
}
