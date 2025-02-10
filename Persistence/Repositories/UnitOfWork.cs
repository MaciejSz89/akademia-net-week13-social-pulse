using SocialPulse.Core.Repositories;
using System;

namespace SocialPulse.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialPulseContext _context;
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly ISocialProfileRepository _socialProfileRepository;
        private readonly IIdentityUserRepository _identityUserRepository;


        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
            _socialNetworkRepository = serviceProvider.GetRequiredService<ISocialNetworkRepository>();
            _socialProfileRepository = serviceProvider.GetRequiredService<ISocialProfileRepository>();
            _identityUserRepository = serviceProvider.GetRequiredService<IIdentityUserRepository>();

        }
        public ISocialNetworkRepository SocialNetworkRepository => _socialNetworkRepository;

        public ISocialProfileRepository SocialProfileRepository => _socialProfileRepository;

        public IIdentityUserRepository IdentityUserRepository => _identityUserRepository;

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
