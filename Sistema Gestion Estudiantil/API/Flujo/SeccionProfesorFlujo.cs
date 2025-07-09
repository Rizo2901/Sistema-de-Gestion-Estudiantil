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
    public class SeccionProfesorFlujo : ISeccionProfesorFlujo
    {
        private  ISeccionProfesorDA _seccionProfesorDA;
        public SeccionProfesorFlujo(ISeccionProfesorDA seccionProfesorDA)
        {
            _seccionProfesorDA = seccionProfesorDA;
        }

        public async Task<Guid> CrearSeccionProfesor(SeccionProfesorRequest relacion)
        {
            return await _seccionProfesorDA.CrearSeccionProfesor(relacion);
        }

        public async Task<Guid> EditarSeccionProfesor(Guid Id, SeccionProfesorRequest relacion)
        {
            return await _seccionProfesorDA.EditarSeccionProfesor(Id, relacion);
        }

        public async Task<Guid> EliminarSeccionProfesor(Guid Id)
        {
            return await _seccionProfesorDA.EliminarSeccionProfesor(Id);
        }

        public async Task<IEnumerable<SeccionProfesorResponse>> ObtenerProfesoresPorSeccion(Guid IdSeccion)
        {
            return await _seccionProfesorDA.ObtenerProfesoresPorSeccion(IdSeccion);
        }

        public async Task<IEnumerable<SeccionProfesorResponse>> ObtenerSeccionesPorProfesor(Guid IdProfesor)
        {
            return await _seccionProfesorDA.ObtenerSeccionesPorProfesor(IdProfesor);
        }

        public async Task<IEnumerable<SeccionProfesorResponse>> ObtenerSeccionesProfesores()
        {
            return await _seccionProfesorDA.ObtenerSeccionesProfesores();
        }

        public async Task<SeccionProfesorResponse> ObtenerSeccionProfesorPorId(Guid Id)
        {
            return await _seccionProfesorDA.ObtenerSeccionProfesorPorId(Id);
        }
    }
}
