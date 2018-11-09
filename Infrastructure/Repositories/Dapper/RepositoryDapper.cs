using Dapper;
using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Repositories.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Dapper
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

        public RepositoryDapper(IOptions<DataOptionFactory> databaseOptions)
        {
            dbConn = databaseOptions.Value.DatabaseConnection;
            dbConn.Open();
        }

        public RepositoryDapper(IDbConnection databaseConnection)
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

        public virtual int Add(TEntity obj)
        {
            TEntity entity = dbConn.QuerySingle<TEntity>(InsertQueryReturnId, obj);
            return entity.Id;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            dbConn.Execute(InsertQuery, entities); 
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

        public virtual void Remove(TEntity obj)
        {
            dbConn.Execute(DeleteByIdQuery, new { obj.Id });
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbConn.Execute(DeleteByIdQuery, entities.Select(obj => new { obj.Id }));
        }

        public virtual void Update(TEntity obj)
        {
            dbConn.Query(UpdateByIdQuery, obj);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbConn.Execute(UpdateByIdQuery, entities.Select(obj => new { obj.Id }));
        }
    }
}
