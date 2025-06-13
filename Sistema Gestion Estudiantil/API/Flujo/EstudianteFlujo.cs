using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;


namespace Flujo
{
    public class EstudianteFlujo : IEstudianteFlujo
    {
        private readonly IEstudianteDA _estudianteDA;

        public EstudianteFlujo(IEstudianteDA estudianteDA)
        {
            _estudianteDA = estudianteDA;
        }

        public async Task<IEnumerable<EstudiantesResponse>> BuscarEstudiantes(string? nombre = null, string? cedula = null, string? grado = null, string? seccionNombre = null)
        {
            return await _estudianteDA.BuscarEstudiantes(nombre, cedula, grado, seccionNombre);
        }

        public async Task<Guid> CrearEstudiante(EstudiantesRequest estudiante)
        {
           return await _estudianteDA.CrearEstudiante(estudiante);
        }

        public async Task<Guid> EditarEstudiante(Guid Id, EstudiantesRequest estudiante)
        {
          return await _estudianteDA.EditarEstudiante(Id, estudiante);
        }

        public async Task<Guid> EliminarEstudiante(Guid Id)
        {
            return await _estudianteDA.EliminarEstudiante(Id);
        }

        public async Task<EstudiantesResponse> ObtenerEstudiantePorId(Guid Id)
        {
            return await _estudianteDA.ObtenerEstudiantePorId(Id);
        }

        public async Task<IEnumerable<EstudiantesResponse>> ObtenerEstudiantes()
        {
           return await _estudianteDA.ObtenerEstudiantes();
        }
    }
}
