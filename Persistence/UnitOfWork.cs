using SocialPulse.Core.Repositories;
using SocialPulse.Core;
using SocialPulse.Persistence.Repositories;
using System;

namespace SocialPulse.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialPulseContext _context;
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly ISocialProfileRepository _socialProfileRepository;
        private readonly Dictionary<Type, object> _repositories = new();


        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
            _socialNetworkRepository = serviceProvider.GetRequiredService<ISocialNetworkRepository>();
            _socialProfileRepository = serviceProvider.GetRequiredService<ISocialProfileRepository>();
        }
        public ISocialNetworkRepository SocialNetworkRepository => _socialNetworkRepository;

        public ISocialProfileRepository SocialProfileRepository => _socialProfileRepository;

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
