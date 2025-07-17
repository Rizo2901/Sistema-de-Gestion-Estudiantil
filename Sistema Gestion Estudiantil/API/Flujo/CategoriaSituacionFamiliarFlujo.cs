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
    public class CategoriaSituacionFamiliarFlujo : ICategoriaSituacionFamiliarFlujo
    {
        private ICategoriaSituacionFamiliarDA _categoriaSituacionFamiliarDA;
        public CategoriaSituacionFamiliarFlujo(ICategoriaSituacionFamiliarDA categoriaSituacionFamiliarDA)
        {
            _categoriaSituacionFamiliarDA = categoriaSituacionFamiliarDA;
        }
        public async Task<Guid> CrearCategoriaSituacionFamiliar(CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar)
        {
           return await _categoriaSituacionFamiliarDA.CrearCategoriaSituacionFamiliar(categoriaSituacionFamiliar);
        }

        public async Task<Guid> EditarCategoriaSituacionFamiliar(Guid Id, CategoriasSituacionFamiliarRequest categoriaSituacionFamiliar)
        {
            return await _categoriaSituacionFamiliarDA.EditarCategoriaSituacionFamiliar(Id, categoriaSituacionFamiliar);
        }

        public async Task<Guid> EliminarCategoriaSituacionFamiliar(Guid Id)
        {
            return await _categoriaSituacionFamiliarDA.EliminarCategoriaSituacionFamiliar(Id);
        }

        public async Task<CategoriasSituacionFamiliarResponse> ObtenerCategoriaSituacionFamiliarPorId(Guid Id)
        {
            return await _categoriaSituacionFamiliarDA.ObtenerCategoriaSituacionFamiliarPorId(Id);
        }

        public async Task<IEnumerable<CategoriasSituacionFamiliarResponse>> ObtenerCategoriasSituacionFamiliar()
        {
            return await _categoriaSituacionFamiliarDA.ObtenerCategoriasSituacionFamiliar();
        }
    }
}
