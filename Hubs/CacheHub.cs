using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SocialPulse.Hubs;

public class CacheHub : Hub
{
    private static ConcurrentDictionary<string, DateTime> _keepAliveMap = new ConcurrentDictionary<string, DateTime>();

    public Task KeepAlive(string sessionGuid)
    {
        _keepAliveMap[sessionGuid] = DateTime.UtcNow;
        return Task.CompletedTask;
    }

    public static void CleanupStaleSessions(int minutes = 5)
    {
        DateTime cutoff = DateTime.UtcNow.AddMinutes(-minutes);
        foreach (var kvp in _keepAliveMap)
        {
            if (kvp.Value < cutoff)
            {
                // remove from the keep-alive map
                _keepAliveMap.TryRemove(kvp.Key, out _);

                // also remove from the in-memory ProfilesCache if you want
                ProfilesCache.LoadedProfileIdsByGuid.Remove(kvp.Key);
            }
        }
    }
}
