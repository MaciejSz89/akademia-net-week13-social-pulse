
using SocialPulse.Hubs;

namespace SocialPulse.Persistence.Services
{
    public class CacheCleanupService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CacheHub.CleanupStaleSessions(5);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
