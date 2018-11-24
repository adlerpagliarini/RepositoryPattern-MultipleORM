using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Standard.EFCore
{
    public class DomainEntityFrameworkRepository<TEntity> : RepositoryEntityFrameworkAsync<TEntity>,
                                                            IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        protected DomainEntityFrameworkRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
