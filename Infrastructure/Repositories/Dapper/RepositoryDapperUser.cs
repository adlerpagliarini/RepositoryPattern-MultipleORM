using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repositories.Dapper
{
    public class RepositoryDapperUser : RepositoryDapperAsync<User>
    {
        protected override string InsertQuery => "INSERT INTO Users VALUES (@Name)";
        protected override string UpdateQuery => "UPDATE Users SET Name = @Name WHERE Id = @Id";
        protected override string DeleteQuery => "DELETE FROM Users WHERE Id = @Id";
        protected override string SelectQuery => "SELECT * FROM Users";

        public RepositoryDapperUser(IOptions<DataOptionFactory> databaseConfiguration) : base(databaseConfiguration)
        {
        }
        public RepositoryDapperUser(IDbConnection databaseConnection) : base(databaseConnection)
        {
        }
    }
}
