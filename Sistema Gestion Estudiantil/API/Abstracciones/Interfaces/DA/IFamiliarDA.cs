using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

public interface IFamiliarDA
{
    
    Task<Guid> CrearFamiliar(FamiliarRequest familiar);
    Task<Guid> EditarFamiliar(Guid IdFamiliar, FamiliarRequest familiar);
    Task<Guid> EliminarFamiliar(Guid IdFamiliar);
    Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares();
    Task<FamiliarResponse> ObtenerFamiliarPorId(Guid IdFamiliar);
}
