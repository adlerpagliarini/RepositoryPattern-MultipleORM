using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Dapper;
using System;
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

        public IRepositoryBase<TEntity> CreateInstance<TEntity>() where TEntity : class, IIdentityEntity
        {
            return null;//return new RepositoryDapper<TEntity>(new SqlConnection(ConnectionString));
        }

        public IRepositoryBaseAsync<TEntity> CreateInstanceAsync<TEntity>() where TEntity : class, IIdentityEntity
        {
            return null;//return new RepositoryDapperAsync<TEntity>(new SqlConnection(ConnectionString));
        }
    }
}
