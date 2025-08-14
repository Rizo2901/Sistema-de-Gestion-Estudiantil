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

        public async Task<Guid> CrearHorario(HorarioRequest horario)
        {
            return await _horarioDA.CrearHorario(horario);
        }

        public async Task<Guid> EditarHorario(Guid IdHorario, HorarioRequest horario)
        {
            return await _horarioDA.EditarHorario(IdHorario, horario);
        }

        public async Task<Guid> EliminarHorario(Guid IdHorario)
        {
            return await _horarioDA.EliminarHorario(IdHorario);
        }

        public async Task<HorarioResponse> ObtenerHorarioPorId(Guid IdHorario)
        {
            return await _horarioDA.ObtenerHorarioPorId(IdHorario);
        }

        public async Task<IEnumerable<HorarioResponse>> ObtenerHorarios()
        {
            return await _horarioDA.ObtenerHorarios();
        }

        public async Task<List<HorarioPorSeccion>> ObtenerHorariosPorSeccion()
        {
            // Obtener lista plana del DA
            var horariosPlanos = await _horarioDA.ObtenerHorarios();

            // Agrupar y transformar
            var resultado = horariosPlanos
                .GroupBy(h => new { h.IdSeccion, h.NombreSeccion })
                .Select(gSeccion => new HorarioPorSeccion
                {
                    IdSeccion = gSeccion.Key.IdSeccion,
                    NombreSeccion = gSeccion.Key.NombreSeccion.Trim(),
                    Dias = gSeccion
                        .GroupBy(h => h.Dia)
                        .Select(gDia => new HorarioPorDia
                        {
                            Dia = gDia.Key,
                            Horarios = gDia.Select(h => new HorarioDetalle
                            {
                                IdHorario = h.IdHorario,
                                IdSeccion = h.IdSeccion,
                                IdMateria = h.IdMateria,
                                IdProfesor = h.IdProfesor,
                                HoraInicio = h.HoraInicio,
                                HoraFin = h.HoraFin,
                                NombreMateria = h.NombreMateria,
                                NombreProfesor = h.NombreProfesor,
                                Aula = h.Aula
                            })
                            .OrderBy(hd => hd.HoraInicio)
                            .ToList()
                        })
                        .OrderBy(d => OrdenDiaSemana(d.Dia))
                        .ToList()
                })
                .OrderBy(s => s.NombreSeccion)
                .ToList();

            return resultado;
        }

        private int OrdenDiaSemana(string dia)
        {
            return dia switch
            {
                "Lunes" => 1,
                "Martes" => 2,
                "Miércoles" => 3,
                "Jueves" => 4,
                "Viernes" => 5,
                "Sábado" => 6,
                "Domingo" => 7,
                _ => 99
            };
        }
    }
}
