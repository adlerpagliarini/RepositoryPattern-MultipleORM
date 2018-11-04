using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories.Dapper
{
    public interface IRepositoryDapper<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IIdentityEntity
    {

    }
}
