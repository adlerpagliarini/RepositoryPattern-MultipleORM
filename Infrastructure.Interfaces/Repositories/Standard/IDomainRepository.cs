using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories.Standard
{
    public interface IDomainRepository<TEntity> : IRepositoryBase<TEntity>, IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {
    }
}
