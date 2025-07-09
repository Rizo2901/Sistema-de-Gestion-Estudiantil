using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaDiscapacidadDA
    {
        Task<Guid> CrearCategoriaDiscapacidad(CategoriasDiscapacidadRequest categoriaDiscapacidad);
        Task<Guid> EditarCategoriaDiscapacidad(Guid Id, CategoriasDiscapacidadRequest categoriaDiscapacidad);
        Task<Guid> EliminarCategoriaDiscapacidad(Guid Id);
        Task<CategoriasDiscapacidadResponse> ObtenerCategoriaDiscapacidadPorId(Guid Id);
        Task<IEnumerable<CategoriasDiscapacidadResponse>> ObtenerCategoriasDiscapacidad();
    }
}
