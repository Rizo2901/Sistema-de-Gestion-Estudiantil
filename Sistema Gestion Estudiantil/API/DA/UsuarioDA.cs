using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        private readonly SqlConnection _conn;
        private readonly IRepositorioDapper _repDapp;

        public UsuarioDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearUsuario(UsuariosRequest usuario)
        {
            const string query = "Usuario.CrearUsuario";

            var parametros = new
            {
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena,
                CorreoElectronico = usuario.CorreoElectronico,
                IdRol = usuario.IdRol,
                TipoEntidad = usuario.TipoEntidad
            };

            var result = await _conn.ExecuteScalarAsync<Guid>(
                query,
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<Guid> EditarUsuario(Guid Id, UsuariosRequest usuario)
        {
            string query = @"Usuario.ActualizarUsuario";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdUsuario = Id,
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena,
                CorreoElectronico = usuario.CorreoElectronico,
                IdRol = usuario.IdRol,
                TipoEntidad = usuario.TipoEntidad
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EliminarUsuario(Guid Id)
        {
            string query = @"Usuario.EliminarUsuario";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdUsuario = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<UsuariosResponse>> ObtenerUsuarios()
        {
            string query = @"Usuario.ConsultarUsuarios";
            var result = await _conn.QueryAsync<UsuariosResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<UsuariosResponse> ObtenerUsuarioPorId(Guid Id)
        {
            string query = @"Usuario.ObtenerUsuarioPorId";
            var result = await _conn.QueryFirstOrDefaultAsync<UsuariosResponse>(query, new
            {
                IdUsuario = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> CambiarEstadoUsuario(Guid Id, bool estado)
        {
            string query = @"Usuario.CambiarEstadoUsuario";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdUsuario = Id,
                NuevoEstado = estado ? 1 : 0
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena)
        {
            string query = @"Usuario.ActualizarContrasena";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdUsuario = Id,
                NuevaContrasena = nuevaContrasena
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> CambiarContrasena(Guid Id, string nuevaContrasena, string contrasenaActual)
        {
            // Esta lógica se implementa en backend, no en SP.
            // Aquí solo se llama a Usuario.ActualizarContrasena si la verificación ya se hizo.
            return await CambiarContrasena(Id, nuevaContrasena);
        }

        public async Task<UsuariosResponse> ObtenerPorNombreOEmail(string identificador)
        {
            string query = @"Usuario.ObtenerPorNombreOEmail";

            var result = await _conn.QueryFirstOrDefaultAsync<UsuariosResponse>(
                query,
                new { Identificador = identificador },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<EntidadBasicaResponse> ObtenerDatosEntidadAsync(string tipoEntidad, Guid idEntidad)
        {
            string sp = tipoEntidad switch
            {
                "Estudiante" => "Entidad.ObtenerEstudianteBasico",
                "Profesor" => "Entidad.ObtenerProfesorBasico",
                "Familiar" => "Entidad.ObtenerFamiliarBasico",
                "Administrador" => "Entidad.ObtenerAdministradorBasico",
                _ => throw new Exception("Tipo de entidad no soportado.")
            };

            string nombreParametro = tipoEntidad switch
            {
                "Estudiante" => "IdEstudiante",
                "Profesor" => "IdProfesor",
                "Familiar" => "IdFamiliar",
                "Administrador" => "IdAdministrador",
                _ => "IdEntidad"
            };

            var parametros = new DynamicParameters();
            parametros.Add(nombreParametro, idEntidad);

            return await _conn.QueryFirstOrDefaultAsync<EntidadBasicaResponse>(
                sp,
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task ActualizarIdUsuarioEnEntidad(string tipoEntidad, Guid idEntidad, Guid idUsuario)
        {
            // 🔍 Obtener el correo del usuario recién creado
            var usuario = await ObtenerUsuarioPorId(idUsuario);
            if (usuario == null)
                throw new Exception("Usuario no encontrado para vinculación.");

            var parametros = new DynamicParameters();
            parametros.Add("@CorreoElectronico", usuario.CorreoElectronico);
            parametros.Add("@IdEntidad", idEntidad);
            parametros.Add("@TipoEntidad", tipoEntidad);

            await _conn.ExecuteAsync("Entidad.VincularUsuarioPorCorreoYEntidad", parametros, commandType: CommandType.StoredProcedure);
        }


    }
}