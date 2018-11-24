using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public interface IEntityFrameworkRepository<TEntity> : IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
    }
}
