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
    public class AsistenciaProfesorFlujo : IAsistenciaProfesorFlujo
    {
        private IAsistenciaProfesorDA _asistenciaProfesorDA;
        public AsistenciaProfesorFlujo(IAsistenciaProfesorDA asistenciaProfesorDA)
        {
            _asistenciaProfesorDA = asistenciaProfesorDA;
        }

        public async Task<Guid> CrearAsitenciaProfesor(AsistenciaProfesorRequest asistenciaProfesor)
        {
            return await _asistenciaProfesorDA.CrearAsitenciaProfesor(asistenciaProfesor);
        }

        public async Task<Guid> EditarAsitenciaProfesor(Guid IdAsistenciaProfesor, AsistenciaProfesorRequest asistenciaProfesor)
        {
            return await _asistenciaProfesorDA.EditarAsitenciaProfesor(IdAsistenciaProfesor, asistenciaProfesor);
        }

        public async Task<Guid> EliminarAsitenciaProfesor(Guid IdAsistenciaProfesor)
        {
            return await _asistenciaProfesorDA.EliminarAsitenciaProfesor(IdAsistenciaProfesor);
        }

        public async Task<IEnumerable<AsistenciaProfesorResponse>> ObtenerAsitenciaProfesores()
        {
            return await _asistenciaProfesorDA.ObtenerAsitenciaProfesores();
        }

        public async Task<IEnumerable<AsistenciaProfesorResponse>> ObtenerAsitenciaProfesoresPorIdProfesor(Guid IdProfesor)
        {
            return await _asistenciaProfesorDA.ObtenerAsitenciaProfesoresPorIdProfesor(IdProfesor);
        }

        public async Task<AsistenciaProfesorResponse> ObtenerAsitenciaProfesorPorId(Guid IdAsistenciaProfesor)
        {
           return await _asistenciaProfesorDA.ObtenerAsitenciaProfesorPorId(IdAsistenciaProfesor);
        }
    }
}
