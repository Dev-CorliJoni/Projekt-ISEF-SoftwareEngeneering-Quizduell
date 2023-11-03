using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Quixduell.ServiceLayer.DataAccessLayer
{
    /// <summary>
    /// Service try to connect to Database, helps to speed up cold start on Azure Server less SQL 
    /// </summary>
    internal class DBStarter : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DBStarter> _logger;


        public DBStarter(ILogger<DBStarter> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            bool sqlAlive = false;
            int checkCounter = 0;
            using (var scope = _serviceProvider.CreateScope())
            {
                var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                while (!cancellationToken.IsCancellationRequested && !sqlAlive)
                {
                    try
                    {
                        var result = await databaseContext.Database.ExecuteSqlRawAsync("Select 1", cancellationToken);
                        sqlAlive = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Database not on-line, check {CheckCounter}", checkCounter);
                        checkCounter++;
                        await Task.Delay(TimeSpan.FromSeconds(5));
                        if (checkCounter > 50)
                        {
                            _logger.LogError("Max Check (50) reached, cannot start DB");
                            return;
                        }
                    }
                }

            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
