using Dapper;
using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Standard.Dapper
{
    public abstract class RepositoryDapperAsync<TEntity> : RepositoryDapper<TEntity>, IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {
        protected RepositoryDapperAsync(IOptions<DataOptionFactory> databaseOptions) : base (databaseOptions)
        {            
        }

        protected RepositoryDapperAsync(IDbConnection databaseConnection) : base (databaseConnection)
        {
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            TEntity entity = await dbConn.QuerySingleAsync<TEntity>(InsertQueryReturnId, obj);
            return entity;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return await dbConn.ExecuteAsync(InsertQuery, entities);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbConn.QueryAsync<TEntity>(SelectAllQuery);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await dbConn.QueryAsync<TEntity>(SelectByIdQuery, new { Id = id });
            return entity.FirstOrDefault();
        }

        public virtual async Task<bool> RemoveAsync(object id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return false;

            return await RemoveAsync(entity) > 0 ? true : false;
        }

        public virtual async Task<int> RemoveAsync(TEntity obj)
        {
            return await dbConn.ExecuteAsync(DeleteByIdQuery, new { obj.Id });
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            return await dbConn.ExecuteAsync(DeleteByIdQuery, entities.Select(obj => new { obj.Id }));
        }

        public virtual async Task<int> UpdateAsync(TEntity obj)
        {
            return await dbConn.ExecuteAsync(UpdateByIdQuery, obj);
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return await dbConn.ExecuteAsync(UpdateByIdQuery, entities.Select(obj => obj));
        }

        /*public Task<int> CommitAsync()
        {
            return Task.FromResult(1);
        }*/
    }
}
