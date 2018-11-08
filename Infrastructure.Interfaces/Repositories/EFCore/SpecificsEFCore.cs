﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public abstract class SpecificsEFCore<TEntity> where TEntity : class, IIdentityEntity
    {
        #region ProtectedMethods
        protected abstract IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "");

        protected abstract IQueryable<TEntity> GenerateQueryableWhereExpression(IQueryable<TEntity> query,
                                                Expression<Func<TEntity, bool>> filter);

        protected abstract IQueryable<TEntity> GenerateIncludeProperties(IQueryable<TEntity> query, string includeProperties);

        protected abstract IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities, Func<TEntity, TEntity> DoAction);
        #endregion ProtectedMethods
    }
}