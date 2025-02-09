using Microsoft.EntityFrameworkCore;
using SocialPulse.Core;
using SocialPulse.Core.Models;
using SocialPulse.Core.Repositories;
using System.Xml.Linq;

namespace SocialPulse.Persistence.Repositories
{
    public class SocialNetworkRepository : ISocialNetworkRepository
    {
        private readonly SocialPulseContext _context;
        public SocialNetworkRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<SocialPulseContext>();
        }

        public async Task<IEnumerable<SocialNetwork>> GetAsync()
        {
            return await _context.SocialNetworks.ToListAsync();
        }

        public async Task<SocialNetwork?> GetAsync(int id)
        {
            return await _context.SocialNetworks.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(SocialNetwork network)
        {
            var newNetwork = await _context.SocialNetworks.AddAsync(network);
        }

        public async Task UpdateAsync(SocialNetwork network)
        {
            var networkToUpdate = await _context.SocialNetworks.SingleAsync(x => x.Id == network.Id);
            if (networkToUpdate == null) throw new NullReferenceException("Social Network not found in database");

            CopyNetworkMembers(network, networkToUpdate);
        }


        public async Task DeleteAsync(int id)
        {
            var networkToDelete = await GetAsync(id);
            if (networkToDelete == null) throw new NullReferenceException("Social Network not found in database");

            _context.SocialNetworks.Remove(networkToDelete);
        }
     
        private static void CopyNetworkMembers(SocialNetwork source, SocialNetwork destination)
        {
            destination.Name = source.Name;
            destination.Url = source.Url;
            destination.BaseDomain = source.BaseDomain;
            destination.Icon = source.Icon.ToArray();
        }
    }
}
    

