namespace SocialPulse.Hubs;

public class ProfilesCache
{
    public static Dictionary<string, List<int>> LoadedProfileIdsByGuid { get; } = new Dictionary<string, List<int>>();
}
