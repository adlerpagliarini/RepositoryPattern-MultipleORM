using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;

namespace Infrastructure.Interfaces.DBConfiguration.Dapper
{
    public interface IDataServiceFactory
    {
        string ConnectionString { get; }
        IRepositoryDapperAsync<TEntity> CreateInstance<TEntity>() where TEntity : class, IIdentityEntity;
    }
}
