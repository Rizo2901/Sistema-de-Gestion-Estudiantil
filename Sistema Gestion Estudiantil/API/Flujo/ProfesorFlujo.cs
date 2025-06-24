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
    public class ProfesorFlujo : IProfesorFlujo
    {
        private IProfesorDA _profesorDA;

        public ProfesorFlujo(IProfesorDA profesorDA)
        {
            _profesorDA = profesorDA;
        }

        public async Task<Guid> CrearProfesor(ProfesorRequest profesor)
        {
           return await _profesorDA.CrearProfesor(profesor);
        }

        public async Task<Guid> EditarProfesor(Guid Id, ProfesorRequest profesor)
        {
            return await _profesorDA.EditarProfesor(Id, profesor);
        }

        public async Task<Guid> EliminarProfesor(Guid Id)
        {
            return await _profesorDA.EliminarProfesor(Id);
        }

        public async Task<ProfesorResponse> ObtenerProfesor(Guid Id)
        {
            return await _profesorDA.ObtenerProfesor(Id);
        }

        public async Task<IEnumerable<ProfesorResponse>> ObtenerProfesores()
        {
            return await _profesorDA.ObtenerProfesores();
        }
    }
}
