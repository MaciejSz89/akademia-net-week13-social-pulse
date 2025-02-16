using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialPulse.Core;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Persistence.Services
{
    public class SocialProfileService : ISocialProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISocialPulseUserService _socialPulseUserService;

        public SocialProfileService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _socialPulseUserService = serviceProvider.GetRequiredService<ISocialPulseUserService>();
        }

        public async Task<SocialProfile> GetSocialProfileAsync()
        {
            var userId = _socialPulseUserService.GetCurrentUserId();

            if (userId == null)
            {
                throw new NullReferenceException("User not found");
            }

            return await _unitOfWork.SocialProfileRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail)
        {
            if (newUserName != null)
            {
                var setUserNameResult = await _unitOfWork.IdentityUserRepository.SetUserNameAsync(socialProfile.SocialPulseUserId, newUserName);
                if (!setUserNameResult.Succeeded)
                    throw new Exception("SetUserNameAsync failed...");
            }

            if (newEmail != null)
            {
                var setEmailResult = await _unitOfWork.IdentityUserRepository.SetEmailAsync(socialProfile.SocialPulseUserId, newEmail);
                if (!setEmailResult.Succeeded)
                    throw new Exception("SetEmailAsync failed...");
            }

            await _unitOfWork.SocialProfileRepository.UpdateAsync(socialProfile);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
