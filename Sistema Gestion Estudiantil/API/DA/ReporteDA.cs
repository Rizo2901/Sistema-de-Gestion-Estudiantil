using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class ReporteDA : IReporteDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;

        public ReporteDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }

        public async Task<IEnumerable<ReporteEstudiantilResponse>> ObtenerDatosReporte(ReporteEstudiantilRequest filtros)
        {
            string query = @"Reporte.SP_ReporteEstudiantilAvanzado";

            var parametros = new
            {
                Alergia = filtros.Alergia,
                Discapacidad = filtros.Discapacidad,
                CondicionEspecial = filtros.CondicionEspecial,
                Enfermedad = filtros.Enfermedad,
                Medicamento = filtros.Medicamento,
                Seccion = filtros.Seccion,
                CategoriaAlergia = filtros.CategoriaAlergia,
                CategoriaDiscapacidad = filtros.CategoriaDiscapacidad,
                CategoriaCondicionEspecial = filtros.CategoriaCondicionEspecial,
                CategoriaSituacionFamiliar = filtros.CategoriaSituacionFamiliar,
                FechaDesde = filtros.FechaDesde,
                FechaHasta = filtros.FechaHasta
            };

            var resultado = await _conn.QueryAsync<ReporteEstudiantilResponse>(query, parametros, commandType: CommandType.StoredProcedure);
            return resultado;
        }
    }
}
