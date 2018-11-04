using Infrastructure.Interfaces.DBConfiguration.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.DBConfiguration.Dapper
{
    public class DataOptionFactory : IDataOptionFactory
    {
        public string DefaultConnection { get; set; }
        public IDbConnection DatabaseConnection => new SqlConnection(DefaultConnection);
    }
}
