using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DA
{
    public class FamiliarDA : IFamiliarDA
    {
        private readonly IRepositorioDapper _repDapp;
        private readonly SqlConnection _conn;

        public FamiliarDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<FamiliarResponse>> ObtenerFamiliares()
        {
            string query = "SP_ObtenerFamiliares";
            var result = await _conn.QueryAsync<FamiliarResponse>(query, commandType: CommandType.StoredProcedure);

            foreach (var f in result)
            {
                f.Estudiante = new EstudianteInfo
                {
                    IdEstudiante = f.IdEstudiante,
                    Nombre = f.NombreEstudiante,
                    Apellido1 = f.Apellido1Estudiante,
                    Apellido2 = f.Apellido2Estudiante,
                    Cedula = f.CedulaEstudiante,
                    GradoActual = f.GradoEstudiante,
                    IdSeccion = f.SeccionEstudiante
                };
            }

            return result;
        }


        public async Task<FamiliarResponse> ObtenerFamiliarPorId(Guid IdFamiliar)
        {
            using var multi = await _conn.QueryMultipleAsync("SP_ObtenerFamiliarPorId",
                new { IdFamiliar },
                commandType: CommandType.StoredProcedure);

            var familiar = await multi.ReadFirstOrDefaultAsync<FamiliarResponse>();

            if (familiar == null)
                return null;

            // Mapear datos del estudiante
            familiar.Estudiante = new EstudianteInfo
            {
                IdEstudiante = familiar.IdEstudiante,
                Nombre = familiar.NombreEstudiante,
                Apellido1 = familiar.Apellido1Estudiante,
                Apellido2 = familiar.Apellido2Estudiante,
                Cedula = familiar.CedulaEstudiante,
                GradoActual = familiar.GradoEstudiante,
                IdSeccion = familiar.SeccionEstudiante
            };

            // Mapear reuniones
            var reuniones = await multi.ReadAsync<ReunionInfo>();
            familiar.Reuniones = reuniones.ToList();

            return familiar;
        }



        public async Task<Guid> CrearFamiliar(FamiliarRequest familiar)
        {
            string query = "SP_CrearFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = familiar.IdEstudiante,
                Nombre = familiar.Nombre,
                Cedula = familiar.Cedula,
                Apellido1 = familiar.Apellido1,
                Apellido2 = familiar.Apellido2,
                Direccion = familiar.Direccion,
                Parentesco = familiar.Parentesco,
                TelefonoCelular = familiar.TelefonoCelular,
                CorreoElectronico = familiar.CorreoElectronico,
                Ocupacion = familiar.Ocupacion,
                EstadoCivil = familiar.EstadoCivil,
                CantidadHijos = familiar.CantidadHijos,
                IdUsuario = familiar.IdUsuario,
                ViveConEstudiante = familiar.ViveConEstudiante,
                EsResponsableLegal = familiar.EsResponsableLegal
            },commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EditarFamiliar(Guid IdFamiliar, FamiliarRequest familiar)
        {
            string query = "SP_EditarFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdFamiliar = IdFamiliar,
                IdEstudiante = familiar.IdEstudiante,
                Nombre = familiar.Nombre,
                Cedula = familiar.Cedula,
                Apellido1 = familiar.Apellido1,
                Apellido2 = familiar.Apellido2,
                Direccion = familiar.Direccion,
                Parentesco = familiar.Parentesco,
                TelefonoCelular = familiar.TelefonoCelular,
                CorreoElectronico = familiar.CorreoElectronico,
                Ocupacion = familiar.Ocupacion,
                EstadoCivil = familiar.EstadoCivil,
                CantidadHijos = familiar.CantidadHijos,
                IdUsuario = familiar.IdUsuario,
                ViveConEstudiante = familiar.ViveConEstudiante,
                EsResponsableLegal = familiar.EsResponsableLegal
            });
            return result;
        }

        public async Task<Guid> EliminarFamiliar(Guid IdFamiliar)
        {
            string query = "SP_EliminarFamiliar";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, 
                new 
                { 
                    IdFamiliar = IdFamiliar
                });
            return result;
        }
    }
}
