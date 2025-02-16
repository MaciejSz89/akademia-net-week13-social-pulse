using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Services
{
    public interface IUserLinkService
    {
        Task<int> AddUserLinkAsync(UserLink userLink);
        Task<IEnumerable<UserLink>> GetUserLinksAsync(string userId);
        Task RemoveUserLinkAsync(int userLinkId);
        Task UpdateUserLinkAsync(UserLink userLink);
    }
}
