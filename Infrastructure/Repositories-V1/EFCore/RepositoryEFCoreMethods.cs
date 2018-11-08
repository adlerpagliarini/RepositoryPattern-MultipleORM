using Infrastructure.Interfaces.Repositories_V1.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories_V1.EFCore
{
    public class RepositoryEFCoreMethods<TEntity> : RepositoryEFCoreBase<TEntity>,
                                                    IRepositoryEFCoreMethods<TEntity> where TEntity : class
    {
        public RepositoryEFCoreMethods(DbContext databaseContext) : base(databaseContext) { }

        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return base.GenerateQuery(filter, orderBy, includeProperties);
        }

        public TEntity GetByWhere(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            return base.GenerateQuery(filter, null, includeProperties).FirstOrDefault();
        }
    }
}
