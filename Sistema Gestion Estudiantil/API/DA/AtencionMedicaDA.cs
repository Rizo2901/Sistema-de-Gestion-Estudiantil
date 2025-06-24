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
    public class AtencionMedicaDA : IAtencionMedicaDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repp;

        public AtencionMedicaDA(IRepositorioDapper repp)
        {
            _repp = repp;
            _conn = _repp.ObtenerRepositorio();
        }

        public async Task<Guid> CrearAtencionMedica(AtencionMedicaRequest atencion)
        {
            string query = @"SP_CrearAtencionMedica";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdEstudiante = atencion.IdEstudiante,
                Fecha = atencion.Fecha,
                Diagnostico = atencion.Diagnostico,
                Tratamiento = atencion.Tratamiento
            });
            return result;

        }

        public async Task<Guid> EditarAtencionMedica(Guid IdAtencionMedica, AtencionMedicaRequest atencion)
        {
            string query = @"SP_EditarAtencionMedica";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAtencionMedica = IdAtencionMedica,
                IdEstudiante = atencion.IdEstudiante,
                Fecha = atencion.Fecha,
                Diagnostico = atencion.Diagnostico,
                Tratamiento = atencion.Tratamiento
            });
            return result;
        }

        public async Task<Guid> EliminarAtencionMedica(Guid IdAtencionMedica)
        {
            string query = @"SP_EliminarAtencionMedica";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdAtencionMedica = IdAtencionMedica
            });
            return result;
        }

        public async Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicas()
        {
            string query = @"SP_ObtenerAtencionesMedicas";
            var result = await _conn.QueryAsync<AtencionMedicaResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<AtencionMedicaResponse>> ObtenerAtencionesMedicasPorEstudiante(Guid IdEstudiante)
        {
            string query = @"SP_ObtenerAtencionesMedicasPorEstudiante";
            var result = await _conn.QueryAsync<AtencionMedicaResponse>(query, new 
            { IdEstudiante = IdEstudiante 
            });
            return result;
        }

        public async Task<AtencionMedicaResponse> ObtenerAtencionMedicaPorId(Guid IdAtencionMedica)
        {
            string query = @"SP_ObtenerAtencionMedicaPorId";
            var result = await _conn.QuerySingleOrDefaultAsync<AtencionMedicaResponse>(query, new
            {
                IdAtencionMedica = IdAtencionMedica
            });
            return result;
        }
    }
}
