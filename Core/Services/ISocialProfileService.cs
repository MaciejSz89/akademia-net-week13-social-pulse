using SocialPulse.Core.Models;

namespace SocialPulse.Core.Services
{
    public interface ISocialProfileService
    {
        Task<SocialProfile> GetSocialProfileByUserIdAsync(string userId);
        Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail);
    }
}
