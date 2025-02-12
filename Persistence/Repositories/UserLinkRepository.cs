using Microsoft.EntityFrameworkCore;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Repositories;

namespace SocialPulse.Persistence.Repositories
{
    public class UserLinkRepository : IUserLinkRepository
    {
        private readonly SocialPulseContext _context;

        public UserLinkRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
        }
        public async Task<IEnumerable<UserLink>> GetUserLinksByUserIdAsync(string userId)
        {
            return await _context.UserLinks
                                 .Include(x => x.SocialProfile)
                                 .Where(x => x.SocialProfile.SocialPulseUserId == userId)
                                 .ToListAsync();
        }
    }
}
