using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Services
{
    public interface ISocialProfileService
    {
        Task<SocialProfile> GetSocialProfileByUserIdAsync(string userId);
        Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail);
    }
}
