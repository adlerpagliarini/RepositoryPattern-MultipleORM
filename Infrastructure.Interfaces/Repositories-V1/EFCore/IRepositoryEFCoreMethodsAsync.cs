using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories_V1.EFCore
{
    public interface IRepositoryDapperAsync<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetByWhereAsync(Expression<Func<TEntity, bool>> filter,
            string includeProperties = "");
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "");

        Task<int> CommitAsync();
    }
}
