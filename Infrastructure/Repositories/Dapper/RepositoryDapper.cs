using Dapper;
using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Infrastructure.Repositories.Dapper
{
    public abstract class RepositoryDapper<TEntity> : IRepositoryDapper<TEntity> where TEntity : class, IIdentityEntity
    {
        protected readonly IDbConnection dbConnection;

        protected abstract string InsertQuery { get; }
        protected abstract string UpdateQuery { get; }
        protected abstract string DeleteQuery { get; }
        protected abstract string SelectQuery { get; }

        public RepositoryDapper(IOptions<DataOptionFactory> databaseConfiguration)
        {
            dbConnection = databaseConfiguration.Value.DatabaseConnection;
            dbConnection.Open();
        }

        public RepositoryDapper(IDbConnection databaseConnection)
        {
            dbConnection = databaseConnection;
            dbConnection.Open();
        }

        public void Add(TEntity obj)
        {
            dbConnection.Execute(InsertQuery, obj);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbConnection.Execute(InsertQuery, entities);
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            dbConnection.Close();
            dbConnection.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbConnection.Query<TEntity>(SelectQuery);
        }

        public TEntity GetById(object id)
        {
            return dbConnection.Query<TEntity>(SelectQuery, new { Id = id }).FirstOrDefault();
        }

        public bool Remove(object id)
        {
            var entity = this.GetById(id);

            if (entity == null)
                return false;

            dbConnection.Execute(DeleteQuery, new { entity.Id });
            return true;
        }

        public void Remove(TEntity obj)
        {
            dbConnection.Execute(DeleteQuery, new { obj.Id });
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbConnection.Execute(DeleteQuery, entities.Select(obj => new { obj.Id }));
        }

        public void Update(TEntity obj)
        {
            dbConnection.Query(UpdateQuery, obj);
        }
    }
}
