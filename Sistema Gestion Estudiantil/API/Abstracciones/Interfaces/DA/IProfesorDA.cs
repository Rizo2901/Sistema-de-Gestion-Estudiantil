using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IProfesorDA
    {
        Task<Guid> CrearProfesor(ProfesorRequest profesor);
        Task<Guid> EditarProfesor(Guid Id, ProfesorRequest profesor);

        Task<Guid> EliminarProfesor(Guid Id);

        Task<IEnumerable<ProfesorResponse>> ObtenerProfesores ();
        Task<ProfesorResponse> ObtenerProfesor(Guid Id);

        Task<int> ObtenerProfesoresTotal();

    }
}
