using Microsoft.EntityFrameworkCore;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Repositories;
using System.Linq;

namespace SocialPulse.Persistence.Repositories
{
    public class UserLinkRepository : IUserLinkRepository
    {
        private readonly SocialPulseContext _context;

        public UserLinkRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
        }

        public async Task AddUserLinkAsync(UserLink userLink)
        {
            await _context.AddAsync(userLink);
        }

        public async Task<IEnumerable<UserLink>> GetUserLinksByUserIdAsync(string userId)
        {
            return await _context.UserLinks
                                 .Include(x => x.SocialProfile)
                                 .Where(x => x.SocialProfile.SocialPulseUserId == userId)
                                 .ToListAsync();
        }

        public async Task RemoveUserLinkAsync(int userLinkId, int socialProfileId)
        {
            var userLinkToRemove = await _context.UserLinks.SingleAsync(x => x.Id == userLinkId 
                                                                          && x.SocialProfileId == socialProfileId);

            _context.UserLinks.Remove(userLinkToRemove);

        }

        public async Task UpdateUserLinkAsync(UserLink userLink)
        {
            var userLinkToUpdate = await _context.UserLinks.SingleAsync(x => x.Id == userLink.Id
                                                                          && x.SocialProfileId == userLink.SocialProfileId);

            userLinkToUpdate.Url = userLink.Url;
            userLinkToUpdate.Title = userLink.Title;
            userLinkToUpdate.Image = userLink.Image;

            _context.UserLinks.Update(userLinkToUpdate);
        }
    }
}
