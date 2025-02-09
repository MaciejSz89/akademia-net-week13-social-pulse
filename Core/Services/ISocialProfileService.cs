using SocialPulse.Core.Models;

namespace SocialPulse.Core.Services
{
    public interface ISocialProfileService
    {
        Task<SocialProfile> GetByUserIdAsync(string userId);
        Task UpdateAsync(SocialProfile socialProfile);
    }
}
