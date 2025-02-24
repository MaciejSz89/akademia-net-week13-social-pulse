using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.ViewModels;

namespace SocialPulse.Core.Models.Services
{
    public interface ISocialProfileService
    {
        Task<(string sessionGuid, IEnumerable<SocialProfile> profiles)> GetInitSocialProfilesAsync(int count);
        Task<IEnumerable<SocialProfile>> GetNextSocialProfilesAsync(string sessionGuid, int count);
        Task<SocialProfile> GetSocialProfileAsync();
        Task<SocialProfile> GetSocialProfileAsync(int id);
        Task<IEnumerable<SocialProfile>> GetSocialProfilesAsync();
        Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail);
    }
}
