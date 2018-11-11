using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;

namespace Infrastructure.Interfaces.DBConfiguration.Dapper
{
    public interface IDataServiceFactory
    {
        string ConnectionString { get; }
        IRepositoryBase<TEntity> CreateInstance<TEntity>() where TEntity : class, IIdentityEntity;
        IRepositoryBaseAsync<TEntity> CreateInstanceAsync<TEntity>() where TEntity : class, IIdentityEntity;
    }
}
