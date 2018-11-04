using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public interface IRepositoryEFCoreMethods<TEntity> where TEntity : class
    {
        TEntity GetByWhere(Expression<Func<TEntity, bool>> filter,
            string includeProperties = "");

        IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /* Expression<Func<TEntity, bool>> filter = null =>
            (student => student.LastName == "Adler") */

        /* Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null =>
            (q => q.OrderBy(s => s.LastName)) */
        /* includeProperties =>
            includeProperties: "Rates" */
    }
}
