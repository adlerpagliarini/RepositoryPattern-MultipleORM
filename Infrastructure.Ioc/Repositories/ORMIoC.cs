using Infrastructure.Interfaces.Repositories;
using Infrastructure.Repositories.Dapper;
using Infrastructure.Repositories.EFCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC
{
    public static class ORMIoC
    {
        public static void InfrastructureORM(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, EntityFrameworkUser>();
            services.AddScoped<IUserRepository, DapperUser>();
        }
    }
}
