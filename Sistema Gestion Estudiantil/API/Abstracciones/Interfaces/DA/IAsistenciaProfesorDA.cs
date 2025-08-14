using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IAsistenciaProfesorDA
    {
        Task<Guid> CrearAsitenciaProfesor(AsistenciaProfesorRequest asistenciaProfesor);
        Task<Guid> EditarAsitenciaProfesor(Guid IdAsistenciaProfesor, AsistenciaProfesorRequest asistenciaProfesor);
        Task<Guid> EliminarAsitenciaProfesor(Guid IdAsistenciaProfesor);
        Task<AsistenciaProfesorResponse> ObtenerAsitenciaProfesorPorId(Guid IdAsistenciaProfesor);
        Task<IEnumerable<AsistenciaProfesorResponse>> ObtenerAsitenciaProfesores();
        Task<IEnumerable<AsistenciaProfesorResponse>> ObtenerAsitenciaProfesoresPorIdProfesor(Guid IdProfesor);
    }
}
