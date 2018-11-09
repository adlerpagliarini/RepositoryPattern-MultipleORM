using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EFCore
{
    public class RepositoryEntityFrameworkAsync<TEntity> : RepositoryEntityFramework<TEntity>, 
                                                           IRepositoryEFCoreAsync, 
                                                           IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {

        public RepositoryEntityFrameworkAsync(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> AddAsync(TEntity obj)
        {
            var r = await dbSet.AddAsync(obj);
            return r.Entity.Id;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
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

            await RemoveAsync(entity);
            return true;
        }

        public async Task RemoveAsync(TEntity obj)
        {
            base.Remove(obj);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(TEntity obj)
        {
            dbContext.Entry(obj).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task<int> CommitAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
