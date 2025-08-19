using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            string query = @"Seccion.SP_CrearSeccion";
            var result = await _sqlConn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = seccion.Nombre,

            });
            return result;
        }

        public Task<Guid> EditarSeccion(Guid Id, SeccionesRequest seccion)
        {
            string query = @"Seccion.SP_EditarSeccion";
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
            string query = @"Seccion.Sp_EliminarSeccion";
            var result = await _sqlConn.ExecuteScalarAsync<Guid>(query, new
            {
                IdSeccion = Id
            });
            return result;
        }

        public async Task<IEnumerable<EstudianteSeccionesRespone>> ObtenerEstudiantesSeccion(Guid IdSeccion)
        {
            string query = @"Seccion.sp_ObtenerEstudiantesPorSeccion";

            var result = await _sqlConn.QueryAsync<EstudianteSeccionesRespone>(query,new 
            { 
                IdSeccion 
            });

            return result;
        }

        public Task<IEnumerable<SeccionesResponse>> ObtenerSecciones()
        {
            string query = @"Seccion.SP_ObtenerSecciones";
            var result = _sqlConn.QueryAsync<SeccionesResponse>(query);
            return result;
        }

        public async Task<SeccionesResponse> ObtenerSeccionPorId(Guid Id)
        {
            string query = @"Seccion.SP_ObtenerSeccionPorId";
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
