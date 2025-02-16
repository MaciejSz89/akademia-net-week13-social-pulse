using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Services
{
    public interface ISocialProfileService
    {
        Task<SocialProfile> GetSocialProfileAsync();
        Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail);
    }
}
