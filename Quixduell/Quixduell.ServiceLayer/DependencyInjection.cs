using Microsoft.Extensions.DependencyInjection;
using Quixduell.ServiceLayer.Services;

namespace Quixduell.ServiceLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddQuixServiceLayer(this IServiceCollection services)
        {
            services.AddSingleton<SayHelloService>();
            return services;
        }
    }
}
