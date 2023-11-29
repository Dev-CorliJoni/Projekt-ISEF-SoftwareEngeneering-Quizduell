using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Options;
using Quixduell.ServiceLayer.DataAccessLayer.Repository;
using Quixduell.ServiceLayer.Services;


namespace Quixduell.ServiceLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddQuixServiceLayer(this IServiceCollection services)
        {
            services.AddHostedService<DBStarter>();
            services.AddSingleton<SayHelloService>();
            return services;
        }

        public static IServiceCollection AddQuixDataLayer(this IServiceCollection services, Action<DataAccessOptions> DataAccessOption)
        {

            services.Configure<DataAccessOptions>(DataAccessOption);

            using (var scope = services.BuildServiceProvider())
            {
                var bBOptions = scope.GetRequiredService<IOptions<DataAccessOptions>>();
                services.AddDbContext<AppDatabaseContext<AppUser>>(option =>
                {
                    option.UseSqlServer(bBOptions.Value.ConnectionString);
                });

                services.AddDbContext<AppDatabaseContext<AppUser>>(option =>
                {
                    option.UseSqlServer(bBOptions.Value.ConnectionString);
                }, ServiceLifetime.Transient);
            }

            services.AddScoped<ILernsetRepository,LernsetRepository>();

            return services;
        }
    }
}
