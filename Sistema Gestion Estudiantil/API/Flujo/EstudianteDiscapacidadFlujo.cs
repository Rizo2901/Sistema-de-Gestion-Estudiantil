using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;


namespace Flujo
{
    public class EstudianteDiscapacidadFlujo : IEstudianteDiscapacidadFlujo
    {
        private IEstudianteDiscapacidadDA _estudianteDiscapacidadDA;
        public EstudianteDiscapacidadFlujo(IEstudianteDiscapacidadDA estudianteDiscapacidadDA)
        {
            _estudianteDiscapacidadDA = estudianteDiscapacidadDA;
        }

        public async Task<Guid> CrearEstudianteDiscapacidad(EstudianteDiscapacidadRequest estudianteDiscapacidad)
        {
            return await _estudianteDiscapacidadDA.CrearEstudianteDiscapacidad(estudianteDiscapacidad);
        }

        public async Task<Guid> EditarEstudianteDiscapacidad(Guid Id, EstudianteDiscapacidadRequest estudianteDiscapacidad)
        {
            return await _estudianteDiscapacidadDA.EditarEstudianteDiscapacidad(Id, estudianteDiscapacidad);
        }

        public async Task<Guid> EliminarEstudianteDiscapacidad(Guid Id)
        {
            return await _estudianteDiscapacidadDA.EliminarEstudianteDiscapacidad(Id);
        }

        public async Task<IEnumerable<EstudianteDiscapacidadResponse>> ObtenerDiscapacidadesConEstudiantes()
        {
            return await _estudianteDiscapacidadDA.ObtenerDiscapacidadesConEstudiantes();

        }

        public async Task<IEnumerable<EstudianteDiscapacidadResponse>> ObtenerDiscapacidadesPorEstudiante(Guid idEstudiante)
        {
            return await _estudianteDiscapacidadDA.ObtenerDiscapacidadesPorEstudiante(idEstudiante);
        }

        public async Task<EstudianteDiscapacidadResponse> ObtenerEstudianteDiscapacidadPorId(Guid Id)
        {
            return await _estudianteDiscapacidadDA.ObtenerEstudianteDiscapacidadPorId(Id);
        }

        public async Task<IEnumerable<EstudianteConDiscapacidadesResponse>> ObtenerResumenDiscapacidadesPorEstudiante()
        {
            return await _estudianteDiscapacidadDA.ObtenerResumenDiscapacidadesPorEstudiante();
        }
    }
}
