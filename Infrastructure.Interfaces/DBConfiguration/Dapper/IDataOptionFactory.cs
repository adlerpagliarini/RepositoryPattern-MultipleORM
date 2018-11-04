using System.Data;

namespace Infrastructure.Interfaces.DBConfiguration.Dapper
{
    public interface IDataOptionFactory
    {
        string DefaultConnection { get; set; }
        IDbConnection DatabaseConnection { get; }
    }
}
