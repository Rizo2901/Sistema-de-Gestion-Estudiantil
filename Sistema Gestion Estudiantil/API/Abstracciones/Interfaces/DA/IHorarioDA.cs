using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

public interface IHorarioDA
{
    Task<IEnumerable<HorarioResponse>> ObtenerHorarios();
    Task<HorarioResponse> ObtenerHorarioPorId(Guid id);
    Task<Guid> CrearHorario(HorarioRequest horario);
    Task<Guid> EditarHorario(Guid id, HorarioRequest horario);
    Task<Guid> EliminarHorario(Guid id);
}

public interface IHorarioFlujo
{
    Task<IEnumerable<HorarioResponse>> ObtenerHorarios();
    Task<HorarioResponse> ObtenerHorarioPorId(Guid id);
    Task<Guid> CrearHorario(HorarioRequest horario);
    Task<Guid> EditarHorario(Guid id, HorarioRequest horario);
    Task<Guid> EliminarHorario(Guid id);
}
