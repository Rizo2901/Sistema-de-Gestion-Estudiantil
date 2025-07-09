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
    public class CategoriaDiscapacidadFlujo : ICategoriaDiscapacidadFlujo
    {
        private ICategoriaDiscapacidadDA _categoriaDiscapacidadDA;
        public CategoriaDiscapacidadFlujo(ICategoriaDiscapacidadDA categoriaDiscapacidadDA)
        {
            _categoriaDiscapacidadDA = categoriaDiscapacidadDA;
        }

        public async Task<Guid> CrearCategoriaDiscapacidad(CategoriasDiscapacidadRequest categoriaDiscapacidad)
        {
            return await _categoriaDiscapacidadDA.CrearCategoriaDiscapacidad(categoriaDiscapacidad);
        }

        public async Task<Guid> EditarCategoriaDiscapacidad(Guid Id, CategoriasDiscapacidadRequest categoriaDiscapacidad)
        {
            return await _categoriaDiscapacidadDA.EditarCategoriaDiscapacidad(Id, categoriaDiscapacidad);
        }

        public async Task<Guid> EliminarCategoriaDiscapacidad(Guid Id)
        {
            return await _categoriaDiscapacidadDA.EliminarCategoriaDiscapacidad(Id);
        }

        public async Task<CategoriasDiscapacidadResponse> ObtenerCategoriaDiscapacidadPorId(Guid Id)
        {
            return await _categoriaDiscapacidadDA.ObtenerCategoriaDiscapacidadPorId(Id);
        }

        public async Task<IEnumerable<CategoriasDiscapacidadResponse>> ObtenerCategoriasDiscapacidad()
        {
            return await _categoriaDiscapacidadDA.ObtenerCategoriasDiscapacidad();
        }
    }
}
