using Infrastructure.Interfaces.Repositories.Dapper;

namespace Infrastructure.Interfaces.DBConfiguration.Dapper
{
    public interface IDataServiceFactory
    {
        string ConnectionString { get; }
        IRepositoryDapper<TEntity> CreateInstance<TEntity>() where TEntity : class;
    }
}
