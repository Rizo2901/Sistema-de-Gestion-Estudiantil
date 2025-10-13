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
    public class AtencionMedicaFlujo : IAtencionMedicaFlujo
    {
        private IAtencionMedicaDA _antencionMedica;

        public AtencionMedicaFlujo(IAtencionMedicaDA antencionMedica)
        {
            _antencionMedica = antencionMedica;
        }

        public async Task<Guid> CrearAtencionMedica(AtencionMedicaRequest atencion)
        {
            return await _antencionMedica.CrearAtencionMedica(atencion);
        }

        public async Task<Guid> EditarAtencionMedica(Guid IdAtencionMedica, AtencionMedicaRequest atencion)
        {
            return await _antencionMedica.EditarAtencionMedica(IdAtencionMedica, atencion);
        }

        public async Task<Guid> EliminarAtencionMedica(Guid IdAtencionMedica)
        {
            return await _antencionMedica.EliminarAtencionMedica(IdAtencionMedica);
        }

        public async Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicas()
        {
            return await _antencionMedica.ObtenerAtencionesMedicas();
        }

        public async Task<IList<EstudianteConAtencionesMedicas>> ObtenerAtencionesMedicasAgrupadas()
        {
            var atencionesPlanas = await _antencionMedica.ObtenerAtencionesMedicas();

            var agrupado = atencionesPlanas
                      .GroupBy(c => new
                      {
                          c.IdEstudiante,
                          c.Cedula,
                          c.Nombre,
                          c.Apellido1,
                          c.Apellido2
                      })
                .Select(g => new EstudianteConAtencionesMedicas
                {
                    IdEstudiante = g.Key.IdEstudiante,
                    Cedula = g.Key.Cedula?.Trim(),
                    Nombre = g.Key.Nombre?.Trim(),
                    Apellido1 = g.Key.Apellido1?.Trim(),
                    Apellido2 = g.Key.Apellido2?.Trim(),
                    AtencionesMedicas = g
                        .Where(a => a.IdAtencionMedica != Guid.Empty)
                        .Select(a => new AtencionMedicaResponse
                        {
                            IdAtencionMedica = a.IdAtencionMedica,
                            IdEstudiante = a.IdEstudiante,
                            NombreCompleto = $"{a.Nombre?.Trim()} {a.Apellido1?.Trim()} {a.Apellido2?.Trim()}".Trim(),
                            Diagnostico = a.Diagnostico,
                            Tratamiento = a.Tratamiento,    
                            Fecha = a.Fecha
                        })
                        .OrderBy(c => c.Fecha)
                        .ToList()
                })
                .OrderBy(e => e.Apellido1)
                .ThenBy(e => e.Apellido2)
                .ToList();

            return agrupado;
        }

        public async Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicasPorEstudiante(Guid IdEstudiante)
        {
           return await _antencionMedica.ObtenerAtencionesMedicasPorEstudiante(IdEstudiante);
        }

        public async Task<AtencionMedicaResponse> ObtenerAtencionMedicaPorId(Guid IdAtencionMedica)
        {
            return await _antencionMedica.ObtenerAtencionMedicaPorId(IdAtencionMedica);
        }
    }
}
