using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace UnitTest.Integration.Repositories.Repositories.Dapper
{
    public class DapperConnection
    {
        private IServiceProvider _provider;

        public IOptions<DataOptionFactory> DataBaseConfiguration()
        {
            var services = new ServiceCollection();
            services.AddTransient<IOptions<DataOptionFactory>>(
                provider => Options.Create<DataOptionFactory>(
                        new DataOptionFactory
                        {
                            DefaultConnection = DatabaseConnection.ConnectionConfiguration.Value.DefaultConnection
                        }
             ));
            _provider = services.BuildServiceProvider();
            return _provider.GetService<IOptions<DataOptionFactory>>();
        }
    }
}
