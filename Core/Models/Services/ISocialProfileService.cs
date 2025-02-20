using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Services
{
    public interface ISocialProfileService
    {
        Task<SocialProfile> GetSocialProfileAsync();
        Task<SocialProfile> GetSocialProfileAsync(int id);
        Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail);
    }
}
