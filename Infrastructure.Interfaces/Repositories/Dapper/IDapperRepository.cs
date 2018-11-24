using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repositories.Dapper
{
    public interface IDapperRepository<TEntity> : IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
    }
}
