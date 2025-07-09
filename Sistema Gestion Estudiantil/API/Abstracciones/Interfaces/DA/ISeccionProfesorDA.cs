using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ISeccionProfesorDA
    {
        Task<IEnumerable<SeccionProfesorResponse>> ObtenerSeccionesProfesores();                        
        Task<SeccionProfesorResponse> ObtenerSeccionProfesorPorId(Guid Id);                            
        Task<IEnumerable<SeccionProfesorResponse>> ObtenerProfesoresPorSeccion(Guid IdSeccion);         
        Task<IEnumerable<SeccionProfesorResponse>> ObtenerSeccionesPorProfesor(Guid IdProfesor);         

        Task<Guid> CrearSeccionProfesor(SeccionProfesorRequest relacion);                                
        Task<Guid> EditarSeccionProfesor(Guid Id, SeccionProfesorRequest relacion);                      
        Task<Guid> EliminarSeccionProfesor(Guid Id);
    }
}
