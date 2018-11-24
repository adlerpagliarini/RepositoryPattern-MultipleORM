using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
