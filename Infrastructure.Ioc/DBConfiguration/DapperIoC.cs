using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC
{
    public static class DapperIoC
    {
        public static void InfrastructureDapper(this IServiceCollection services, IConfiguration configuration)
        {
            /* Using IOptions on constructor to create a instance of SqlConnection */
            var databaseSettingsSection = configuration.GetSection("ConnectionStrings");
            services.Configure<DataOptionFactory>(databaseSettingsSection);

            /* Using a Factory on constructor to create a instance of RepostioryDapper
             * with IDBConnection on constructor to return a instance of SqlConnection */
            //services.AddScoped<IDataServiceFactory, DataServiceFactory>(_ => new DataServiceFactory(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
