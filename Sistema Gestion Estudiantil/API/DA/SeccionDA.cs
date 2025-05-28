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
    public class SeccionDA : ISeccionDA
    {
        private  IRepositorioDapper _repDapp;
        private SqlConnection _sqlConn;

        public SeccionDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _sqlConn = _repDapp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearSeccion(SeccionesRequest seccion)
        {
            string query = @"SP_Crear_Seccion";
            var result = await _sqlConn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = seccion.Nombre,

            });
            return result;
        }

        public Task<Guid> EditarSeccion(Guid Id, SeccionesRequest seccion)
        {
            string query = @"SP_Actualizar_Seccion";
            var result = _sqlConn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccion = Id,
                NuevoNombre = seccion.Nombre
            });
            return result;
        }

        public async Task<Guid> EliminarSeccion(Guid Id)
        {
            await verificarSeccionExiste(Id);
            string query = @"Sp_Eliminar_Seccion";
            var result = await _sqlConn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccion = Id
            });
            return result;
        }

        public Task<IEnumerable<SeccionesResponse>> ObtenerSecciones()
        {
            string query = @"SP_Obtener_Secciones";
            var result = _sqlConn.QueryAsync<SeccionesResponse>(query);
            return result;
        }

        public async Task<SeccionesResponse> ObtenerSeccionPorId(Guid Id)
        {
            string query = @"SP_Obtener_Seccion_Por_Id";
            var result = await _sqlConn.QueryAsync<SeccionesResponse>(query, new
            {
                IdSeccion = Id
            });
            return result.FirstOrDefault();
        }


        private async Task verificarSeccionExiste(Guid Id)
        {
            SeccionesResponse? resultadoConsulta = await ObtenerSeccionPorId(Id);
            if(resultadoConsulta == null)
            {
                throw new Exception("La seccion no existe");
            }
        }
    }
}
