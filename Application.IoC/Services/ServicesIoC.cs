using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Standard;
using Application.Services.Domain;
using Application.Services.Standard;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ServicesIoC
    {
        public static void ApplicationServicesIoC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped(typeof(IUserService<>), typeof(UserService<>));
            services.AddScoped<IUserEntityFrameworkService, UserEntityFrameworkService>();
            services.AddScoped<IUserDapperService, UserDapperService>();

            services.AddScoped(typeof(ITaskToDoService<>), typeof(TaskToDoService<>));
            services.AddScoped<ITaskToDoEntityFrameworkService, TaskToDoEntityFrameworkService>();
            services.AddScoped<ITaskToDoDapperService, TaskToDoDapperService>();
        }
    }
}
