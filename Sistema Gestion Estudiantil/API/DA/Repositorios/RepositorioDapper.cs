
using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _cnfg;
        private readonly SqlConnection _conn;

        public RepositorioDapper(IConfiguration cnfg)
        {
            _cnfg = cnfg;
            _conn = new SqlConnection(_cnfg.GetConnectionString("BD"));
        }
        
        public SqlConnection ObtenerRepositorio()
        {

            return _conn;
        }

    }
}
