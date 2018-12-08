using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Infrastructure.Repositories.Standard.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Infrastructure.Repositories.Domain
{
    public class DapperTaskToDo : DomainDapperRepository<TaskToDo>, 
                                  ITaskToDoDapperRepository,
                                  ITaskToDoRepository
    {
        protected override string InsertQuery => "INSERT INTO [TaskToDo] VALUES (@Title, @Start, @Deadline, @Status, @UserId)";
        protected override string InsertQueryReturnId => "INSERT INTO [TaskToDo] OUTPUT INSERTED.* VALUES (@Title, @Start, @Deadline, @Status, @UserId)";
        protected override string UpdateByIdQuery => "UPDATE [TaskToDo] SET Title = @Title, Start = @Start, Deadline = @Deadline, Status = @Status WHERE Id = @Id";
        protected override string DeleteByIdQuery => "DELETE FROM [TaskToDo] WHERE Id = @Id";
        protected override string SelectAllQuery => "SELECT * FROM [TaskToDo]";
        protected override string SelectByIdQuery => "SELECT t.*, u.* FROM [TaskToDo] t INNER JOIN [User] u ON u.Id = t.UserId AND t.Id = @Id";

        private string SelectAllIncludingRelation => "SELECT t.*, u.* FROM [TaskToDo] t INNER JOIN [User] u ON u.Id = t.UserId";

        public DapperTaskToDo(IOptions<DataOptionFactory> databaseOptions) : base(databaseOptions)
        {
        }

        public override TaskToDo GetById(object id)
        {
            var queryResult = dbConn.Query<TaskToDo, User, TaskToDo>(SelectByIdQuery,
                map: (taskToDo, user) => FuncMapRelation(taskToDo, user), param: new { Id = id }).FirstOrDefault();

            return queryResult;
        }

        public async override Task<TaskToDo> GetByIdAsync(object id)
        {
            var queryResult = await dbConn.QueryAsync<TaskToDo, User, TaskToDo>(SelectByIdQuery,
                map: (taskToDo, user) => FuncMapRelation(taskToDo, user), param: new { Id = id });

            return queryResult.FirstOrDefault();
        }

        public override IEnumerable<TaskToDo> GetAll()
        {
            var queryResult = dbConn.Query<TaskToDo, User, TaskToDo>(SelectAllIncludingRelation,
                map: (taskToDo, user) => FuncMapRelation(taskToDo, user));

            return queryResult.Distinct();
        }

        public async override Task<IEnumerable<TaskToDo>> GetAllAsync()
        {
            var queryResult = await dbConn.QueryAsync<TaskToDo, User, TaskToDo>(SelectAllIncludingRelation,
                map: (taskToDo, user) => FuncMapRelation(taskToDo, user));

            return queryResult.Distinct();
        }

        private readonly Func<TaskToDo, User, TaskToDo> FuncMapRelation = (taskToDo, user) =>
        {
            taskToDo.User = user;
            return taskToDo;
        };
    }
}
