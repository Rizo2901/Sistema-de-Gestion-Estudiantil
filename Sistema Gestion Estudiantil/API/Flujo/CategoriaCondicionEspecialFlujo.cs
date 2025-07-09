using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class CategoriaCondicionEspecialFlujo : ICategoriaCondicionEspecialFlujo
    {
        private ICategoriaCondicionEspecialDA _categoriaCondicionEspecialDA;
        public CategoriaCondicionEspecialFlujo(ICategoriaCondicionEspecialDA categoriaCondicionEspecialDA)
        {
            _categoriaCondicionEspecialDA = categoriaCondicionEspecialDA;
        }

        public async Task<Guid> CrearCategoriaCondicionEspecial(CategoriasCondicionEspecialRequest categoriaCondicionEspecial)
        {
            return await _categoriaCondicionEspecialDA.CrearCategoriaCondicionEspecial(categoriaCondicionEspecial);
        }

        public async Task<Guid> EditarCategoriaCondicionEspecial(Guid Id, CategoriasCondicionEspecialRequest categoriaCondicionEspecial)
        {
            return await _categoriaCondicionEspecialDA.EditarCategoriaCondicionEspecial(Id, categoriaCondicionEspecial);
        }

        public async Task<Guid> EliminarCategoriaCondicionEspecial(Guid Id)
        {
            return await _categoriaCondicionEspecialDA.EliminarCategoriaCondicionEspecial(Id);
        }

        public async Task<CategoriasCondicionEspecialResponse> ObtenerCategoriaCondicionEspecialPorId(Guid Id)
        {
            return await _categoriaCondicionEspecialDA.ObtenerCategoriaCondicionEspecialPorId(Id);
        }

        public async Task<IEnumerable<CategoriasCondicionEspecialResponse>> ObtenerCategoriasCondicionEspeciales()
        {
            return await _categoriaCondicionEspecialDA.ObtenerCategoriasCondicionEspeciales();
        }
    }
}
