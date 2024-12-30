using SocialPulse.Core.Repositories;

namespace SocialPulse.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISocialNetworkRepository SocialNetworkRepository { get; }
        Task<int> SaveChangesAsync();
    }

}
