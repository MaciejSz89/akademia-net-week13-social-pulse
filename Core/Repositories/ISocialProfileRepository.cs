using SocialPulse.Core.Models;

namespace SocialPulse.Core.Repositories
{
    public interface ISocialProfileRepository
    {
        Task<IEnumerable<SocialProfile>> GetAsync();
        Task<SocialProfile?> GetAsync(int id);
        Task<SocialProfile> GetByUserIdAsync(string userId);
        Task UpdateAsync(SocialProfile socialProfile);
    }
}
