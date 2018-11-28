using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;
using Infrastructure.Interfaces.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Standard.EFCore
{
    public class RepositoryEntityFramework<TEntity> : SpecificMethodsEFCore<TEntity>, 
                                                      IRepositoryBase<TEntity> where TEntity : class, IIdentityEntity
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        protected RepositoryEntityFramework(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            var rID = dbSet.Add(obj).Entity;
            return Commit() > 0 ? rID : obj;
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            return Commit();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetYieldManipulated(dbSet, DoAction);
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public bool Remove(object id)
        {
            TEntity entity = GetById(id);

            if (entity == null)
                return false;

            return Remove(entity) > 0 ? true : false;
        }

        public int Remove(TEntity obj)
        {
            dbSet.Remove(obj);
            return Commit();
        }

        public int RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return Commit();
        }

        public int Update(TEntity obj)
        {
            dbContext.Entry(obj).State = EntityState.Modified;
            return Commit();
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return Commit();
        }

        private int Commit()
        {
            return dbContext.SaveChanges();
        }

        private readonly Func<TEntity, TEntity> DoAction = (entity) =>
        {
            return entity;
        };

        #region ProtectedMethods
        protected override IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            query = GenerateQueryableWhereExpression(query, filter);
            query = GenerateIncludeProperties(query, includeProperties);

            if (orderBy != null)
                return orderBy(query);

            return query;
        }
        protected override IQueryable<TEntity> GenerateQueryableWhereExpression(IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
                return query.Where(filter);

            return query;
        }

        protected override IQueryable<TEntity> GenerateIncludeProperties(IQueryable<TEntity> query, params string[] includeProperties)
        {
            foreach (string includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        protected override IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities, Func<TEntity, TEntity> DoAction)
        {
            foreach (var e in entities)
            {
                yield return DoAction(e);
            }
        }
        #endregion ProtectedMethods
    }
}
