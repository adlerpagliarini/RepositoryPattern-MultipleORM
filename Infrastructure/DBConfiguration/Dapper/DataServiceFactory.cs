using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Dapper;
using System;

namespace Infrastructure.DBConfiguration.Dapper
{
    public class DataServiceFactory : IDataServiceFactory
    {
        public string ConnectionString { get; private set; }

        public DataServiceFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IRepositoryDapperAsync<TEntity> CreateInstance<TEntity>() where TEntity : class, IIdentityEntity
        {
            throw new Exception();//return new RepositoryDapperAsync<TEntity>(new SqlConnection(ConnectionString));
        }
    }
}
