using SocialPulse.Core.Models;

namespace SocialPulse.Core.Services
{
    public interface ISocialNetworkService
    {
        Task<IEnumerable<SocialNetwork>> GetAsync();
        Task PopulateSocialNetworksFromJsonAsync(string jsonFilePath);
        Task UpsertSocialNetworksAsync(List<SocialNetwork> networks);
    }
}
