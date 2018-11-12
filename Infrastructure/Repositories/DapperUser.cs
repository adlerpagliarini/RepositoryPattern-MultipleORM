using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.Options;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Infrastructure.Repositories.Standard.Dapper;
using Infrastructure.Interfaces.Repositories.Standard;

namespace Infrastructure.Repositories.Dapper
{
    public class DapperUser : RepositoryDapperAsync<User>, IUserRepository, IRepositoryBaseAsync<User>
    {
        protected override string InsertQuery => "INSERT INTO Users VALUES (@Name)";
        protected override string InsertQueryReturnId => "INSERT INTO Users OUTPUT INSERTED.* VALUES (@Name)";
        protected override string UpdateByIdQuery => "UPDATE Users SET Name = @Name WHERE Id = @Id";
        protected override string DeleteByIdQuery => "DELETE FROM Users WHERE Id = @Id";
        protected override string SelectAllQuery => "SELECT * FROM Users";
        protected override string SelectByIdQuery => "SELECT * FROM Users WHERE Id = @Id";

        private string SelectAllIncludingRelation => "SELECT u.*, t.* FROM Users u LEFT JOIN ToDoList t ON t.UserId = u.Id";

        public DapperUser(IOptions<DataOptionFactory> databaseOptions) : base(databaseOptions)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            var userDictionary = new Dictionary<int, User>();
            var queryResult = dbConn.Query<User, ToDoList, User>(SelectAllIncludingRelation,
                map: (user, toDoList) => FuncMapRelation(user, toDoList, userDictionary));

            return queryResult.Distinct();
        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            var userDictionary = new Dictionary<int, User>();
            var queryResult = await dbConn.QueryAsync<User, ToDoList, User>(SelectAllIncludingRelation,
                map: (user, toDoList) => FuncMapRelation(user, toDoList, userDictionary));

            return queryResult.Distinct();
        }

        private readonly Func<User, ToDoList, Dictionary<int, User>, User> FuncMapRelation = (user, toDoList, userDictionary) =>
        {            
            if (!userDictionary.TryGetValue(user.Id, out User userEntry))
            {
                userEntry = user;
                userDictionary.Add(userEntry.Id, userEntry);
            }

            userEntry.AddItemToDo(toDoList);
            return userEntry;
        };
    }
}
