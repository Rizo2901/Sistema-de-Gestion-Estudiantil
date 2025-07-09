using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;


namespace Flujo
{
    public class CategoriaAlergiaFlujo : ICategoriaAlergiaFlujo
    {
        private ICategoriaAlergiaDA _categoriaAlergiaDA;
        public CategoriaAlergiaFlujo(ICategoriaAlergiaDA categoriaAlergiaDA)
        {
            _categoriaAlergiaDA = categoriaAlergiaDA;
        }
        public async Task<Guid> CrearCategoriaAlergia(CategoriaAlergiaRequest categoriaAlergia)
        {
            return await _categoriaAlergiaDA.CrearCategoriaAlergia(categoriaAlergia);
        }

        public async Task<Guid> EditarCategoriaAlergia(Guid Id, CategoriaAlergiaRequest categoriaAlergia)
        {
            return await _categoriaAlergiaDA.EditarCategoriaAlergia(Id, categoriaAlergia);
        }

        public async Task<Guid> EliminarCategoriaAlergia(Guid Id)
        {
            return await _categoriaAlergiaDA.EliminarCategoriaAlergia(Id);
        }

        public async Task<CategoriaAlergiaResponse> ObtenerCategoriaAlergiaPorId(Guid IdCategoria)
        {
            return await _categoriaAlergiaDA.ObtenerCategoriaAlergiaPorId(IdCategoria);
        }

        public async Task<IEnumerable<CategoriaAlergiaResponse>> ObtenerCategoriasAlergia()
        {
            return await _categoriaAlergiaDA.ObtenerCategoriasAlergia();
        }
    }
}
