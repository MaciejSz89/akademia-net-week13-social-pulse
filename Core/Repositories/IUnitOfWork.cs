namespace SocialPulse.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ISocialNetworkRepository SocialNetworkRepository { get; }
        ISocialProfileRepository SocialProfileRepository { get; }
        IIdentityUserRepository IdentityUserRepository { get; }
        Task<int> SaveChangesAsync();
    }

}
