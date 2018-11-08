using Dapper;
using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories_V1.Dapper;

namespace Infrastructure.Repositories_V1.Dapper
{
    public abstract class RepositoryDapperAsync<TEntity> : RepositoryDapper<TEntity>, IRepositoryDapperAsync<TEntity>
                            where TEntity : class, IIdentityEntity
    {
        public RepositoryDapperAsync(IOptions<DataOptionFactory> databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public RepositoryDapperAsync(IDbConnection databaseConnection) : base(databaseConnection)
        {
        }

        public async Task AddAsync(TEntity obj)
        {
            await dbConnection.ExecuteAsync(InsertQuery, obj);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbConnection.ExecuteAsync(InsertQuery, entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbConnection.QueryAsync<TEntity>(SelectQuery);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await dbConnection.QueryAsync<TEntity>(SelectQuery, new { Id = id });
            return entity.FirstOrDefault();
        }

        public async Task<bool> RemoveAsync(object id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return false;

            await RemoveAsync(entity);
            return true;
        }

        public async Task RemoveAsync(TEntity obj)
        {
            await dbConnection.ExecuteAsync(DeleteQuery, new { obj.Id });
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbConnection.ExecuteAsync(DeleteQuery, entities.Select(obj => new { obj.Id }));
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await dbConnection.QueryAsync(UpdateQuery, obj);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbConnection.ExecuteAsync(UpdateQuery, entities);
        }
    }
}
