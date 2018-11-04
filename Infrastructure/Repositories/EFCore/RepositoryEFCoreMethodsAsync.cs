using Infrastructure.Interfaces.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EFCore
{
    public class RepositoryEFCoreMethodsAsync<TEntity> : RepositoryEFCoreMethods<TEntity>,
                                                         IRepositoryDapperAsync<TEntity> where TEntity : class
    {
        public RepositoryEFCoreMethodsAsync(DbContext databaseContext) : base(databaseContext) { }

        public async Task AddAsync(TEntity obj)
        {
            await base._dbSet.AddAsync(obj);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await base._dbSet.AddRangeAsync(entities);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
           return await base.GenerateQuery(filter, orderBy, includeProperties).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await base._dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByWhereAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            return await base.GenerateQuery(filter, null, includeProperties).FirstOrDefaultAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await base._databaseContext.SaveChangesAsync();
        }
    }
}
