using Microsoft.EntityFrameworkCore;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Repositories;

namespace SocialPulse.Persistence.Repositories
{
    public class SocialProfileRepository : ISocialProfileRepository
    {
        private readonly SocialPulseContext _context;

        public SocialProfileRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
        }

        public async Task<IEnumerable<SocialProfile>> GetAsync()
        {
            return await _context.SocialProfiles
                                 .Include(x=>x.SocialPulseUser)
                                 .ToListAsync();
        }

        public async Task<SocialProfile?> GetAsync(int id)
        {
            return await _context.SocialProfiles.Include(x => x.SocialLinks)
                                 .ThenInclude(x => x.SocialNetwork)
                                 .Include(x => x.UserLinks)
                                 .Include(x => x.SocialPulseUser)
                                 .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(SocialProfile socialProfile)
        {
            var socialProfileToUpdate = await _context.SocialProfiles
                                                      .Include(x => x.SocialLinks)
                                                      .SingleAsync(x => x.Id == socialProfile.Id);

            CopySocialProfileMembers(socialProfile, socialProfileToUpdate);
        }

        private static void CopySocialProfileMembers(SocialProfile source, SocialProfile destination)
        {
            destination.Content = source.Content;
            if (source.ProfileImage.Length != 0)
                destination.ProfileImage = source.ProfileImage;

            var socialLinksToRemove = destination.SocialLinks
                                                 .Where(x => !source.SocialLinks
                                                                    .Select(y => y.SocialNetworkId)
                                                                    .Contains(x.SocialNetworkId))
                                                 .ToList();

            var socialLinksToUpdate = destination.SocialLinks
                                                 .Where(x => source.SocialLinks
                                                                   .Select(y => y.SocialNetworkId)
                                                                   .Contains(x.SocialNetworkId))
                                                 .ToList();

            var socialLinksToAdd = source.SocialLinks
                                         .Where(x => !destination.SocialLinks
                                                                 .Select(y => y.SocialNetworkId)
                                                                 .Contains(x.SocialNetworkId))
                                         .ToList();

            socialLinksToRemove.ForEach(x => destination.SocialLinks
                                                                  .Remove(x));

            socialLinksToUpdate.ForEach(x => destination.SocialLinks
                                                        .Single(y => y.SocialNetworkId == x.SocialNetworkId)
                                                        .RemainingUrl = x.RemainingUrl);

            socialLinksToAdd.ForEach(destination.SocialLinks
                                                .Add);
        }

        public async Task<SocialProfile> GetByUserIdAsync(string userId)
        {
            return await _context.SocialProfiles
                                 .Include(x => x.SocialLinks)
                                 .ThenInclude(x => x.SocialNetwork)
                                 .Include(x => x.UserLinks)
                                 .Include(x => x.SocialPulseUser)
                                 .SingleAsync(x => x.SocialPulseUserId == userId);
        }
    }
}
