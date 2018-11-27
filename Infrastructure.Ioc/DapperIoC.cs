using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class DapperIoC
    {
        public static void InfrastructureDapper(this IServiceCollection services, IConfiguration configuration = null)
        {
            IConfigurationSection dbConnectionSettings = ResolveConfiguration.GetConnectionSettings(configuration)
                                                                             .GetSection("ConnectionStrings");
            /* Using IOptions on constructor to create a instance of SqlConnection */
            services.Configure<DataOptionFactory>(dbConnectionSettings);

            /* Using a Factory on constructor to create a instance of RepositoryDapper
             * with IDBConnection on constructor to return a instance of SqlConnection 
             * I won't use it any more because I'd turned the RepositoryDapperBase an abstract class
             */
            //services.AddScoped<IDataServiceFactory, DataServiceFactory>(_ => new DataServiceFactory(configuration.GetConnectionString("DefaultConnection")));

            /*Users*/
            services.AddScoped<IUserRepository, DapperUser>();
            services.AddScoped<IUserDapperRepository, DapperUser>();
        }
    }
}
