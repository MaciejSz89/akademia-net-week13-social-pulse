using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Repositories
{
    public interface IUserLinkRepository
    {
        Task<IEnumerable<UserLink>> GetUserLinksByUserIdAsync(string userId);
    }
}
