using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IIdentityEntity
    {
        int Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> entities);

        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();

        void Update(TEntity obj);
        void UpdateRange(IEnumerable<TEntity> entities);

        bool Remove(object id);
        void Remove(TEntity obj);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
