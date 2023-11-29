using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository;

namespace Quixduell.ServiceLayer.DataAccessLayer
{
    /// <summary>
    /// Service try to connect to Database, helps to speed up cold start on Azure Server less SQL 
    /// </summary>
    internal class DBStarter : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DBStarter> _logger;
        private Timer? _timer = null;
        private int _executionCount = 0;
        private bool _sqlAlive = false;

        public DBStarter(ILogger<DBStarter> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref _executionCount);

            if (count > 50)
            {
                _logger.LogError("DB starter reached max connection attempts");
                _timer?.Change(Timeout.Infinite, 0);
            }

            if (_sqlAlive)
            {
                _logger.LogInformation("DB starter successfully start DB");
                _timer?.Change(Timeout.Infinite, 0);
            }

            _logger.LogInformation(
                "DB starter Hosted Service is working. Count: {Count}", count);

            _ = DoAsyncWork();
        }

        private async Task DoAsyncWork()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var databaseContext = scope.ServiceProvider.GetRequiredService<AppDatabaseContext<AppUser>>();

                    if (await databaseContext.Database.CanConnectAsync())
                    {
                        var result = await databaseContext.Database.ExecuteSqlRawAsync("Select 1");
                        _sqlAlive = true;

                    }
                    _logger.LogInformation("Database not on-line");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Database not on-line: {Exception}", ex);
                }
            }
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
