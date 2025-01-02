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

        public async Task<SocialNetwork> GetAsync(int id)
        {
            return await _context.SocialNetworks.SingleAsync(x => x.Id == id);
        }

        public async Task AddAsync(SocialNetwork network)
        {
            var newNetwork = await _context.SocialNetworks.AddAsync(network);
        }

        public async Task UpdateAsync(SocialNetwork network)
        {
            var networkToUpdate = await _context.SocialNetworks.SingleAsync(x => x.Id == network.Id);

            CopyNetworkMembers(network, networkToUpdate);
        }


        public async Task DeleteAsync(int id)
        {
            var networkToDelete = await GetAsync(id);

            _context.SocialNetworks.Remove(networkToDelete);
        }

        public IEnumerable<SocialNetwork> Get()
        {
            return _context.SocialNetworks.ToList();
        }

        public SocialNetwork Get(int id)
        {
            return _context.SocialNetworks.Single(x => x.Id == id);
        }

        public void Add(SocialNetwork network)
        {
            var newNetwork = _context.SocialNetworks.Add(network);
        }

        public void Update(SocialNetwork network)
        {
            var networkToUpdate = _context.SocialNetworks.Single(x => x.Id == network.Id);

            CopyNetworkMembers(network, networkToUpdate);
        }

        public void Delete(int id)
        {
            var networkToDelete = Get(id);

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
    

