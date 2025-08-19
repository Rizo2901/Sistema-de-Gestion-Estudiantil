using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class SeccionFlujo: ISeccionFlujo
    {
        private readonly ISeccionDA _seccDa;

        public SeccionFlujo(ISeccionDA seccDa)
        {
            _seccDa = seccDa;
        }

        public async Task<Guid> CrearSeccion(SeccionesRequest seccion)
        {
            return await _seccDa.CrearSeccion(seccion);
        }

        public  async Task<Guid> EditarSeccion(Guid Id, SeccionesRequest seccion)
        {
           return await _seccDa.EditarSeccion(Id, seccion);
        }

        public async Task<Guid> EliminarSeccion(Guid Id)
        {
            return await _seccDa.EliminarSeccion(Id);
        }

        public async Task<IEnumerable<EstudianteSeccionesRespone>> ObtenerEstudiantesSeccion(Guid IdSeccion)
        {
            return await _seccDa.ObtenerEstudiantesSeccion(IdSeccion);
        }

        public async Task<IEnumerable<SeccionesResponse>> ObtenerSecciones()
        {
            return await _seccDa.ObtenerSecciones();
        }

        public async Task<SeccionesResponse> ObtenerSeccionPorId(Guid Id)
        {
            return await _seccDa.ObtenerSeccionPorId(Id);
        }
    }
}
