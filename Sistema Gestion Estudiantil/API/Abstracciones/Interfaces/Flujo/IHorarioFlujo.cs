using Abstracciones.Modelos;


namespace Abstracciones.Interfaces.Flujo
{
    public interface IHorarioFlujo
    {
  
        Task<Guid> CrearHorario(HorarioRequest horario);
        Task<Guid> EditarHorario(Guid IdHorario, HorarioRequest horario);
        Task<Guid> EliminarHorario(Guid IdHorario);
        Task<IEnumerable<HorarioResponse>> ObtenerHorarios();
        Task<HorarioResponse> ObtenerHorarioPorId(Guid IdHorario);

        Task<List<HorarioPorSeccion>> ObtenerHorariosPorSeccion();
    }
}
