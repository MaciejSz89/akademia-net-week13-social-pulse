using Microsoft.EntityFrameworkCore;
using SocialPulse.Core;
using SocialPulse.Core.Models;
using SocialPulse.Core.Repositories;

namespace SocialPulse.Persistence.Repositories
{
    public class SocialNetworkRepository : Repository<SocialNetwork>, ISocialNetworkRepository
    {
        public SocialNetworkRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
