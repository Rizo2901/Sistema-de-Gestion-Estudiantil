
using Abstracciones.Modelos;

public interface IHorarioDA
{
   
    Task<Guid> CrearHorario(HorarioRequest horario);
    Task<Guid> EditarHorario(Guid IdHorario, HorarioRequest horario);
    Task<Guid> EliminarHorario(Guid IdHorario);

    Task<IEnumerable<HorarioResponse>> ObtenerHorarios();
    Task<HorarioResponse> ObtenerHorarioPorId(Guid IdHorario);
}


