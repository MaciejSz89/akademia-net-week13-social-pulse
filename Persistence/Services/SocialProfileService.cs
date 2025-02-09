using AutoMapper;
using SocialPulse.Core;
using SocialPulse.Core.Models;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Persistence.Services
{
    public class SocialProfileService : ISocialProfileService
    {
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SocialProfileService(IServiceProvider serviceProvider)
        {
            _socialNetworkService = serviceProvider.GetRequiredService<ISocialNetworkService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task<SocialProfile> GetByUserIdAsync(string userId)
        {
            return await _unitOfWork.SocialProfileRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateAsync(SocialProfile socialProfile)
        {
            await _unitOfWork.SocialProfileRepository.UpdateAsync(socialProfile);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
