using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Core.Services
{
    public interface ISocialProfileService
    {
        Task<SocialProfileViewModel?> CreateSocialProfileViewModelAsync(ClaimsPrincipal user);
    }
}
