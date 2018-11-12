using Infrastructure.DBConfiguration.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC
{
    public static class EntityFrameworkIoC
    {
        public static void InfrastructureEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
