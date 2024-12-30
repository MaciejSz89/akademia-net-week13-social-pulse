using Microsoft.EntityFrameworkCore;
using SocialPulse.Core;
using SocialPulse.Core.Models;
using SocialPulse.Core.Repositories;
using SocialPulse.Core.Services;

namespace SocialPulse.Persistence.Services
{
    public class SocialNetworkService : ISocialNetworkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SocialNetworkService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task UpsertSocialNetworksAsync(List<SocialNetwork> networks)
        {
            foreach (var network in networks)
            {
                var existing = await _unitOfWork.SocialNetworkRepository.GetAsync(network.Id);
                if (existing != null)
                {
                    existing.Name = network.Name;
                    existing.Url = network.Url;
                    existing.BaseDomain = network.BaseDomain;
                    existing.Icon = network.Icon;
                }
                else
                {
                    await _unitOfWork.SocialNetworkRepository.AddAsync(network);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
