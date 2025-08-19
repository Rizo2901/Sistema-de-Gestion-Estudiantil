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
    public class CondicionEspecialFlujo : ICondicionEspecialFlujo
    {
        private ICondicionEspecialDA _condEspecDA;

        public CondicionEspecialFlujo(ICondicionEspecialDA condEspecDA)
        {
            _condEspecDA = condEspecDA;
        }

        public async Task<Guid> CrearCondicionEspecial(CondicionesEspecialesRequest condicionEspecial)
        {
            return await _condEspecDA.CrearCondicionEspecial(condicionEspecial);
        }

        public async Task<Guid> EditarCondicionEspecial(Guid Id, CondicionesEspecialesRequest condicionEspecial)
        {
            return await _condEspecDA.EditarCondicionEspecial(Id, condicionEspecial);
        }

        public async Task<Guid> EliminarCondicionEspecial(Guid Id)
        {
            return await _condEspecDA.EliminarCondicionEspecial(Id);
        }

        public async Task<IEnumerable<CondicionesEspecialesResponse>> ObtenerCondicionesEspeciales()
        {
            return await _condEspecDA.ObtenerCondicionesEspeciales();
        }

        public async Task<List<EstudianteConCondicionesResponse>> ObtenerCondicionesAgrupadas()
        {
            var condicionesPlanas = await _condEspecDA.ObtenerCondicionesEspeciales();

            var agrupado = condicionesPlanas
                .GroupBy(c => new
                {
                    c.IdEstudiante,
                    c.Cedula,
                    c.Nombre,
                    c.Apellido1,
                    c.Apellido2
                })
                .Select(g => new EstudianteConCondicionesResponse
                {
                    IdEstudiante = g.Key.IdEstudiante,
                    Cedula = g.Key.Cedula?.Trim(),
                    Nombre = g.Key.Nombre?.Trim(),
                    Apellido1 = g.Key.Apellido1?.Trim(),
                    Apellido2 = g.Key.Apellido2?.Trim(),
                    Condiciones = g
                        .Where(c => c.IdCondicion != Guid.Empty)
                        .Select(c => new CondicionesEspecialesResponse
                        {
                            IdCondicion = c.IdCondicion,
                            IdCategoria = c.IdCategoria,
                            FechaDiagnostico = c.FechaDiagnostico,
                            Observaciones = c.Observaciones?.Trim(),
                            NombreCategoria = c.NombreCategoria?.Trim(),
                            DescripcionCategoria = c.DescripcionCategoria?.Trim()
                        })
                        .OrderBy(c => c.FechaDiagnostico)
                        .ToList()
                })
                .OrderBy(e => e.Apellido1)
                .ThenBy(e => e.Apellido2)
                .ToList();

            return agrupado;
        }

        public async Task<CondicionesEspecialesResponse> ObtenerCondicionEspecial(Guid Id)
        {
           return await _condEspecDA.ObtenerCondicionEspecial(Id);
        }

        
    }
}
