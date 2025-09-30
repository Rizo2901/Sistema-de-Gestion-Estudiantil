using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DA
{
    public class ReporteDA : IReporteDA
    {
        private readonly SqlConnection _conn;
        private readonly IRepositorioDapper _repDapp;

        public ReporteDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<ReporteEstudiantilResponse>> ObtenerDatosReporte(ReporteEstudiantilRequest filtros)
        {
            // Obtenemos todos los datos de la vista
            var sql = "SELECT * FROM vw_PerfilEstudiantilCompleto";
            var resultado = await _conn.QueryAsync<ReporteEstudiantilResponse>(sql);
            return resultado;
        }
    }
}
