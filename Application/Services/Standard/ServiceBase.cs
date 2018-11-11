using Domain.Entities;
using Domain.Interfaces.Services.Standard;
using Infrastructure.Interfaces.Repositories.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Standard
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IIdentityEntity
    {
        protected readonly IRepositoryBaseAsync<TEntity> repository;

        public ServiceBase(IRepositoryBaseAsync<TEntity> repository)
        {
            this.repository = repository;
        }
        public async Task<TEntity> AddAsync(TEntity obj)
        {
            return await repository.AddAsync(obj);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await repository.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(object id)
        {
            return await repository.RemoveAsync(id);
        }

        public async Task RemoveAsync(TEntity obj)
        {
            await repository.RemoveAsync(obj);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            await repository.RemoveRangeAsync(entities);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await repository.UpdateAsync(obj);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await repository.UpdateRangeAsync(entities);
        }
    }
}
