using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repositories.Standard.Dapper
{
    public abstract class DomainDapperRepository<TEntity> : RepositoryDapperAsync<TEntity>,
                                                            IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        protected DomainDapperRepository(IOptions<DataOptionFactory> databaseOptions) : base(databaseOptions)
        {
        }

        protected DomainDapperRepository(IDbConnection databaseConnection) : base(databaseConnection)
        {
        }
    }
}
