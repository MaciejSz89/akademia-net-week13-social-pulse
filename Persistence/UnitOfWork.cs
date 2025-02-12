using SocialPulse.Core;
using SocialPulse.Core.Models.Repositories;
using System;

namespace SocialPulse.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialPulseContext _context;
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly ISocialProfileRepository _socialProfileRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IUserLinkRepository _userLinkRepository;


        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
            _socialNetworkRepository = serviceProvider.GetRequiredService<ISocialNetworkRepository>();
            _socialProfileRepository = serviceProvider.GetRequiredService<ISocialProfileRepository>();
            _identityUserRepository = serviceProvider.GetRequiredService<IIdentityUserRepository>();
            _userLinkRepository = serviceProvider.GetRequiredService<IUserLinkRepository>();
        }
        public ISocialNetworkRepository SocialNetworkRepository => _socialNetworkRepository;

        public ISocialProfileRepository SocialProfileRepository => _socialProfileRepository;

        public IIdentityUserRepository IdentityUserRepository => _identityUserRepository;

        public IUserLinkRepository UserLinkRepository => _userLinkRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
