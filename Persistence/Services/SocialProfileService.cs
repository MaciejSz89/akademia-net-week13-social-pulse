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

        public SocialProfile GetSocialProfileByUserId(string userId)
        {
            return _unitOfWork.SocialProfileRepository.GetByUserId(userId);
        }

        public void UpdateSocialProfile(SocialProfile socialProfile)
        {
            throw new NotImplementedException();
        }
    }
}
