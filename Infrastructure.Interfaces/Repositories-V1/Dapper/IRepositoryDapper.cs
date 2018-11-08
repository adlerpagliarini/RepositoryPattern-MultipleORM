using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories_V1.Dapper
{
    public interface IRepositoryDapper<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IIdentityEntity
    {

    }
}
