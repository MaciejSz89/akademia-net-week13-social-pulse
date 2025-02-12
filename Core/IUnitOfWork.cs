using SocialPulse.Core.Models.Repositories;

namespace SocialPulse.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISocialNetworkRepository SocialNetworkRepository { get; }
        ISocialProfileRepository SocialProfileRepository { get; }
        IIdentityUserRepository IdentityUserRepository { get; }
        IUserLinkRepository UserLinkRepository { get; }
        Task<int> SaveChangesAsync();
    }

}
