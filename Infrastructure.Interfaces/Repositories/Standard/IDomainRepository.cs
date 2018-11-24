using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;

namespace Infrastructure.Interfaces.Repositories.Standard
{
    public interface IDomainRepository<TEntity> : IRepositoryBase<TEntity>, IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {
    }
}
