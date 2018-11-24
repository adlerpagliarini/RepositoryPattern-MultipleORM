using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace UnitTest.Integration.Repositories.Repositories
{
    public class DatabaseConnection
    {
        public static IOptions<DataOptionFactory> ConnectionConfiguration
        {
            get
            {
                IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.test.json")
                    .Build();               
                return Options.Create(Configuration.GetSection("ConnectionStrings").Get<DataOptionFactory>());        
            }
        }
    }
}
