using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Services
{
    public interface IUserLinkService
    {
        Task<IEnumerable<UserLink>> GetUserLinksAsync(string userId);
    }
}
