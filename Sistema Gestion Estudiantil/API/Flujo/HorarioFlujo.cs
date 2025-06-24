using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class HorarioFlujo : IHorarioFlujo
    {
        private readonly IHorarioDA _horarioDA;

        public HorarioFlujo(IHorarioDA horarioDA)
        {
            _horarioDA = horarioDA;
        }

        public Task<IEnumerable<HorarioResponse>> ObtenerHorarios() => _horarioDA.ObtenerHorarios();

        public Task<HorarioResponse> ObtenerHorarioPorId(Guid id) => _horarioDA.ObtenerHorarioPorId(id);

        public Task<Guid> CrearHorario(HorarioRequest horario) => _horarioDA.CrearHorario(horario);

        public Task<Guid> EditarHorario(Guid id, HorarioRequest horario) => _horarioDA.EditarHorario(id, horario);

        public Task<Guid> EliminarHorario(Guid id) => _horarioDA.EliminarHorario(id);
    }
}
