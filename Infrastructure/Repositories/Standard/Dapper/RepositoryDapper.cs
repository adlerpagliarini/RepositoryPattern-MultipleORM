using Dapper;
using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Infrastructure.Repositories.Standard.Dapper
{
    public abstract class RepositoryDapper<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IIdentityEntity
    {
        protected readonly IDbConnection dbConn;

        protected abstract string InsertQuery { get; }
        protected abstract string InsertQueryReturnId { get; }
        protected abstract string UpdateByIdQuery { get; }
        protected abstract string DeleteByIdQuery { get; }
        protected abstract string SelectByIdQuery { get; }
        protected abstract string SelectAllQuery { get; }

        protected RepositoryDapper(IOptions<DataOptionFactory> databaseOptions)
        {
            dbConn = databaseOptions.Value.DatabaseConnection;
            dbConn.Open();
        }

        protected RepositoryDapper(IDbConnection databaseConnection)
        {
            dbConn = databaseConnection;
            dbConn.Open();
        }

        public void Dispose()
        {
            dbConn.Close();
            dbConn.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual TEntity Add(TEntity obj)
        {
            TEntity entity = dbConn.QuerySingle<TEntity>(InsertQueryReturnId, obj);
            return entity;
        }

        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            return dbConn.Execute(InsertQuery, entities); 
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbConn.Query<TEntity>(SelectAllQuery);
        }

        public virtual TEntity GetById(object id)
        {
            return dbConn.Query<TEntity>(SelectByIdQuery, new { Id = id }).FirstOrDefault();
        }

        public virtual bool Remove(object id)
        {
            var entity = GetById(id);

            if (entity == null)
                return false;

            Remove(entity);
            return true;
        }

        public virtual int Remove(TEntity obj)
        {
            return dbConn.Execute(DeleteByIdQuery, new { obj.Id });
        }

        public virtual int RemoveRange(IEnumerable<TEntity> entities)
        {
            return dbConn.Execute(DeleteByIdQuery, entities.Select(obj => new { obj.Id }));
        }

        public virtual int Update(TEntity obj)
        {
            return dbConn.Execute(UpdateByIdQuery, obj);
        }

        public virtual int UpdateRange(IEnumerable<TEntity> entities)
        {
            return dbConn.Execute(UpdateByIdQuery, entities.Select(obj => obj));
        }
    }
}
