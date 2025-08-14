using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class AdministradorDA : IAdministradorDA
    {
        private  IRepositorioDapper _repDapp;
        private  SqlConnection _conn;
        public AdministradorDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearAdministrador(AdministradorRequest administrador)
        {
            string query = @"Administrador.Sp_CrearAdministrador";
            var result = await  _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = administrador.Nombre,
                Apellido1 = administrador.Apellido1,
                Apellido2 = administrador.Apellido2,
                Cedula = administrador.Cedula,
                Direccion = administrador.Direccion,
                TelefonoCelular = administrador.TelefonoCelular,
                CorreoElectronico = administrador.CorreoElectronico,
                IdUsuario = administrador.IdUsuario
            });
            return result;
        }

        public async Task<Guid> EditarAdministrador(Guid IdAdministrador, AdministradorRequest administrador)
        {
            string query = @"Administrador.Sp_ActualizarAdministrador";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAdministrador = IdAdministrador,
                Nombre = administrador.Nombre,
                Apellido1 = administrador.Apellido1,
                Apellido2 = administrador.Apellido2,
                Cedula = administrador.Cedula,
                Direccion = administrador.Direccion,
                TelefonoCelular = administrador.TelefonoCelular,
                CorreoElectronico = administrador.CorreoElectronico,
                IdUsuario = administrador.IdUsuario
            });
            return result;
        }

        public async Task<Guid> EliminarAdministrador(Guid IdAdministrador)
        {
            string query = @"Administrador.Sp_EliminarAdministrador";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAdministrador = IdAdministrador
            });
            return result;
        }

        public async Task<IEnumerable<AdministradorResponse>> ObtenerAdministradores()
        {
            string query = @"Administrador.Sp_ObtenerAdministradores";
            var result = await  _conn.QueryAsync<AdministradorResponse>(query);
            return result;
        }

        public async Task<AdministradorResponse> ObtenerAdministradorPorId(Guid IdAdministrador)
        {
            string query = @"Administrador.Sp_ObtenerAdministradorPorId";
            var result = await _conn.QueryAsync<AdministradorResponse>(query, new
            {
                IdAdministrador = IdAdministrador
            });
            return result.FirstOrDefault();
        }
    }
}
