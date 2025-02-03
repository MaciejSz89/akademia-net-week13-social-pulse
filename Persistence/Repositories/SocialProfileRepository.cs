using Microsoft.EntityFrameworkCore;
using SocialPulse.Core.Models;
using SocialPulse.Core.Repositories;

namespace SocialPulse.Persistence.Repositories
{
    public class SocialProfileRepository : ISocialProfileRepository
    {
        private readonly SocialPulseContext _context;

        public SocialProfileRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
        }

        public void Add(SocialProfile entity)
        {

        }

        public async Task AddAsync(SocialProfile socialProfile)
        {
            var newNetwork = await _context.SocialProfiles.AddAsync(socialProfile);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SocialProfile> Get()
        {
            throw new NotImplementedException();
        }

        public SocialProfile? Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SocialProfile>> GetAsync()
        {
            return await _context.SocialProfiles.ToListAsync();
        }

        public async Task<SocialProfile?> GetAsync(int id)
        {
            return await _context.SocialProfiles.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(SocialProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SocialProfile socialProfile)
        {
            var socialProfileToUpdate = await _context.SocialProfiles
                                                      .Include(x => x.SocialLinks)
                                                      .SingleAsync(x => x.Id == socialProfile.Id);
            if (socialProfileToUpdate == null) throw new NullReferenceException("Social Profile not found in database");

            CopySocialProfileMembers(socialProfile, socialProfileToUpdate);
        }

        private static void CopySocialProfileMembers(SocialProfile source, SocialProfile destination)
        {
            destination.ProfileImage = source.ProfileImage;
            destination.UserLinkStyle = source.UserLinkStyle;
        }

        public SocialProfile GetByUserId(string userId)
        {
            return _context.SocialProfiles.Single(x => x.SocialPulseUserId == userId);
        }
        public async Task<SocialProfile> GetByUserIdAsync(string userId)
        {
            return await _context.SocialProfiles.SingleAsync(x => x.SocialPulseUserId == userId);
        }
    }
}
