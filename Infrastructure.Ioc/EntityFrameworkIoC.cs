using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class EntityFrameworkIoC
    {
        public static void InfrastructureEntityFramework(this IServiceCollection services, IConfiguration configuration = null)
        {
            IConfiguration dbConnectionSettings = ResolveConfiguration.GetConnectionSettings(configuration);

            string conn = dbConnectionSettings.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conn));

            /*Users*/
            services.AddScoped<IUserRepository, EntityFrameworkUser>();
            services.AddScoped<IUserEntityFrameworkRepository, EntityFrameworkUser>();
        }
    }
}
