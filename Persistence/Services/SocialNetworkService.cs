using Microsoft.EntityFrameworkCore;
using SocialPulse.Core;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models;
using SocialPulse.Core.Repositories;
using SocialPulse.Core.Services;
using System.Text.Json;

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
                    await _unitOfWork.SocialNetworkRepository.UpdateAsync(network);
                }
                else
                {
                    await _unitOfWork.SocialNetworkRepository.AddAsync(network);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task PopulateSocialNetworksFromJsonAsync(string jsonFilePath)
        {
            // Read and deserialize the JSON file
            var jsonData = await File.ReadAllTextAsync(jsonFilePath);
            var networks = JsonSerializer.Deserialize<List<SocialNetworkDto>>(jsonData);

            if (networks == null || networks.Count == 0)
            {
                throw new InvalidOperationException("No data found in the JSON file.");
            }

            // Convert to entities and load images
            var socialNetworks = new List<SocialNetwork>();
            foreach (var networkDto in networks)
            {
                // Convert image path to byte array
                var iconBytes = File.Exists(networkDto.IconPath)
                    ? await File.ReadAllBytesAsync(networkDto.IconPath)
                    : Array.Empty<byte>();

                // Map DTO to entity
                socialNetworks.Add(new SocialNetwork
                {
                    Id = networkDto.Id,
                    Name = networkDto.Name,
                    Url = networkDto.Url,
                    BaseDomain = networkDto.BaseDomain,
                    Icon = iconBytes
                });
            }

            // Upsert data using the existing service method
            await UpsertSocialNetworksAsync(socialNetworks);
        }
    }
}
