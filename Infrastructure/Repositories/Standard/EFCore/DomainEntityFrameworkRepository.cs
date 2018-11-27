using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Standard.EFCore
{
    public class DomainEntityFrameworkRepository<TEntity> : RepositoryEntityFrameworkAsync<TEntity>,
                                                            IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        protected DomainEntityFrameworkRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
