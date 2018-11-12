using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC
{
    public static class InfrastructureIoC
    {
        public static void InfrastructureFullDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.InfrastructureDapper(configuration);
            services.InfrastructureEntityFramework(configuration);
            services.InfrastructureORM();
        }
    }
}
