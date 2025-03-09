using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.Models.Repositories
{
    public class SocialProfileSearchParams
    {
        public IEnumerable<int>? AlreadyReturnedIds { get; set; }
        public int? Count { get; set; }
        public string? Query { get; set; }
    }

    public interface ISocialProfileRepository
    {
        Task<IEnumerable<SocialProfile>> GetAsync(SocialProfileSearchParams param);
        Task<SocialProfile?> GetAsync(int id);
        Task<SocialProfile> GetByUserIdAsync(string userId);
        Task UpdateAsync(SocialProfile socialProfile);
    }
}
