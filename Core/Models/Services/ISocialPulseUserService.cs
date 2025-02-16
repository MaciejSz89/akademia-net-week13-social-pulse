using SocialPulse.Areas.Identity.Data;

namespace SocialPulse.Core.Models.Services
{
    public interface ISocialPulseUserService
    {
        SocialPulseUser GetCurrentUser();
        string GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentUserEmail();
    }
}
