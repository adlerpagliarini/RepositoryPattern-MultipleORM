using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Repositories.Dapper;
using System.Data.SqlClient;

namespace Infrastructure.DBConfiguration.Dapper
{
    public class DataServiceFactory : IDataServiceFactory
    {
        public string ConnectionString { get; private set; }

        public DataServiceFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IRepositoryDapper<TEntity> CreateInstance<TEntity>() where TEntity : class
        {
            return new RepositoryDapper<TEntity>(new SqlConnection(ConnectionString));
        }
    }
}
