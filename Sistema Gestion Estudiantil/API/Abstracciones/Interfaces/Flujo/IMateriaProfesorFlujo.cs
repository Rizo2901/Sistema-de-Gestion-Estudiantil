using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMateriaProfesorFlujo
    {
        Task<IEnumerable<MateriaProfesorResponse>> ObtenerMateriasProfesores();
        Task<IEnumerable<MateriaPorProfesorResponse>> MateriasPorIdProfesor(Guid IdProfesor);
        Task<IEnumerable<MateriaProfesorResponse>> ProfesoresPorMateria(Guid IdMateria);
        Task<MateriaProfesorResponse> ObtenerMateriaProfesorPorId(Guid Id);
        Task<Guid> CrearMateriaProfesor(MateriaProfesorRequest materiaProfesor);

        Task<Guid> EditarMateriaProfesor(Guid Id, MateriaProfesorRequest materiaProfesor);
        Task<Guid> EliminarMateriaProfesor(Guid Id);

    }
}
