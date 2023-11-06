using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Options;
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
                services.AddDbContext<DatabaseContext>(option =>
                {
                    option.UseSqlServer(bBOptions.Value.ConnectionString);
                });

                services.AddDbContext<DatabaseContext>(option =>
                {
                    option.UseSqlServer(bBOptions.Value.ConnectionString);
                }, ServiceLifetime.Transient);
            }

            return services;
        }
    }
}
