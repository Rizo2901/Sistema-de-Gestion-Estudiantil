using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IEstudianteDA
    {
        Task<IEnumerable<EstudiantesResponse>> ObtenerEstudiantes();
        Task<IEnumerable<EstudiantesResumenResponse>> ObtenerEstudiantesResumen();
        Task<int> ObtenerTotalEstudiantesActivos();
        Task<IEnumerable<EstudiantesResponse>> BuscarEstudiantes(string? nombre = null, string? cedula = null, string? grado = null, string? seccionNombre = null);

        Task<EstudiantesResponse> ObtenerEstudiantePorId(Guid Id);

        Task<Guid> CrearEstudiante(EstudiantesRequest estudiante);

        Task<Guid> EditarEstudiante(Guid Id, EstudiantesRequest estudiante);

        Task<Guid> EliminarEstudiante(Guid Id);
    }
}
