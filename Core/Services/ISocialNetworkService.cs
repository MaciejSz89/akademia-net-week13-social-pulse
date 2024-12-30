using SocialPulse.Core.Models;

namespace SocialPulse.Core.Services
{
    public interface ISocialNetworkService
    {
        Task UpsertSocialNetworksAsync(List<SocialNetwork> networks);
    }
}
