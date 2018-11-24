using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;
using Infrastructure.Interfaces.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Standard.EFCore
{
    public class RepositoryEntityFrameworkAsync<TEntity> : RepositoryEntityFramework<TEntity>,  
                                                           IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {

        protected RepositoryEntityFrameworkAsync(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            var r = await dbSet.AddAsync(obj);
            await CommitAsync();
            return r.Entity;
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return await CommitAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(dbSet);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(object id)
        {
            TEntity entity = await GetByIdAsync(id);

            if (entity == null) return false;

            return await RemoveAsync(entity) > 0 ? true : false;
        }

        public async Task<int> RemoveAsync(TEntity obj)
        {
            dbSet.Remove(obj);
            return await CommitAsync();
        }

        public async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return await CommitAsync();
        }

        public async Task<int> UpdateAsync(TEntity obj)
        {
            dbContext.Entry(obj).State = EntityState.Modified;
            return await CommitAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return await CommitAsync();
        }

        private async Task<int> CommitAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
