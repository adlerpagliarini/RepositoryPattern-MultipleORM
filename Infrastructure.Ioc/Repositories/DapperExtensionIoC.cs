using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC.Repositories
{
    public static class DapperExtensionIoC
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
