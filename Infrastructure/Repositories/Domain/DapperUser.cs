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
    public class DapperUser : DomainDapperRepository<User>, 
                              IUserDapperRepository,
                              IUserRepository
    {
        protected override string InsertQuery => "INSERT INTO [User] VALUES (@Name)";
        protected override string InsertQueryReturnId => "INSERT INTO [User] OUTPUT INSERTED.* VALUES (@Name)";
        protected override string UpdateByIdQuery => "UPDATE [User] SET Name = @Name WHERE Id = @Id";
        protected override string DeleteByIdQuery => "DELETE FROM [User] WHERE Id = @Id";
        protected override string SelectAllQuery => "SELECT * FROM [User]";
        protected override string SelectByIdQuery => "SELECT * FROM [User] WHERE Id = @Id";

        private string SelectAllIncludingRelation => "SELECT u.*, t.* FROM [User] u LEFT JOIN [TaskToDo] t ON t.UserId = u.Id";
        
        public DapperUser(IOptions<DataOptionFactory> databaseOptions) : base(databaseOptions)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            var userDictionary = new Dictionary<int, User>();
            var queryResult = dbConn.Query<User, TaskToDo, User>(SelectAllIncludingRelation,
                map: (user, tasksToDo) => FuncMapRelation(user, tasksToDo, userDictionary));

            return queryResult.Distinct();
        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            var userDictionary = new Dictionary<int, User>();
            var queryResult = await dbConn.QueryAsync<User, TaskToDo, User>(SelectAllIncludingRelation,
                map: (user, toDoList) => FuncMapRelation(user, toDoList, userDictionary));

            return queryResult.Distinct();
        }

        private readonly Func<User, TaskToDo, Dictionary<int, User>, User> FuncMapRelation = (user, tasksToDo, userDictionary) =>
        {            
            if (!userDictionary.TryGetValue(user.Id, out User userEntry))
            {
                userEntry = user;
                userDictionary.Add(userEntry.Id, userEntry);
            }

            userEntry.AddItemToDo(tasksToDo);
            return userEntry;
        };
    }
}
