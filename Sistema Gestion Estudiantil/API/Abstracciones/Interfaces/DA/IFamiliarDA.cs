using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

public interface IFamiliarDA
{
    Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares();
    Task<FamiliarResponse> ObtenerFamiliarPorId(Guid id);
    Task<Guid> CrearFamiliar(FamiliarRequest familiar);
    Task<Guid> EditarFamiliar(Guid id, FamiliarRequest familiar);
    Task<Guid> EliminarFamiliar(Guid id);
}
