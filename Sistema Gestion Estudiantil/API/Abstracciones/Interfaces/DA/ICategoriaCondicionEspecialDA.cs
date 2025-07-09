using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaCondicionEspecialDA
    {
        Task<Guid> CrearCategoriaCondicionEspecial(CategoriasCondicionEspecialRequest categoriaCondicionEspecial);
        Task<Guid> EditarCategoriaCondicionEspecial(Guid Id, CategoriasCondicionEspecialRequest categoriaCondicionEspecial);
        Task<Guid> EliminarCategoriaCondicionEspecial(Guid Id);
        Task<CategoriasCondicionEspecialResponse> ObtenerCategoriaCondicionEspecialPorId(Guid Id);
        Task<IEnumerable<CategoriasCondicionEspecialResponse>> ObtenerCategoriasCondicionEspeciales();
    }
}
