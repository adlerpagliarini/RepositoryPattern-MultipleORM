using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories_V1
{
    public interface IRepositoryAsyncBase<TEntity> where TEntity : class
    {
        Task<bool> AddAsync(TEntity obj);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(object id);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "");

        Task<bool> UpdateAsync(TEntity obj);

        Task<bool> RemoveAsync(object id);
        Task<bool> RemoveAsync(TEntity obj);
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}
