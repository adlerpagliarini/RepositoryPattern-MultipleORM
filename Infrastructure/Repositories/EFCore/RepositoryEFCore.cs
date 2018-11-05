using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.EFCore
{
    public class RepositoryEFCore<TEntity> : RepositoryEFCoreMethodsAsync<TEntity>, 
                                             IRepositoryEFCore<TEntity> where TEntity : class
    {
        public RepositoryEFCore(ApplicationContext databaseContext) : base(databaseContext) {}
    }
}
