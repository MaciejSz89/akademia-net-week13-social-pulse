using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Repositories
{
    public interface IUserLinkRepository
    {
        Task AddUserLinkAsync(UserLink userLink);
        Task<IEnumerable<UserLink>> GetUserLinksByUserIdAsync(string userId);
        Task RemoveUserLinkAsync(int userLinkId, int socialProfileId);
        Task UpdateUserLinkAsync(UserLink userLink);
    }
}
