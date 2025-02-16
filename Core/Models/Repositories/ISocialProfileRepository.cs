using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Repositories
{
    public interface ISocialProfileRepository
    {
        Task<IEnumerable<SocialProfile>> GetAsync();
        Task<SocialProfile?> GetAsync(int id);
        Task<SocialProfile> GetByUserIdAsync(string userId);
        Task UpdateAsync(SocialProfile socialProfile);
    }
}
