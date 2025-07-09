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
    public class ActividadDA : IActividadDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;
        public ActividadDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearActividad(ActividadRequest actividad)
        {
            string query = @"Actividad.SP_CrearActividad";
            var result =await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Titulo = actividad.Titulo,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                Tipo = actividad.Tipo,
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Guid> EditarActividad(Guid Id, ActividadRequest actividad)
        {
            string query = @"Actividad.SP_EditarActividad";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdActividad = Id,
                Titulo = actividad.Titulo,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                Tipo = actividad.Tipo,
            }, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public Task<Guid> EliminarActividad(Guid Id)
        {
           string query = @"Actividad.SP_EliminarActividad";
            var result = _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdActividad = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }

        public Task<IEnumerable<ActividadResponse>> ObtenerActividades()
        {
            string query = @"Actividad.SP_ObtenerActividades";
            var result = _conn.QueryAsync<ActividadResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Task<ActividadResponse> ObtenerActividadPorId(Guid Id)
        {
            string query = @"Actividad.SP_ObtenerActividadPorId";
            var result = _conn.QueryFirstOrDefaultAsync<ActividadResponse>(query, new
            {
                IdActividad = Id
            }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
