using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.EFCore
{
    public class RepositoryEFCoreBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbContext _databaseContext;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryEFCoreBase(DbContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<TEntity>();
        }
        #region IRepositoryBase
        public void Add(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(TEntity obj)
        {
            _databaseContext.Entry(obj).State = EntityState.Modified;
        }

        public bool Remove(object id)
        {
            TEntity entity = GetById(id);

            if (entity == null)
                return false;

            Remove(entity);
            return true;
        }

        public void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IEnumerable<TEntity> GetAll() => GetYield(_dbSet);

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public int Commit()
        {
            return _databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }
        #endregion IRepositoryBase

        #region ProtectedMethods
        protected IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            query = GenerateQueryableWhereExpression(query, filter);
            query = GenerateIncludeProperties(query, includeProperties);

            if (orderBy != null)
                return orderBy(query);

            return query;
        }
        protected IQueryable<TEntity> GenerateQueryableWhereExpression(IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
                return query.Where(filter);

            return query;
        }

        protected IQueryable<TEntity> GenerateIncludeProperties(IQueryable<TEntity> query, string includeProperties)
        {
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return query;
        }

        protected IEnumerable<TEntity> GetYield(IEnumerable<TEntity> entities)
        {
            foreach (var e in entities)
            {
                yield return e;
            }
        }
        #endregion ProtectedMethods
    }
}
