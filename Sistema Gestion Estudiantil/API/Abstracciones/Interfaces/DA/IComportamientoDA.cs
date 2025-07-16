using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

public interface IComportamientoDA
{
    Task<IEnumerable<ComportamientoResponse>> ObtenerComportamientos();
    Task<ComportamientoResponse> ObtenerComportamientoPorId(Guid id);
    Task<Guid> CrearComportamiento(ComportamientoRequest comportamiento);
    Task<Guid> EditarComportamiento(Guid id, ComportamientoRequest comportamiento);
    Task<Guid> EliminarComportamiento(Guid id);
}

