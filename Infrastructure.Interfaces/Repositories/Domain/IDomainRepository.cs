using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IDomainRepository<TEntity> : IRepositoryBase<TEntity>, IRepositoryBaseAsync<TEntity> where TEntity : class, IIdentityEntity
    {
    }
}
