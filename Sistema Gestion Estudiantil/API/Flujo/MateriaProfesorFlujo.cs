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
    public class MateriaProfesorFlujo : IMateriaProfesorFlujo
    {
        private IMateriaProfesorDA _materiaProfesorDA;

        public MateriaProfesorFlujo(IMateriaProfesorDA materiaProfesorDA)
        {
            _materiaProfesorDA = materiaProfesorDA;
        }

        public async Task<Guid> CrearMateriaProfesor(MateriaProfesorRequest materiaProfesor)
        {
            return await _materiaProfesorDA.CrearMateriaProfesor(materiaProfesor);
        }

        public async Task<Guid> EditarMateriaProfesor(Guid Id, MateriaProfesorRequest materiaProfesor)
        {
            return await _materiaProfesorDA.EditarMateriaProfesor(Id, materiaProfesor);
        }

        public async Task<Guid> EliminarMateriaProfesor(Guid Id)
        {
            return await _materiaProfesorDA.EliminarMateriaProfesor(Id);
        }

        public async Task<IEnumerable<MateriaPorProfesorResponse>> MateriasPorIdProfesor(Guid IdProfesor)
        {
            return await _materiaProfesorDA.MateriasPorIdProfesor(IdProfesor);
        }

        public async Task<MateriaProfesorResponse> ObtenerMateriaProfesorPorId(Guid Id)
        {
            return await _materiaProfesorDA.ObtenerMateriaProfesorPorId(Id);
        }

        public async Task<IEnumerable<MateriaProfesorResponse>> ObtenerMateriasProfesores()
        {
            return await _materiaProfesorDA.ObtenerMateriasProfesores();
        }

        public async Task<IEnumerable<MateriaProfesorResponse>> ProfesoresPorMateria(Guid IdMateria)
        {
           return await _materiaProfesorDA.ProfesoresPorMateria(IdMateria);
        }
    }
}
