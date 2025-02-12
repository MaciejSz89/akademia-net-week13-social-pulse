using NuGet.Protocol.Core.Types;
using SocialPulse.Core.Models;

namespace SocialPulse.Core.Repositories
{
    public interface ISocialNetworkRepository
    {
        Task AddAsync(SocialNetwork network);
        Task DeleteAsync(int id);
        Task<IEnumerable<SocialNetwork>> GetAsync();
        Task<SocialNetwork?> GetAsync(int id);
        Task UpdateAsync(SocialNetwork network);
    }
}
