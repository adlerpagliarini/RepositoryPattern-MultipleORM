using Dapper;
using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Repositories.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Dapper
{
    public abstract class RepositoryDapperAsync<TEntity> : RepositoryDapper<TEntity>, IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {
        public RepositoryDapperAsync(IOptions<DataOptionFactory> databaseOptions) : base (databaseOptions)
        {            
        }

        public RepositoryDapperAsync(IDbConnection databaseConnection) : base (databaseConnection)
        {
        }

        public virtual async Task<int> AddAsync(TEntity obj)
        {
            TEntity entity = await dbConn.QuerySingleAsync<TEntity>(InsertQueryReturnId, obj);
            return entity.Id;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbConn.ExecuteAsync(InsertQuery, entities);
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

            await RemoveAsync(entity);
            return true;
        }

        public virtual async Task RemoveAsync(TEntity obj)
        {
            await dbConn.ExecuteAsync(DeleteByIdQuery, new { obj.Id });
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbConn.ExecuteAsync(DeleteByIdQuery, entities.Select(obj => new { obj.Id }));
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await dbConn.QueryAsync(UpdateByIdQuery, obj);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbConn.ExecuteAsync(UpdateByIdQuery, entities.Select(obj => obj));
        }

        public Task<int> CommitAsync()
        {
            return Task.FromResult(1);
        }
    }
}
