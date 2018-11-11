﻿using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Interfaces.Repositories.Standard
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IIdentityEntity
    {
        TEntity Add(TEntity obj);
        int AddRange(IEnumerable<TEntity> entities);

        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();

        int Update(TEntity obj);
        int UpdateRange(IEnumerable<TEntity> entities);

        bool Remove(object id);
        int Remove(TEntity obj);
        int RemoveRange(IEnumerable<TEntity> entities);
    }
}
