using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped(typeof(IRepositoryEFCore<>), typeof(RepositoryEFCore<>));
            /* Using IOptions on constructor to create a instance of SqlConnection */
            //services.AddScoped(typeof(IRepositoryDapper<>), typeof(RepositoryDapper<>));
            var databaseSettingsSection = Configuration.GetSection("ConnectionStrings");
            services.Configure<DataOptionFactory>(databaseSettingsSection);

            /* Using a Factory on constructor to create a instance of RepostioryDapper
             * with IDBConnection on constructor to return a instance of SqlConnection */
            services.AddScoped<IDataServiceFactory, DataServiceFactory>(_ => new DataServiceFactory(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped(typeof(IRepositoryDapperAsync<>), typeof(RepositoryDapperAsync<>));

            services.AddScoped<IUserRepository, DapperUser>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
