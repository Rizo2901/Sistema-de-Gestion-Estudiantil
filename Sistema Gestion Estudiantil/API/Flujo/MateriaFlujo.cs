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
    public class MateriaFlujo : IMateriaFlujo
    {
        private IMateriaDA _materiaDA;

        public MateriaFlujo(IMateriaDA materiaDA)
        {
            _materiaDA = materiaDA;
        }

        public async Task<Guid> CrearMateria(MateriasRequest materia)
        {
            return await _materiaDA.CrearMateria(materia);
        }

        public async Task<Guid> EditarMateria(Guid Id, MateriasRequest materia)
        {
           return await _materiaDA.EditarMateria(Id, materia);
        }

        public async Task<Guid> EliminarMateria(Guid Id)
        {
            return await _materiaDA.EliminarMateria(Id);
        }

        public async Task<MateriasResponse> ObtenerMateria(Guid Id)
        {
            return await _materiaDA.ObtenerMateria(Id);
        }

        public async Task<IEnumerable<MateriasResponse>> ObtenerMaterias()
        {
            return await _materiaDA.ObtenerMaterias();
        }
    }
}
