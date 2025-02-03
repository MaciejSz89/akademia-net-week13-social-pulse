using SocialPulse.Core.Models;

namespace SocialPulse.Core.Services
{
    public interface ISocialProfileService
    {
        SocialProfile GetSocialProfileByUserId(string userId);
        void UpdateSocialProfile(SocialProfile socialProfile);
    }
}
