using Data.SqlServer.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.SqlServer
{
    public static class DbDependencyInjector
    {
        #region Public Methods

        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("defaultConnection");
            services.AddDbContext<AppDbContext>((serviceProviders, options) =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }

        #endregion Public Methods
    }
}
