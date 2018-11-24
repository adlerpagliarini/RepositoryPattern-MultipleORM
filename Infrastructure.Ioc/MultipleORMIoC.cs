using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Repositories.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Infrastructure.IoC
{
    public static class MultipleORMIoC
    {
        public static void InfrastructureORMs(this IServiceCollection services)
        {
            services.AddScoped<DapperUser>();
            services.AddScoped<EntityFrameworkUser>();

            services.AddScoped<Func<Type, IUserRepository>>(provider => type =>
            {
                var instance = provider.GetService(type);
                return (IUserRepository)instance ?? throw new KeyNotFoundException();
                /*if (type == typeof(DapperUser) || type == typeof(IDapperRepository<User>))
                    return provider.GetService<DapperUser>();

                if (type == typeof(EntityFrameworkUser) || type == typeof(IEntityFrameworkRepository<User>))
                    return provider.GetService<EntityFrameworkUser>();

                throw new KeyNotFoundException();*/
            });
        }
    }
}
