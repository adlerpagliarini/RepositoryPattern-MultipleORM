using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Dapper
{
    public interface IRepositoryDapperAsync<TEntity> where TEntity : class, IIdentityEntity
    {
        Task AddAsync(TEntity obj);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity obj);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> RemoveAsync(object Id);
        Task RemoveAsync(TEntity obj);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
