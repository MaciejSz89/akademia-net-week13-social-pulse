using SocialPulse.Core.Models;

namespace SocialPulse.Core.Repositories
{
    public interface ISocialProfileRepository : IRepository<SocialProfile, int>
    {
        SocialProfile GetByUserId(string userId);
        Task<SocialProfile> GetByUserIdAsync(string userId);
    }
}
