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
    public class MedicamentoDA : IMedicamentoDA
    {
        private SqlConnection _conn;
        private IRepositorioDapper _repDapp;

        public MedicamentoDA(IRepositorioDapper repDapp)
        {
            _repDapp = repDapp;
            _conn = _repDapp.ObtenerRepositorio();
        }


        public async Task<Guid> CrearMedicamento(MedicamentoResponse medicamento)
        {
            string query = @"EstudianteMedicamento.Sp_CrearMedicamento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                Nombre = medicamento.Nombre,
                Descripcion = medicamento.Descripcion
            });
            return result;
        }

        public async Task<Guid> EditarMedicamento(Guid Id, MedicamentoResponse medicamento)
        {
            string query = @"EstudianteMedicamento.SP_EditarMedicamento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMedicamento = Id,
                Nombre = medicamento.Nombre,
                Descripcion = medicamento.Descripcion
            });
            return result;
        }

        public async Task<Guid> EliminarMedicamento(Guid Id)
        {
            string query = @"EstudianteMedicamento.SP_EliminarMedicamento";
            var result = await _conn.ExecuteScalarAsync<Guid>(query, new
            {
                IdMedicamento = Id
            });
            return result;
        }

        public async Task<MedicamentoResponse> ObtenerMedicamentoPorId(Guid Id)
        {
            string query = @"EstudianteMedicamento.SP_ObtenerMedicamentoPorId";
            var result = await _conn.QueryAsync<MedicamentoResponse>(query, new
            {
                IdMedicamento = Id
            });
            return result.FirstOrDefault();
        }

        public Task<IEnumerable<MedicamentoResponse>> ObtenerMedicamentos()
        {
            string query = @"EstudianteMedicamento.SP_ObtenerTodosMedicamentos";
            var result = _conn.QueryAsync<MedicamentoResponse>(query);
            return result;
        }
    }
}
