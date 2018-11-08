using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories_V1.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories_V1.EFCore
{
    public class RepositoryEFCore<TEntity> : RepositoryEFCoreMethodsAsync<TEntity>, 
                                             IRepositoryEFCore<TEntity> where TEntity : class, IIdentityEntity
    {
        public RepositoryEFCore(ApplicationContext databaseContext) : base(databaseContext) {}
    }
}
