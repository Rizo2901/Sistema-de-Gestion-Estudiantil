using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class SeccionProfesorDA : ISeccionProfesorDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public SeccionProfesorDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }


        public async Task<Guid> CrearSeccionProfesor(SeccionProfesorRequest relacion)
        {
            string query = @"SeccionProfesor.SP_CrearSeccionProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccion = relacion.IdSeccion,
                IdProfesor = relacion.IdProfesor
            });
            return result;
        }

        public async Task<Guid> EditarSeccionProfesor(Guid Id, SeccionProfesorRequest relacion)
        {
            string query = @"SeccionProfesor.SP_EditarSeccionProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccionProfesor = Id,
                IdSeccion = relacion.IdSeccion,
                IdProfesor = relacion.IdProfesor
            });
            return result;
        }

        public async Task<Guid> EliminarSeccionProfesor(Guid Id)
        {
            string query = @"SeccionProfesor.SP_EliminarSeccionProfesor";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccionProfesor = Id,
            });
            return result;
        }

        public async Task<IEnumerable<SeccionProfesorResponse>> ObtenerProfesoresPorSeccion(Guid IdSeccion)
        {
            string query = @"SeccionProfesor.SP_ObtenerProfesoresPorSeccion";
            var result = await _conn.QueryAsync<SeccionProfesorResponse>(query, new 
            { 
                IdSeccion = IdSeccion
            });
            return result;
        }

        public async Task<IEnumerable<SeccionProfesorResponse>> ObtenerSeccionesPorProfesor(Guid IdProfesor)
        {
            string query = @"SeccionProfesor.SP_ObtenerSeccionesPorProfesor";
            var result = await _conn.QueryAsync<SeccionProfesorResponse>(query, new
            {
                IdProfesor = IdProfesor
            });
            return result;
        }

        public async Task<IEnumerable<SeccionProfesorResponse>> ObtenerSeccionesProfesores()
        {
            string query = @"SeccionProfesor.SP_ObtenerTodasSeccionesProfesores";
            var result =await _conn.QueryAsync<SeccionProfesorResponse>(query);
            return result;
        }

        public async Task<SeccionProfesorResponse> ObtenerSeccionProfesorPorId(Guid Id)
        {
            string query = @"SeccionProfesor.SP_ObtenerSeccionProfesorPorId";
            var result = await _conn.QueryAsync<SeccionProfesorResponse>(query, new
            {
                IdSeccionProfesor = Id
            });
            return result.FirstOrDefault();

        }
    }
}
