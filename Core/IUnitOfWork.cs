using SocialPulse.Core.Repositories;

namespace SocialPulse.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISocialNetworkRepository SocialNetworkRepository { get; }
        ISocialProfileRepository SocialProfileRepository { get; }
        Task<int> SaveChangesAsync();
    }

}
